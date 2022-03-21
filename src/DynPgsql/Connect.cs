using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using aj = Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using System.Diagnostics;
using DynPgsql.Lib;

namespace DynPgsql
{
	public class Connect
	{
		private Connect() { }
		[MultiReturn(new[] { "Name", "Author", "Source code" })]
		public static Dictionary<string, string> PackageInfo()
		{
			return new Dictionary<string, string>()
			{
				{"Name", "DynPgsql package for AutodeskDynamo" },
				{"Author", "GeorgGrebenyuk" },
				{"Source code", "https://github.com/GeorgGrebenyuk/DynPgsql" }
			};
		}
		public static string SetConnectSettings(string host_name, string Port, string Username, string Password, string db_name)
		{
			return $"Host={host_name};Port={Port};Username={Username};Password={Password};Database={db_name};";
			//return $"host={host_name} port={Port} user={Username} password={Password} dbname={db_name}";
		}
		public static string SaveQuerry (string row_sql)
        {
			string path = $@"C:\Users\{Environment.UserName}\AppData\Local\Temp\{Guid.NewGuid()}.sql";
			File.WriteAllText(path, row_sql);
			return path;
        }
		[IsVisibleInDynamoLibrary(false)]
		public static string RunQuerry0 (string path_to_sql, string path_to_pgsql_dir_bin, string conn_config)
        {
			string command = $"cd {path_to_pgsql_dir_bin}" + Environment.NewLine + "psql " + "\"" +
				conn_config + "\"" + " >" + path_to_sql.Replace("\\","/") + Environment.NewLine;

			Process cmd_process = new Process();
			cmd_process.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
			cmd_process.StartInfo.Arguments = command;
			cmd_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			cmd_process.Start();
			return command;
        }
		public static void RunQuerry(string sql, string conn_config)
		{
			db_actions.RunQuerry(sql, conn_config);
		}



	}
}
