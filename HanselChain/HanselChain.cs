using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HanselChain
{
	[Serializable]
	public class HanselChain : IComparable<HanselChain>
	{
		public List<NPoint> chain { get; set; }  //按顺序排序
		public int id { get; set; }
		public HanselChain()
		{
			chain = new List<NPoint>();
		}

		public void Paint(Graphics g)
		{
			Pen pen = new Pen(Color.Pink, 3);
			for (int i = 0; i < chain.Count-1; i++)
			{
				g.DrawLine(pen, chain[i].drawPoint, chain[i + 1].drawPoint);
			}
		}

		public override string ToString()
		{
			String line = "";
			for (int i = 0; i < chain.Count; i++)
			{
				line += String.Format("Point{0}:{1} ", i, chain[i].ToString());
			}
			return line;
		}

		public HanselChain DeepClone()
		{
			using (Stream objectStream = new MemoryStream())
			{
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(objectStream, this);
				objectStream.Seek(0, SeekOrigin.Begin);
				return formatter.Deserialize(objectStream) as HanselChain;
			}
		}
		//决定Hansel链排序的比较方式
		public int CompareTo(HanselChain other)
		{
			//降序
			return -chain.Count.CompareTo(other.chain.Count);
		}
	}
}
