using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace lab4
{
    [Serializable]
    public class Matrix4x4
    {
        private Vector4D[] columns;

        /// <summary>
        /// Creates identity matrix 
        /// </summary>

        public Matrix4x4()
        {
            columns = new Vector4D[4];
            columns[0] = new Vector4D(1, 0, 0, 0);
            columns[1] = new Vector4D(0, 1, 0, 0);
            columns[2] = new Vector4D(0, 0, 1, 0);
            columns[3] = new Vector4D(0, 0, 0, 1);
        }

        /// <summary>
        /// Creates 4 dimension column major matrix
        /// </summary>
        /// <param name="v1"> column 1</param>
        /// <param name="v2"> column 2 </param>
        /// <param name="v3"> column 3</param>
        /// <param name="v4"> column 3</param>
        public Matrix4x4(Vector4D v1, Vector4D v2, Vector4D v3, Vector4D v4)
        {
            columns = new Vector4D[4];
            columns[0] = v1;
            columns[1] = v2;
            columns[2] = v3;
            columns[3] = v4;
        }

        /// <summary>
        /// Multiplies two 4-dimensional matrices 
        /// </summary>
        /// <param name="m1">matrix 1</param>
        /// <param name="m2">matrix 2</param>
        /// <returns></returns>
        public static Matrix4x4 operator *(Matrix4x4 m1, Matrix4x4 m2)
        {
            Vector4D[] v = new Vector4D[4];
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = new Vector4D(0, 0, 0, 0);
            }

            for (int k = 0; k < 4; k++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        v[k].values[j] += m1.columns[i].values[j] * m2.columns[k].values[i];
                    }
                }
            }
            return new Matrix4x4(v[0], v[1], v[2], v[3]);
        }

        public double this[int row, int col]
        {
            get { return columns[col].values[row]; }
            set { columns[col].values[row] = value; }
        }

        /// <summary>
        /// Yields vector product of matrix multiplied by vector
        /// </summary>
        /// <param name="m">matrix</param>
        /// <param name="v">vector</param>
        /// <returns></returns>
        public static Vector4D operator *(Matrix4x4 m, Vector4D v)
        {
            Vector4D result = new Vector4D(0, 0, 0, 0);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result.values[i] += m.columns[j].values[i] * v.values[j];
                }
            }
            return result;
        }

        public Matrix4x4 Transpose()
        {
            Matrix4x4 result = new Matrix4x4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i, j] = columns[i].values[j];
                }
            }

            return result;
        }
        
        /// <summary>
        /// Yields inverted matrix. Works only for affine matrices
        /// </summary>
        //public Matrix4x4 Inverse()
        //{
        //    Matrix3x3 M = new Matrix3x3(columns[0].values[0], columns[1].values[0], columns[2].values[0],
        //                                columns[0].values[1], columns[1].values[1], columns[2].values[1],
        //                                columns[0].values[2], columns[1].values[2], columns[2].values[2]);
        //    Vector3D b = new Vector3D(columns[3].values[0], columns[3].values[1], columns[3].values[2]);
        //    M = M.Inverse();

        //    Vector4D v1 = new Vector4D(M.values[0, 0], M.values[1, 0], M.values[2, 0], 0);
        //    Vector4D v2 = new Vector4D(M.values[0, 1], M.values[1, 1], M.values[2, 1], 0);
        //    Vector4D v3 = new Vector4D(M.values[0, 2], M.values[1, 2], M.values[2, 2], 0);
        //    Vector3D invMb = M.VectorProduct(b);
        //    Vector4D v4 = new Vector4D(-invMb.X, -invMb.Y, -invMb.Z, 1);

        //    return new Matrix4x4(v1, v2, v3, v4);
        //}

        public override string ToString()
        {
            string str = "```\n";
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    str += columns[i].values[j] + " ";
                }
                str += "\n";
            }
            str += "```\n";

            return str;
        }
    }
    
    class Matrix3x3
    {
        public double[,] values;
        public Matrix3x3(double v11, double v12, double v13, double v21, double v22, double v23, double v31, double v32, double v33)
        {
            values = new double[3,3];
            values[0, 0] = v11;
            values[0, 1] = v12;
            values[0, 2] = v13;
            values[1, 0] = v21;
            values[1, 1] = v22;
            values[1, 2] = v23;
            values[2, 0] = v31;
            values[2, 1] = v32;
            values[2, 2] = v33;
        }

        public Matrix3x3(double[,] vs)
        {
            values = new double[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    values[i, j] = vs[i, j];
                }
            }
        }

        public Matrix3x3 Inverse()
        {
            double[,] dets = new double[3, 3];
            double det = this.Determinant();

            dets[0, 0] = values[1, 1] * values[2, 2] - values[1, 2] * values[2, 1];
            dets[0, 1] = values[0, 2] * values[2, 1] - values[0, 1] * values[2, 2];
            dets[0, 2] = values[0, 1] * values[1, 2] - values[0, 2] * values[1, 1];

            dets[1, 0] = values[1, 2] * values[2, 0] - values[1, 0] * values[2, 2];
            dets[1, 1] = values[0, 0] * values[2, 2] - values[0, 2] * values[2, 0];
            dets[1, 2] = values[0, 2] * values[1, 0] - values[0, 0] * values[1, 2];

            dets[2, 0] = values[1, 0] * values[2, 1] - values[1, 1] * values[2, 0];
            dets[2, 1] = values[0, 1] * values[2, 0] - values[0, 0] * values[2, 1];
            dets[2, 2] = values[0, 0] * values[1, 1] - values[0, 1] * values[1, 0];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    dets[i, j] /= det;
                }
            }
            return new Matrix3x3(dets);
        }

        public Vector3D VectorProduct(Vector3D v)
        {
            var result = new double[3];
            result[0] = values[0, 0] * v.X + values[0, 1] * v.Y + values[0, 2] * v.Z;
            result[1] = values[1, 0] * v.X + values[1, 1] * v.Y + values[1, 2] * v.Z;
            result[2] = values[2, 0] * v.X + values[2, 1] * v.Y + values[2, 2] * v.Z;
            Vector3D vec = new Vector3D(result[0], result[1], result[2]);
            return vec;
        }

        public double Determinant()
        {
            return values[0, 0] * values[1, 1] * values[2, 2] + values[0, 1] * values[1, 2] * values[2, 0] + values[1, 0] * values[2, 1] * values[0, 2] - values[2, 0] * values[1, 1] * values[0, 2] - values[0, 1] * values[1, 0] * values[2, 2] - values[0, 0] * values[2, 1] * values[1, 2];
        }

    }

    [Serializable]
    public struct Vector4D
    {
        public double[] values;

        public Vector4D(double v1, double v2, double v3, double v4)
        {
            values = new double[4];
            values[0] = v1;
            values[1] = v2;
            values[2] = v3;
            values[3] = v4;
        }

        public Vector4D(double v1, double v2, double v3)
        {
            values = new double[4];
            values[0] = v1;
            values[1] = v2;
            values[2] = v3;
            values[3] = 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            double eps = 0.1;
            Vector4D v = (Vector4D)obj;
            return Math.Abs(v.values[0]-values[0]) < eps && Math.Abs(v.values[1] - values[1]) < eps && Math.Abs(v.values[2] - values[2]) < eps && Math.Abs(v.values[3] - values[3]) < eps;
        }

        /// <summary>
        /// Creates 4 dimensional vector with w=0
        /// </summary>
        /// <param name="v">X Y Z coordinates</param>
        /// <returns></returns>
        public static Vector4D CreateDirectionVector(Vector3D v)
        {
            return new Vector4D(v.X, v.Y, v.Z, 0);
        }

        /// <summary>
        /// Creates 4 dimensional vector with w=1
        /// </summary>
        /// <param name="v">X Y Z coordinates</param>
        /// <returns></returns>
        public static Vector4D CreatePositionVector(Vector3D v)
        {
            return new Vector4D(v.X, v.Y, v.Z, 1);
        }

        public static Vector4D operator+ (Vector4D v1, Vector4D v2)
        {
            return new Vector4D(v1.values[0] + v2.values[0], v1.values[1] + v2.values[1], v1.values[2] + v2.values[2], v1.values[3] + v2.values[3]);
        }

        public static Vector4D operator - (Vector4D v1, Vector4D v2)
        {
            return new Vector4D(v1.values[0] - v2.values[0], v1.values[1] - v2.values[1], v1.values[2] - v2.values[2], v1.values[3] - v2.values[3]);
        }

        public static Vector4D operator *(Vector4D v1, Vector4D v2)
        {
            return new Vector4D(v1.values[0] - v2.values[0], v1.values[1] - v2.values[1], v1.values[2] - v2.values[2], v1.values[3] - v2.values[3]);
        }

        public Vector4D Normalize()
        {
            Vector4D newV= new Vector4D(0,0,0,0);

            var d = Math.Sqrt(values[0] * values[0] + values[1] * values[1] + values[2] * values[2] + values[3] * values[3]);
            for (int i = 0; i < 4; i++)
            {
                newV.values[i] = values[i] / d;
            }

            return newV;
        }

        public string ToString(string eps)
        {
            string str = "[";
            foreach (var v in values)
                str += v.ToString(eps) + " ";
            str += "]";
            //str = values[2].ToString("0.000") + " ";
            return str;
        }
        public override string ToString()
        {
            string str = "[";
            foreach (var v in values)
                str += v.ToString("0.00000") + " ";
            str += "]";
            //str = values[2].ToString("0.000") + " ";
            return str;
        }
    }
}
