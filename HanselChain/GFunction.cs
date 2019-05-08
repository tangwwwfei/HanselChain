using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HanselChain
{
	class GFunction
	{
		static GFunction instance = null;
		public String g_function {
			get
			{
				return _g;
			}
			set
			{
				_g = value;
				List<NPoint> ps = GFunction.getInstance().mapGValue.Values.ToList<NPoint>();
				ps.Sort(
					delegate (NPoint p1, NPoint p2)
					{
						BigInteger i1 = p1.toInt();
						BigInteger i2 = p2.toInt();
						return -i1.CompareTo(i2);
					}
				);
				for (int i = 0; i < _g.Length; ++i)
				{
					ps[i].gfuncValue = int.Parse(_g.Substring(i, 1));
				}
			}
		}
		private String _g;

		public Dictionary<BigInteger, NPoint> mapGValue { get; set; }
		private GFunction()
		{
			mapGValue = new Dictionary<BigInteger, NPoint>();
		}
		public static GFunction getInstance()
		{
			if (instance == null)
			{
				instance = new GFunction();
			}
			return instance;
		}
		public int Calculate(NPoint points)
		{
			NPoint val;
			mapGValue.TryGetValue(points.toInt(), out val);
			Debug.Assert(val != null && val.gfuncValue != null);
			return val.gfuncValue.Value;
		}
	}
}
