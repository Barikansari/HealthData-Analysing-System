namespace HealthData_Analysing_System
{
    partial class IndividualGraph
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
            this.speedpanel1 = new System.Windows.Forms.Panel();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.heartratepanel = new System.Windows.Forms.Panel();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.cedencepanel = new System.Windows.Forms.Panel();
            this.zedGraphControl3 = new ZedGraph.ZedGraphControl();
            this.powerpanel = new System.Windows.Forms.Panel();
            this.zedGraphControl4 = new ZedGraph.ZedGraphControl();
            this.speedpanel1.SuspendLayout();
            this.heartratepanel.SuspendLayout();
            this.cedencepanel.SuspendLayout();
            this.powerpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // speedpanel1
            // 
            this.speedpanel1.Controls.Add(this.zedGraphControl1);
            this.speedpanel1.Location = new System.Drawing.Point(12, 37);
            this.speedpanel1.Name = "speedpanel1";
            this.speedpanel1.Size = new System.Drawing.Size(776, 195);
            this.speedpanel1.TabIndex = 0;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(15, 17);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(748, 168);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // heartratepanel
            // 
            this.heartratepanel.Controls.Add(this.zedGraphControl2);
            this.heartratepanel.Location = new System.Drawing.Point(12, 238);
            this.heartratepanel.Name = "heartratepanel";
            this.heartratepanel.Size = new System.Drawing.Size(776, 195);
            this.heartratepanel.TabIndex = 1;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Location = new System.Drawing.Point(15, 17);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 0D;
            this.zedGraphControl2.ScrollMaxY = 0D;
            this.zedGraphControl2.ScrollMaxY2 = 0D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = 0D;
            this.zedGraphControl2.ScrollMinY2 = 0D;
            this.zedGraphControl2.Size = new System.Drawing.Size(748, 168);
            this.zedGraphControl2.TabIndex = 0;
            // 
            // cedencepanel
            // 
            this.cedencepanel.Controls.Add(this.zedGraphControl3);
            this.cedencepanel.Location = new System.Drawing.Point(12, 449);
            this.cedencepanel.Name = "cedencepanel";
            this.cedencepanel.Size = new System.Drawing.Size(776, 195);
            this.cedencepanel.TabIndex = 2;
            // 
            // zedGraphControl3
            // 
            this.zedGraphControl3.Location = new System.Drawing.Point(15, 17);
            this.zedGraphControl3.Name = "zedGraphControl3";
            this.zedGraphControl3.ScrollGrace = 0D;
            this.zedGraphControl3.ScrollMaxX = 0D;
            this.zedGraphControl3.ScrollMaxY = 0D;
            this.zedGraphControl3.ScrollMaxY2 = 0D;
            this.zedGraphControl3.ScrollMinX = 0D;
            this.zedGraphControl3.ScrollMinY = 0D;
            this.zedGraphControl3.ScrollMinY2 = 0D;
            this.zedGraphControl3.Size = new System.Drawing.Size(748, 168);
            this.zedGraphControl3.TabIndex = 0;
            // 
            // powerpanel
            // 
            this.powerpanel.Controls.Add(this.zedGraphControl4);
            this.powerpanel.Location = new System.Drawing.Point(12, 650);
            this.powerpanel.Name = "powerpanel";
            this.powerpanel.Size = new System.Drawing.Size(776, 195);
            this.powerpanel.TabIndex = 3;
            // 
            // zedGraphControl4
            // 
            this.zedGraphControl4.Location = new System.Drawing.Point(15, 17);
            this.zedGraphControl4.Name = "zedGraphControl4";
            this.zedGraphControl4.ScrollGrace = 0D;
            this.zedGraphControl4.ScrollMaxX = 0D;
            this.zedGraphControl4.ScrollMaxY = 0D;
            this.zedGraphControl4.ScrollMaxY2 = 0D;
            this.zedGraphControl4.ScrollMinX = 0D;
            this.zedGraphControl4.ScrollMinY = 0D;
            this.zedGraphControl4.ScrollMinY2 = 0D;
            this.zedGraphControl4.Size = new System.Drawing.Size(748, 168);
            this.zedGraphControl4.TabIndex = 0;
            // 
            // IndividualGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 749);
            this.Controls.Add(this.powerpanel);
            this.Controls.Add(this.cedencepanel);
            this.Controls.Add(this.heartratepanel);
            this.Controls.Add(this.speedpanel1);
            this.Name = "IndividualGraph";
            this.Text = "IndividualGraph";
            this.speedpanel1.ResumeLayout(false);
            this.heartratepanel.ResumeLayout(false);
            this.cedencepanel.ResumeLayout(false);
            this.powerpanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel speedpanel1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Panel heartratepanel;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.Panel cedencepanel;
        private ZedGraph.ZedGraphControl zedGraphControl3;
        private System.Windows.Forms.Panel powerpanel;
        private ZedGraph.ZedGraphControl zedGraphControl4;
    }
}