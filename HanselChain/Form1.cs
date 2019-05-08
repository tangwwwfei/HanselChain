using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HanselChain
{
	public partial class Form1 : Form
	{
		PaintCube paintCube = new PaintCube();
		List<GFunctionNode> gLoadNodes = null;
		List<HanselChain> result;
		List<HanselChain> forDesign = null;
		FunctionInference functionInference;
		Thread runAlgorithm = null;
		AutoResetEvent runal = null;
		List<NPoint> queryList = null;
		int nAskedCount;
		int nDim;
		public Form1()
		{
			InitializeComponent();
		}

		private void Start_Click(object sender, EventArgs e)
		{
			nDim = int.Parse(n_dim.Text); //维度
			HanselChain hc1 = new HanselChain();
			NPoint p1 = new NPoint();
			NPoint p2 = new NPoint();
			p2.x = new List<int>() { 0 };
			p1.x = new List<int>() { 1 };
			hc1.chain.Add(p1);
			hc1.chain.Add(p2);
			List<HanselChain> ls = new List<HanselChain>();
			ls.Add(hc1);
			forDesign = new List<HanselChain>();
			GFunction.getInstance().mapGValue.Clear();
			result = GenerateNdimCubeAndHanselChain(nDim, 1, ls);
			//给Hansel链排序并赋一个id
			result.Sort();
			for (int i = 0; i < result.Count; i++)
			{
				result[i].id = i;
				forDesign.Add(result[i].DeepClone());
			}

			Console.Out.WriteLine(String.Format("We get {0} HanselChain(s)", result.Count));
			foreach (HanselChain hc in result)
			{
				Console.Out.WriteLine(hc.ToString());
			}
			


			InitPaintCube(nDim, forDesign);
			functionInference = new FunctionInference(nDim, result);
			Next.Text = "执行算法";
			runal = new AutoResetEvent(false);
			listA.Items.Clear();
			listB.Items.Clear();
			listQuery.Items.Clear();
			listGFunc.Items.Clear();
			GValue.Text = "G:";
			queryList = null;
			if (runAlgorithm != null)
			{
				runAlgorithm.Abort();
				runAlgorithm = null;
				
			}
			CubeRepaint();
		}

		//递归生成Hansel链
		public static List<HanselChain> GenerateNdimCubeAndHanselChain(int NDim, int currentDim, List<HanselChain> hcs)
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
			return GenerateNdimCubeAndHanselChain(NDim, currentDim + 1, newChainLst);
		}

		public void InitPaintCube(int nDim, List<HanselChain> hcs)
		{
			List<NPoint> points = new List<NPoint>();
			foreach (HanselChain hc in hcs)
			{
				points.AddRange(hc.chain);
			}
			points.Sort(
				delegate (NPoint p1, NPoint p2)
				{
					BigInteger i1 = p1.toInt();
					BigInteger i2 = p2.toInt();
					return -i1.CompareTo(i2);
				}
			);
			paintCube.points = points;
			paintCube.nDim = nDim;
			paintCube.bInit = true;
			paintCube.chains = hcs;
			paintCube.askedList = queryList;
			paintCube.GenerateCube();
		}

		private void CubeRepaint()
		{
			Rectangle rect = cube_show.ClientRectangle;
			BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
			Graphics eg = cube_show.CreateGraphics();
			BufferedGraphics myBuffer = currentContext.Allocate(eg, rect);
			Graphics g = myBuffer.Graphics;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
			g.Clear(BackColor);
			paintCube.Paint(cube_show.Width, g);
			myBuffer.Render(eg);
			g.Dispose();

			myBuffer.Dispose();//释放资源
		}

		private void Cube_show_Paint(object sender, PaintEventArgs e)
		{
			if (paintCube.bInit)
			{
				Rectangle rect = e.ClipRectangle;
				BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
				BufferedGraphics myBuffer = currentContext.Allocate(e.Graphics, e.ClipRectangle);
				Graphics g = myBuffer.Graphics;
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
				g.Clear(BackColor);
				paintCube.Paint(cube_show.Width, g);
				myBuffer.Render(e.Graphics);
				g.Dispose();

				myBuffer.Dispose();//释放资源
			}
		}

		private void Next_Click(object sender, EventArgs e)
		{
			if (runAlgorithm == null)
			{
				//运行算法
				Next.Text = "下一步";
				runAlgorithm = new Thread(StartAlgorithm);
				//updateData = new Thread(UpdateData);
				//updateData.Start();
				runAlgorithm.Start();
			}
			else
			{
				//下一步
				runal.Set();
				UpdateListView();
				if (runAlgorithm == null)
				{
					askedCount.Text = nAskedCount.ToString();
					Next.Text = "执行算法";
					MessageBox.Show(String.Format("算法运行结束，询问次数：{0}", nAskedCount));
				}
			}
		}

		void StartAlgorithm()
		{
			nAskedCount = functionInference.RunAlgorithm(runal, listA, listB, GValue);
			runAlgorithm = null;
		}
		void UpdateListView()
		{
			List<NPoint> B = functionInference.B;
			List<NPoint> A = functionInference.A;
			queryList = functionInference.asked;
			//diffList = functionInference.Diff;
			paintCube.askedList = queryList;
			NPoint G = functionInference.G;
			//更新B控件
			listB.BeginUpdate();
			listB.Items.Clear();
			//if (listB.Items.Count < B.Count)
			//{
			for (int i = 0; i < B.Count; i++)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = (i + 1).ToString();
				lvi.SubItems.Add(B[i].ToString());
				listB.Items.Add(lvi);
			}
			//}
			//else
			//{
			//	for (int i = B.Count; i < listB.Items.Count; i++)
			//	{
			//		listB.Items.RemoveAt(i);
			//	}	
			//}
			listB.EndUpdate();

			listA.BeginUpdate();
			//更新A控件
			listA.Items.Clear();
			for (int i = 0; i < A.Count; i++)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = (listA.Items.Count+1).ToString();
				//lvi.ForeColor = Color.Red;
				lvi.SubItems.Add(A[i].ToString());
				lvi.SubItems.Add(A[i].realValue.ToString());
				lvi.SubItems.Add(A[i].gfuncValue.ToString());
				listA.Items.Add(lvi);
			}
			listA.EndUpdate();

			listQuery.BeginUpdate();
			//更新Query控件
			//listQuery.Items.Clear();
			for (int i = listQuery.Items.Count; i < queryList.Count; i++)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = (listQuery.Items.Count + 1).ToString();
				//lvi.ForeColor = Color.Red;
				lvi.SubItems.Add(queryList[i].ToString());
				listQuery.Items.Add(lvi);
			}
			listQuery.EndUpdate();

			//更新G
			if (G != null)
			{
				GValue.Text = String.Format("G:{0}", G.ToString());
			}

			//更新函数g
			listGFunc.BeginUpdate();
			//更新Query控件
			//listGFunc.Items.Clear();
			List<NPoint> gps = GFunction.getInstance().mapGValue.Values.ToList<NPoint>();
			for (int i = listGFunc.Items.Count; i < gps.Count; i++)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = (listGFunc.Items.Count + 1).ToString();
				//lvi.ForeColor = Color.Red;
				lvi.SubItems.Add(gps[i].ToString());
				lvi.SubItems.Add(gps[i].gfuncValue.ToString());
				listGFunc.Items.Add(lvi);
			}
			listGFunc.EndUpdate();

			CubeRepaint();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (runAlgorithm != null)
			{
				runAlgorithm.Abort();
			}
		}

		private void CreateFunc_Click(object sender, EventArgs e)
		{
			GFunction.getInstance().mapGValue.Clear();

			//所有点的G值设为0，并显示
			foreach (HanselChain hc in forDesign)
			{
				foreach (NPoint p in hc.chain)
				{
					p.gfuncValue = 0;
					GFunction.getInstance().mapGValue.Add(p.toInt(), p);
				}
			}

			GFunction.getInstance().g_function = g_function.Text;//函数g
			CubeRepaint();
		}

		private void SaveFunc_Click(object sender, EventArgs e)
		{
			//保存g函数值

			//重新初始化Cube
			InitPaintCube(nDim, result);
			CubeRepaint();
			//cube_show.Refresh();
		}

		private void Cube_show_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) {
				Point pos = Cursor.Position;
				Point rel = cube_show.PointToClient(pos);
				foreach (NPoint p in paintCube.points)
				{
					if (p.IsClicked(rel, paintCube.Radius))
					{
						p.gfuncValue = 1 - p.gfuncValue;
						break;
					}
				}

				CubeRepaint();
			}
			else if (e.Button == MouseButtons.Right)
			{
				contextMenuCube.Show(Cursor.Position);
			}
		}

		private void All_one_Click(object sender, EventArgs e)
		{
			foreach (HanselChain hc in forDesign)
			{
				foreach (NPoint p in hc.chain)
				{
					p.gfuncValue = 1;
					//GFunction.getInstance().mapGValue.Add(p.toInt(), p);
				}
			}
			CubeRepaint();
		}

		private void LoadfuncMenuItem_Click(object sender, EventArgs e)
		{
			//加载g函数
			GFunctionFile.LoadGFunction(nDim, out gLoadNodes);
			contextMenuLoadgf.Items.Clear();
			foreach (GFunctionNode node in gLoadNodes)
			{

				ToolStripMenuItem item = new ToolStripMenuItem
				{
					Tag = gLoadNodes.IndexOf(node),
					Text = node.name
				};
				contextMenuLoadgf.Items.Add(item);
			}
			contextMenuLoadgf.Show(Cursor.Position);
		}

		private void SavefuncMenuItem_Click(object sender, EventArgs e)
		{
			//保存g函数
			SaveDialog sd = new SaveDialog();
			sd.nDim = nDim;
			sd.lstPoints = GFunction.getInstance().mapGValue.Values.ToList<NPoint>();
			sd.ShowDialog(this);
		}

		private void ContextMenuLoadgf_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			String name = e.ClickedItem.Text;
			int order = (int)e.ClickedItem.Tag;
			GFunctionNode node = gLoadNodes[order];
			GFunction.getInstance().g_function = node.gFunction;
			//List<NPoint> ps = GFunction.getInstance().mapGValue.Values.ToList<NPoint>();
			//ps.Sort(
			//	delegate (NPoint p1, NPoint p2)
			//	{
			//		BigInteger i1 = p1.toInt();
			//		BigInteger i2 = p2.toInt();
			//		return i1.CompareTo(i2);
			//	}
			//);
			//for(int i = 0; i < ps.Count; ++i)
			//{
			//	ps[i].gfuncValue = node.points[i].gfuncValue;
			//}
			CubeRepaint();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer |
					ControlStyles.ResizeRedraw |
					ControlStyles.AllPaintingInWmPaint, true);
		}
	}
}
