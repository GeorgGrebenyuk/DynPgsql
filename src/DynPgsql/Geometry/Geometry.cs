using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aj = Autodesk.DesignScript.Geometry;

namespace DynPgsql.Geometry
{
    public class Geometry
    {
        public string geom;
        

        private string GetPointZ(aj.Point point)
        {
            return $"{point.X} {point.Y} {point.Z}";
        }
        public Geometry (aj.Point point)
        {
            this.geom = $"POINT ({GetPointZ(point)})";
        }
        public Geometry(aj.Line line)
        {
            this.geom = $"LINESTRING ({GetPointZ(line.StartPoint)},{GetPointZ(line.EndPoint)})";
        }
        public Geometry (aj.PolyCurve poly_curve)
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
                    coords.Add($"{GetPointZ(curve.StartPoint)}");

                }
                if (!pnts_last.Contains(curve.EndPoint)) 
                {
                    pnts_last.Add(curve.EndPoint);
                    coords.Add($"{GetPointZ(curve.EndPoint)}");
                }
            }
            this.geom = $"LINESTRING ({String.Join(",", coords)})";
        }
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
        public Geometry (aj.Mesh m)
        {

        }
    }
   
}
