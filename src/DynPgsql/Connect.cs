using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


using System.Diagnostics;

using aj = Autodesk.DesignScript.Geometry;
using dr = Autodesk.DesignScript.Runtime;

namespace DynPgsql
{
	public class Connect
	{
		private Connect() { }
		/// <summary>
		/// Getting info about package
		/// </summary>
		/// <returns></returns>
		[dr.MultiReturn(new[] { "Name", "Author", "Source code" })]
		public static Dictionary<string, string> PackageInfo()
		{
			return new Dictionary<string, string>()
			{
				{"Name", "DynPgsql package for AutodeskDynamo" },
				{"Author", "GeorgGrebenyuk" },
				{"Source code", "https://github.com/GeorgGrebenyuk/DynPgsql" }
			};
		}
		/// <summary>
		/// Creaion connection's settings to connect to PostgreSQL
		/// </summary>
		/// <param name="host_name"></param>
		/// <param name="Port"></param>
		/// <param name="Username"></param>
		/// <param name="Password"></param>
		/// <param name="db_name"></param>
		/// <returns></returns>
		public static string CreateConnectSettings(string host_name, string Port, string Username, string Password, string db_name)
		{
			//return $"Host={host_name};Port={Port};Username={Username};Password={Password};Database={db_name};";
			return $"--host {host_name} --port {Port} --username {Username} --{Password} --dbname {db_name}";
		}
		/// <summary>
		/// Save the result of sql-queries to file
		/// </summary>
		/// <param name="sql_queries"></param>
		/// <returns></returns>
		public static string SaveQuerry (List<string> sql_queries)
        {
			string path = $@"C:\Users\{Environment.UserName}\AppData\Local\Temp\{Guid.NewGuid()}.sql";
			File.WriteAllLines(path, sql_queries);
			return path;
        }
		/// <summary>
		/// Running query as sending command to psql.exe by system Postgresql bin folder
		/// </summary>
		/// <param name="path_to_sql_file">Path to file with sql-querries</param>
		/// <param name="conn_config">Configs to connect to database</param>
		/// <param name="path_to_pgsql_dir_bin">Path to bin folder of PostgreSQL</param>
		/// <returns></returns>
		[dr.IsVisibleInDynamoLibrary(false)]
		public static string RunQuerry (string path_to_sql_file,  string conn_config, string path_to_pgsql_dir_bin = @"C:\Program Files\PostgreSQL\14\bin")
        {
			string command = $"cd {path_to_pgsql_dir_bin}" + Environment.NewLine + "psql " + "\"" +
				conn_config + "\"" + " >" + path_to_sql_file.Replace("\\","/") + Environment.NewLine;

			Process cmd_process = new Process();
			cmd_process.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
			cmd_process.StartInfo.Arguments = command;
			cmd_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			cmd_process.Start();
			return command;
        }



	}
}
