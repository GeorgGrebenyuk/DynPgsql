using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynPgsql.Core;

namespace DynPgsql
{
	public class Connect
	{
		public static string SetConnectSettings(string host_name, string Username, string Password, string db_name)
		{
			return $"Host={host_name};Username={Username};Password={Password};Host={host_name};Database={db_name}";
		}
		public static void RunCommand(string conn_settings, string command)
        {
			ManageConnection conn = new ManageConnection(conn_settings);
			conn.RunCommand(command);
        }
		
	}
}
