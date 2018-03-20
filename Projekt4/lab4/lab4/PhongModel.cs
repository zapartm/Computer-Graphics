using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace lab4
{
    class PhongModel
    {
        public double k_d;
        public double k_s;
        public double I_light;
        public double n_shiny;

        public Vector3D lightPosition;

        public PhongModel(double x, double y, double z)
        {
            k_d = 0.5;
            k_s = 0.5;
            I_light = 0.8;
            n_shiny = 1;
            lightPosition = new Vector3D(x, y, z);
        }

        public double calculateIntensity(Camera c, Vector3D vertex, Vector4D N)
        {
            N.Normalize();
            Vector3D L = new Vector3D(lightPosition.X - vertex.X, lightPosition.Y - vertex.Y, lightPosition.Z - vertex.Z);

            L.Normalize();
            double Id = k_d * I_light * (N.values[0] * L.X + N.values[1] * L.Y + N.values[2] * L.Z);
            if (Id < 0) return 0;

            double tmp = 2 * (N.values[0] * L.X + N.values[1] * L.Y + N.values[2] * L.Z);
            Vector3D R = new Vector3D(tmp * N.values[0] - L.X, tmp * N.values[1] - L.Y, tmp * N.values[2] - L.Z);
            Vector3D V = new Vector3D(vertex.X - c.position.X, vertex.Y - c.position.Y, vertex.Z - c.position.Z);
            R.Normalize();
            V.Normalize();
            double VRdot = V.X * R.X + V.Y * R.Y + V.Z * R.Z;
            double Is = 0;
            if(VRdot >=0 )
                Is = k_s * I_light * Math.Pow(VRdot, n_shiny);

            //System.Diagnostics.Debug.WriteLine(L.X + " " + L.Y + " " + L.Z);
            return Id + Is;
        }
    }
    
}
