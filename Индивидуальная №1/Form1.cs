using affine_transformations.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace affine_transformations
{
    public partial class Form1 : Form
    {
        private Graphics graphics;

        private List<Point2D> points = new List<Point2D>();
        private List<Point2D> pointsToDraw = new List<Point2D>();
        private List<Edge> edges = new List<Edge>();
        private List<Polygon> polygons = new List<Polygon>();

        private bool shouldStartNewPolygon = true;
        private bool shouldStartNewEdge = true;
        private Point2D edgeFirstPoint;

        private bool shouldShowDistance = false;
        private Edge previouslySelectedEdge;

        private Edge lastEdge;
        private Edge previousEdge;
        private Polygon lastPolygon;
        private Point2D lastPoint;


        private Primitive SelectedPrimitive
        {
            get
            {
                if (null == treeView1.SelectedNode) return null;
                var p = (Primitive)treeView1.SelectedNode.Tag;
                if (p is Edge) previouslySelectedEdge = (Edge)p;
                if (p is Point2D && shouldShowDistance)
                {
                    MessageBox.Show("Расстояние от отрезка до точки: " +
                        previouslySelectedEdge.Distance((Point2D)p));
                }
                if (!(p is Edge)) shouldShowDistance = false;
                return p;
            }
            set
            {
                Redraw();
            }
        }

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(2048, 2048);
            graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.Clear(Color.White);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            Point2D p = Point2D.FromPoint(args.Location);
            if (rbPoint.Checked)
            {
                TreeNode node = treeView1.Nodes.Add("Точка");
                node.Tag = p;
                points.Add(p);
            }
            Redraw();
        }

        private void Redraw()
        {
            graphics.Clear(Color.White);
            if (!shouldStartNewEdge) edgeFirstPoint.Draw(graphics, false);
            points.ForEach((p) => p.Draw(graphics, p == SelectedPrimitive));
            edges.ForEach((e) => e.Draw(graphics, e == SelectedPrimitive));
            polygons.ForEach((p) => p.Draw(graphics, p == SelectedPrimitive));
            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shouldStartNewPolygon = true;
        }

        private void rbPolygon_CheckedChanged(object sender, EventArgs e)
        {
            shouldStartNewPolygon = true;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedPrimitive = (Primitive)e.Node.Tag;
            if (SelectedPrimitive is Point2D)
                lastPoint = (Point2D)SelectedPrimitive;
            else if (SelectedPrimitive is Polygon)
                lastPolygon = (Polygon)SelectedPrimitive;
            else if (SelectedPrimitive is Edge)
            {
                previousEdge = lastEdge;
                lastEdge = (Edge)SelectedPrimitive;
            }

            Redraw();
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete != e.KeyCode) return;
            if (null == SelectedPrimitive) return;
            if (SelectedPrimitive is Point2D) points.Remove((Point2D)SelectedPrimitive);
            if (SelectedPrimitive is Edge) edges.Remove((Edge)SelectedPrimitive);
            if (SelectedPrimitive is Polygon) polygons.Remove((Polygon)SelectedPrimitive);
            treeView1.SelectedNode.Remove();
            if (null != treeView1.SelectedNode)
                SelectedPrimitive = (Primitive)treeView1.SelectedNode.Tag;
            else
                SelectedPrimitive = null;
            Redraw();
        }



        private void buttonDistance_Click(object sender, EventArgs e)
        {
            shouldShowDistance = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!shouldShowDistance) return;
        }



        // ---------- блок Ани

        // Уравнение прямой, проходящей через две заданные точки (x1,y1) и (x2,y2):
        // (y1 - y2)x + (x2 - x1)y + (x1y2 - x2y1) = 0
        private void lineEquation(Edge e, out float A, out float B, out float C)
        {
            A = e.A.Y - e.B.Y;
            B = e.B.X - e.A.X;
            C = e.A.X * e.B.Y - e.B.X * e.A.Y;
        }


        double rotate(Point2D A, Point2D B, Point2D C)
        {
            return (((B.X - A.X) * (C.Y - B.Y) - (B.Y - A.Y) * (C.X - B.X)));
        }
        bool compbyX(Point2D A, Point2D B)
        {
            return (A.X<B.X || A.X == B.X && A.Y<B.Y);
        }

        void GrahamSort()
        {
            int n = points.Count;
            //var p = new List<int>(n);
            for(int i=1;i<n;i++)
            {
                if (points[i].X < points[0].X)
                {
                    Point2D tmp = points[0];
                    points[0] = points[i];
                    points[i] = tmp;
                 }
            }
            for (int i = 2; i < n; i++)
            {
                int j = i;
                while (j > 1 && rotate(points[0], points[j-1], points[j]) < 0)
                {
                    Point2D tmp = points[j];
                    points[j] = points[j-1];
                    points[j-1] = tmp;
                    j -= 1;
                }
            }
            int p;
            pointsToDraw.Add(points[0]);
            pointsToDraw.Add(points[1]);
            

            for (int i = 2; i < n; i++)
            {
                while (rotate(pointsToDraw[pointsToDraw.Count - 2], pointsToDraw[pointsToDraw.Count - 1], points[i]) < 0)
                {
                    if (pointsToDraw.Count == 2) break;
                    pointsToDraw.Remove(pointsToDraw[pointsToDraw.Count - 1]);
                }
                pointsToDraw.Add(points[i]);
            }

        }

        void DrawEdges()
        {
            for (int i = 0; i < pointsToDraw.Count; i++)
            {
                edges.Add(new Edge(pointsToDraw[i], pointsToDraw[(i + 1) % pointsToDraw.Count]));
            }
            Redraw();
            
        }


        // Зная уравнения прямых : A1x + B1y + C1 = 0 и A2x + B2y + C2 = 0, 
        // получим точку пересечения по формуле Крамера:
        // x = - (C1B2 - C2B1)/(A1B2 - A2B1)
        // y = - (A1C2 - A2C1)/(A1B2 - A2B1)
        private Point2D findPoint(float A1, float A2, float B1, float B2, float C1, float C2, out float x, out float y)
        {
            var denom = A1 * B2 - A2 * B1;
            x = -1 * (C1 * B2 - C2 * B1) / denom;
            y = -1 * (A1 * C2 - A2 * C1) / denom;
            return (new Point2D(x, y));
        }

		private void button2_Click(object sender, EventArgs e)
		{
			if (edges.Count < 2)
				MessageBox.Show("Нарисуйте хотя бы два ребра");
			else
			{
				// var e1 = edges[edges.Count - 1];
				// var e2 = edges[edges.Count - 2];

				var e1 = previousEdge;
				var e2 = lastEdge;

				if (e1 == null || e2 == null)
					MessageBox.Show("Выберите два ребра");
				else
				{
					float A1, B1, C1, A2, B2, C2, xRes, yRes;
					lineEquation((Edge)e1, out A1, out B1, out C1);
					lineEquation(e2, out A2, out B2, out C2);
					var p = findPoint(A1, A2, B1, B2, C1, C2, out xRes, out yRes);

					if ((p.X >= Math.Min(e1.A.X, e1.B.X)) && (p.X <= Math.Max(e1.A.X, e1.B.X)) &&
						(p.X >= Math.Min(e2.A.X, e2.B.X)) && (p.X <= Math.Max(e2.A.X, e2.B.X)) &&
						(p.Y >= Math.Min(e1.A.Y, e1.B.Y)) && (p.Y <= Math.Max(e1.A.Y, e1.B.Y)) &&
						(p.Y >= Math.Min(e2.A.Y, e2.B.Y)) && (p.Y <= Math.Max(e2.A.Y, e2.B.Y)))
					{
						graphics.FillEllipse(new SolidBrush(Color.Yellow), p.X - 3, p.Y - 3, 6, 6);
						pictureBox1.Refresh();
					}
					else
						MessageBox.Show("Ребра не имеют общей точки");
				}
            }

        }



		// arccos( ( PVi-1 * PVi ) / |PVi-1| * |PVi| ) * sign ( det (PVi-1 PVi) )
		private double degreeBetweenEdges(Edge e1, Edge e2)
        {
            // находим координаты векторов по начальной и конечной точке
            var xE1 = e1.B.X - e1.A.X;
            var yE1 = e1.B.Y - e1.A.Y;

            var xE2 = e2.B.X - e2.A.X;
            var yE2 = e2.B.Y - e2.A.Y;

            // находим длины векторов 
            var lenE1 = Math.Sqrt(xE1 * xE1 + yE1 * yE1);
            var lenE2 = Math.Sqrt(xE2 * xE2 + yE2 * yE2);

            // Находим скалярное произведение PVi-1 * PVi
            var scalarProd = xE1 * xE2 + yE1 * yE2;
			
			return Math.Acos(scalarProd / (lenE1 * lenE2)) * Math.Sign(xE1 * yE2 - yE1 * xE2);
        }

        // определить принадлежность точки полигону методом углов
        private void button3_Click(object sender, EventArgs e)
        {
            if (polygons.Count == 0)
                MessageBox.Show("Нарисуйте хотя бы один многоугольник");
            else if (points.Count == 0)
                MessageBox.Show("Нарисуйте хотя бы одну точку");
            else 
            {
				var p = lastPoint;
				var poly = lastPolygon;

				if (p == null || poly == null)
					MessageBox.Show("Выберите точку и многоугольник");
				else
				{
					if (poly.Points.Count < 3)
						MessageBox.Show("Составьте полигон верно");
					else
					{
						double sumDegree = 0;

						for (int i = 0; i < poly.Points.Count - 1; ++i)
							sumDegree += degreeBetweenEdges(new Edge(p, poly.Points[i]), new Edge(p, poly.Points[i + 1]));

						sumDegree += degreeBetweenEdges(new Edge(p, poly.Points[poly.Points.Count - 1]), new Edge(p, poly.Points[0]));

						var eps = 0.0001;
						if (Math.Abs(sumDegree - 0) < eps)
							MessageBox.Show("Точка лежит снаружи многоугольника");
						else
							MessageBox.Show("Точка лежит внутри многоугольника");
					}
				}

            }
        }


		// ---------- конец блока

		private bool even_od_rule(Polygon pol, Point2D p2D)
		{
			graphics.FillEllipse(new SolidBrush(Color.Red), p2D.X - 3, p2D.Y - 3, 8, 8);

			for (int i = 0; i < pol.Points.Count - 1; i++)
				graphics.DrawLine(new Pen(Color.Red, 3), pol.Points[i].X, pol.Points[i].Y, pol.Points[i + 1].X, pol.Points[i + 1].Y);

			graphics.DrawLine(new Pen(Color.Red, 3), pol.Points[0].X, pol.Points[0].Y, pol.Points[pol.Points.Count - 1].X, pol.Points[pol.Points.Count - 1].Y);

			int avg_x = (int)(pol.Points[1].X + pol.Points[0].X) / 2;
			int avg_y = (int)(pol.Points[1].Y + pol.Points[0].Y) / 2;

			Point2D p_avg = Point2D.FromPoint(new Point(avg_x, avg_y));

			float rX = p2D.X - p_avg.X;
			float rY = p2D.Y - p_avg.Y;

			graphics.DrawLine(new Pen(Color.DeepPink, 1), new Point((int)p2D.X, (int)p2D.Y), new Point((int)p_avg.X, (int)p_avg.Y));
			pictureBox1.Refresh();

			var e = new Edge(p2D, p_avg);
			float A, B, C;
			lineEquation(e, out A, out B, out C); //Прямая, проходящая через заданую точку и середину первого ребра

			int count_point = 1;

			for (int i = 1; i < pol.Points.Count; ++i)
			{

				var e1 = new Edge(pol.Points[i], pol.Points[(i + 1) % pol.Points.Count]);


				float A1, B1, C1, xRes, yRes;
				lineEquation((Edge)e1, out A1, out B1, out C1);
				Point2D pr = findPoint(A1, A, B1, B, C1, C, out xRes, out yRes);

				if ((pr.X >= Math.Min(e1.A.X, e1.B.X)) && (pr.X <= Math.Max(e1.A.X, e1.B.X)) &&
					(pr.Y >= Math.Min(e1.A.Y, e1.B.Y)) && (pr.Y <= Math.Max(e1.A.Y, e1.B.Y)) &&
					(p2D.X - pr.X) * rX > 0 && (p2D.Y - pr.Y) * rY > 0)
				{
					graphics.DrawLine(new Pen(Color.Red, 1), new Point((int)p2D.X, (int)p2D.Y), new Point((int)pr.X, (int)pr.Y));
					pictureBox1.Refresh();
					++count_point;
				}
			}

			return count_point % 2 == 1;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (points.Count < 1 | polygons.Count < 1)
				MessageBox.Show("Не хватает точек или полигонов");
			else
			{
				if (even_od_rule(polygons[polygons.Count - 1], points[points.Count - 1]))
					MessageBox.Show("Да");
				else
					MessageBox.Show("Нет");
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{

			var edge = (Primitive)SelectedPrimitive;
			/* if (edge == null)
				 edge = points[points.Count - 1];*/

			var center = points[points.Count - 1];
			var moveToCenter = Transformation.Translate(-center.X, -center.Y);

			pictureBox1.Refresh();

		}

        private void button1_Click_1(object sender, EventArgs e)
        {
            GrahamSort();
            DrawEdges();
        }
    }
}
