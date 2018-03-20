using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Media3D;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab4

{
    [Serializable]
    class Cone : Primitive, ISerializable
    {
        private static int counter = 0;
        public double Radius;
        public double Height;
        public int N;
        public Cone(Color color) : base()
        {
            base.color = color;
            N = 3;
            Radius = 1;
            Height = 2;
            points = new List<Vector3D>();
            for (int i = 0; i < N; i++)
            {
                points.Add(new Vector3D(Radius * Math.Sin(Math.PI * 2 * i / N), 0, Radius * Math.Cos(Math.PI * 2 * i / N)));
            }
            points.Add(new Vector3D(0, 0, 0));
            points.Add(new Vector3D(0, Height, 0));
            id = counter++;
            base.SaveOriginalState();
        }

        public Cone(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            N = (int)info.GetValue("N", typeof(int));
            Radius = (double)info.GetValue("R", typeof(double));
            Height = (double)info.GetValue("H", typeof(double));
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("N", N);
            info.AddValue("R", Radius);
            info.AddValue("H", Height);
        }

        public override List<Triangle> GetTriangles()
        {
            ApplyTrasformations();
            List<Triangle> result = new List<Triangle>();

            for (int i = 0; i < N; i++)
            {
                result.Add(new Triangle(points[N], points[i], points[(i + 1) % N], color));
                result.Add(new Triangle(points[N + 1], points[(i + 1) % N], points[i], color));
            }

            return result;
        }

        public override string ToString()
        {
            return "Cone " + counter;
        }
    }
}
