using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab4
{
    [Serializable]
    class Box : Primitive, ISerializable
    {
        private static int counter = 0;
        public Box(Color color) : base()
        {
            points = new List<Vector3D>();
            pointsForNormals = new List<Vector3D>();
            this.color = color;
            points.Add(new Vector3D(-0.5, 0, -0.5));
            points.Add(new Vector3D(0.5, 0, -0.5));
            points.Add(new Vector3D(0.5, 1, -0.5));
            points.Add(new Vector3D(-0.5, 1, -0.5));
            points.Add(new Vector3D(-0.5, 0, 0.5));
            points.Add(new Vector3D(0.5, 0, 0.5));
            points.Add(new Vector3D(0.5, 1, 0.5));
            points.Add(new Vector3D(-0.5, 1, 0.5));

            pointsForNormals.Add(new Vector3D(-0.5, -1, -0.5)); // 0
            pointsForNormals.Add(new Vector3D(-0.5, 0, -1.5)); // 1
            pointsForNormals.Add(new Vector3D(-1.5, 0, -0.5)); // 2

            pointsForNormals.Add(new Vector3D(0.5, -1, -0.5)); // 3
            pointsForNormals.Add(new Vector3D(1.5, 0, -0.5)); // 4
            pointsForNormals.Add(new Vector3D(0.5, 0, -1.5)); // 5

            pointsForNormals.Add(new Vector3D(0.5, 2, -0.5)); // 6
            pointsForNormals.Add(new Vector3D(0.5, 1, -1.5)); // 7
            pointsForNormals.Add(new Vector3D(1.5, 1, -0.5)); // 8

            pointsForNormals.Add(new Vector3D(-0.5, 2, -0.5)); // 9
            pointsForNormals.Add(new Vector3D(-1.5, 1, -0.5)); // 10
            pointsForNormals.Add(new Vector3D(-0.5, 1, -1.5)); // 11

            pointsForNormals.Add(new Vector3D(-0.5, -1, 0.5)); // 12
            pointsForNormals.Add(new Vector3D(-0.5, 0, 1.5)); // 13
            pointsForNormals.Add(new Vector3D(-1.5, 0, 0.5)); // 14

            pointsForNormals.Add(new Vector3D(0.5, -1, 0.5)); // 15
            pointsForNormals.Add(new Vector3D(0.5, 0, 1.5)); // 16
            pointsForNormals.Add(new Vector3D(1.5, 0, 0.5)); // 17

            pointsForNormals.Add(new Vector3D(0.5, 2, 0.5)); // 18
            pointsForNormals.Add(new Vector3D(0.5, 1, 1.5)); // 19
            pointsForNormals.Add(new Vector3D(1.5, 1, 0.5)); // 20

            pointsForNormals.Add(new Vector3D(-0.5, 2, 0.5)); // 21
            pointsForNormals.Add(new Vector3D(-1.5, 1, 0.5)); // 22
            pointsForNormals.Add(new Vector3D(-0.5, 1, 1.5)); // 23
            id = counter++;
            base.SaveOriginalState();
        }

        public Box(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public override List<Triangle> GetTriangles()
        {
            ApplyTrasformations();
            List<Triangle> result = new List<Triangle>();
            result.Add(new Triangle(points[0], points[5], points[1], pointsForNormals[0], pointsForNormals[15], pointsForNormals[3], points[0], points[5], points[1], color));
            result.Add(new Triangle(points[0], points[4], points[5], pointsForNormals[0], pointsForNormals[12], pointsForNormals[15], points[0], points[4], points[5], color));
            result.Add(new Triangle(points[0], points[1], points[2], pointsForNormals[1], pointsForNormals[5], pointsForNormals[7], points[0], points[1], points[2], color));
            result.Add(new Triangle(points[0], points[2], points[3], pointsForNormals[1], pointsForNormals[7], pointsForNormals[11], points[0], points[2], points[3], color));
            result.Add(new Triangle(points[1], points[5], points[2], pointsForNormals[4], pointsForNormals[17], pointsForNormals[8], points[1], points[5], points[2], color));
            result.Add(new Triangle(points[2], points[5], points[6], pointsForNormals[8], pointsForNormals[17], pointsForNormals[20], points[2], points[5], points[6], color));
            result.Add(new Triangle(points[0], points[3], points[7], pointsForNormals[2], pointsForNormals[10], pointsForNormals[22], points[0], points[3], points[7], color));
            result.Add(new Triangle(points[0], points[7], points[4], pointsForNormals[2], pointsForNormals[22], pointsForNormals[14], points[0], points[7], points[4], color));
            result.Add(new Triangle(points[4], points[7], points[5], pointsForNormals[13], pointsForNormals[23], pointsForNormals[16], points[4], points[7], points[5], color));
            result.Add(new Triangle(points[7], points[6], points[5], pointsForNormals[23], pointsForNormals[19], pointsForNormals[16], points[7], points[6], points[5], color));
            result.Add(new Triangle(points[3], points[2], points[7], pointsForNormals[9], pointsForNormals[6], pointsForNormals[21], points[3], points[2], points[7], color));
            result.Add(new Triangle(points[2], points[6], points[7], pointsForNormals[6], pointsForNormals[18], pointsForNormals[21], points[2], points[6], points[7], color));

            return result;
        }

        public override string ToString()
        {
            return "Box " + counter;
        }
    }
}
