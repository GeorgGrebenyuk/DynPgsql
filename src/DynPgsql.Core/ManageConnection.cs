using System;
using Npgsql;

namespace DynPgsql.Core
{
	public class ManageConnection
	{
		public NpgsqlConnection connetion;
		public NpgsqlCommand command;

		public ManageConnection(string connect_settings)
		{
			this.connetion = new NpgsqlConnection(connect_settings);
			this.connetion.Open();
			this.command = new NpgsqlCommand();
			this.command.Connection = this.connetion;
		}
		public void Close()
		{
			this.command.Dispose();
			if (this.connetion.State != System.Data.ConnectionState.Closed) this.connetion.Close();
		}
		public void RunCommand(string command, bool ExecuteWithoutReading = true)
		{
			this.command.CommandText = command;
			if (ExecuteWithoutReading == true) this.command.ExecuteNonQuery();
		}
	}
}
