using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HanselChain
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(String[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (args.Length > 2)
			{
				// -i
				// -o
				String input = args[1];
				String output = args[3];
				CMDTest.Test(input, output);
			}
			else
			{
				Application.Run(new Form1());
			}
		}
	}
}
