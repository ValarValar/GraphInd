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

        private void button1_Click_1(object sender, EventArgs e)
        {
            GrahamSort();
            DrawEdges();
        }
    }
}
