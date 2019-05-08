using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MonotonicityTest.gGenerate;
using static MonotonicityTest.I_f;
using static MonotonicityTest.gNode;
using System.Diagnostics;

namespace MonotonicityTest
{
	class Test
	{
		static List<UInt32> lstData = new List<uint>();
		static UInt32 Func(UInt32 inf, int x)
		{
			return 1 & (inf >> x);
		}

		static bool? Monotone(int x, UInt32 fx, int y, UInt32 fy)
		{
			if (x == y) return true;
			int v = x | y;
			if (v == x)
			{
				//x > y
				return fy > fx ? false : true;
			}
			else if (v == y)
			{
				//y > x
				return fy < fx ? false : true;
			}
			else
			{
				return null;
			}
		}

		public static void GenerateMonotone()
		{
			const int n = 5;
			int xMax = (int)Math.Pow(2, n);
			UInt32 f = 0;
			DateTime time = DateTime.Now;
			int x = 0;
			bool bMonotone = true;
			FileStream fs = new FileStream("monotone.txt", FileMode.Create);
			StreamWriter sw = new StreamWriter(fs);
			int[] y = new int[n];
			UInt32[] fy = new UInt32[n];
			bool?[] mono = new bool?[n];
			for (f = UInt32.MaxValue; f > 0; f--)
			{
				bMonotone = true;
				for (x = 0; x < xMax; x++)
				{
					//x是一组5位的值，是函数的输入
					UInt32 fx = Func(f, x);//求函数值
										   //将x的n位分别翻转
					y[0] = x ^ 0b00001;//数组y表示翻转后的结果
					y[1] = x ^ 0b00010;
					y[2] = x ^ 0b00100;
					y[3] = x ^ 0b01000;
					y[4] = x ^ 0b10000;
					fy[0] = Func(f, y[0]);//
					fy[1] = Func(f, y[1]);
					fy[2] = Func(f, y[2]);
					fy[3] = Func(f, y[3]);
					fy[4] = Func(f, y[4]);
					mono[0] = Monotone(x, fx, y[0], fy[0]);
					mono[1] = Monotone(x, fx, y[1], fy[1]);
					mono[2] = Monotone(x, fx, y[2], fy[2]);
					mono[3] = Monotone(x, fx, y[3], fy[3]);
					mono[4] = Monotone(x, fx, y[4], fy[4]);

					if (mono[0] == false ||
						mono[1] == false ||
						mono[2] == false ||
						mono[3] == false ||
						mono[4] == false
						)
					{
						//非单调
						bMonotone = false;
						break;
					}
				}
				if (bMonotone)
				{
					lstData.Add(f);
					String str = Convert.ToString(f, 2);
					sw.WriteLine(str);
					//Console.Out.WriteLine(str);
				}
			}
			DateTime time2 = DateTime.Now;
			Console.Out.WriteLine((time2 - time).ToString());
			Console.Out.WriteLine("f:{0}. Get {0} Functions", f, lstData.Count);
			//清空缓冲区
			sw.Flush();
			//关闭流
			sw.Close();
			fs.Close();
		}

		public static void Main()
		{
			String s = Convert.ToString(UInt32.MaxValue, 2).PadLeft(32, '0');


			gGenerate generate = new gGenerate();
			FileStream fs = new FileStream("monotone.txt", FileMode.Open);
			StreamReader sr = new StreamReader(fs);
			String line;
			generate.nDim = 5;
			List<I_f> lstIf = new List<I_f>();
			while((line = sr.ReadLine()) != null)
			{
				BigInteger ffunc = Convert.ToUInt32(line, 2);    //把string转换成十进制
				UInt32 dd = (UInt32)ffunc;
				generate.ffunc = ffunc;
				I_f i = generate.Generate();
				lstIf.Add(i);
			}
			ToGenerateXML(lstIf);

			//TestDuplication
			List<BigInteger> integers = new List<BigInteger>();
			foreach (var ii in lstIf)
			{
				foreach (var g in ii.glist)
				{
					if (integers.Contains(g.g))
					{
						Console.Out.WriteLine(g.g);
					}
					integers.Add(g.g);
				}
			}
			return;
		}
		const int nDim = 5;
		const int Bits = 32;
		static void ToGenerateXML(List<I_f> lstIf)
		{
			List<XElement> lstI = new List<XElement>();
			foreach(var i in lstIf)
			{
				List<XElement> lstg = new List<XElement>();
				foreach (var g in i.glist)
				{
					XElement eleg = new XElement("g",
						new XAttribute("G",
							Convert.ToString(g.G, 2).PadLeft(nDim,'0')
						),
						ToBinaryString(g.g).PadLeft(Bits, '0')
					);
					lstg.Add(eleg);
				}

				XElement ele = new XElement("I_f",
					new XElement("f", ToBinaryString(i.f).PadLeft(Bits, '0')),
					lstg);
				lstI.Add(ele);
			}
			

			XDocument myXDoc = new XDocument(
			new XElement("I",
				lstI));
			myXDoc.Save("gGfunc.xml");
		}

		public static string ToBinaryString(BigInteger bigint)
		{
			var bytes = bigint.ToByteArray();
			var idx = bytes.Length - 1;

			// Create a StringBuilder having appropriate capacity.
			var base2 = new StringBuilder(bytes.Length * 8);

			// Convert first byte to binary.
			var binary = Convert.ToString(bytes[idx], 2);

			// Ensure leading zero exists if value is positive.
			if (binary[0] != '0' && bigint.Sign == 1)
			{
				base2.Append('0');
			}

			// Append binary string to StringBuilder.
			base2.Append(binary);

			// Convert remaining bytes adding leading zeros.
			for (idx--; idx >= 0; idx--)
			{
				base2.Append(Convert.ToString(bytes[idx], 2).PadLeft(8, '0'));
			}
			int n = (int)Math.Pow(2, bytes.Length);
			if (base2.Length > n && base2[n].Equals('0'))
			{
				base2 = base2.Remove(0, 1);
			}
			return base2.ToString();
		}
		public string BinToDec(string value)
		{
			// BigInteger can be found in the System.Numerics dll
			BigInteger res = 0;

			// I'm totally skipping error handling here
			foreach (char c in value)
			{
				res <<= 1;
				res += c == '1' ? 1 : 0;
			}

			return res.ToString();
		}

	}
}
