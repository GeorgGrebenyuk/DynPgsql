using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynPgsql
{
    /// <summary>
    /// Class for creating SQL-querries
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// Getting command to Table creation
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string TableCreate (DynPgsql.Table.Table table)
        {
            return table.create_command;
        }
        /// <summary>
        /// Getting command to Table deliting
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
        public static string TableDrop (DynPgsql.Table.Table Table)
        {
            return $"DROP TABLE if exists {Table.name};";
        }
        /// <summary>
        /// Insert data to tables with optional geometry representation
        /// </summary>
        /// <param name="Table">Table in database/param>
        /// <param name="data_values">Data values</param>
        /// <returns></returns>
        public static string InsertDataToTable(Table.Table Table, List<string> data_values, Geometry.Geometry Geometry = null)
        {
            List<string> data_values_new = new List<string>();
            for (int i = 0; i < data_values.Count; i++)
            {
                var d_type = Table.columns[i].type;
                if (d_type == DynPgsql.Table.Data_type.String | d_type == DynPgsql.Table.Data_type.Uuid) data_values_new.Add("'" + data_values[i] + "'");
                else data_values_new.Add(data_values[i]);
            }
            List<string> column_names = Table.columns.Select(a => a.name).ToList();
            if (Geometry == null) return $"INSERT INTO {Table.name} ({string.Join(",", column_names)}) VALUES ({string.Join(",", data_values_new)})";
            else
            {
                List<string> data_and_geometry_names = column_names;
                data_values_new.Add("'" + Geometry.geom + "'");
                data_and_geometry_names.Add(Table.Geometry_column.name);
                return $"INSERT INTO {Table.name} ({string.Join(",", data_and_geometry_names)}) VALUES ({string.Join(",", data_values_new)});";
            }
        }
    }
}
