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
		public String g_function { get; set; }

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
