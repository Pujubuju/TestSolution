namespace TestSolution.AcceleratingQueriesUsingGPU
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnGenerateRoute = new System.Windows.Forms.Button();
            this.tbNoPoints = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRadius = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudNoTargets = new System.Windows.Forms.NumericUpDown();
            this.lblTimeRange = new System.Windows.Forms.Label();
            this.lblAreaRange = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnRun = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cobTarget = new System.Windows.Forms.ComboBox();
            this.chartPerformance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnGenerateTargets = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoTargets)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerateRoute
            // 
            this.btnGenerateRoute.Location = new System.Drawing.Point(230, 91);
            this.btnGenerateRoute.Name = "btnGenerateRoute";
            this.btnGenerateRoute.Size = new System.Drawing.Size(128, 23);
            this.btnGenerateRoute.TabIndex = 1;
            this.btnGenerateRoute.Text = "Generate Test Track";
            this.btnGenerateRoute.UseVisualStyleBackColor = true;
            this.btnGenerateRoute.Click += new System.EventHandler(this.btnGenerateRoute_Click);
            // 
            // tbNoPoints
            // 
            this.tbNoPoints.Location = new System.Drawing.Point(104, 21);
            this.tbNoPoints.Name = "tbNoPoints";
            this.tbNoPoints.Size = new System.Drawing.Size(100, 20);
            this.tbNoPoints.TabIndex = 2;
            this.tbNoPoints.Text = "10000000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "No. GPS Points";
            // 
            // tbRadius
            // 
            this.tbRadius.Location = new System.Drawing.Point(98, 98);
            this.tbRadius.Name = "tbRadius";
            this.tbRadius.Size = new System.Drawing.Size(100, 20);
            this.tbRadius.TabIndex = 6;
            this.tbRadius.Text = "1000";
            this.tbRadius.TextChanged += new System.EventHandler(this.tbRadius_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Radius (m)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "No. Targets";
            // 
            // nudNoTargets
            // 
            this.nudNoTargets.Location = new System.Drawing.Point(98, 21);
            this.nudNoTargets.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNoTargets.Name = "nudNoTargets";
            this.nudNoTargets.Size = new System.Drawing.Size(100, 20);
            this.nudNoTargets.TabIndex = 8;
            this.nudNoTargets.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNoTargets.ValueChanged += new System.EventHandler(this.nudNoTargets_ValueChanged);
            // 
            // lblTimeRange
            // 
            this.lblTimeRange.AutoSize = true;
            this.lblTimeRange.Location = new System.Drawing.Point(21, 55);
            this.lblTimeRange.Name = "lblTimeRange";
            this.lblTimeRange.Size = new System.Drawing.Size(100, 13);
            this.lblTimeRange.TabIndex = 3;
            this.lblTimeRange.Text = "Time range: Not set";
            // 
            // lblAreaRange
            // 
            this.lblAreaRange.AutoSize = true;
            this.lblAreaRange.Location = new System.Drawing.Point(21, 79);
            this.lblAreaRange.Name = "lblAreaRange";
            this.lblAreaRange.Size = new System.Drawing.Size(99, 13);
            this.lblAreaRange.TabIndex = 3;
            this.lblAreaRange.Text = "Area range: Not set";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Date and Time";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(98, 54);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(100, 20);
            this.dtpStartTime.TabIndex = 10;
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndTime.Location = new System.Drawing.Point(214, 55);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(100, 20);
            this.dtpEndTime.TabIndex = 10;
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(97, 77);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 13);
            this.lblDate.TabIndex = 3;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(211, 78);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(0, 13);
            this.lblEndTime.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGenerateRoute);
            this.groupBox1.Controls.Add(this.tbNoPoints);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblTimeRange);
            this.groupBox1.Controls.Add(this.lblAreaRange);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 128);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test Track";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGenerateTargets);
            this.groupBox2.Controls.Add(this.dtpEndTime);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbRadius);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dtpStartTime);
            this.groupBox2.Controls.Add(this.lblEndTime);
            this.groupBox2.Controls.Add(this.lblDate);
            this.groupBox2.Controls.Add(this.nudNoTargets);
            this.groupBox2.Location = new System.Drawing.Point(391, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 128);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Query Parameters";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 342);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(787, 22);
            this.statusStrip.TabIndex = 13;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // btnRun
            // 
            this.btnRun.Enabled = false;
            this.btnRun.Location = new System.Drawing.Point(645, 153);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(117, 23);
            this.btnRun.TabIndex = 14;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Device";
            // 
            // cobTarget
            // 
            this.cobTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobTarget.FormattingEnabled = true;
            this.cobTarget.Location = new System.Drawing.Point(85, 155);
            this.cobTarget.Name = "cobTarget";
            this.cobTarget.Size = new System.Drawing.Size(554, 21);
            this.cobTarget.TabIndex = 16;
            // 
            // chartPerformance
            // 
            this.chartPerformance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal;
            chartArea1.Name = "ChartArea1";
            this.chartPerformance.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPerformance.Legends.Add(legend1);
            this.chartPerformance.Location = new System.Drawing.Point(12, 182);
            this.chartPerformance.Name = "chartPerformance";
            this.chartPerformance.Size = new System.Drawing.Size(751, 144);
            this.chartPerformance.TabIndex = 18;
            this.chartPerformance.Text = "chart1";
            // 
            // btnGenerateTargets
            // 
            this.btnGenerateTargets.Enabled = false;
            this.btnGenerateTargets.Location = new System.Drawing.Point(237, 93);
            this.btnGenerateTargets.Name = "btnGenerateTargets";
            this.btnGenerateTargets.Size = new System.Drawing.Size(128, 23);
            this.btnGenerateTargets.TabIndex = 11;
            this.btnGenerateTargets.Text = "Generate Targets";
            this.btnGenerateTargets.UseVisualStyleBackColor = true;
            this.btnGenerateTargets.Click += new System.EventHandler(this.btnGenerateTargets_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 364);
            this.Controls.Add(this.chartPerformance);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cobTarget);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CUDA on NVIDIA GPU, AMD GPU and x86 GPU with CUDAfy.NET";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudNoTargets)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateRoute;
        private System.Windows.Forms.TextBox tbNoPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRadius;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudNoTargets;
        private System.Windows.Forms.Label lblTimeRange;
        private System.Windows.Forms.Label lblAreaRange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cobTarget;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerformance;
        private System.Windows.Forms.Button btnGenerateTargets;
    }
}

