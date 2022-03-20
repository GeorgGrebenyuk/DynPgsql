using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynPgsql
{
    public enum data_type : int
    {
        String,
        Int,
        Float,
        Uuid,
        Boolean
    }
    public enum geometry_type : int
    {
        Point,
        LineString,
        LinearRing,
        Polygon,
        MultiPoint,
        MultiLineString,
        MultiPolygon,
        GeometryCollection
    }
    public class data_column
    {
        public string name;
        public string data_type;
        private data_type type;
        public string comment = null;
        
        private void to_data_type()
        {
            switch (this.type)
            {
                case DynPgsql.data_type.String: 
                    this.data_type = "CHARACTER VARYING(1000)";
                    break;
                case DynPgsql.data_type.Int:
                    this.data_type = "integer";
                    break;
                case DynPgsql.data_type.Float:
                    this.data_type = "real";
                    break;
                case DynPgsql.data_type.Uuid:
                    this.data_type = "uuid";
                    break;
                case DynPgsql.data_type.Boolean:
                    this.data_type = "boolean";
                    break;

            }
        }
        public data_column(string name, data_type type, string comment = null) 
        {
            this.name = name;
            this.type = type;
            to_data_type();
            this.comment = comment;
        }

    }
    public class geometry_column
    {
        public string name;
        public string data_type;
        private geometry_type type;
        public string comment = null;
        public int SRID = 3857;
        private void to_data_type()
        {
            switch (type)
            {
                case geometry_type.Point:
                    this.data_type = "POINT Z";
                    break;
                case geometry_type.LineString:
                    this.data_type = "LINESTRING";
                    break;
                case geometry_type.LinearRing:
                    this.data_type = "LINEARRING";
                    break;
                case geometry_type.Polygon:
                    this.data_type = "POLYGON";
                    break;
                case geometry_type.MultiPoint:
                    this.data_type = "MULTIPOINT";
                    break;
                case geometry_type.MultiLineString:
                    this.data_type = "MULTILINESTRING";
                    break;
                case geometry_type.MultiPolygon:
                    this.data_type = "MULTIPOLYGON";
                    break;
                case geometry_type.GeometryCollection:
                    this.data_type = "GEOMETRYCOLLECTION";
                    break;
            }
        }
        public geometry_column(string name, geometry_type type, string comment = null)
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
        public Table(string t_name, string t_comment, data_column[] columns, geometry_column geom = null, string geom_comment = null)
        {
            
            //List<string> col_names = new List<string>();
            //List<string> col_comments = new List<string>();
            string[] col_names = new string[columns.Length];
            string[] col_comments = new string[columns.Length + 1];
            col_comments[0] = ($"comment on table {t_name} is '{t_comment}';");
            int i1 = 0;
            int i2 = 1;
            foreach (data_column data in columns)
            {
                col_names[i1] = ($"{data.name} {data.data_type}");
                col_comments[i2] = ($"comment on column {t_name}.{data.name} is '{data.comment}'; ");
                i1++; 
                i2++;

            }
            this.create_command = $"CREATE TABLE {t_name} ({String.Join(",", col_names)}); {String.Join(",", col_comments)}";
            if (geom != null)
            {
                string create_geom_column = $"ALTER TABLE {t_name} ADD COLUMN {geom.name} geometry({geom.data_type},{geom.SRID});";
                string create_geo_index = $"CREATE INDEX {t_name}_table_geom ON {t_name} USING gist({geom.name});";
                string create_geo_comment = $"comment on column {t_name}.{geom.name} is '{geom.comment}'; ";
                this.create_command += $"{create_geom_column}; {create_geo_index}; {create_geo_comment};";
            }
        }
        public static string GetCreationCommand(Table table)
        {
            return table.create_command;
        }
    }
}
