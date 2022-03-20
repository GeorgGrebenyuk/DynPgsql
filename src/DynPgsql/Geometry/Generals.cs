using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynPgsql.Geometry
{
   public  class Generals
    {
        private Generals() { }
        /// <summary>
        /// Set data geometry type in table's field (standard PostGIS types)
        /// </summary>
        public enum Geometry_type : int
        {
            Point,
            PointZ,
            PointZM,
            LineString,
            LinearRing,
            Polygon,
            MultiPoint,
            MultiLineString,
            MultiPolygon,
            GeometryCollection,
            PolyhedralSurface,
            Triangle,
            TIN

        }
        public class Geometry_column
        {
            public string name;
            public string data_type;
            private Geometry_type type;
            public string comment = null;
            public int SRID = 0;
            private void to_data_type()
            {
                switch (this.type)
                {
                    case DynPgsql.Geometry.Generals.Geometry_type.Point:
                        this.data_type = "POINT";
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.PointZ:
                        this.data_type = "POINT Z";
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.PointZM:
                        this.data_type = "POINT ZM";
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.LineString:
                        this.data_type = "LINESTRING";
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.LinearRing:
                        this.data_type = "LINEARRING";
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.Polygon:
                        this.data_type = "POLYGON";
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.MultiPoint:
                        this.data_type = "MULTIPOINT";
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.MultiLineString:
                        this.data_type = "MULTILINESTRING";
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.MultiPolygon:
                        this.data_type = "MULTIPOLYGON";
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.GeometryCollection:
                        this.data_type = "GEOMETRYCOLLECTION";
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.PolyhedralSurface:
                        this.data_type = "PolyhedralSurface".ToUpper();
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.TIN:
                        this.data_type = "TIN".ToUpper();
                        break;
                    case DynPgsql.Geometry.Generals.Geometry_type.Triangle:
                        this.data_type = "Triangle".ToUpper();
                        break;
                }
            }
            public Geometry_column(string name, Geometry_type type, int SRID = 0, string comment = null)
            {
                this.SRID = SRID;
                this.name = name;
                this.type = type;
                to_data_type();
                this.comment = comment;
            }
        }
        public static string GetGeometryRepresentation(bool isCollect, Geometry geometry, int SRID = 0)
        {
            if (isCollect) return $"ST_GeomFromText('{geometry.geom}',{SRID})";
            else return $"ST_GeomFromText('GEOMETRYCOLLECTION({geometry.geom})',{SRID})";
        }

    }
    
}
