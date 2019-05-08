using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanselChain
{
	class GenerateCube
	{
		public static List<HanselChain> GenerateNdimCubeAndHanselChain(int nDim)
		{
			HanselChain hc1 = new HanselChain();
			NPoint p1 = new NPoint();
			NPoint p2 = new NPoint();
			p2.x = new List<int>() { 0 };
			p1.x = new List<int>() { 1 };
			hc1.chain.Add(p1);
			hc1.chain.Add(p2);
			List<HanselChain> ls = new List<HanselChain>();
			ls.Add(hc1);

			List<HanselChain> result = _RNdimCubeAndHanselChain(nDim, 1, ls);
			//给Hansel链排序并赋一个id
			result.Sort();
			for (int i = 0; i < result.Count; i++)
			{
				result[i].id = i;
			}
			return result;
		}

		//递归生成Hansel链
		public static List<HanselChain> _RNdimCubeAndHanselChain(int NDim, int currentDim, List<HanselChain> hcs)
		{
			if (currentDim >= NDim)
			{
				return hcs;
			}
			List<HanselChain> newChainLst = new List<HanselChain>();
			foreach (HanselChain hc in hcs)
			{
				HanselChain hc1 = hc.DeepClone();
				HanselChain hc2 = hc.DeepClone();
				for (int i = 0; i < hc.chain.Count; i++)
				{
					hc1.chain[i].x.Insert(0, 0);    //在左侧加0
					hc1.chain[i].belong = hc1;
					hc2.chain[i].x.Insert(0, 1);    //在左侧加1
					hc2.chain[i].belong = hc2;
				}
				NPoint minPoint = hc1.chain[hc1.chain.Count - 1].DeepClone();
				hc1.chain.RemoveAt(hc1.chain.Count - 1);
				minPoint.belong = hc2;
				hc2.chain.Add(minPoint);
				if (hc1.chain.Count != 0) newChainLst.Add(hc1);
				if (hc2.chain.Count != 0) newChainLst.Add(hc2);
			}
			return _RNdimCubeAndHanselChain(NDim, currentDim + 1, newChainLst);
		}
	}
}
