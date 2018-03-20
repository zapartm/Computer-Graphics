using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace lab4
{
    class Triangle
    {
        public Vector4D[] vertices;
        public Vector4D[] normalVectors;
        public Vector3D[] WorldCoordinates;
        public Color color { get; }

        public Triangle(Vector3D v1, Vector3D v2, Vector3D v3, Color color)
        {
            this.color = color;
            vertices = new Vector4D[3];
            vertices[0] = Vector4D.CreatePositionVector(v1);
            vertices[1] = Vector4D.CreatePositionVector(v2);
            vertices[2] = Vector4D.CreatePositionVector(v3);

            normalVectors = new Vector4D[3];
        }

        public Triangle(Vector4D v1, Vector4D v2, Vector4D v3, Color color)
        {
            this.color = color;
            vertices = new Vector4D[3];
            vertices[0] = v1;
            vertices[1] = v2;
            vertices[2] = v3;

            normalVectors = new Vector4D[3];
        }

        public Triangle(Vector3D v1, Vector3D v2, Vector3D v3, Vector3D vn1, Vector3D vn2, Vector3D vn3, Vector3D wc1, Vector3D wc2, Vector3D wc3, Color color)
        {
            this.color = color;
            vertices = new Vector4D[3];
            vertices[0] = Vector4D.CreatePositionVector(v1);
            vertices[1] = Vector4D.CreatePositionVector(v2);
            vertices[2] = Vector4D.CreatePositionVector(v3);

            normalVectors = new Vector4D[3];
            normalVectors[0] = Vector4D.CreatePositionVector(vn1);
            normalVectors[1] = Vector4D.CreatePositionVector(vn2);
            normalVectors[2] = Vector4D.CreatePositionVector(vn3);

            WorldCoordinates = new Vector3D[3];
            WorldCoordinates[0] = wc1;
            WorldCoordinates[1] = wc2;
            WorldCoordinates[2] = wc3;
        }
        public Triangle(Vector4D v1, Vector4D v2, Vector4D v3, Vector3D vn1, Vector3D vn2, Vector3D vn3, Vector3D wc1, Vector3D wc2, Vector3D wc3, Color color)
        {
            this.color = color;
            vertices = new Vector4D[3];
            vertices[0] = v1;
            vertices[1] = v2;
            vertices[2] = v3;

            normalVectors = new Vector4D[3];
            normalVectors[0] = Vector4D.CreatePositionVector(vn1);
            normalVectors[1] = Vector4D.CreatePositionVector(vn2);
            normalVectors[2] = Vector4D.CreatePositionVector(vn3);

            WorldCoordinates = new Vector3D[3];
            WorldCoordinates[0] = wc1;
            WorldCoordinates[1] = wc2;
            WorldCoordinates[2] = wc3;
        }

        public Triangle(Vector4D v1, Vector4D v2, Vector4D v3, Vector4D vn1, Vector4D vn2, Vector4D vn3, Vector3D wc1, Vector3D wc2, Vector3D wc3,  Color color)
        {
            this.color = color;
            vertices = new Vector4D[3];
            vertices[0] = v1;
            vertices[1] = v2;
            vertices[2] = v3;

            normalVectors = new Vector4D[3];
            normalVectors[0] = vn1;
            normalVectors[1] = vn2;
            normalVectors[2] = vn3;

            WorldCoordinates = new Vector3D[3];
            WorldCoordinates[0] = wc1;
            WorldCoordinates[1] = wc2;
            WorldCoordinates[2] = wc3;
        }

        private Vector4D GetNormalToPoint(Vector4D p, Vector3D n)
        {
            var v = new Vector3D(p.values[0] + n.X, p.values[1] + n.Y, p.values[2] + n.Z);
            return Vector4D.CreatePositionVector(v);
        }

        private Vector3D GetNormalToFace(Vector3D mid)
        {
            Vector3D n1, n2;
            double xa = vertices[2].values[0] - vertices[0].values[0];
            double xb = vertices[2].values[0] - vertices[1].values[0];
            double ya = vertices[2].values[1] - vertices[0].values[1];
            double yb = vertices[2].values[1] - vertices[1].values[1];
            double za = vertices[2].values[2] - vertices[0].values[2];
            double zb = vertices[2].values[2] - vertices[1].values[2];

            n1 = new Vector3D(ya * zb - za * yb, za * xb - xa * zb, xa * yb - ya * xb);
            n2 = new Vector3D(yb * za - zb * ya, zb * xa - xb * za, xb * ya - yb * xa);
            n1.Normalize();
            n2.Normalize();
            return Distance(n1, mid) > Distance(n2, mid) ? n1 : n2;
        }

        private double Distance(Vector3D v1, Vector3D v2)
        {
            return Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y) + (v1.Z - v2.Z) * (v1.Z - v2.Z));
        }

        /// <summary>
        /// Splits one triangle into two adjecent triangles - upper and bottom respectively
        /// In upper triangle vertex with the greatest Y value is 1 vertices[0]
        /// In bottom triangle vertex with the lowest Y value is 3 vertices[2]
        /// </summary>
        /// <returns>upper and bottom triangle</returns>
        public Tuple<Triangle, Triangle> SplitHorizontal()
        {
            Vector4D v1 = new Vector4D(); // highest
            Vector4D v2 = new Vector4D(); // middle 
            Vector4D v3 = new Vector4D(); // lowest
            Vector4D vn1 = new Vector4D();
            Vector4D vn2 = new Vector4D();
            Vector4D vn3 = new Vector4D();
            Vector3D ws1 = new Vector3D();
            Vector3D ws2 = new Vector3D();
            Vector3D ws3 = new Vector3D();

            double min = double.MaxValue;
            double max = double.MinValue;
            int ind_min = -1;
            int ind_max = -1;

            for (int i = 0; i < 3; i++)
            {
                if (vertices[i].values[1] > max) // compare Y values
                {
                    v1 = vertices[i];
                    vn1 = normalVectors[i];
                    ws1 = WorldCoordinates[i];
                    max = vertices[i].values[1];
                    ind_max = i;
                }
                if (vertices[i].values[1] < min) // compare Y values
                {
                    v3 = vertices[i];
                    vn3 = normalVectors[i];
                    ws3 = WorldCoordinates[i];
                    min = vertices[i].values[1];
                    ind_min = i;
                }
            }
            for (int j = 0; j < 3; j++)
            {
                if (j != ind_min && j != ind_max)
                {
                    v2 = vertices[j];
                    ws2 = WorldCoordinates[j];
                    vn2 = normalVectors[j];
                    break;
                }
            }

            // cases when triangle is already upper or bottom triangle
            double eps2 = 0.1;
            var tmp4 = new Vector4D();
            var tmp3 = new Vector3D();
            if (Math.Abs(v2.values[1] - v1.values[1]) < eps2)
            {
                if (v1.values[0] > v2.values[0])
                {
                    return new Tuple<Triangle, Triangle>(null, new Triangle(v2, v1, v3, tmp4, tmp4, tmp4, tmp3, tmp3, tmp3, this.color));
                }
                else
                {
                    return new Tuple<Triangle, Triangle>(null, new Triangle(v1, v2, v3, tmp4, tmp4, tmp4, tmp3, tmp3, tmp3, this.color));
                }
            }
            if (Math.Abs(v2.values[1] - v3.values[1]) < eps2)
            {
                if (v2.values[0] > v3.values[0])
                {
                    return new Tuple<Triangle, Triangle>(new Triangle(v1, v2, v3, tmp4, tmp4, tmp4, tmp3, tmp3, tmp3, this.color), null);
                }
                else
                {
                    return new Tuple<Triangle, Triangle>(new Triangle(v1, v3, v2, tmp4, tmp4, tmp4, tmp3, tmp3, tmp3, this.color), null);
                }
            }

            double m = (v1.values[1] - v3.values[1]) / (v1.values[0] - v3.values[0]);
            double b = v1.values[1] - v1.values[0] * m;
            double Ys = v2.values[1];
            double eps = 0.001;
            double Xs = 0;
            if (Math.Abs(m) < eps)
                Xs = v1.values[0];
            else
                Xs = (Ys - b) / m;

            double mz = (v1.values[1] - v3.values[1]) / (v1.values[2] - v3.values[2]);
            double bz = v1.values[1] - v1.values[2] * mz;
            double Zs = 0;
            if (Math.Abs(mz) < eps)
                Zs = v1.values[2];
            else
                Zs = (Ys - bz) / mz;

            Vector4D VS = new Vector4D(Xs, Ys, Zs, 1);

            // distance between v1 and v3
            double dist1 = Math.Sqrt(Math.Pow(v1.values[0] - v3.values[0], 2) + Math.Pow(v1.values[1] - v3.values[1], 2) + Math.Pow(v1.values[2] - v3.values[2], 2));
            // distance between v1 and vs
            double dist2 = Math.Sqrt(Math.Pow(v1.values[0] - VS.values[0], 2) + Math.Pow(v1.values[1] - VS.values[1], 2) + Math.Pow(v1.values[2] - VS.values[2], 2));
            double diff = dist2 / dist1;

            // interpolate normal vector in the S point
            Vector3D wcS = new Vector3D(ws1.X * (1-diff) + ws3.X * (diff), ws1.Y * (1-diff) + ws3.Y * (diff), ws1.Z * (1 - diff) + ws3.Z * diff);
            Vector3D vnS = new Vector3D(vn1.values[0] * (1 - diff) + vn3.values[0] * diff, vn1.values[1] * (1 - diff) + vn3.values[1] * diff, vn1.values[2] * (1 - diff) + vn3.values[2] * diff);
            
            var vnS4 = Vector4D.CreatePositionVector(vnS);

            // ensure to create CW oriented triangles
            if (Xs < v2.values[0])
            {
                return new Tuple<Triangle, Triangle>(new Triangle(v1, v2, VS, vn1, vn2, vnS4, ws1, ws3, wcS, this.color), new Triangle(VS, v2, v3, vnS4, vn2, vn3, wcS, ws2, ws3, this.color));
            }
            else
            {
                return new Tuple<Triangle, Triangle>(new Triangle(v1, VS, v2, vn1, vnS4, vn2, ws1, wcS, ws2, this.color), new Triangle(v2, VS, v3, vn2, vnS4, vn3, ws2, wcS, ws3, this.color));
            }
        }

        public void TransformToNDC()
        {
            foreach (var v in vertices)
            {
                v.values[0] = v.values[0] / v.values[3];
                v.values[1] = v.values[1] / v.values[3];
                v.values[2] = v.values[2] / v.values[3];
                v.values[3] = 1;
            }
        }

        public void TransformToScreen(int w, int h)
        {
            foreach (var v in vertices)
            {
                v.values[0] = v.values[0] * (w / 2) + (w / 2);
                v.values[1] = (v.values[1] * (h / 2) + (h / 2));
                v.values[3] = 1;
            }
        }

        public void ApplyTransformationMatrix(Matrix4x4 m)
        {
            vertices[0] = m * vertices[0];
            vertices[1] = m * vertices[1];
            vertices[2] = m * vertices[2];
        }

        public override string ToString()
        {
            return "Triangle: " + vertices[0].ToString() + "  " + vertices[1].ToString() + "  " + vertices[2].ToString();
        }
    }
}
