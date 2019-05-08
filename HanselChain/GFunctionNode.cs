using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanselChain
{
	class GFunctionNode
	{
		public String name { get; set; }
		public int nDim { get; set; }
		public List<NPoint> points { get; set; }
		public String gFunction { get; set; }
		public GFunctionNode()
		{
			points = new List<NPoint>();
		}
	}
}
