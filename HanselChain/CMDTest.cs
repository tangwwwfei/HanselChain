﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HanselChain
{
	class CMDTest
	{
		public static void Test(String input, String output)
		{
			const int nDim = 5;
			int runTimes = 0;
			DateTime startTime = DateTime.Now;
			List<HanselChain> hcs = GenerateCube.GenerateNdimCubeAndHanselChain(nDim);
			FunctionInference functionInference = new FunctionInference();
			foreach (HanselChain hc in hcs)
			{
				foreach (NPoint p in hc.chain)
				{
					NPoint dp = p.DeepClone();
					dp.gfuncValue = 0;
					GFunction.getInstance().mapGValue.Add(dp.toInt(), dp);
				}
			}
			XElement xe = XElement.Load(input);
			IEnumerable<XElement> elements = from ele in xe.Elements("I_f") select ele;
			foreach (XElement e in elements)
			{
				String f = e.Element("f").Value;
				var gs = e.Elements("g");
				foreach (var g in gs)
				{
					String gfunc = g.Value;
					GFunction.getInstance().g_function = gfunc;
					functionInference.RunAlgorithm(null, nDim, hcs);
					functionInference.A.Sort(delegate (NPoint p1, NPoint p2)
					{
						BigInteger i1 = p1.toInt();
						BigInteger i2 = p2.toInt();
						return -i1.CompareTo(i2);
					});
					String getf = "";
					for (int i = 0; i < f.Length; i++)
					{
						getf += functionInference.A[i].realValue.Value;
						functionInference.A[i].Destory();
					}
					if (!f.Equals(getf) || functionInference.asked.Count > 20)
					{
						Console.Out.WriteLine("g:{0},f:{1}", gfunc, f);
					}
					++runTimes;
					if (runTimes % 1000 == 0)
					{
						Console.Out.WriteLine("运行{0}次!用时{1}", runTimes, (DateTime.Now-startTime).ToString());
					}
				}
			}
		}
	}
}