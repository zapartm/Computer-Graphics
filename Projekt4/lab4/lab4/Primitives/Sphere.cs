using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab4
{
    [Serializable]
    class Sphere : Primitive, ISerializable
    {
        public int M; // number of horizontal slices (latitude)
        public int N; // number of vertical slices (longtitude)
        public double R; // radius
        private static int counter = 0;

        public Sphere(Color color) : base()
        {
            this.color = color;
            points = new List<Vector3D>();
            M = 15;
            N = 15;
            R = 1;

            points.Add(new Vector3D(0, R, 0));
            for (int i = 1; i < M - 1; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    double x = R * Math.Cos(2 * Math.PI * j / N) * Math.Sin(Math.PI * i / M);
                    double z = R * Math.Sin(2 * Math.PI * j / N) * Math.Sin(Math.PI * i / M);
                    double y = R * Math.Cos(Math.PI * i / M);
                    points.Add(new Vector3D(x, y, z));
                }
            }
            points.Add(new Vector3D(0, -R, 0));
            id = counter++;
            base.SaveOriginalState();
        }

        public Sphere(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            N = (int)info.GetValue("N", typeof(int));
            M = (int)info.GetValue("M", typeof(int));
            R = (double)info.GetValue("R", typeof(double));
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("N", N);
            info.AddValue("M", M);
            info.AddValue("R", R);
        }

        public override List<Triangle> GetTriangles()
        {
            ApplyTrasformations();
            var result = new List<Triangle>();

            //for (int j = 0; j < N - 1; j++)
            //{
            //    result.Add(new Triangle(points[0], points[j + 1], points[j + 2], color));
            //}

            //for (int i = 0; i < M - 3; i++)
            //{
            //    for (int j = 0; j <= N; j++)
            //    {
            //        result.Add(new Triangle(points[i * N + j + 1], points[i * N + j], points[(i + 1) * N + j], color));
            //        result.Add(new Triangle(points[i * N + j + 1], points[(i + 1) * N + j], points[(i + 1) * N + j + 1], color));
            //    }
            //}
            //for (int j = 1; j < N; j++)
            //{
            //    result.Add(new Triangle(points[points.Count - 1], points[(M - 3) * N + j + 1], points[(M - 3) * N + j], color));
            //}

            return result;
        }

        public override string ToString()
        {
            return "Sphere " + counter;
        }
    }

}
