using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DynPgsql.Geometry.Generals;
namespace DynPgsql.Table
{
    /// <summary>
    /// Set data type in table's field (standard Pqsql types)
    /// </summary>
    public enum Data_type : int
    {
        String,
        Int,
        Float,
        Uuid,
        Boolean
    }

   
    public class Data_column
    {
        public string name;
        public string data_type;
        private Data_type type;
        public string comment = null;
        
        private void to_data_type()
        {
            switch (this.type)
            {
                case DynPgsql.Table.Data_type.String: 
                    this.data_type = "CHARACTER VARYING(1000)";
                    break;
                case DynPgsql.Table.Data_type.Int:
                    this.data_type = "integer";
                    break;
                case DynPgsql.Table.Data_type.Float:
                    this.data_type = "real";
                    break;
                case DynPgsql.Table.Data_type.Uuid:
                    this.data_type = "uuid";
                    break;
                case DynPgsql.Table.Data_type.Boolean:
                    this.data_type = "boolean";
                    break;

            }
        }
        public Data_column(string name, Data_type type, string comment = null) 
        {
            this.name = name;
            this.type = type;
            to_data_type();
            this.comment = comment;
        }

    }
    
    public class Table
    {
        public string create_command;
        public Table(string t_name, string t_comment, Data_column[] columns, Geometry_column geom = null, string geom_comment = null)
        {
            //data_column[] List<data_column>
            //List<string> col_names = new List<string>();
            //List<string> col_comments = new List<string>();
            string[] col_names = new string[columns.Length];
            string[] col_comments = new string[columns.Length + 1];
            col_comments[0] = ($"comment on table {t_name} is '{t_comment}';");

            for (int i1 = 0; i1 < columns.Length; i1++)
            {
                Data_column data = columns[i1];
                col_names[i1] = ($"{data.name} {data.data_type}");
                col_comments[i1 + 1] = ($"comment on column {t_name}.{data.name} is '{data.comment}'; ");
            }
            this.create_command = $"CREATE TABLE if not exists {t_name} ({String.Join(",", col_names)}); {String.Join(" ", col_comments)}";
            if (geom != null)
            {
                string create_geom_column = $"ALTER TABLE {t_name} ADD COLUMN if not exists {geom.name} geometry({geom.data_type},{geom.SRID});";
                string create_geo_index = $"CREATE INDEX if not exists {t_name}_table_geom ON {t_name} USING gist({geom.name});";
                string create_geo_comment = $"comment on column {t_name}.{geom.name} is '{geom.comment}'; ";
                this.create_command += $"{create_geom_column} {create_geo_index} {create_geo_comment}";
            }
        }

        
    }
    
}
