using System;
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

   /// <summary>
   /// Class for creation data columns in Table
   /// </summary>
    public class Data_column
    {
        public string name;
        public string data_type;
        public Data_type type;
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
        /// <summary>
        /// Create data column
        /// </summary>
        /// <param name="name">Name of column</param>
        /// <param name="type">Type of data in column</param>
        /// <param name="comment">Optional cooment to column</param>
        public Data_column(string name, Data_type type, string comment = null) 
        {
            this.name = name;
            this.type = type;
            to_data_type();
            this.comment = comment;
        }

    }
    
    /// <summary>
    /// Class for creation Tables
    /// </summary>
    public class Table
    {
        internal string create_command;
        internal string name = null;
        internal Data_column[] columns = null;
        internal Geometry_column Geometry_column = null;
        /// <summary>
        /// Create Table by parameters
        /// </summary>
        /// <param name="t_name">Name of table</param>
        /// <param name="table_comment">Optional comment to table</param>
        /// <param name="columns">Couple of columns</param>
        /// <param name="Geometry_column">Single Geometry column, class Geometry_column</param>
        /// <param name="geom_comment"></param>
        public Table(string t_name,  Data_column[] columns, Geometry_column Geometry_column = null, string table_comment = null, string geom_comment = null)
        {
            this.name = t_name;
            this.columns = columns;
            this.Geometry_column = Geometry_column;

            //data_column[] List<data_column>
            //List<string> col_names = new List<string>();
            //List<string> col_comments = new List<string>();
            string[] col_names = new string[columns.Length];
            string[] col_comments = new string[columns.Length + 1];
            col_comments[0] = ($"comment on table {t_name} is '{table_comment}';");

            for (int i1 = 0; i1 < columns.Length; i1++)
            {
                Data_column data = columns[i1];
                col_names[i1] = ($"{data.name} {data.data_type}");
                col_comments[i1 + 1] = ($"comment on column {t_name}.{data.name} is '{data.comment}'; ");
            }
            this.create_command = $"CREATE TABLE if not exists {t_name} ({String.Join(",", col_names)}); \n {String.Join(" ", col_comments)} \n";
            if (Geometry_column != null)
            {
                string create_geom_column = $"ALTER TABLE {t_name} ADD COLUMN if not exists {Geometry_column.name} geometry('{Geometry_column.data_type}',{Geometry_column.SRID});\n";
                string create_geo_index = $"CREATE INDEX if not exists {t_name}_table_geom ON {t_name} USING gist({Geometry_column.name});\n";
                string create_geo_comment = $"comment on column {t_name}.{Geometry_column.name} is '{Geometry_column.comment}';\n ";
                this.create_command += $"{create_geom_column} {create_geo_index} {create_geo_comment}";
            }
        }

        
    }
    
}
