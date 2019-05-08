namespace HanselChain
{
	partial class SaveDialog
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
			this.save = new System.Windows.Forms.Button();
			this.save_file_name = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// save
			// 
			this.save.Location = new System.Drawing.Point(193, 202);
			this.save.Name = "save";
			this.save.Size = new System.Drawing.Size(210, 59);
			this.save.TabIndex = 0;
			this.save.Text = "保存";
			this.save.UseVisualStyleBackColor = true;
			this.save.Click += new System.EventHandler(this.Save_Click);
			// 
			// save_file_name
			// 
			this.save_file_name.Location = new System.Drawing.Point(193, 68);
			this.save_file_name.Name = "save_file_name";
			this.save_file_name.Size = new System.Drawing.Size(386, 35);
			this.save_file_name.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(178, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "Function Name:";
			// 
			// SaveDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(611, 305);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.save_file_name);
			this.Controls.Add(this.save);
			this.Name = "SaveDialog";
			this.Text = "SaveDialog";
			this.Load += new System.EventHandler(this.SaveDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button save;
		private System.Windows.Forms.TextBox save_file_name;
		private System.Windows.Forms.Label label1;
	}
}