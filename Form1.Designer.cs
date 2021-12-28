namespace SnapCRM
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBoxIIN = new System.Windows.Forms.ListBox();
            this.buttonCopySelectedIIN = new System.Windows.Forms.Button();
            this.textBoxNewInfo = new System.Windows.Forms.TextBox();
            this.buttonAddInfo = new System.Windows.Forms.Button();
            this.labelControlStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelConnectedLineNum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxIIN
            // 
            this.listBoxIIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxIIN.FormattingEnabled = true;
            this.listBoxIIN.ItemHeight = 20;
            this.listBoxIIN.Location = new System.Drawing.Point(5, 62);
            this.listBoxIIN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBoxIIN.Name = "listBoxIIN";
            this.listBoxIIN.Size = new System.Drawing.Size(235, 64);
            this.listBoxIIN.TabIndex = 0;
            // 
            // buttonCopySelectedIIN
            // 
            this.buttonCopySelectedIIN.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopySelectedIIN.Image")));
            this.buttonCopySelectedIIN.Location = new System.Drawing.Point(244, 62);
            this.buttonCopySelectedIIN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCopySelectedIIN.Name = "buttonCopySelectedIIN";
            this.buttonCopySelectedIIN.Size = new System.Drawing.Size(73, 64);
            this.buttonCopySelectedIIN.TabIndex = 1;
            this.buttonCopySelectedIIN.UseVisualStyleBackColor = true;
            this.buttonCopySelectedIIN.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxNewInfo
            // 
            this.textBoxNewInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNewInfo.Location = new System.Drawing.Point(5, 133);
            this.textBoxNewInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxNewInfo.Name = "textBoxNewInfo";
            this.textBoxNewInfo.Size = new System.Drawing.Size(235, 23);
            this.textBoxNewInfo.TabIndex = 2;
            // 
            // buttonAddInfo
            // 
            this.buttonAddInfo.Location = new System.Drawing.Point(244, 131);
            this.buttonAddInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonAddInfo.Name = "buttonAddInfo";
            this.buttonAddInfo.Size = new System.Drawing.Size(72, 23);
            this.buttonAddInfo.TabIndex = 3;
            this.buttonAddInfo.Text = "Добавить";
            this.buttonAddInfo.UseVisualStyleBackColor = true;
            this.buttonAddInfo.Click += new System.EventHandler(this.buttonAddInfo_Click);
            // 
            // labelControlStatus
            // 
            this.labelControlStatus.AutoSize = true;
            this.labelControlStatus.Location = new System.Drawing.Point(32, 15);
            this.labelControlStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelControlStatus.Name = "labelControlStatus";
            this.labelControlStatus.Size = new System.Drawing.Size(35, 13);
            this.labelControlStatus.TabIndex = 4;
            this.labelControlStatus.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.pictureBox1.Location = new System.Drawing.Point(5, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 23);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // labelConnectedLineNum
            // 
            this.labelConnectedLineNum.AutoSize = true;
            this.labelConnectedLineNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectedLineNum.Location = new System.Drawing.Point(8, 38);
            this.labelConnectedLineNum.Name = "labelConnectedLineNum";
            this.labelConnectedLineNum.Size = new System.Drawing.Size(52, 18);
            this.labelConnectedLineNum.TabIndex = 6;
            this.labelConnectedLineNum.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(328, 366);
            this.Controls.Add(this.labelConnectedLineNum);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControlStatus);
            this.Controls.Add(this.buttonAddInfo);
            this.Controls.Add(this.textBoxNewInfo);
            this.Controls.Add(this.buttonCopySelectedIIN);
            this.Controls.Add(this.listBoxIIN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Snap CRM / Mediker";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxIIN;
        private System.Windows.Forms.Button buttonCopySelectedIIN;
        private System.Windows.Forms.TextBox textBoxNewInfo;
        private System.Windows.Forms.Button buttonAddInfo;
        private System.Windows.Forms.Label labelControlStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelConnectedLineNum;
    }
}

