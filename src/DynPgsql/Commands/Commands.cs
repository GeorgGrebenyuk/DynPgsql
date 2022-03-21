using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynPgsql.Commands
{
    public class Commands
    {
        public static string TableCreate (DynPgsql.Table.Table table)
        {
            return table.create_command;
        }
        public static string TableDrop (string t_name)
        {
            return $"DROP TABLE if exists {t_name};";
        }
        public static string GetGeometryRepresentation(bool isCollect, DynPgsql.Geometry.Geometry geometry, int SRID = 0)
        {
            if (isCollect) return $"ST_GeomFromText('{geometry.geom}',{SRID})";
            else return $"ST_GeomFromText('GEOMETRYCOLLECTION({geometry.geom})',{SRID})";
        }
    }
}
