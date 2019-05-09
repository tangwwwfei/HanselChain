using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace HanselChain
{
	class UnhandledReverseOrder
	{
		public NPoint alpha { get; set; }
		public NPoint beta { get; set; }
	}
	class FunctionInference
	{
		List<HanselChain> HanselChains { set; get; }
		Dictionary<BigInteger, NPoint> allPoints = new Dictionary<BigInteger, NPoint>();
		//Dictionary<BigInteger, NPoint> A = new Dictionary<BigInteger, NPoint>();
		public List<NPoint> A { get; }
		public List<NPoint> B { get; }
		public List<NPoint> Diff { set; get; }
		public List<NPoint> asked { get; }

		List<UnhandledReverseOrder> unhandledReverseOrders { get; set; }
		public NPoint G {
			get { return _G; }
		}
		private NPoint _G;
		int nDim { get; set; }
		bool isFromList;
		//
		AutoResetEvent runal;
		public FunctionInference()
		{
			A = new List<NPoint>();
			B = new List<NPoint>();
			asked = new List<NPoint>();
			unhandledReverseOrders = new List<UnhandledReverseOrder>();
		}

		public int RunAlgorithm(AutoResetEvent runal, int dim, List<HanselChain> chains)
		{
			A.Clear();
			B.Clear();
			asked.Clear();
			allPoints.Clear();
			unhandledReverseOrders.Clear();
			Diff = null;
			_G = null;

			this.runal = runal;
			nDim = dim;
			HanselChains = chains;
			foreach (HanselChain chain in HanselChains)
			{
				B.AddRange(chain.chain);
			}
			B.Sort(
				delegate (NPoint p1, NPoint p2) {
					return -p1.belong.id.CompareTo(p2.belong.id);
				}
			);
			//初始化Map allPoints
			foreach (NPoint p in B)
			{
				allPoints.Add(p.toInt(), p);
			}
			
			return Algorithm();
		}

		//return asked count
		private int Algorithm()
		{
			
			while(B.Count != 0)
			{
				if (runal != null) runal.WaitOne();
				//1.选点
				NPoint alpha_i = B[0];
				if (asked.Count >= 1)
				{
					NPoint alpha_i_1 = asked[asked.Count - 1];
					//如果αi的绝对值等于αi-1的绝对值，那么就选αi
					alpha_i = PickFromBShortest(alpha_i_1);
				}
				int val = GFunction.getInstance().Calculate(alpha_i);
				alpha_i.gfuncValue = val;
				//AddPointToA(alpha_i);
				AddPointsToA(new List<NPoint>() { alpha_i }, null, null);
				asked.Add(alpha_i);

				//2.查看是否出现逆序对
				//先判断是否有之前未处理的逆序对
				isFromList = true;
				foreach (var reverseOrder in unhandledReverseOrders)
				{
					HandleReverseOrder(reverseOrder.alpha, reverseOrder.beta);
				}
				isFromList = false;
				//
				bool bReverseOrder = false;
				NPoint getVal = null;
				if (val == 1)
				{
					List<NPoint> upoints = getDirectUpPoints(alpha_i);
					foreach (NPoint p in upoints)
					{
						if (A.Contains(p))
						{
							if (p.gfuncValue == 0)
							{
								//一次只会出现一个逆序对
								Debug.Assert(bReverseOrder != true);
								getVal = p;
								bReverseOrder = true;
								//break;
							}
						}
					}
				}
				else
				{
					List<NPoint> dpoints = getDirectDownPoints(alpha_i);
					foreach (NPoint p in dpoints)
					{
						
						if (A.Contains(p))
						{
							if (p.gfuncValue == 1)
							{
								Debug.Assert(bReverseOrder != true);
								getVal = p;
								bReverseOrder = true;
								//break;
							}
						}
					}
				}
				if (bReverseOrder)
				{
					Debug.Assert(getVal != null);
					//出现逆序对
					if (alpha_i.gfuncValue == 1)
					{
						HandleReverseOrder(alpha_i, getVal);
					}
					else
					{
						HandleReverseOrder(getVal, alpha_i);
					}
				}

				//3.共同确定
				//未出现逆序对 或 已经处理完逆序对
				for (int i = 0; i < A.Count; i++)
				{
					for (int j = i+1; j < A.Count; j++)
					{
						NPoint beta1 = A[i];
						NPoint beta2 = A[j];
						if (!beta1.Equals(beta2))
						{
							if (beta1.HasNOne() == beta2.HasNOne() && beta1.gfuncValue == beta2.gfuncValue)
							{
								HandleBeta1AndBeta2(beta1, beta2);
							}
						}
					}
				}

				//4.遍历A寻找非G
				for (int i = 0; i < A.Count; ++i)
				{
					NPoint beta = A[i];
					//找到全部的β∈A，g(β)==0？
					if (beta.gfuncValue == 0)
					{
						//找到β的所有直接下级γ，如果使g(γ)==1的γ的数量为0且g(γ)==0的γ的数量为|β|-1
						List<NPoint> dbeta = getDirectDownPoints(beta);
						int oneCnt = 0;
						int zeroCnt = 0;
						foreach (NPoint gamma in dbeta)
						{
							if (gamma.realValue == 1)
							{
								oneCnt++;
							}
							else if (gamma.realValue == 0)
							{
								zeroCnt++;
							}
						}
						if (oneCnt == 0 && (zeroCnt == (beta.HasNOne() - 1)))
						{
							//令β的所有下级δ，f（δ）=0，所有δ归于A
							AddPointsToA(dbeta, 0, null);
							//foreach (NPoint delta in dbeta)
							//{
							//	delta.ffuncValue = 0;
							//	AddPointToA(delta);
							//}
						}
					}
					else
					{
						//找到β的所有直接上级γ，如果使g(γ)==0的γ的数量为0且g(γ)==1的γ的数量为n-|β|-1
						List<NPoint> ubeta = getDirectUpPoints(beta);
						int oneCnt = 0;
						int zeroCnt = 0;
						foreach (NPoint gamma in ubeta)
						{
							if (gamma.realValue == 1)
							{
								oneCnt++;
							}
							else if (gamma.realValue == 0)
							{
								zeroCnt++;
							}
						}
						if (zeroCnt == 0 && (oneCnt == (nDim - beta.HasNOne() - 1)))
						{
							//令β的所有上级δ，f（δ）=1，所有δ归于A
							AddPointsToA(ubeta, 1, null);
							//foreach (NPoint delta in ubeta)
							//{
							//	delta.ffuncValue = 1;
							//	AddPointToA(delta);
							//}
						}
					}
				}
				
			}
			return asked.Count;
		}

		void HandleBeta1AndBeta2(NPoint beta1, NPoint beta2)
		{
			if (beta1.realValue == beta2.realValue && beta1.realValue == 1)
			{
				//计算γ==β1 or β2,按位或。令f(γ) = 1，γ归到A中
				BigInteger b = beta1.toInt() | beta2.toInt();
				NPoint gamma;
				allPoints.TryGetValue(b, out gamma);
				//gamma中1的数量-β1中1的数量，如果大于1则这个gamma不能用
				if (gamma.HasNOne() - beta1.HasNOne() > 1)
				{
					return;
				}

				gamma.ffuncValue = 1;
				//AddPointToA(gamma);
				AddPointsToA(new List<NPoint>() { gamma }, null, null);
				//另所有大于γ的γ’，f（γ‘）都等于1，所有的γ’归到A中
				List<NPoint> gammas = new List<NPoint>();
				for (int i =0; i < B.Count; ++i)
				{
					NPoint p = B[i];
					bool? greater = p > gamma;
					if (greater != null && greater == true)
					{
						gammas.Add(p);
						//p.ffuncValue = 1;
						//AddPointToA(p);
					}
				}
				AddPointsToA(gammas, 1, null);
			}
			else
			{
				//计算γ==β1 and β2,按位与。令f(γ) = 0，γ归到A中
				BigInteger b = beta1.toInt() & beta2.toInt();
				NPoint gamma;
				allPoints.TryGetValue(b, out gamma);
				//β1中1的数量-gamma中1的数量，如果大于1则这个gamma不能用
				if (beta1.HasNOne() - gamma.HasNOne() > 1)
				{
					return;
				}

				gamma.ffuncValue = 0;
				//AddPointToA(gamma);
				AddPointsToA(new List<NPoint>() { gamma }, null, null);
				//另所有小于γ的γ’，f（γ‘）都等于0，所有的γ’归到A中
				List<NPoint> gammas = new List<NPoint>();
				for (int i = 0; i < B.Count; ++i)
				{
					NPoint p = B[i];
					bool? less = p < gamma;
					if (less != null && less == true)
					{
						gammas.Add(p);
						//p.ffuncValue = 0;
						//AddPointToA(p);
					}
				}
				AddPointsToA(gammas, 0, null);
			}
		}

		void HandleReverseOrder(NPoint alpha/*g(α)=1*/, NPoint beta/*g(β)=0*/)
		{
			Debug.Assert(alpha.gfuncValue == 1);
			Debug.Assert(beta.gfuncValue == 0);
			List<NPoint> gamma = getDirectDownPoints(beta);
			int b1 = 0, b0 = 0;
			foreach (NPoint p in gamma)
			{
				if (A.Contains(p))
				{
					if (p.realValue == 1)
					{
						b1++;
					}
					else if (p.realValue == 0)
					{
						b0++;
					}
				}
			}

			gamma = getDirectUpPoints(alpha);
			int a1 = 0, a0 = 0;
			foreach (NPoint p in gamma)
			{
				if (A.Contains(p))
				{
					if (p.realValue == 1)
					{
						a1++;
					}
					else if (p.realValue == 0)
					{
						a0++;
					}
				}
			}

			if (b1 >= 2 || ((nDim - alpha.HasNOne()-a1) == 1))
			{
				//G=β，f（β）=1，令所有β的上级f都等于1且归于A
				_G = beta;
				//beta.ffuncValue = 1;
				//AddPointToA(beta);
				AddPointsToA(new List<NPoint>() { beta }, 1, null);
				List<NPoint> ubeta = getDirectUpPoints(beta);
				AddPointsToA(ubeta, 1, null);
				//foreach (NPoint p in ubeta)
				//{
				//	p.ffuncValue = 1;
				//	AddPointToA(p);
				//}
				
			}
			else
			{
				if (a0 >= 2 || ((beta.HasNOne()-b0) == 1))
				{
					//G = α，f（α）= 0，令所有α的下级f都等于0且归于A
					_G = alpha;
					//alpha.ffuncValue = 0;
					//AddPointToA(alpha);
					AddPointsToA(new List<NPoint>() { alpha }, 0, null);
					List<NPoint> dalpha = getDirectDownPoints(alpha);

					AddPointsToA(dalpha, 0, null);
					//foreach (NPoint p in dalpha)
					//{
					//	p.ffuncValue = 0;
					//	AddPointToA(p);
					//}
				}
				else
				{
					//两个条件都不满足
					//保存这个逆序对到逆序list当中
					if (!isFromList)
					{
						UnhandledReverseOrder uro = new UnhandledReverseOrder();
						uro.alpha = alpha;
						uro.beta = beta;
						unhandledReverseOrders.Add(uro);
					}
				}
			}
		}

		void AddPointsToA(List<NPoint> points, int? fv = null, int? gv = null)
		{
			Diff = points;
			foreach (NPoint p in points)
			{
				if (fv != null)
				{
					p.ffuncValue = fv.Value;
				}
				if (gv != null)
				{
					p.gfuncValue = gv.Value;
				}
				AddPointToA(p);
			}
		}

		void AddPointToA(NPoint p)
		{
			if (!A.Contains(p))
			{
				A.Add(p);
				B.Remove(p);
			}
		}

		List<NPoint> getDirectUpPoints(NPoint alpha)
		{
			if (alpha.upper.Count != 0)
			{
				return alpha.upper;
			}
			//将α中的0依次改为1
			for (int i = 0; i < alpha.x.Count; i++)
			{
				if (alpha.x[i] == 0)
				{
					//x[] = 10010
					//
					//NPoint upoint = alpha.DeepClone();
					alpha.x[i] = 1;
					NPoint val;
					if (allPoints.TryGetValue(alpha.toInt(), out val))
					{
						alpha.upper.Add(val);
					}
					alpha.x[i] = 0;
				}
			}
			return alpha.upper;
		}

		List<NPoint> getDirectDownPoints(NPoint alpha)
		{
			if (alpha.lower.Count != 0)
			{
				return alpha.lower;
			}
			//将α中的1依次改为0
			for (int i = 0; i < alpha.x.Count; i++)
			{
				if (alpha.x[i] == 1)
				{
					//NPoint dpoint = alpha.DeepClone();
					alpha.x[i] = 0;
					NPoint val;
					if (allPoints.TryGetValue(alpha.toInt(), out val))
					{
						alpha.lower.Add(val);
					}
					alpha.x[i] = 1;
				}
			}
			return alpha.lower;
		}

		private NPoint PickFromBShortest(NPoint pre)
		{
			NPoint ret = B[0];
			int len = ret.belong.chain.Count;//最短链的长度
			for (int j = 0; j < B.Count; j++)
			{
				if (B[j].belong.chain.Count != len)
				{
					break;
				}
				if (B[j].HasNOne() == pre.HasNOne())
				{
					ret = B[j];
					break;
				}
			}
			return ret;
		}
	}
}
