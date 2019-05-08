using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;
using System.Drawing;

namespace HanselChain
{
	
	class PaintCube
	{
		public bool bInit { get; set; }
		public int nDim { get; set; }
		public List<NPoint> points { get; set; }
		public List<HanselChain> chains { get; set; }
		private const int correctY = 30;
		private const int unitLength = 80;
		private Point origin = new Point(0, 10);
		private List<Cube> cubes = new List<Cube>();
		public const int Diameter = 26;    //点的直径
		public readonly int Radius = Diameter/2;    //点的半径
		public List<NPoint> askedList { get; set; }
		public class Cube
		{
			public NPoint[] points { get; set; }
			public Cube(NPoint[] ps)
			{
				points = ps;
			}
			public void DrawCube(PaintCube pc, Graphics g)
			{
				
				foreach (NPoint item in points)
				{
					Point p = item.drawPoint;
					p.X = p.X - 15;
					p.Y = p.Y + 15;
					const int radius = Diameter / 2;
					g.DrawString(item.ToString(), SystemFonts.DefaultFont, Brushes.Black, p);
					Brush bush = Brushes.Yellow;
					if (pc.askedList != null && pc.askedList.Contains(item))
					{
						bush = Brushes.LightBlue;
					}
					g.FillEllipse(bush, item.drawPoint.X- radius, item.drawPoint.Y- radius, Diameter, Diameter);
				}
			}
		}

		List<NPoint> getPointsLayer(int k)
		{
			//int count = Cnk(nDim, k);
			List<NPoint> layer = new List<NPoint>();
			foreach(NPoint p in points)
			{
				if (k == p.HasNOne())
				{
					layer.Add(p);
				}
			}
			return layer;
		}

		
		public void Paint(int width, Graphics g)
		{
			//int cubeCnt = (int)Math.Pow(2, (nDim - 3));
			//if (cubes == null || cubes.Count != cubeCnt)
			//{
				
			//}
			Pen layer_pen = new Pen(Color.Blue, 2);
			Pen line_pen = new Pen(Color.Green, 1);
			Point start = new Point(0,10);
			Point end = start;
			end.X = start.X + width;
			for (int i = 0; i < points.Count; ++i)
			{
				for (int j = i + 1; j < points.Count; ++j)
				{
					if (points[i].OneBitDiff(points[j]))
					{
						g.DrawLine(line_pen, points[i].drawPoint, points[j].drawPoint);

					}

				}
			}
			foreach (HanselChain hc in chains)
			{
				hc.Paint(g);
			}
			for (int i = nDim; i >= 0; i--)
			{
				//Draw layer line
				//g.DrawLine(layer_pen, start, end);
				//DrawLayerPoint
				//List<NPoint> layer = getPointsLayer(i);
				foreach(Cube cube in cubes)
				{
					cube.DrawCube(this, g);
				}
				start.Y += unitLength;
				end.Y += unitLength;
			}


			//显示f和G的函数值
			Brush bush = new SolidBrush(Color.Black);//填充的颜色
			foreach (NPoint p in points)
			{
				String gf = String.Format("{0}", p.realValue);
				Point loc = p.drawPoint;
				loc.Y = loc.Y - Radius/2-1;
				loc.X = loc.X - Radius/2-1;
				g.DrawString(gf, SystemFonts.DefaultFont, bush, loc);
			}

			//将修改过的值用高亮显示出来
			//if (askedList != null)
			//{
			//	bush = new SolidBrush(Color.Red);//填充的颜色
			//	foreach (NPoint p in askedList)
			//	{
			//		String gf = String.Format("{0}", p.realValue);
			//		Point loc = p.drawPoint;
			//		loc.Y = loc.Y - Radius / 2 - 1;
			//		loc.X = loc.X - Radius / 2 - 1;
			//		g.DrawString(gf, SystemFonts.DefaultFont, bush, loc);
			//	}
			//}
		}

		public List<Cube> GenerateCube()
		{
			cubes = new List<Cube>();
			List<NPoint> lst = points.GetRange(0, 8);
			lst[0].drawPoint = new Point(2 * unitLength, correctY);
			lst[1].drawPoint = new Point(unitLength, 1 * unitLength + correctY);
			lst[2].drawPoint = new Point(2 * unitLength, unitLength + correctY);
			lst[4].drawPoint = new Point(3 * unitLength, unitLength + correctY);
			lst[3].drawPoint = new Point(unitLength, 2 * unitLength + correctY);
			lst[5].drawPoint = new Point(unitLength * 2, 2 * unitLength + correctY);
			lst[6].drawPoint = new Point(3 * unitLength, 2 * unitLength + correctY);
			lst[7].drawPoint = new Point(2 * unitLength, 3 * unitLength + correctY);
			Cube cube = new Cube(lst.ToArray());
			cubes.Add(cube);
			int cubeCnt = (int)Math.Pow(2, (nDim - 3));
			for (int i = 1; i < cubeCnt; i++)
			{
				Cube preCube = cubes[i - 1];
				lst = points.GetRange(i*8, 8);
				for (int j = 0; j < preCube.points.Length; j++)
				{
					Point newPoint = new Point();
					newPoint.X = preCube.points[j].drawPoint.X + 3 * unitLength;
					if (i != 2)
					{
						newPoint.Y = preCube.points[j].drawPoint.Y + unitLength;
					}
					else
					{
						newPoint.Y = preCube.points[j].drawPoint.Y;
					}
					lst[j].drawPoint = newPoint;
				}
				cube = new Cube(lst.ToArray());
				cubes.Add(cube);
			}
			return cubes;
		}

		int Cnk(int n, int k)
		{
			int ret;
			BigInteger x = Factorial(n);
			BigInteger y = Factorial(k);
			BigInteger z = Factorial(n - k);
			BigInteger rr = x / y / z;
			ret = (int)rr;
			return ret;
		}

		BigInteger Factorial(BigInteger n)
		{
			BigInteger bigInteger = 1;
			for (int i = 2; i <= n; i++)
			{
				bigInteger *= i;
			}
			return bigInteger;
		}
	}
}
