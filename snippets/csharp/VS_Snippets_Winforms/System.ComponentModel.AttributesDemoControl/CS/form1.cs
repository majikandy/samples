﻿// <snippet100>
// <snippet101>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AttributesDemoControlLibrary;
// </snippet101>

// This sample demonstrates using the AttributesDemoControl to log
// data from a data source. 
namespace AttributesDemoControlTest
{
    public class Form1 : Form
    {	
		private BindingSource bindingSource1;
        private System.Diagnostics.PerformanceCounter performanceCounter1;
        private Button startButton;
        private Button stopButton;
        private System.Timers.Timer timer1;
        private ToolStripStatusLabel statusStripPanel1;
		private NumericUpDown numericUpDown1;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private TableLayoutPanel tableLayoutPanel1;
		private AttributesDemoControl attributesDemoControl1;
		private System.ComponentModel.IContainer components = null;

        // This form uses an AttributesDemoControl to display a stream
        // of LogEntry objects. The data stream is generated by polling
        // a performance counter and communicating the counter values 
        // to the control with data binding.
        public Form1()
        {
            InitializeComponent();

            // Set the initial value of the threshold up/down control 
            // to the control's threshold value.
            this.numericUpDown1.Value = 
                (decimal)(float)this.attributesDemoControl1.Threshold;

            // Assign the performance counter's name to the control's 
            // title text.
            this.attributesDemoControl1.TitleText = 
                this.performanceCounter1.CounterName;
        }

        // This method handles the ThresholdExceeded event. It posts
        // the value that exceeded the threshold to the status strip.  
		private void attributesDemoControl1_ThresholdExceeded(
            ThresholdExceededEventArgs e)
        {
			string msg = String.Format(
                "{0}: Value {1} exceeded threshold {2}", 
				this.attributesDemoControl1.CurrentLogTime, 
				e.ExceedingValue, 
				e.ThresholdValue);

			this.ReportStatus( msg );
        }

        // <snippet110>
        // This method handles the timer's Elapsed event. It queries
        // the performance counter for the next value, packs the 
        // value in a LogEntry object, and adds the new LogEntry to
        // the list managed by the BindingSource.
        private void timer1_Elapsed(
            object sender, 
            System.Timers.ElapsedEventArgs e)
        {	
            // Get the latest value from the performance counter.
			float val = this.performanceCounter1.NextValue();

            // The performance counter returns values of type float, 
            // but any type that implements the IComparable interface
            // will work.
			LogEntry<float> entry = new LogEntry<float>(val, DateTime.Now);

            // Add the new LogEntry to the BindingSource list.
            this.bindingSource1.Add(entry);
        }
        // </snippet110>

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.attributesDemoControl1.Threshold = 
                (float)this.numericUpDown1.Value;

            string msg = String.Format(
                "Threshold changed to {0}", 
                this.attributesDemoControl1.Threshold);

            this.ReportStatus(msg);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.ReportStatus(DateTime.Now + ": Starting");

            this.timer1.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.ReportStatus(DateTime.Now + ": Stopping");

            this.timer1.Stop();
        }

        private void ReportStatus(string msg)
        {
            if (msg != null)
            {
                this.statusStripPanel1.Text = msg;
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
			this.startButton = new System.Windows.Forms.Button();
			this.stopButton = new System.Windows.Forms.Button();
			this.timer1 = new System.Timers.Timer();
			this.statusStripPanel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.attributesDemoControl1 = new AttributesDemoControlLibrary.AttributesDemoControl();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
			
			
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// performanceCounter1
			// 
			this.performanceCounter1.CategoryName = ".NET CLR Memory";
			this.performanceCounter1.CounterName = "Gen 0 heap size";
			this.performanceCounter1.InstanceName = "_Global_";
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(31, 25);
			this.startButton.Name = "startButton";
			this.startButton.TabIndex = 1;
			this.startButton.Text = "Start";
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// stopButton
			// 
			this.stopButton.Location = new System.Drawing.Point(112, 25);
			this.stopButton.Name = "stopButton";
			this.stopButton.TabIndex = 2;
			this.stopButton.Text = "Stop";
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.SynchronizingObject = this;
			this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
			// 
			// statusStripPanel1
			// 
			this.statusStripPanel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.statusStripPanel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.statusStripPanel1.Name = "statusStripPanel1";
			this.statusStripPanel1.Text = "Ready";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(37, 29);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.TabIndex = 7;
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.groupBox1.Controls.Add(this.numericUpDown1);
			this.groupBox1.Location = new System.Drawing.Point(280, 326);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 70);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Threshold Value";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.groupBox2.Controls.Add(this.startButton);
			this.groupBox2.Controls.Add(this.stopButton);
			this.groupBox2.Location = new System.Drawing.Point(26, 327);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(214, 68);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Logging";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.attributesDemoControl1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(514, 411);
			this.tableLayoutPanel1.TabIndex = 15;
			// 
			// attributesDemoControl1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.attributesDemoControl1, 2);
			this.attributesDemoControl1.DataMember = "";
			this.attributesDemoControl1.DataSource = this.bindingSource1;
			this.attributesDemoControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.attributesDemoControl1.Location = new System.Drawing.Point(13, 13);
			this.attributesDemoControl1.Name = "attributesDemoControl1";
			this.attributesDemoControl1.Padding = new System.Windows.Forms.Padding(10);
			this.attributesDemoControl1.Size = new System.Drawing.Size(488, 306);
			this.attributesDemoControl1.TabIndex = 0;
			this.attributesDemoControl1.Threshold = 200000F;
			this.attributesDemoControl1.TitleText = "TITLE";
			this.attributesDemoControl1.ThresholdExceeded += new AttributesDemoControlLibrary.ThresholdExceededEventHandler(this.attributesDemoControl1_ThresholdExceeded);
			// 
			// Form1
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(514, 430);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
// </snippet100>