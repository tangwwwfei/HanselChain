using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HanselChain
{
	class GFunctionFile
	{
		const String saveLocation = "gfunctions.xml";
		public static bool LoadGFunction(int nDim, out List<GFunctionNode> nodes)
		{
			nodes = new List<GFunctionNode>();
			List<String> pathes = new List<string>();
			XElement xe = XElement.Load(saveLocation);
			IEnumerable<XElement> elements = from ele in xe.Elements("GFunction")
											 where ele.Attribute("Dimension").Value.Equals(nDim.ToString())
											 select ele;
			foreach (XElement e in elements)
			{
				GFunctionNode node = new GFunctionNode();
				node.nDim = nDim;
				node.name = e.Attribute("name").Value;
				String vals = e.Element("value").Value;
				node.gFunction = vals;
				nodes.Add(node);
			}

			return true;
		}

		public static void SaveGFunction(GFunctionNode node)
		{
			if (!File.Exists(saveLocation))
			{
				XDocument xdoc = new XDocument();
				XElement rec = new XElement(
				new XElement("GFunctions"));
				xdoc.Add(rec);
				xdoc.Save(saveLocation);
			}
			node.points.Sort(
				delegate (NPoint p1, NPoint p2)
				{
					BigInteger i1 = p1.toInt();
					BigInteger i2 = p2.toInt();
					return -i1.CompareTo(i2);
				}
			);
			String value = "";
			foreach (NPoint p in node.points)
			{
				value += p.gfuncValue;
			}
			XElement xe = XElement.Load(saveLocation);
			XElement record = new XElement(
			new XElement("GFunction",
				new XAttribute("name", node.name),
				new XAttribute("Dimension", node.nDim),
				new XAttribute("date", DateTime.Now.ToString()),
				new XElement("value", value)));
			xe.Add(record);
			xe.Save(saveLocation);
		}
	}
}
