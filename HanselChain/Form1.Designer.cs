namespace HanselChain
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.listA = new System.Windows.Forms.ListView();
			this.IDA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.CoordinateA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ffunc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.gfunc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.g_function = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listB = new System.Windows.Forms.ListView();
			this.IDB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.CoordinateB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.Start = new System.Windows.Forms.Button();
			this.n_dim = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.Next = new System.Windows.Forms.Button();
			this.askedCount = new System.Windows.Forms.Label();
			this.GValue = new System.Windows.Forms.Label();
			this.listQuery = new System.Windows.Forms.ListView();
			this.Step = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Coordinate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label5 = new System.Windows.Forms.Label();
			this.createFunc = new System.Windows.Forms.Button();
			this.saveFunc = new System.Windows.Forms.Button();
			this.cube_show = new System.Windows.Forms.Panel();
			this.all_one = new System.Windows.Forms.Button();
			this.contextMenuCube = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.loadfuncMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.savefuncMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuLoadgf = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.listGFunc = new System.Windows.Forms.ListView();
			this.gID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.gCoordinate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label6 = new System.Windows.Forms.Label();
			this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuCube.SuspendLayout();
			this.SuspendLayout();
			// 
			// listA
			// 
			this.listA.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IDA,
            this.CoordinateA,
            this.ffunc,
            this.gfunc});
			this.listA.Location = new System.Drawing.Point(544, 33);
			this.listA.Name = "listA";
			this.listA.Size = new System.Drawing.Size(560, 397);
			this.listA.TabIndex = 0;
			this.listA.UseCompatibleStateImageBehavior = false;
			this.listA.View = System.Windows.Forms.View.Details;
			// 
			// IDA
			// 
			this.IDA.Text = "ID";
			this.IDA.Width = 40;
			// 
			// CoordinateA
			// 
			this.CoordinateA.Text = "Coordinate";
			this.CoordinateA.Width = 150;
			// 
			// ffunc
			// 
			this.ffunc.Text = "f";
			this.ffunc.Width = 40;
			// 
			// gfunc
			// 
			this.gfunc.Text = "g";
			this.gfunc.Width = 40;
			// 
			// g_function
			// 
			this.g_function.Location = new System.Drawing.Point(183, 92);
			this.g_function.Name = "g_function";
			this.g_function.Size = new System.Drawing.Size(341, 35);
			this.g_function.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(59, 92);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "输入函数g";
			// 
			// listB
			// 
			this.listB.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IDB,
            this.CoordinateB});
			this.listB.Location = new System.Drawing.Point(1391, 33);
			this.listB.Name = "listB";
			this.listB.Size = new System.Drawing.Size(440, 397);
			this.listB.TabIndex = 0;
			this.listB.UseCompatibleStateImageBehavior = false;
			this.listB.View = System.Windows.Forms.View.Details;
			// 
			// IDB
			// 
			this.IDB.Text = "ID";
			this.IDB.Width = 40;
			// 
			// CoordinateB
			// 
			this.CoordinateB.Text = "Coordinate";
			this.CoordinateB.Width = 150;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(540, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(22, 24);
			this.label2.TabIndex = 2;
			this.label2.Text = "A";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(1387, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(22, 24);
			this.label3.TabIndex = 2;
			this.label3.Text = "B";
			// 
			// Start
			// 
			this.Start.Location = new System.Drawing.Point(63, 184);
			this.Start.Name = "Start";
			this.Start.Size = new System.Drawing.Size(156, 47);
			this.Start.TabIndex = 3;
			this.Start.Text = "生成立方体";
			this.Start.UseVisualStyleBackColor = true;
			this.Start.Click += new System.EventHandler(this.Start_Click);
			// 
			// n_dim
			// 
			this.n_dim.Location = new System.Drawing.Point(183, 28);
			this.n_dim.Name = "n_dim";
			this.n_dim.Size = new System.Drawing.Size(341, 35);
			this.n_dim.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(35, 28);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(142, 24);
			this.label4.TabIndex = 2;
			this.label4.Text = "输入n(维度)";
			// 
			// Next
			// 
			this.Next.Location = new System.Drawing.Point(63, 327);
			this.Next.Name = "Next";
			this.Next.Size = new System.Drawing.Size(129, 47);
			this.Next.TabIndex = 3;
			this.Next.Text = "执行算法";
			this.Next.UseVisualStyleBackColor = true;
			this.Next.Click += new System.EventHandler(this.Next_Click);
			// 
			// askedCount
			// 
			this.askedCount.AutoSize = true;
			this.askedCount.Location = new System.Drawing.Point(241, 350);
			this.askedCount.Name = "askedCount";
			this.askedCount.Size = new System.Drawing.Size(130, 24);
			this.askedCount.TabIndex = 6;
			this.askedCount.Text = "AskedCount";
			// 
			// GValue
			// 
			this.GValue.AutoSize = true;
			this.GValue.Location = new System.Drawing.Point(540, 442);
			this.GValue.Name = "GValue";
			this.GValue.Size = new System.Drawing.Size(34, 24);
			this.GValue.TabIndex = 7;
			this.GValue.Text = "G:";
			// 
			// listQuery
			// 
			this.listQuery.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Step,
            this.Coordinate});
			this.listQuery.Location = new System.Drawing.Point(1127, 33);
			this.listQuery.Name = "listQuery";
			this.listQuery.Size = new System.Drawing.Size(239, 397);
			this.listQuery.TabIndex = 8;
			this.listQuery.UseCompatibleStateImageBehavior = false;
			this.listQuery.View = System.Windows.Forms.View.Details;
			// 
			// Step
			// 
			this.Step.Text = "Step";
			this.Step.Width = 40;
			// 
			// Coordinate
			// 
			this.Coordinate.Text = "Coordinate";
			this.Coordinate.Width = 200;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(1127, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 24);
			this.label5.TabIndex = 9;
			this.label5.Text = "Query";
			// 
			// createFunc
			// 
			this.createFunc.Location = new System.Drawing.Point(63, 261);
			this.createFunc.Name = "createFunc";
			this.createFunc.Size = new System.Drawing.Size(156, 45);
			this.createFunc.TabIndex = 10;
			this.createFunc.Text = "自定义函数";
			this.createFunc.UseVisualStyleBackColor = true;
			this.createFunc.Click += new System.EventHandler(this.CreateFunc_Click);
			// 
			// saveFunc
			// 
			this.saveFunc.Location = new System.Drawing.Point(245, 261);
			this.saveFunc.Name = "saveFunc";
			this.saveFunc.Size = new System.Drawing.Size(156, 45);
			this.saveFunc.TabIndex = 10;
			this.saveFunc.Text = "保存函数";
			this.saveFunc.UseVisualStyleBackColor = true;
			this.saveFunc.Click += new System.EventHandler(this.SaveFunc_Click);
			// 
			// cube_show
			// 
			this.cube_show.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cube_show.Location = new System.Drawing.Point(7, 499);
			this.cube_show.Name = "cube_show";
			this.cube_show.Size = new System.Drawing.Size(2359, 662);
			this.cube_show.TabIndex = 11;
			this.cube_show.Paint += new System.Windows.Forms.PaintEventHandler(this.Cube_show_Paint);
			this.cube_show.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Cube_show_MouseClick);
			// 
			// all_one
			// 
			this.all_one.Location = new System.Drawing.Point(409, 261);
			this.all_one.Name = "all_one";
			this.all_one.Size = new System.Drawing.Size(129, 47);
			this.all_one.TabIndex = 12;
			this.all_one.Text = "全1";
			this.all_one.UseVisualStyleBackColor = true;
			this.all_one.Click += new System.EventHandler(this.All_one_Click);
			// 
			// contextMenuCube
			// 
			this.contextMenuCube.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.contextMenuCube.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadfuncMenuItem,
            this.savefuncMenuItem});
			this.contextMenuCube.Name = "contextMenuCube";
			this.contextMenuCube.Size = new System.Drawing.Size(185, 76);
			// 
			// loadfuncMenuItem
			// 
			this.loadfuncMenuItem.Name = "loadfuncMenuItem";
			this.loadfuncMenuItem.Size = new System.Drawing.Size(184, 36);
			this.loadfuncMenuItem.Text = "载入函数";
			this.loadfuncMenuItem.Click += new System.EventHandler(this.LoadfuncMenuItem_Click);
			// 
			// savefuncMenuItem
			// 
			this.savefuncMenuItem.Name = "savefuncMenuItem";
			this.savefuncMenuItem.Size = new System.Drawing.Size(184, 36);
			this.savefuncMenuItem.Text = "保存函数";
			this.savefuncMenuItem.Click += new System.EventHandler(this.SavefuncMenuItem_Click);
			// 
			// contextMenuLoadgf
			// 
			this.contextMenuLoadgf.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.contextMenuLoadgf.Name = "contextMenuLoadgf";
			this.contextMenuLoadgf.Size = new System.Drawing.Size(61, 4);
			this.contextMenuLoadgf.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuLoadgf_ItemClicked);
			// 
			// listGFunc
			// 
			this.listGFunc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.gID,
            this.gCoordinate,
            this.value});
			this.listGFunc.Location = new System.Drawing.Point(1849, 33);
			this.listGFunc.Name = "listGFunc";
			this.listGFunc.Size = new System.Drawing.Size(477, 397);
			this.listGFunc.TabIndex = 0;
			this.listGFunc.UseCompatibleStateImageBehavior = false;
			this.listGFunc.View = System.Windows.Forms.View.Details;
			// 
			// gID
			// 
			this.gID.Text = "ID";
			this.gID.Width = 40;
			// 
			// gCoordinate
			// 
			this.gCoordinate.Text = "Coordinate";
			this.gCoordinate.Width = 150;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(1845, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(118, 24);
			this.label6.TabIndex = 2;
			this.label6.Text = "Black box";
			// 
			// value
			// 
			this.value.Text = "Value";
			this.value.Width = 80;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(2370, 1161);
			this.Controls.Add(this.all_one);
			this.Controls.Add(this.cube_show);
			this.Controls.Add(this.saveFunc);
			this.Controls.Add(this.createFunc);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.listQuery);
			this.Controls.Add(this.GValue);
			this.Controls.Add(this.askedCount);
			this.Controls.Add(this.Next);
			this.Controls.Add(this.Start);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.n_dim);
			this.Controls.Add(this.g_function);
			this.Controls.Add(this.listGFunc);
			this.Controls.Add(this.listB);
			this.Controls.Add(this.listA);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.contextMenuCube.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listA;
		private System.Windows.Forms.TextBox g_function;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView listB;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button Start;
		private System.Windows.Forms.TextBox n_dim;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button Next;
		private System.Windows.Forms.Label askedCount;
		private System.Windows.Forms.Label GValue;
		private System.Windows.Forms.ColumnHeader IDA;
		private System.Windows.Forms.ColumnHeader CoordinateA;
		private System.Windows.Forms.ColumnHeader IDB;
		private System.Windows.Forms.ColumnHeader CoordinateB;
		private System.Windows.Forms.ColumnHeader ffunc;
		private System.Windows.Forms.ColumnHeader gfunc;
		private System.Windows.Forms.ListView listQuery;
		private System.Windows.Forms.ColumnHeader Step;
		private System.Windows.Forms.ColumnHeader Coordinate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button createFunc;
		private System.Windows.Forms.Button saveFunc;
		private System.Windows.Forms.Panel cube_show;
		private System.Windows.Forms.Button all_one;
		private System.Windows.Forms.ContextMenuStrip contextMenuCube;
		private System.Windows.Forms.ToolStripMenuItem loadfuncMenuItem;
		private System.Windows.Forms.ToolStripMenuItem savefuncMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuLoadgf;
		private System.Windows.Forms.ListView listGFunc;
		private System.Windows.Forms.ColumnHeader gID;
		private System.Windows.Forms.ColumnHeader gCoordinate;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ColumnHeader value;
	}
}

