using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab4
{
    class GraphicAlgorithms
    {
        public static void FillTriangle(Graphics g, Triangle t)
        {
            Tuple<Triangle, Triangle> tt = t.SplitHorizontal();
            if (tt.Item1 != null)
                FillBottomFlatTriangle(g, tt.Item1);
            if (tt.Item2 != null)
                FillUpperFlatTriangle(g, tt.Item2);
        }

        private static void FillBottomFlatTriangle(Graphics g, Triangle t)
        {
            //if (Math.Abs(t.vertices[0].values[1] - t.vertices[1].values[1]) <= 1) return;

            double invslope1 = Math.Abs((t.vertices[1].values[0] - t.vertices[0].values[0]) / (t.vertices[1].values[1] - t.vertices[0].values[1]));
            double invslope2 = Math.Abs((t.vertices[2].values[0] - t.vertices[0].values[0]) / (t.vertices[2].values[1] - t.vertices[0].values[1]));
            if (invslope1 > 100 || invslope2 > 100)
                return;
            if (t.vertices[1].values[0] < t.vertices[0].values[0])
                invslope1 = -invslope1;
            if (t.vertices[2].values[0] < t.vertices[0].values[0])
                invslope2 = -invslope2;


            double curx1 = t.vertices[0].values[0];
            double curx2 = t.vertices[0].values[0];

            Pen pen = Pens.Aquamarine;
            for (int scanLine = (int)t.vertices[0].values[1]; scanLine >= t.vertices[1].values[1]; scanLine--)
            {
                g.DrawLine(pen, (int)curx1, scanLine, (int)curx2, scanLine);
                curx1 += invslope1;
                curx2 += invslope2;
            }
        }

        private static void FillUpperFlatTriangle(Graphics g, Triangle t)
        {
            //if (Math.Abs(t.vertices[0].values[1] - t.vertices[2].values[1]) <= 1) return;

            double invslope1 = Math.Abs((t.vertices[2].values[0] - t.vertices[0].values[0]) / (t.vertices[2].values[1] - t.vertices[0].values[1]));
            double invslope2 = Math.Abs((t.vertices[2].values[0] - t.vertices[1].values[0]) / (t.vertices[2].values[1] - t.vertices[1].values[1]));
            if (invslope1 > 100 || invslope2 > 100)
                return;
            if (t.vertices[0].values[0] < t.vertices[2].values[0])
                invslope1 = -invslope1;
            if (t.vertices[1].values[0] < t.vertices[2].values[0])
                invslope2 = -invslope2;

            double curx1 = t.vertices[2].values[0];
            double curx2 = t.vertices[2].values[0];

            Pen pen = Pens.Aquamarine;
            for (int scanLine = (int)t.vertices[2].values[1]; scanLine <= t.vertices[0].values[1]; scanLine++)
            {
                g.DrawLine(pen, (int)curx1, scanLine, (int)curx2, scanLine);
                curx1 += invslope1;
                curx2 += invslope2;
            }
        }

    }
}
