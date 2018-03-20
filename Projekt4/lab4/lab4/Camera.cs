using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace lab4
{
    class Camera
    {
        private static int counter = 0;
        public Vector3D target;
        public Vector3D position;
        public int fov;
        private int id;
        public Camera(Vector3D position, Vector3D target, int fov)
        {
            this.position = position;
            this.target = target;
            this.fov = fov;
            id = counter++;
        }

        public void MoveTarget(double dx, double dy, double dz)
        {
            this.target = new Vector3D(target.X + dx, target.Y + dy, target.Z + dz);
        }

        public void MovePosition(double dx, double dy, double dz)
        {
            this.position = new Vector3D(position.X + dx, position.Y + dy, position.Z + dz);
        }

        public override string ToString()
        {
            return "Camera " + id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false; 
            var c = obj as Camera;
            if (c == null) return false;
            return c.fov == this.fov && c.position.Equals(this.position) && c.target.Equals(this.target);
        }
    }
}
