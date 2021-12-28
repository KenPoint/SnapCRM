using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapCRM
{
    class SessionPropertie
    {
        public int ID { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DimensionID { get; set; }
        public int Type { get; set; }

        public SessionPropertie()
        {
            ID = ID;
            Order = Order;
            Name = Name;
            Description = Description;
            DimensionID = DimensionID;
            Type = Type;
        }

    }
}
