using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using AsterNET.Manager.Event;
using AsterNET.Manager;
using System.Configuration;
using System.Diagnostics;


namespace SnapCRM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SessionPropertie[] props; //Declareted for accessibility in any class methods, array will contains list of controls
        private int sessionID;
        private string customerPhone;
        private DateTime startTime;
        private int userID;

        private string server = Properties.Settings.Default.server;
        private string databaseName = Properties.Settings.Default.databaseName;
        private string userName = Properties.Settings.Default.userName;
        private string password = Properties.Settings.Default.password;

        private ManagerConnection AstCon;

        private void Form1_Load(object sender, EventArgs e)
        {
            //_ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height-30;
            this.Top = 35;
            this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            DrawComponents();
            ConnectionToPBX();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawComponents();
        }

        private void DrawComponents()
        {
            var dbCon = DBConnection.Instance();
            dbCon.Server = server;
            dbCon.DatabaseName = databaseName;
            dbCon.UserName = userName;
            dbCon.Password = password;
            if (dbCon.IsConnect())
            {
                //Let's fill the array with all sessionproperties
                string query = "SELECT COUNT(*) FROM sessionproperties";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var totalRows = cmd.ExecuteScalar();
                this.props = new SessionPropertie[Convert.ToInt32(totalRows)]; //That's array

                query = "SELECT * FROM sessionproperties ORDER BY `Order`";
                cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                for (int i = 0; i < props.Length; i++)
                {
                    reader.Read();
                    props[i] = new SessionPropertie();
                    props[i].ID = reader.GetInt32(0);
                    props[i].Order = reader.GetInt32(1);
                    props[i].Name = reader.GetString(2);
                    props[i].Description = reader.GetString(3);
                    props[i].DimensionID = reader.GetInt32(4);
                    props[i].Type = reader.GetInt32(5);
                }

                int j = 120; //Vertical start drawing point 
                reader.Close(); //now we can to re-use our connection for filling Listbox

                for (int i = 0; i < props.Length; i++)
                {
                    if (props[i].Type == 3) //Determinated Listbox control
                    {
                        Label dinLbl = new Label
                        {
                            Text = props[i].Description+":",
                            Location = new System.Drawing.Point(7, j),
                            Size = new System.Drawing.Size(this.Width - 30, 15)
                        };
                        this.Controls.Add(dinLbl);

                        
                        ListBox dinListBox = new ListBox
                        {
                            Name = props[i].Name,
                            Location = new System.Drawing.Point(7, j + 18),
                            Size = new System.Drawing.Size(this.Width-30, 70),
                            Font = new Font(Font.Name, 10, Font.Style)
                        };
                        //Loading list of data
                        query = "SELECT * FROM enumerators WHERE DimensionID = " + props[i].DimensionID.ToString() + " ORDER BY `Order`";
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            dinListBox.Items.Add(reader.GetString(3));
                        }
                        reader.Close();

                        //Selecting first item
                        dinListBox.SetSelected(0,true);

                        //Loading to form
                        this.Controls.Add(dinListBox);
                        j = j + 90;
                    }
                }

                for (int i = 0; i < props.Length; i++)
                {
                    if (props[i].Type == 1) //Determinated Checkbox control
                    {
                        CheckBox dinCheckBox = new CheckBox
                        {
                            Name = props[i].Name,
                            Location = new System.Drawing.Point(7, j),
                            Text = props[i].Description,
                            Font = new Font(Font.Name, 10, Font.Style),
                            Size = new System.Drawing.Size(this.Width - 30, 20)
                        };
                        //Loading to form
                        this.Controls.Add(dinCheckBox);
                        j = j + 20;
                    }
                }
                
                dbCon.Close();
            }
        }
        private void SelectUser()
        {
            var dbCon = DBConnection.Instance();
            dbCon.Server = server;
            dbCon.DatabaseName = databaseName;
            dbCon.UserName = userName;
            dbCon.Password = password;
            if (dbCon.IsConnect())
            {
                string query = "SELECT * FROM users u WHERE TIME(u.StartTime) <= TIME(NOW()) AND (u.EndTime) >= TIME(NOW()) AND u.AgentID ='701'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

            }
         }

        private void ConnectionToPBX()
        {
            if (Properties.Settings.Default.astHost == null)
            {
                MessageBox.Show(
                    "Файл конфигурации не содержит правильных настроек агента AMI. Необходимо проверить настройку", "Определитель CRM");
                Environment.Exit(0);
            }
            //XtraMessageBox.Show("Список okuHastaneListesi получен");

            try
            {
                AstCon = new ManagerConnection(Properties.Settings.Default.astHost,
                    int.Parse(Properties.Settings.Default.astPort),
                    Properties.Settings.Default.astUser, Properties.Settings.Default.astPass);

                AstCon.NewState += astCon_NewState;
                //AstCon.Link += astCon_Link; // added to support AMI 1.0 (Asterisk 1.4)
                AstCon.ConnectionState += astCon_ConnectionState;
                AstCon.Hangup += AstCon_Hangup;
                //AstCon.ExtensionStatus += astCon_Test;

                AstCon.Login();

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error connecting to {ConfigurationManager.AppSettings["astHost"]}. Error: {ex.Message}");

                // Terminate Application
                Application.Exit();
            }
        }

        private void AstCon_Hangup(object sender, HangupEvent e)
        {
            //if ((e.Channel.ToUpper().StartsWith(Properties.Settings.Default.astPeerType + "/" + Properties.Settings.Default.agentID)) || (e.Channel.ToUpper().StartsWith("Local/" + Properties.Settings.Default.agentID)))
            if (e.CallerIdNum == Properties.Settings.Default.agentID)
            {

                this.Invoke((MethodInvoker)delegate ()
                {
                    Debug.Write(e.Channel + " Hangup " + e.Connectedlinenum);
                });
            }
                //throw new NotImplementedException();
            
        }

        void astCon_ConnectionState(object sender, ConnectionStateEvent e)
        {
            // Connection state has changed
            this.Invoke((MethodInvoker)delegate ()
            {
                labelControlStatus.Text = AstCon.IsConnected() ? $"Connected to: {AstCon.Username}@{AstCon.Hostname}" : $"Disconnected, reconnecting to {AstCon.Hostname}...";
            });
            
        }

        private void astCon_NewState(object sender, NewStateEvent e)
        {
            Debug.Write("Activities " + e.CallerIdNum);
            if (e.ChannelStateDesc != null)
            {
                if (e.Channel.ToUpper().StartsWith(Properties.Settings.Default.astPeerType + "/" + Properties.Settings.Default.agentID))
                {
                    switch (e.ChannelStateDesc.ToLower())
                    {
                        case "ringing":
                            
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                labelConnectedLineNum.Text = e.Connectedlinenum;
                                Debug.Write("Ringing " + e.Connectedlinenum);
                                this.WindowState = FormWindowState.Normal;
                            });
                            break;
                        case "up":
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                //Подняли трубку
                                Debug.Write("Up " + e.Connectedlinenum);
                            });
                            break;
                    }
                }
            }
        }
        private void ClearControls()
        {
            for (int i = 0; i < props.Length; i++)
            {
                if (props[i].Type == 1) (Controls[props[i].Name] as CheckBox).Checked = false; //Unchecking Checkbox control
                if (props[i].Type == 3) (Controls[props[i].Name] as ListBox).SetSelected(0, true);//Setting ListBox to Zero value (Nothing is selected)
            }
        }

        /// <summary>
        /// CollectValues()
        /// This method collect all not zero values to sessionvalues
        /// </summary>
        private void CollectValues()
        {
            var dbCon = DBConnection.Instance();
            dbCon.Server = server;
            dbCon.DatabaseName = databaseName;
            dbCon.UserName = userName;
            dbCon.Password = password;

            string query = "";

            for (int i = 0; i < props.Length; i++)
            {
                if (props[i].Type == 1)
                {
                    if ((Controls[props[i].Name] as CheckBox).Checked == true) query = query + $"INSERT INTO sessionvalues (SessionID, DimensionID, Value) VALUES ({sessionID}, {props[i].DimensionID},' {props[i].Description}');";
                }
                if (props[i].Type == 3)
                {
                    if ((Controls[props[i].Name] as ListBox).SelectedIndex != 0) query = query + $"INSERT INTO sessionvalues (SessionID, DimensionID, Value) VALUES ({sessionID}, {props[i].DimensionID},' {(Controls[props[i].Name] as ListBox).SelectedItem}');";
                }
            }

            if (query.Length != 0)
            {
                if (dbCon.IsConnect())
                {
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var totalRows = cmd.ExecuteScalar();
                }
                dbCon.Close();
            }

        }

        private void AddNewCustomerInfo()
        {
            if (textBoxNewInfo.Text.Length == 0) return;
            string query = $"INSERT INTO customers (CustomerPhone, CustomerInfo) VALUES ({customerPhone}, ' {textBoxNewInfo.Text}');";
            var dbCon = DBConnection.Instance();
            dbCon.Server = server;
            dbCon.DatabaseName = databaseName;
            dbCon.UserName = userName;
            dbCon.Password = password;
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var newID = cmd.ExecuteScalar();
            }
            dbCon.Close();
            textBoxNewInfo.Text = "";
        }

        private string NormalizePhone(string stringForNorm)
        {
            if (stringForNorm.Length >= 10) stringForNorm = stringForNorm.Trim().Substring(stringForNorm.Length-10,10);
            return stringForNorm;
        }

        private void ShowCustomerInfo()
        {
            string query = $"SELECT CustomerInfo FROM customers WHERE CustomerPhone ='{customerPhone}';";
            var dbCon = DBConnection.Instance();
            dbCon.Server = server;
            dbCon.DatabaseName = databaseName;
            dbCon.UserName = userName;
            dbCon.Password = password;

            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBoxIIN.Items.Add(reader.GetString(0));
                }
            }
            dbCon.Close();
        }

        private void InitSession()
        {
            string query = $"INSERT INTO session (UserID, CustomerPhone) VALUES ({userID}, ' {customerPhone}');";
            var dbCon = DBConnection.Instance();
            dbCon.Server = server;
            dbCon.DatabaseName = databaseName;
            dbCon.UserName = userName;
            dbCon.Password = password;
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(query, dbCon.Connection);
                sessionID = (Int32)cmd.ExecuteScalar();
            }
            dbCon.Close();
            startTime = DateTime.Now;
        }

        private void buttonAddInfo_Click(object sender, EventArgs e)
        {
            customerPhone = NormalizePhone("+77019991224");

            ShowCustomerInfo();
        }
    }
}
