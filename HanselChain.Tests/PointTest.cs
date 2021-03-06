// <copyright file="PointTest.cs">Copyright ©  2019</copyright>
using System;
using System.Collections.Generic;
using System.Numerics;
using HanselChain;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HanselChain.Tests
{
    /// <summary>This class contains parameterized unit tests for Point</summary>
    [PexClass(typeof(NPoint))]
	[PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class PointTest
    {
		[TestMethod]
		public void Test()
		{
			NPoint p1 = new NPoint();
			NPoint p2 = new NPoint();
			p1.x = new List<int> { 1, 0, 1 };
			p2.x = new List<int> { 0, 1, 0 };
			AssertNull(p1 > p2);
		}

		[TestMethod]
		public void Test2()
		{
			NPoint p1 = new NPoint();
			NPoint p2 = new NPoint();
			p1.x = new List<int> { 1, 0, 1 };
			p2.x = new List<int> { 0, 0, 0 };
			AssertTrue(p1 > p2);
		}

		[TestMethod]
		public void Test3()
		{
			NPoint p1 = new NPoint();
			NPoint p2 = new NPoint();
			p1.x = new List<int> { 0, 1, 0 };
			p2.x = new List<int> { 1, 1, 1 };
			AssertFalse(p1 > p2);
		}

		[TestMethod]
		public void TestGenerateChain()
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
			List<HanselChain> result = Form1.GenerateNdimCubeAndHanselChain(2, 1, ls);
			Console.Out.WriteLine(String.Format("We get {0} HanselChain(s)", result.Count));
			foreach (HanselChain hc in result)
			{
				Console.Out.WriteLine(hc.ToString());
			}
		}

		[TestMethod]
		public void TestBitOr()
		{
			BigInteger b1 = new BigInteger(32);
			BigInteger b2 = new BigInteger(16);
			BigInteger b = b1 | b2;
			AssertTrue(b == 48);
			b = b1 & b2;
			AssertTrue(b == 0);
		}

		private void AssertTrue(bool? v)
		{
			if (v == null)
			{
				throw new Exception("NULL");
			}
			if (v != true)
			{
				throw new Exception("Wrong result");
			}
		}

		private void AssertFalse(bool? v)
		{
			if (v == null)
			{
				throw new Exception("NULL");
			}
			if (v != false)
			{
				throw new Exception("Wrong result");
			}

		}

		private void AssertNull(bool? v)
		{
			if (v != null)
			{
				throw new Exception("Wrong result");
			}
			
		}
	}
}
