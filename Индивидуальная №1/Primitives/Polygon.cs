using System;
using System.Collections.Generic;
using System.Drawing;

namespace affine_transformations.Primitives
{
    class Polygon : Primitive
    {
        private List<Point2D> points = new List<Point2D>();

        public List<Point2D> Points { get { return points; } set { points = value; } }

        public Polygon() { }

        public Polygon(List<Point2D> points)
        {
            this.points = points;
        }

        public void Draw(Graphics g, bool selected)
        {
            if (1 == Points.Count)
                Points[0].Draw(g, selected);
            else
            {
                Pen pen = new Pen(selected ? Color.Red : Color.Black);
                pen.Width = 2;
                for (int i = 0; i < Points.Count - 1; ++i)
                    g.DrawLine(pen, Points[i].X, Points[i].Y, Points[i + 1].X, Points[i + 1].Y);
                g.DrawLine(pen, Points[0].X, Points[0].Y, Points[Points.Count - 1].X, Points[Points.Count - 1].Y);
            }
        }

        public void Apply(Transformation t)
        {
            foreach (var point in Points)
                point.Apply(t);
        }
    }
}
