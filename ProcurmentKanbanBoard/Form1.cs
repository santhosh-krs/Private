namespace ProcurmentKanbanBoard
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class Form1 : Form
    {
        private Button button1;
        private IContainer components = null;
        private PRDataCollection datacoll = PRDataCollection.Instance;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private int lightColor = 1;
        private NumericUpDown P1_X;
        private NumericUpDown P1_Y;
        private NumericUpDown P2_X;
        private NumericUpDown P2_Y;
        private NumericUpDown P3_X;
        private NumericUpDown P3_Y;
        private NumericUpDown P4_X;
        private NumericUpDown P4_Y;
        private NumericUpDown P5_X;
        private NumericUpDown P5_Y;

        public Form1()
        {
            this.InitializeComponent();
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x124, 0x10f);
            base.Paint += new PaintEventHandler(this.Form1_Paint);
            base.Click += new EventHandler(this.Form1_Click);
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected void DoFillPolygon(Graphics grfx, Color clr, int cx, int cy)
        {
            Brush brush = new SolidBrush(clr);
            Point[] points = new Point[4];
            for (int i = 0; i < points.Length; i++)
            {
                double d = ((i * 0.8) - 0.5) * 3.1415926535897931;
                points[i] = new Point((int) (cx * (0.25 + (0.24 * Math.Cos(d)))), (int) (cy * (0.5 + (0.48 * Math.Sin(d)))));
            }
            grfx.FillPolygon(brush, points, FillMode.Alternate);
        }

        protected void DoPage(Graphics grfx, Color clr, int cx, int cy)
        {
            Pen pen = new Pen(clr, 25f);
            Brush brush = new SolidBrush(clr);
            grfx.SmoothingMode = SmoothingMode.AntiAlias;
            Point[] points = new Point[] { new Point((int) this.P1_X.Value, (int) this.P1_Y.Value), new Point((int) this.P2_X.Value, (int) this.P2_Y.Value), new Point((int) this.P3_X.Value, (int) this.P3_Y.Value), new Point((int) this.P4_X.Value, (int) this.P4_Y.Value) };
            grfx.FillClosedCurve(brush, points);
        }

        protected void DoPageClosdeCurve(Graphics grfx, Color clr, int cx, int cy)
        {
            int num;
            Brush brush = new SolidBrush(clr);
            Point[] points = new Point[4];
            for (num = 0; num < points.Length; num++)
            {
                double d = ((num * 0.8) - 0.5) * 3.1415926535897931;
                points[num] = new Point((int) (cx * (0.25 + (0.24 * Math.Cos(d)))), (int) (cy * (0.5 + (0.48 * Math.Sin(d)))));
            }
            for (num = 0; num < points.Length; num++)
            {
                points[num].X += cx / 2;
            }
            grfx.FillClosedCurve(brush, points, FillMode.Alternate);
        }

        protected void DoPageEcllipse(Graphics grfx, Color clr, int cx, int cy)
        {
            grfx.DrawEllipse(new Pen(clr), 0f, 0f, grfx.DpiX, grfx.DpiY);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.lightColor++;
            if (this.lightColor == 4)
            {
                this.lightColor = 1;
            }
            base.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.DoPage(e.Graphics, this.ForeColor, base.ClientSize.Width, base.ClientSize.Height - 100);
        }

        private void InitializeComponent()
        {
            this.P1_X = new NumericUpDown();
            this.label1 = new Label();
            this.label10 = new Label();
            this.P1_Y = new NumericUpDown();
            this.label2 = new Label();
            this.P2_Y = new NumericUpDown();
            this.label3 = new Label();
            this.P2_X = new NumericUpDown();
            this.label4 = new Label();
            this.P4_Y = new NumericUpDown();
            this.label5 = new Label();
            this.P4_X = new NumericUpDown();
            this.label6 = new Label();
            this.P3_Y = new NumericUpDown();
            this.label7 = new Label();
            this.P3_X = new NumericUpDown();
            this.label8 = new Label();
            this.P5_Y = new NumericUpDown();
            this.label9 = new Label();
            this.P5_X = new NumericUpDown();
            this.button1 = new Button();
            this.P1_X.BeginInit();
            this.P1_Y.BeginInit();
            this.P2_Y.BeginInit();
            this.P2_X.BeginInit();
            this.P4_Y.BeginInit();
            this.P4_X.BeginInit();
            this.P3_Y.BeginInit();
            this.P3_X.BeginInit();
            this.P5_Y.BeginInit();
            this.P5_X.BeginInit();
            base.SuspendLayout();
            this.P1_X.Location = new Point(0x51, 380);
            int[] bits = new int[4];
            bits[0] = 500;
            this.P1_X.Maximum = new decimal(bits);
            this.P1_X.Name = "P1_X";
            this.P1_X.Size = new Size(0x2f, 20);
            this.P1_X.TabIndex = 0;
            bits = new int[4];
            bits[0] = 120;
            this.P1_X.Value = new decimal(bits);
            this.P1_X.ValueChanged += new EventHandler(this.P1_X_ValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(40, 0x17e);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "p1_X";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x86, 0x180);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x21, 13);
            this.label10.TabIndex = 0x13;
            this.label10.Text = "P1_Y";
            this.P1_Y.Location = new Point(0xac, 0x17e);
            bits = new int[4];
            bits[0] = 500;
            this.P1_Y.Maximum = new decimal(bits);
            this.P1_Y.Name = "P1_Y";
            this.P1_Y.Size = new Size(0x2f, 20);
            this.P1_Y.TabIndex = 0x12;
            bits = new int[4];
            bits[0] = 100;
            this.P1_Y.Value = new decimal(bits);
            this.P1_Y.ValueChanged += new EventHandler(this.P1_X_ValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(310, 0x184);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x21, 13);
            this.label2.TabIndex = 0x17;
            this.label2.Text = "P2_Y";
            this.P2_Y.Location = new Point(0x15f, 0x182);
            bits = new int[4];
            bits[0] = 500;
            this.P2_Y.Maximum = new decimal(bits);
            this.P2_Y.Name = "P2_Y";
            this.P2_Y.Size = new Size(0x2f, 20);
            this.P2_Y.TabIndex = 0x16;
            bits = new int[4];
            bits[0] = 100;
            this.P2_Y.Value = new decimal(bits);
            this.P2_Y.ValueChanged += new EventHandler(this.P1_X_ValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xdb, 0x182);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x20, 13);
            this.label3.TabIndex = 0x15;
            this.label3.Text = "p2_X";
            this.P2_X.Location = new Point(0xfd, 0x180);
            bits = new int[4];
            bits[0] = 500;
            this.P2_X.Maximum = new decimal(bits);
            this.P2_X.Name = "P2_X";
            this.P2_X.Size = new Size(0x2f, 20);
            this.P2_X.TabIndex = 20;
            bits = new int[4];
            bits[0] = 130;
            this.P2_X.Value = new decimal(bits);
            this.P2_X.ValueChanged += new EventHandler(this.P1_X_ValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x14d, 0x1b0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x21, 13);
            this.label4.TabIndex = 0x1f;
            this.label4.Text = "P4_Y";
            this.P4_Y.Location = new Point(0x176, 430);
            bits = new int[4];
            bits[0] = 500;
            this.P4_Y.Maximum = new decimal(bits);
            this.P4_Y.Name = "P4_Y";
            this.P4_Y.Size = new Size(0x2f, 20);
            this.P4_Y.TabIndex = 30;
            bits = new int[4];
            bits[0] = 50;
            this.P4_Y.Value = new decimal(bits);
            this.P4_Y.ValueChanged += new EventHandler(this.P1_X_ValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xea, 430);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x20, 13);
            this.label5.TabIndex = 0x1d;
            this.label5.Text = "p4_X";
            this.P4_X.Location = new Point(0x113, 0x1ac);
            bits = new int[4];
            bits[0] = 500;
            this.P4_X.Maximum = new decimal(bits);
            this.P4_X.Name = "P4_X";
            this.P4_X.Size = new Size(0x2f, 20);
            this.P4_X.TabIndex = 0x1c;
            bits = new int[4];
            bits[0] = 200;
            this.P4_X.Value = new decimal(bits);
            this.P4_X.ValueChanged += new EventHandler(this.P1_X_ValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x86, 0x1ac);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x21, 13);
            this.label6.TabIndex = 0x1b;
            this.label6.Text = "P3_Y";
            this.P3_Y.Location = new Point(0xaf, 0x1aa);
            bits = new int[4];
            bits[0] = 500;
            this.P3_Y.Maximum = new decimal(bits);
            this.P3_Y.Name = "P3_Y";
            this.P3_Y.Size = new Size(0x2f, 20);
            this.P3_Y.TabIndex = 0x1a;
            bits = new int[4];
            bits[0] = 50;
            this.P3_Y.Value = new decimal(bits);
            this.P3_Y.ValueChanged += new EventHandler(this.P1_X_ValueChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(40, 0x1aa);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x20, 13);
            this.label7.TabIndex = 0x19;
            this.label7.Text = "p3_X";
            this.P3_X.Location = new Point(0x51, 0x1a8);
            bits = new int[4];
            bits[0] = 500;
            this.P3_X.Maximum = new decimal(bits);
            this.P3_X.Name = "P3_X";
            this.P3_X.Size = new Size(0x2f, 20);
            this.P3_X.TabIndex = 0x18;
            bits = new int[4];
            bits[0] = 0x7d;
            this.P3_X.Value = new decimal(bits);
            this.P3_X.ValueChanged += new EventHandler(this.P1_X_ValueChanged);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(510, 0x184);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x21, 13);
            this.label8.TabIndex = 0x23;
            this.label8.Text = "P5_Y";
            this.P5_Y.Location = new Point(0x228, 0x182);
            bits = new int[4];
            bits[0] = 500;
            this.P5_Y.Maximum = new decimal(bits);
            this.P5_Y.Name = "P5_Y";
            this.P5_Y.Size = new Size(0x2f, 20);
            this.P5_Y.TabIndex = 0x22;
            bits = new int[4];
            bits[0] = 100;
            this.P5_Y.Value = new decimal(bits);
            this.P5_Y.ValueChanged += new EventHandler(this.P1_X_ValueChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x194, 0x182);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x20, 13);
            this.label9.TabIndex = 0x21;
            this.label9.Text = "p5_X";
            this.P5_X.Location = new Point(0x1be, 0x180);
            bits = new int[4];
            bits[0] = 500;
            this.P5_X.Maximum = new decimal(bits);
            this.P5_X.Name = "P5_X";
            this.P5_X.Size = new Size(0x2f, 20);
            this.P5_X.TabIndex = 0x20;
            bits = new int[4];
            bits[0] = 200;
            this.P5_X.Value = new decimal(bits);
            this.P5_X.ValueChanged += new EventHandler(this.P1_X_ValueChanged);
            this.button1.Location = new Point(0x2ec, 0xfe);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0x24;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x394, 0x2b8);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.P5_Y);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.P5_X);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.P4_Y);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.P4_X);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.P3_Y);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.P3_X);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.P2_Y);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.P2_X);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.P1_Y);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.P1_X);
            base.Name = "Form1";
            this.Text = "Form1";
            base.Paint += new PaintEventHandler(this.Form1_Paint);
            this.P1_X.EndInit();
            this.P1_Y.EndInit();
            this.P2_Y.EndInit();
            this.P2_X.EndInit();
            this.P4_Y.EndInit();
            this.P4_X.EndInit();
            this.P3_Y.EndInit();
            this.P3_X.EndInit();
            this.P5_Y.EndInit();
            this.P5_X.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void P1_X_ValueChanged(object sender, EventArgs e)
        {
            base.Invalidate();
        }
    }
}

