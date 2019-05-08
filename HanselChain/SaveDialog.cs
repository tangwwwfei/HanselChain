using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HanselChain
{
	public partial class SaveDialog : Form
	{
		public List<NPoint> lstPoints { get; set; }
		public int nDim { get; set; }
		public SaveDialog()
		{
			InitializeComponent();
		}

		private void SaveDialog_Load(object sender, EventArgs e)
		{
			save_file_name.Text = DateTime.Now.ToString();
		}

		private void Save_Click(object sender, EventArgs e)
		{
			GFunctionNode node = new GFunctionNode();
			node.name = save_file_name.Text;
			node.nDim = nDim;
			node.points = lstPoints;
			GFunctionFile.SaveGFunction(node);
			Close();
		}
	}
}
