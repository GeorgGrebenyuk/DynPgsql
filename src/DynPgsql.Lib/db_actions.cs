using System;
using System.Collections.Generic;
using System.Text;

namespace DynPgsql.Lib
{
    public class db_actions
    {
        public static void RunQuerry (string sql_command, string conn_config)
        {
            ManageConnection conn = new ManageConnection(conn_config);
            conn.RunCommand(sql_command);
            conn.Close();
        }
    }
}
