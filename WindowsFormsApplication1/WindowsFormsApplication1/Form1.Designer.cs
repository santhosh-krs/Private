namespace WindowsFormsApplication1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new Siemens.Automation.UI.Controls.Button();
            this.panel1 = new Siemens.Automation.UI.Controls.Panel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.IconDisabled = ((System.Drawing.Bitmap)(resources.GetObject("button1.IconDisabled")));
            this.button1.IconDown = ((System.Drawing.Bitmap)(resources.GetObject("button1.IconDown")));
            this.button1.IconNormal = ((System.Drawing.Bitmap)(resources.GetObject("button1.IconNormal")));
            this.button1.IconOver = ((System.Drawing.Bitmap)(resources.GetObject("button1.IconOver")));
            this.button1.Location = new System.Drawing.Point(27, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 63);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(49, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Siemens.Automation.UI.Controls.Button button1;
        private Siemens.Automation.UI.Controls.Panel panel1;
    }
}

