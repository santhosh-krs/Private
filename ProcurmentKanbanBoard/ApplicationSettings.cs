namespace ProcurmentKanbanBoard
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public sealed class ApplicationSettings
    {
        public ApplicationSettings()
        {
            this.NumberofCluster = 4;
            this.BoardForeColor = Color.WhiteSmoke;
            this.BoardBackColor = Color.DimGray;
            this.ViewTabsHeight = 30;
            this.HeaderHeight = 0x19;
            this.HeaderShowCount = true;
            this.HeaderForeColor = Color.DarkOliveGreen;
            this.HeaderBackColor = Color.MintCream;
            this.DefaultTaskForeColor = Color.WhiteSmoke;
            this.DefaultTaskBackColor = Color.Gray;
            this.HotTaskBackColor = Color.Blue;
            this.SelectedTaskForeColor = Color.WhiteSmoke;
            this.SelectedTaskBackColor = Color.Orchid;
            this.CardHeight = 80;
            this.CardShowPerson = true;
            this.ToolTipShowText = true;
            this.DataPath = @"D:\IndustryPRDetails.Xls";
        }

        [DisplayName("Text Color"), Category("1. Miscellaneous"), Description("Text color used in toolbar."), XmlIgnore]
        public Color BoardBackColor { get; set; }

        [XmlElement("BoardBackColor"), Browsable(false)]
        public string BoardBackColorHtml
        {
            get
            {
                return ColorTranslator.ToHtml(this.BoardBackColor);
            }
            set
            {
                this.BoardBackColor = ColorTranslator.FromHtml(value);
            }
        }

        [Description("Text color used in toolbar."), XmlIgnore, DisplayName("Text Color"), Category("1. Miscellaneous")]
        public Color BoardForeColor { get; set; }

        [Browsable(false), XmlElement("BoardForeColor")]
        public string BoardForeColorHtml
        {
            get
            {
                return ColorTranslator.ToHtml(this.BoardForeColor);
            }
            set
            {
                this.BoardForeColor = ColorTranslator.FromHtml(value);
            }
        }

        public uint CardHeight { get; set; }

        public bool CardShowPerson { get; set; }

        public Guid CurrentView { get; set; }

        public string CurrentYear { get; set; }

        public string DataPath { get; set; }

        public Color DefaultTaskBackColor { get; set; }

        public Color DefaultTaskForeColor { get; set; }

        [XmlIgnore]
        public Color HeaderBackColor { get; set; }

        [XmlIgnore]
        public Color HeaderForeColor { get; set; }

        public uint HeaderHeight { get; set; }

        public bool HeaderShowCount { get; set; }

        public Color HotTaskBackColor { get; set; }

        public int NumberofCluster { get; set; }

        public Color SelectedTaskBackColor { get; set; }

        public Color SelectedTaskForeColor { get; set; }

        public Color SelectedViewTabBackColor { get; set; }

        public Color SelectedViewTabForeColor { get; set; }

        public bool ToolTipShowPerson { get; set; }

        public bool ToolTipShowText { get; set; }

        public uint ViewTabsHeight { get; set; }
    }
}

