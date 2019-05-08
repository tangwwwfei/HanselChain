using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;
using System.Drawing;

namespace HanselChain
{
	[Serializable]
	public class NPoint : IComparable<NPoint>
	{
		public HanselChain belong { get; set; }
		//public int N { get; set; }
		public List<int> x { get; set; }
		public Point drawPoint { get; set; }
		public int? gfuncValue { get; set; }    //函数初始值为NULL
		public int? ffuncValue { get; set; }	//函数初始值为NULL
		public int? realValue
		{
			get
			{
				if (ffuncValue != null)
				{
					return ffuncValue;
				}
				if (gfuncValue != null)
				{
					return gfuncValue;
				}
				return null;
			}
		}
		public List<NPoint> upper { get; set; }
		public List<NPoint> lower { get; set; }
		public NPoint()
		{
			upper = new List<NPoint>();
			lower = new List<NPoint>();
		}

		public bool IsClicked(Point pos, int radius)
		{
			int x = pos.X - drawPoint.X;
			int y = pos.Y - drawPoint.Y;
			double v = Math.Pow(x, 2) + Math.Pow(y, 2);
			double r2 = Math.Pow(radius, 2);
			if (v <= r2)
			{
				return true;
			}
			return false;
		}
		public int HasNOne()
		{
			int count = 0;
			foreach(int c in x)
			{
				if (c == 1)
				{
					++count;
				}
			}
			return count;
		}
		public bool OneBitDiff(NPoint other)
		{
			int n = 0;
			for (int i = 0; i < x.Count; i++)
			{
				if (x[i] != other.x[i])
				{
					n++;
				}
				if (n > 1)
				{
					return false;
				}
			}
			return true;
		}
		public BigInteger toInt()
		{
			BigInteger ret = new BigInteger(0);
			int cnt = x.Count - 1;
			for (int i = cnt; i >= 0; i--)
			{
				if (x[i] == 1)
				{
					ret += (BigInteger)Math.Pow(2,cnt-i);
				}
			}

			return ret;
		}

		public static bool? operator >(NPoint p1, NPoint p2)
		{
			bool? p1_greater = null;
			for (int i = 0; i < p1.x.Count; i++)
			{
				if (p1.x[i] > p2.x[i])
				{
					if (p1_greater != null && p1_greater == false)
					{
						return null;
					}
					p1_greater = true;
				}
				else if (p1.x[i] == p2.x[i])
				{
					//Do nothing
				}
				else
				{
					if (p1_greater != null && p1_greater == true)
					{
						return null;
					}
					p1_greater = false;
				}
			}
			return p1_greater;
		}

		public static bool? operator <(NPoint p1, NPoint p2)
		{
			bool? b = p1 > p2;
			if (b == null) return null;
			return !b;
		}

		public override string ToString()
		{
			String points = "";
			foreach (int p in x)
			{
				points += p + ",";
			}
			points = points.Remove(points.Length - 1);
			return String.Format("({0})", points);
		}

		public NPoint DeepClone()
		{
			using (Stream objectStream = new MemoryStream())
			{
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(objectStream, this);
				objectStream.Seek(0, SeekOrigin.Begin);
				return formatter.Deserialize(objectStream) as NPoint;
			}
		}

		public int CompareTo(NPoint other)
		{
			int nOne = HasNOne();
			int oOne = other.HasNOne();
			BigInteger ts = toInt();
			BigInteger ot = other.toInt();
			if (nOne == oOne)
			{
				if (ts > ot)
				{
					return 1;
				}
				else
				{
					return -1;
				}
			}
			else if (nOne < oOne)
			{
				return 1;
			}
			else
			{
				return -1;
			}
		}
	}
}
