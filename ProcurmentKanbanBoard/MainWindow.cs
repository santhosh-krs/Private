namespace ProcurmentKanbanBoard
{
    using ProcurmentKanbanBoard.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text;
    using System.Windows.Forms;

    public class MainWindow : Form
    {
        // Fields
        private int activeListBox = -1;
        private Font cardFont;
        private int cardHeight;

        private readonly string[] ColoumnsName = new string[]
                                        {
                                            "SM/PM Requested", "PR Recived", " SCM Approved",
                                            "Reporting Manager", "IT-Coordinator", "LOB Approved", /*"Commercial Approved",*/
                                            "PO Released", "Received", "Rejected"
                                        };

        private IContainer components;
        private Label CurrentDatelabel;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private Label Fromtodatelabel;
        private int gap;
        private Font headerFont;
        private int headerHeight;
        private List<Label> headers = new List<Label>();
        private Kanban kanban = Kanban.Instance;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label labelExit;
        private List<TaskCard> listBoxes = new List<TaskCard>();
        private Dictionary<string, PRData> m_POItemList = new Dictionary<string, PRData>();
        private ComboBox ManagerscomboBox;
        private Point mouseDownPoint;
        private Panel panel1;
        private PRDataCollection prdatacollection = PRDataCollection.Instance;
        private ComboBox ProjectcomboBox;
        private bool readyToDisplay;
        private Font subTextFont;
        private Label TitleLabel;
        private ToolTip toolTip1;
        private List<Kanban.Column> view = Kanban.Instance.CurrentView;
        private List<Label> viewTabs = new List<Label>();
        private Font viewTabsFont;

        // Methods
        public MainWindow()
        {
            InitializeComponent();
            prdatacollection.ReadDataFromExcel(kanban.ApplicationSettings.DataPath,
                kanban.ApplicationSettings.CurrentYear);
            ManagerscomboBox.Items.Add("All");
            ManagerscomboBox.SelectedIndex = 0;
            ProjectcomboBox.Items.Add("All");
            ProjectcomboBox.SelectedIndex = 0;
            BackColor = kanban.ApplicationSettings.BoardBackColor;
            StorePODataitemintoCollection();
            Font = SystemFonts.MessageBoxFont;
            new Font(Font.FontFamily, (float) (Font.Size*1.0), Font.Style);
            Text = Application.ProductName;
            gap = 0;
            CurrentDatelabel.Text = DateTime.Now.ToLongDateString();
            PopulateProjectandManager();
        }

        private void CreateColoums()
        {
            kanban.Columns.Clear();
            Kanban.Column item = new Kanban.Column
                                 {
                                     Name = ColoumnsName[0]
                                 };
            kanban.Columns.Add(item);
            item = new Kanban.Column
                   {
                       Name = ColoumnsName[1]
                   };
            kanban.Columns.Add(item);
            item = new Kanban.Column
                   {
                       Name = ColoumnsName[2]
                   };
            kanban.Columns.Add(item);
            item = new Kanban.Column
                   {
                       Name = ColoumnsName[3]
                   };
            kanban.Columns.Add(item);
            item = new Kanban.Column
                   {
                       Name = ColoumnsName[4]
                   };
            kanban.Columns.Add(item);
            item = new Kanban.Column
                   {
                       Name = ColoumnsName[5]
                   };
            kanban.Columns.Add(item);
            item = new Kanban.Column
                   {
                       Name = ColoumnsName[6]
                   };
            kanban.Columns.Add(item);
            item = new Kanban.Column
                   {
                       Name = ColoumnsName[7]
                   };
            kanban.Columns.Add(item);
            item = new Kanban.Column
                   {
                       Name = ColoumnsName[8]
                   };
            kanban.Columns.Add(item);
            /*item = new Kanban.Column
                   {
                       Name = ColoumnsName[9]
                   };
            kanban.Columns.Add(item);*/
            CreateTasksForRespectiveColoumn();
        }

        private void CreateTasksForRespectiveColoumn()
        {
            foreach (PRData data in POItemList.Values)
            {
                Kanban.Task item = new Kanban.Task();
                Kanban.Person person = null;
                person = new Kanban.Person
                         {
                             Name = data.ManagerName
                         };
                kanban.Persons.Add(person);
                item.PersonId = person.Id;
                item.Text = data.SerialNo;
                string str = string.Empty;
                if (data.Status != null)
                {
                    str = data.Status.Trim();
                }
                if ((data.Status != null) && data.Status.Equals("Rejected"))
                {
                    kanban.Columns[8].Tasks.Add(item);
                }
                else if ((data.Status != null) && 
                         !data.SmPmRequestedDate.Equals(data.Defaultdate) &&
                         !data.PRCreatedDate.Equals(data.Defaultdate) &&
                         !data.SCMApprovedDate.Equals(data.Defaultdate) &&
                         !data.ReportingManagerApproved.Equals(data.Defaultdate) &&
                         (!data.IT_CoordinatorApproved.Equals(data.Defaultdate) ||
                         !data.LOBApproved.Equals(data.Defaultdate) )&&
                         !data.POReleasedDate.Equals(data.Defaultdate) &&
                          str.Equals("Received"))
                {
                    kanban.Columns[7].Tasks.Add(item);
                }
                else if (!data.SmPmRequestedDate.Equals(data.Defaultdate) &&
                         !data.PRCreatedDate.Equals(data.Defaultdate) &&
                         !data.SCMApprovedDate.Equals(data.Defaultdate) &&
                         !data.ReportingManagerApproved.Equals(data.Defaultdate) &&
                         !data.POReleasedDate.Equals(data.Defaultdate) &&
                         (!data.IT_CoordinatorApproved.Equals(data.Defaultdate) ||
                         !data.LOBApproved.Equals(data.Defaultdate)))
                {
                    kanban.Columns[6].Tasks.Add(item);
                }
                else if((!data.SmPmRequestedDate.Equals(data.Defaultdate) &&
                        !data.PRCreatedDate.Equals(data.Defaultdate) &&
                        !data.SCMApprovedDate.Equals(data.Defaultdate) &&
                        !data.ReportingManagerApproved.Equals(data.Defaultdate)) &&
                         !data.LOBApproved.Equals(data.Defaultdate))
                {
                    kanban.Columns[5].Tasks.Add(item);
                }
                else if ((!data.SmPmRequestedDate.Equals(data.Defaultdate) &&
                        !data.PRCreatedDate.Equals(data.Defaultdate) &&
                        !data.SCMApprovedDate.Equals(data.Defaultdate) &&
                        !data.ReportingManagerApproved.Equals(data.Defaultdate)) &&
                        !data.IT_CoordinatorApproved.Equals(data.Defaultdate))
                {
                    kanban.Columns[4].Tasks.Add(item);
                }
                else if (!data.SmPmRequestedDate.Equals(data.Defaultdate) &&
                        !data.PRCreatedDate.Equals(data.Defaultdate) &&
                        !data.SCMApprovedDate.Equals(data.Defaultdate) && 
                        !data.ReportingManagerApproved.Equals(data.Defaultdate))
                {
                    kanban.Columns[3].Tasks.Add(item);
                }
                else if (!data.SmPmRequestedDate.Equals(data.Defaultdate) &&
                        !data.PRCreatedDate.Equals(data.Defaultdate) &&
                        !data.SCMApprovedDate .Equals(data.Defaultdate))
                {
                    kanban.Columns[2].Tasks.Add(item);
                }
                else if (!data.SmPmRequestedDate.Equals(data.Defaultdate)&&
                    !data.PRCreatedDate.Equals(data.Defaultdate))
                {
                    kanban.Columns[1].Tasks.Add(item);
                }
               else
                {
                    kanban.Columns[0].Tasks.Add(item);
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
        }

        private void DisplayData()
        {
            SuspendLayout();
            foreach (Control control in panel1.Controls)
            {
                control.Dispose();
            }
            panel1.Controls.Clear();
            viewTabs.Clear();
            headers.Clear();
            listBoxes.Clear();
            activeListBox = -1;
            UpdateFontsAndSizes();
            foreach (View view in kanban.Views)
            {
                Label item = new Label
                             {
                                 Font = viewTabsFont
                             };
                item.AutoSize = false;
                item.BorderStyle = BorderStyle.None;
                item.TextAlign = ContentAlignment.MiddleCenter;
                item.AutoEllipsis = true;
                viewTabs.Add(item);
                panel1.Controls.Add(item);
            }
            this.view = kanban.CurrentView;
            foreach (Kanban.Column column in this.view)
            {
                Label label3 = new Label
                               {
                                   Font = headerFont,
                                   ForeColor = kanban.ApplicationSettings.HeaderForeColor,
                                   BackColor = kanban.ApplicationSettings.HeaderBackColor,
                                   AutoSize = false,
                                   BorderStyle = BorderStyle.None,
                                   TextAlign = ContentAlignment.MiddleCenter,
                                   AutoEllipsis = true
                               };
                label3.MouseDown += new MouseEventHandler(header_MouseDown);
                headers.Add(label3);
                panel1.Controls.Add(label3);
                TaskCard card = new TaskCard
                                {
                                    Font = cardFont,
                                    BackColor = BackColor,
                                    AllowDrop = false,
                                    Visible = true,
                                    ItemHeight = 100
                                };
                card.DrawItem += new DrawItemEventHandler(takcard_DrawItem);
                card.MouseDown += new MouseEventHandler(takcard_MouseDown);
                card.MouseMove += new MouseEventHandler(takcard_MouseMove);
                card.GotFocus += new EventHandler(takcard_GotFocus);
                listBoxes.Add(card);
                panel1.Controls.Add(card);
            }
            readyToDisplay = true;
            SyncListBoxes(false);
            UpdateViewTabs();
            UpdateHeaders();
            ResumeLayout();
            PerformLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void DrawRoundRect(Graphics g, SolidBrush p, float x, float y, float width, float height, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(x + radius, y, x + width - radius*2f, y);
            path.AddArc(x + width - radius*2f, y, radius*2f, radius*2f, 270f, 90f);
            path.AddLine((float) (x + width), (float) (y + radius), (float) (x + width),
                (float) (y + height - radius*2f));
            path.AddArc((float) (x + width - radius*2f), (float) (y + height - radius*2f), (float) (radius*2f),
                (float) (radius*2f), 0f, 90f);
            path.AddLine((float) (x + width - radius*2f), (float) (y + height), (float) (x + radius),
                (float) (y + height));
            path.AddArc(x, y + height - radius*2f, radius*2f, radius*2f, 90f, 90f);
            path.AddLine(x, y + height - radius*2f, x, y + radius);
            path.AddArc(x, y, radius*2f, radius*2f, 180f, 90f);
            path.CloseFigure();
            g.FillPath(p, path);
            path.Dispose();
        }

        private string GetSubText(Kanban.Task task, Kanban.Person person, bool showAssignedTo)
        {
            string str = string.Empty;
            List<string> list = new List<string>();
            if (!(!showAssignedTo || (person == null) || string.IsNullOrEmpty(person.Name)))
            {
                list.Add(person.DisplayName);
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append(list[i]);
                if (i < list.Count - 1)
                {
                    builder.Append(str);
                }
            }
            return builder.ToString();
        }

        private string GetTaskTooltipText(Kanban.Task task)
        {
            if (task == null)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            if (kanban.ApplicationSettings.ToolTipShowText)
            {
                PRData data;
                POItemList.TryGetValue(task.Text, out data);
                builder.AppendLine("PR No           : " + data.PRNumber);
                builder.AppendLine("Description   : " + data.Description);
                builder.AppendLine("Project           : " + data.Project);
                builder.AppendLine("SW/HW         : " + data.SWHW);
                builder.AppendLine("Quantity        : " + data.Quantity);
            }
            Kanban.Person person = kanban.FindPersonById(task.PersonId);
            string str = GetSubText(task, person, kanban.ApplicationSettings.ToolTipShowPerson);
            if (str.Length > 0)
            {
                if (builder.Length > 0)
                {
                    builder.AppendLine();
                }
                builder.AppendLine(str);
            }
            return builder.ToString();
        }

        private void header_MouseDown(object sender, MouseEventArgs e)
        {
            int num = -1;
            num = 0;
            while (num < headers.Count)
            {
                if (headers[num].Equals(sender))
                {
                    break;
                }
                num++;
            }
            if ((num != -1) && (e.Button == MouseButtons.Left) && (e.Clicks == 1) && !listBoxes[num].Focused)
            {
                Trace.WriteLine("Activating listbox " + num.ToString());
                listBoxes[num].Focus();
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelExit = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.TitleLabel = new System.Windows.Forms.Label();
            this.CurrentDatelabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ManagerscomboBox = new System.Windows.Forms.ComboBox();
            this.ProjectcomboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Fromtodatelabel = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.MintCream;
            this.panel1.Location = new System.Drawing.Point(0, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1501, 870);
            this.panel1.TabIndex = 2;
            // 
            // labelExit
            // 
            this.labelExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelExit.BackColor = System.Drawing.Color.Gray;
            this.labelExit.Location = new System.Drawing.Point(1454, 10);
            this.labelExit.Name = "labelExit";
            this.labelExit.Size = new System.Drawing.Size(32, 32);
            this.labelExit.TabIndex = 3;
            this.labelExit.Click += new System.EventHandler(this.labelExit_Click);
            this.labelExit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelExit_MouseClick);
            // 
            // TitleLabel
            // 
            this.TitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Font = new System.Drawing.Font("Lucida Calligraphy", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(361, 10);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(752, 32);
            this.TitleLabel.TabIndex = 4;
            this.TitleLabel.Text = "Procurement Dash Board";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CurrentDatelabel
            // 
            this.CurrentDatelabel.BackColor = System.Drawing.Color.Transparent;
            this.CurrentDatelabel.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentDatelabel.Location = new System.Drawing.Point(12, 9);
            this.CurrentDatelabel.Name = "CurrentDatelabel";
            this.CurrentDatelabel.Size = new System.Drawing.Size(311, 32);
            this.CurrentDatelabel.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1199, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Project Manager";
            // 
            // ManagerscomboBox
            // 
            this.ManagerscomboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ManagerscomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ManagerscomboBox.FormattingEnabled = true;
            this.ManagerscomboBox.Location = new System.Drawing.Point(1292, 15);
            this.ManagerscomboBox.Name = "ManagerscomboBox";
            this.ManagerscomboBox.Size = new System.Drawing.Size(121, 21);
            this.ManagerscomboBox.TabIndex = 9;
            this.ManagerscomboBox.SelectedIndexChanged += new System.EventHandler(this.ManagerscomboBox_SelectedIndexChanged);
            this.ManagerscomboBox.SelectionChangeCommitted += new System.EventHandler(this.ManagerscomboBox_SelectionChangeCommitted);
            // 
            // ProjectcomboBox
            // 
            this.ProjectcomboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProjectcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProjectcomboBox.FormattingEnabled = true;
            this.ProjectcomboBox.Location = new System.Drawing.Point(782, 42);
            this.ProjectcomboBox.Name = "ProjectcomboBox";
            this.ProjectcomboBox.Size = new System.Drawing.Size(121, 21);
            this.ProjectcomboBox.TabIndex = 11;
            this.ProjectcomboBox.Visible = false;
            this.ProjectcomboBox.SelectedIndexChanged += new System.EventHandler(this.ProjectcomboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(823, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Project";
            this.label2.Visible = false;
            // 
            // Fromtodatelabel
            // 
            this.Fromtodatelabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fromtodatelabel.AutoSize = true;
            this.Fromtodatelabel.Location = new System.Drawing.Point(996, 42);
            this.Fromtodatelabel.Name = "Fromtodatelabel";
            this.Fromtodatelabel.Size = new System.Drawing.Size(0, 13);
            this.Fromtodatelabel.TabIndex = 12;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(1192, 45);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(89, 20);
            this.dateTimePicker1.TabIndex = 13;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(1315, 45);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(89, 20);
            this.dateTimePicker2.TabIndex = 14;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1290, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "To";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1077, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "PR Raised Date From";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Gray;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(1413, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 25);
            this.label6.TabIndex = 18;
            this.label6.Text = " Go";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // MainWindow
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1500, 950);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.Fromtodatelabel);
            this.Controls.Add(this.ProjectcomboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ManagerscomboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CurrentDatelabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.labelExit);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
            PopulateAdditionalTasksByPOCreatedDate();
            kanban.Columns.Clear();
            PopulateTasks();
            DisplayData();
            if (POItemList.Count == 0)
            {
                MessageBox.Show(
                    " PR not avilabele B/W :" + dateTimePicker1.Value.ToShortDateString() + " TO " +
                    dateTimePicker2.Value.ToShortDateString(), "Information", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
        }

        private void labelExit_MouseClick(object sender, MouseEventArgs e)
        {
            kanban.Save();
            Close();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            PopulateTasks();
            DisplayData();
            UpdateColorsAndImages();
        }

        private void ManagerscomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ManagerscomboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ManagerscomboBox.SelectedItem != null)
            {
                PopulateAdditionalTasksByUser(ManagerscomboBox.SelectedItem.ToString());
                kanban.Columns.Clear();
                PopulateTasks();
                DisplayData();
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (readyToDisplay)
            {
                int y = 0;
                int num2 = 0;
                int num3 = 0;
                int count = 0;
                count = view.Count;
                if (count > 0)
                {
                    num2 = (panel1.ClientRectangle.Width - (count - 1)*gap)/count;
                    num3 = panel1.ClientRectangle.Width - (count - 1)*(num2 + gap);
                    for (int i = 0; i < count; i++)
                    {
                        headers[i].SetBounds(i*num2, y, i < count - 1 ? num2 - 3 : num3 - 3,
                            headerHeight);
                        listBoxes[i].SetBounds(i*(gap + num2), y + headerHeight + gap,
                            i < count - 1 ? (num2 - 3) : num3,
                            (panel1.ClientRectangle.Height - headerHeight) - gap);
                    }
                }
            }
        }

        private void PopulateAdditionalTasksByPOCreatedDate()
        {
            POItemList.Clear();
            foreach (PRData data in prdatacollection.PRDataEntries)
            {
                if ((data.PRCreatedDate.Date >= dateTimePicker1.Value.Date) &&
                    (data.PRCreatedDate.Date <= dateTimePicker2.Value.Date))
                {
                    POItemList.Add(data.SerialNo, data);
                }
            }
        }

        private void PopulateAdditionalTasksByUser(string user)
        {
            POItemList.Clear();
            if (user == "All")
            {
                StorePODataitemintoCollection();
            }
            else
            {
                foreach (PRData data in prdatacollection.PRDataEntries)
                {
                    if (data.ManagerName.Contains(user))
                    {
                        POItemList.Add(data.SerialNo, data);
                    }
                }
            }
        }

        private void PopulateProjectandManager()
        {
            List<string> manager = new List<string>();
            foreach (PRData data in prdatacollection.PRDataEntries)
            {
                if (((data.Project != null) && !ProjectcomboBox.Items.Contains(data.Project)) &&
                    (data.Project != string.Empty))
                {
                    ProjectcomboBox.Items.Add(data.Project);
                }
                if (((data.ManagerName != null) && !manager.Contains(data.ManagerName)) &&
                    (data.ManagerName != string.Empty))
                {
                    manager.Add(data.ManagerName);
                }
            }
            manager.Sort();
            ManagerscomboBox.Items.AddRange(manager.ToArray());

        }

        private void PopulateTasks()
        {
            CreateColoums();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    Trace.WriteLine("Left");
                    SelectNextColumn(false);
                    return true;

                case Keys.Right:
                    Trace.WriteLine("Right");
                    SelectNextColumn(true);
                    return true;

                case (Keys.Alt | Keys.F4):
                    Trace.WriteLine("Alt + F4");
                    Close();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ProjectcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void SelectNextColumn(bool next)
        {
            int count = listBoxes.Count;
            int num2 = -1;
            int num3 = -1;
            for (int i = 0; i < count; i++)
            {
                if (listBoxes[i].Focused)
                {
                    num2 = i;
                    break;
                }
            }
            if (num2 != -1)
            {
                int num5 = ((listBoxes[num2].SelectedIndex != -1) ? listBoxes[num2].SelectedIndex : 0) -
                           ((listBoxes[num2].TopIndex != -1) ? listBoxes[num2].TopIndex : 0);
                if (next)
                {
                    for (int j = num2 + 1; j < count; j++)
                    {
                        if ((listBoxes[j].Visible && (listBoxes[j].Count > 0)) &&
                            ((listBoxes[j].TopIndex + num5) < listBoxes[j].Count))
                        {
                            num3 = j;
                            break;
                        }
                    }
                }
                else
                {
                    for (int k = num2 - 1; k >= 0; k--)
                    {
                        if ((listBoxes[k].Visible && (listBoxes[k].Count > 0)) &&
                            ((listBoxes[k].TopIndex + num5) < listBoxes[k].Count))
                        {
                            num3 = k;
                            break;
                        }
                    }
                }
                if ((num2 != num3) && (num3 != -1))
                {
                    if ((listBoxes[num3].TopIndex + num5) < listBoxes[num3].Count)
                    {
                        listBoxes[num3].SelectedIndex = listBoxes[num3].TopIndex + num5;
                    }
                    listBoxes[num3].Focus();
                }
            }
        }

        private void StorePODataitemintoCollection()
        {
            POItemList.Clear();
            foreach (PRData data in prdatacollection.PRDataEntries)
            {
                POItemList.Add(data.SerialNo, data);
            }
        }

        private void SyncListBoxes(bool restore)
        {
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            if (restore)
            {
                for (int j = 0; j < listBoxes.Count; j++)
                {
                    list.Add(listBoxes[j].TopIndex);
                    list2.Add(listBoxes[j].SelectedIndex);
                }
            }
            for (int i = 0; i < listBoxes.Count; i++)
            {
                listBoxes[i].Count = view[i].Tasks.Count;
            }
            if (restore)
            {
                for (int k = 0; k < listBoxes.Count; k++)
                {
                    if (listBoxes[k].Visible)
                    {
                        if ((list[k] >= 0) && (list[k] < listBoxes[k].Count))
                        {
                            listBoxes[k].TopIndex = list[k];
                        }
                        if ((list2[k] >= 0) && (list2[k] < listBoxes[k].Count))
                        {
                            listBoxes[k].SelectedIndex = list2[k];
                        }
                    }
                }
            }
        }

        private void takcard_DrawItem(object sender, DrawItemEventArgs e)
        {
            int index = listBoxes.IndexOf((TaskCard) sender);
            if ((index != -1) && ((index >= 0) && (index < view.Count)))
            {
                Kanban.Column column = view[index];
                if ((e.Index >= 0) && (e.Index < column.Tasks.Count))
                {
                    PRData data;
                    Kanban.Task task = column.Tasks[e.Index];
                    Kanban.Topic topic = kanban.FindTopicById(task.TopicId);
                    Kanban.Person person = kanban.FindPersonById(task.PersonId);
                    DrawItemState selected = DrawItemState.Selected;
                    bool flag = (e.State & selected) == selected;
                    SolidBrush brush = null;
                    SolidBrush brush2 = null;
                    SolidBrush brush3 = new SolidBrush(BackColor);
                    Pen pen = new Pen(BackColor)
                              {
                                  DashStyle = DashStyle.Dash
                              };
                    if (flag && (index == activeListBox))
                    {
                        brush = new SolidBrush(kanban.ApplicationSettings.SelectedTaskBackColor);
                        brush2 = new SolidBrush(kanban.ApplicationSettings.SelectedTaskForeColor);
                    }
                    else if (topic != null)
                    {
                        brush =
                            new SolidBrush(topic.TaskBackColor.IsEmpty
                                ? kanban.ApplicationSettings.DefaultTaskBackColor
                                : topic.TaskBackColor);
                        brush2 =
                            new SolidBrush(topic.TaskForeColor.IsEmpty
                                ? kanban.ApplicationSettings.DefaultTaskForeColor
                                : topic.TaskForeColor);
                    }
                    else
                    {
                        brush = new SolidBrush(kanban.ApplicationSettings.DefaultTaskBackColor);
                        brush2 = new SolidBrush(kanban.ApplicationSettings.DefaultTaskForeColor);
                    }
                    Bitmap image = null;
                    StringBuilder builder = new StringBuilder();
                    StringFormat format = new StringFormat();
                    POItemList.TryGetValue(task.Text, out data);
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    format.Trimming = StringTrimming.EllipsisCharacter;
                    format.FormatFlags = StringFormatFlags.NoWrap;
                    builder.AppendLine(data.Description);
                    if (data != null)
                    {
                        image = PoResource.todo;
                        if ((column.Name == ColoumnsName[0]) && (data.SmPmRequestedDate != data.Defaultdate))
                        {
                            builder.AppendLine("SM/PM Requested : ");
                            builder.AppendLine(data.SmPmRequestedDate.ToShortDateString());
                        }
                        if ((column.Name == ColoumnsName[1]) && (data.PRCreatedDate  != data.Defaultdate))
                        {
                            //builder.AppendLine("Quotation Requested :");
                            builder.AppendLine("Created :" + data.PRCreatedDate.ToShortDateString());
                        }
                        if ((column.Name == ColoumnsName[2]) && (data.SCMApprovedDate != data.Defaultdate))
                        {
                            builder.AppendLine("Quotation Recived :");
                            builder.AppendLine(data.SCMApprovedDate.ToShortDateString());
                        }
                        if ((column.Name == ColoumnsName[3]) && (data.ReportingManagerApproved != data.Defaultdate))
                        {
                            builder.AppendLine("ReportingManager Approved :");
                            builder.AppendLine(data.ReportingManagerApproved.ToShortDateString());
                        }
                        if ((column.Name == ColoumnsName[4]) && (data.IT_CoordinatorApproved != data.Defaultdate))
                        {
                            builder.AppendLine(data.IT_CoordinatorApproved.ToShortDateString());
                        }
                        if ((column.Name == ColoumnsName[5]) && (data.LOBApproved  != data.Defaultdate))
                        {
                            builder.AppendLine("LOB Approval :" + data.SCMApprovedDate.ToShortDateString());
                        }
                        //if ((column.Name == ColoumnsName[6]) && (data.ReportingManagerApproved != data.Defaultdate))
                        //{
                        //    builder.AppendLine("Com Approval :" + data.ReportingManagerApproved.ToShortDateString());
                        //}
                        if (column.Name == ColoumnsName[6])
                        {
                            if (data.POReleasedDate != data.Defaultdate)
                            {
                                builder.AppendLine("Released :" + data.POReleasedDate.ToShortDateString());
                            }
                            TimeSpan span = (TimeSpan) (data.ExpectedDate - data.PRRecivedDate);
                            TimeSpan span2 = (TimeSpan) (data.ExpectedDate - data.CurrentDate);
                            if ((span2.Days <= 15) && (span2.Days > 0))
                            {
                                image = PoResource.yellowcard;
                            }
                            else if (((data.CurrentDate.Date > data.ExpectedDate.Date) &&
                                      (data.PRRecivedDate == data.Defaultdate)) &&
                                     (data.ExpectedDate.Date != data.Defaultdate))
                            {
                                image = PoResource.redcard;
                            }
                            if (data.ExpectedDate == data.Defaultdate)
                            {
                                builder.AppendLine("Expected : ????? ");
                                image = PoResource.orangecard;
                            }
                            else
                            {
                                builder.AppendLine("Expected :" + data.ExpectedDate.ToShortDateString());
                            }
                        }
                        if (column.Name == ColoumnsName[7])
                        {
                            if (data.PRCreatedDate != data.Defaultdate)
                            {
                                builder.AppendLine("Created :" + data.PRCreatedDate.ToShortDateString());
                            }
                            if (data.PRRecivedDate != data.Defaultdate)
                            {
                                builder.AppendLine("Received  :" + data.POReleasedDate.ToShortDateString());
                            }
                        }
                        if ((column.Name == ColoumnsName[8]) && (data.POReleasedDate != data.Defaultdate))
                        {
                            builder.AppendLine("Date  :" + data.POReleasedDate.ToShortDateString());
                        }
                        if (flag && (index == activeListBox))
                        {
                            image = PoResource.selectedcard;
                        }
                        int height = TextRenderer.MeasureText(person.DisplayName.ToString(), subTextFont).Height;
                        RectangleF layoutRectangle = new RectangleF((float) (e.Bounds.Left + 5),
                            (float) (e.Bounds.Top - 9), (float) (e.Bounds.Width - 5),
                            (float) ((e.Bounds.Height - 4) - height));
                        RectangleF ef2 = new RectangleF((float) (e.Bounds.Left + 3),
                            (float) ((e.Bounds.Top + height) + 10), (float) (e.Bounds.Width - 5),
                            (float) ((e.Bounds.Height - 4) - height));
                        brush = new SolidBrush(Color.Black);
                        brush2 = new SolidBrush(Color.WhiteSmoke);
                        if (image != null)
                        {
                            e.Graphics.DrawImage(image, (int) (e.Bounds.Left + 2), e.Bounds.Top,
                                (int) (e.Bounds.Width - 4), (int) (e.Bounds.Height - gap));
                        }
                        e.Graphics.DrawString(builder.ToString(), e.Font, brush, ef2, format);
                        e.Graphics.DrawString(data.ManagerName, subTextFont, brush2, layoutRectangle, format);
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                    brush.Dispose();
                    brush2.Dispose();
                    brush3.Dispose();
                    pen.Dispose();
                }
            }
        }

        private void takcard_GotFocus(object sender, EventArgs e)
        {
            int activeListBox = this.activeListBox;
            this.activeListBox = listBoxes.IndexOf((TaskCard) sender);
            if (Environment.OSVersion.Version.Major < 6)
            {
                if (activeListBox != -1)
                {
                    listBoxes[activeListBox].Refresh();
                }
                if (this.activeListBox != -1)
                {
                    listBoxes[this.activeListBox].Refresh();
                }
            }
        }

        private void takcard_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBoxes.IndexOf((TaskCard) sender) != -1)
            {
                int num = ((TaskCard) sender).IndexFromPoint(e.X, e.Y);
                if (num != -1)
                {
                    if (!((TaskCard) sender).Focused)
                    {
                        ((TaskCard) sender).Focus();
                    }
                    ((TaskCard) sender).SelectedIndex = num;
                    if ((e.Button == MouseButtons.Left) && (e.Clicks == 1))
                    {
                        mouseDownPoint.X = e.X;
                        mouseDownPoint.Y = e.Y;
                    }
                }
            }
        }

        private void takcard_MouseMove(object sender, MouseEventArgs e)
        {
            int index = listBoxes.IndexOf((TaskCard) sender);
            if (index != -1)
            {
                int num2 = ((TaskCard) sender).IndexFromPoint(e.X, e.Y);
                if (num2 == -1)
                {
                    toolTip1.Hide((TaskCard) sender);
                }
                else
                {
                    Kanban.Column column = view[index];
                    Kanban.Task task = column.Tasks[num2];
                    try
                    {
                        string taskTooltipText = GetTaskTooltipText(task);
                        if (toolTip1.GetToolTip((TaskCard) sender) != taskTooltipText)
                        {
                            toolTip1.SetToolTip((TaskCard) sender, taskTooltipText);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void UpdateColorsAndImages()
        {
            labelExit.BackColor = BackColor;
            int imageSize = 2*labelExit.Height/3;
            labelExit.Image = HelperClass.CreateButtonImage(PoResource.exit, BackColor, ForeColor,
                imageSize);
            label6.BackColor = BackColor;
        }

        private void UpdateFontsAndSizes()
        {
            int num = 0x24;
            if (kanban.ApplicationSettings.ViewTabsHeight > 0)
            {
            }
            viewTabsFont = new Font("Ariel", 10f, FontStyle.Bold);
            headerFont = new Font("Ariel", 9f, FontStyle.Bold);
            if (kanban.ApplicationSettings.HeaderHeight > 0)
            {
                headerHeight = (int) kanban.ApplicationSettings.HeaderHeight;
            }
            else
            {
                headerHeight = num + 2;
                kanban.ApplicationSettings.HeaderHeight = (uint) headerHeight;
            }
            cardFont = new Font("Ariel", 8.25f, FontStyle.Regular);
            subTextFont = cardFont;
            if (kanban.ApplicationSettings.CardHeight > 0)
            {
                cardHeight = (int) kanban.ApplicationSettings.CardHeight + gap;
            }
            else
            {
                cardHeight = 3*num/2 + gap;
                kanban.ApplicationSettings.CardHeight = (uint) (cardHeight - gap);
            }
        }

        private void UpdateHeaders()
        {
            for (int i = 0; i < view.Count; i++)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(view[i].Name);
                if (kanban.ApplicationSettings.HeaderShowCount)
                {
                    builder.Append(" (");
                    builder.Append(view[i].Tasks.Count.ToString());
                    builder.Append(")");
                }
                headers[i].Text = builder.ToString();
            }
        }

        private void UpdateViewTabs()
        {
            int count = kanban.Views.Count;
            for (int i = 0; i < count; i++)
            {
                viewTabs[i].Text = kanban.Views[i].Name;
                viewTabs[i].Visible = count > 1;
            }
        }

        // Properties
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.Style |= 0x20000;
                createParams.ClassStyle |= 8;
                return createParams;
            }
        }

        public Dictionary<string, PRData> POItemList
        {
            get { return m_POItemList; }
            set { m_POItemList = value; }
        }
    }


}

