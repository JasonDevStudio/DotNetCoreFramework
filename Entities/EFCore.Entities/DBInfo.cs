using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Entities
{
    public class DBInfo
    {
        public string HOST { get; set; }
        public string PORT { get; set; }
        public string SID { get; set; }
        public string SERVER_NAME { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string CONN_STRING { get; set; }
    }
}
