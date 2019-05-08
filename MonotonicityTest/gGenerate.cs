using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonotonicityTest
{
	//函数g
	//属性g：函数本身
	//属性G：G点
	class gNode
	{
		public BigInteger g { get; set; }
		public int G { get; set; }
	}
	/*
		集合I(f)
		属性f ： 父函数
		属性glist ： 所有的g函数，来自f的
	*/
	class I_f
	{
		public BigInteger f { get; set; }
		public List<gNode> glist { get; set; }
	}

	class gGenerate
	{
		public BigInteger ffunc { get; set; }  //函数f
		public int nDim { get; set; }

		//输入x，求f(x)的函数值
		public BigInteger Func(int x)
		{
			return 1 & (ffunc >> x);
		}
		public I_f Generate()
		{
			I_f ret = new I_f();
			////////////////////test
			//ffunc = 0;
			///////////////////////
			ret.f = ffunc;
			ret.glist = new List<gNode>();

			//由于int只有32个二进制位的限制，最多支持到31维
			//点用int表示
			int pointsCount = (int)Math.Pow(2, nDim);
			List<BigInteger> deDup = new List<BigInteger>();
			for (int alpha = 0; alpha < pointsCount; alpha++)
			{
				int count = 0;
				int isFAlpha1 = Func(alpha) == 1 ? 1 : 0;	//f(α)等于1? 是，为1；否则为0
				List<int> betas = null;

				betas = FindDownUpPoints(alpha, isFAlpha1);	//如果f(α)==1，寻找下级。反之寻找上级
				foreach (int beta in betas)
				{
					if (Func(beta) == isFAlpha1)
					{
						count++;
						if (count >= 2)
						{
							//生成一个g
							gNode node = new gNode();
							BigInteger g = ffunc;
							BigInteger mod = 1;
							mod = mod << alpha;
							g = g ^ mod;
							if (!deDup.Contains(g))
							{
								deDup.Add(g);
								node.g = g;
								node.G = alpha;
								ret.glist.Add(node);
							}
							break;
						}
					}
				}
			}

			return ret;
		}

		private List<int> FindDownUpPoints(int alpha, int isDownOrUp/*down:1,up:0*/)
		{
			List<int> downUpPoints = new List<int>();
			//如果寻找下级，依次把alpha中的1换成0
			//反之，将0换成1
			int copy_alpha = alpha;
			int shift = 1;
			for (int k = 0; k < nDim; k++)
			{
				if ((copy_alpha & 1) == isDownOrUp) //寻找alpha中的1或0
				{
					downUpPoints.Add(alpha ^ shift);
				}
				copy_alpha = copy_alpha >> 1;
				shift = shift << 1;
			}
			return downUpPoints;
		}

		private List<int> FindUpPoints(int alpha)
		{
			List<int> upPoints = new List<int>();
			//依次把alpha中的0换成1
			for (int i = 0; i < nDim; i++)
			{
				int copy_alpha = alpha;
				int shift = 1;
				for (int k = 0; k < nDim; k++)
				{
					if ((copy_alpha & 1) == 0)
					{
						upPoints.Add(alpha ^ shift);
					}
					copy_alpha = copy_alpha >> 1;
					shift = shift << 1;
				}
			}
			return upPoints;
		}
	}
}
