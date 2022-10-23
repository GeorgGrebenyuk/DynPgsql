using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aj = Autodesk.DesignScript.Geometry;
using dr = Autodesk.DesignScript.Runtime;

namespace DynPgsql.Geometry
{
    /// <summary>
    /// Class for working with geometry from Dynamo to Pgsql
    /// </summary>
    public class Geometry
    {
        public string geom;
        
        private string GetPointZ(aj.Point point)
        {
            return $"{point.X} {point.Y} {point.Z}";
        }
        private string GetPoint(aj.Point point)
        {
            return $"{point.X} {point.Y}";
        }
        /// <summary>
        /// Create postgis geometry as Point in 3D
        /// </summary>
        /// <param name="point"></param>
        /// <param name="is_3d">Use 3D data or not</param>
        public Geometry (aj.Point point, bool is_3d = false)
        {
            if (is_3d) this.geom = $"POINT Z({GetPointZ(point)})";
            else this.geom = $"POINT ({GetPoint(point)})";
        }
        /// <summary>
        /// Create postgis geometry as Line
        /// </summary>
        /// <param name="line"></param>
        /// <param name="is_3d">Use 3D data or not</param>
        public Geometry(aj.Line line, bool is_3d = false)
        {
            if (is_3d) this.geom = $"LINESTRING Z({GetPointZ(line.StartPoint)},{GetPointZ(line.EndPoint)})";
            else this.geom = $"LINESTRING ({GetPoint(line.StartPoint)},{GetPoint(line.EndPoint)})";
        }
        /// <summary>
        /// Create postgis geometry as Polyline
        /// </summary>
        /// <param name="poly_curve"></param>
        /// <param name="is_3d">Use 3D data or not</param>
        public Geometry (aj.PolyCurve poly_curve, bool is_3d = false)
        {
            aj.Curve[] collection = poly_curve.Curves();
            List<string> coords = new List<string>();
            List<aj.Point> pnts_last = new List<aj.Point>();
            for (int i1 = 0; i1 < poly_curve.NumberOfCurves; i1++)
            {
                aj.Curve curve = collection[i1];
                if (!pnts_last.Contains(curve.StartPoint)) 
                {
                    pnts_last.Add(curve.StartPoint);

                    if (is_3d) coords.Add($"{GetPointZ(curve.StartPoint)}");
                    else coords.Add($"{GetPoint(curve.StartPoint)}");
                }
                if (!pnts_last.Contains(curve.EndPoint)) 
                {
                    if (is_3d) coords.Add($"{GetPointZ(curve.EndPoint)}");
                    else coords.Add($"{GetPoint(curve.EndPoint)}");
                }
            }

            if (is_3d) this.geom = $"LINESTRING Z({String.Join(",", coords)})";
            else this.geom = $"LINESTRING ({String.Join(",", coords)})";
        }
        /// <summary>
        /// Create postgis geometry as Mesh-data (PolyHedralSurface) in 3D
        /// </summary>
        /// <param name="solid"></param>
        public Geometry (aj.Solid solid)
        {
            aj.Face[] solid_faces = solid.Faces;
            List<string> faces_geom = new List<string>();
            foreach (aj.Face face in solid_faces)
            {
                List<string> coords = new List<string>();
                foreach (aj.Vertex vertex in face.Vertices)
                {
                    string p_geom = 
                        $"{vertex.PointGeometry.X} {vertex.PointGeometry.Y} {vertex.PointGeometry.Z}";
                    coords.Add(p_geom);
                }
                faces_geom.Add("((" + String.Join(",", coords) + "))");
            }
            this.geom = $"POLYHEDRALSURFACE Z ({String.Join(",", faces_geom)})";
        }
        [dr.IsVisibleInDynamoLibrary(false)]
        public Geometry (aj.Mesh m)
        {
            //
        }
    }
   
}
