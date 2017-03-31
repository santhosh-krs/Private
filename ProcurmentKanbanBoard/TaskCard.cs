namespace ProcurmentKanbanBoard
{
    using System;
    using System.Windows.Forms;

    internal class TaskCard : ListBox
    {
        private int selectedIndex = -1;
        private bool verticalScrollbar;

        public TaskCard()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            base.HorizontalScrollbar = false;
            base.IntegralHeight = false;
            base.MultiColumn = false;
            base.ScrollAlwaysVisible = false;
            this.SelectionMode = SelectionMode.One;
            base.BorderStyle = BorderStyle.None;
        }

        public bool ItemSelected(int index)
        {
            bool flag = false;
            if (this.SelectionMode == SelectionMode.One)
            {
                flag = this.SelectedIndex == index;
            }
            return flag;
        }

        public void ItemSelected(int index, bool state)
        {
            if ((this.SelectionMode == SelectionMode.One) && state)
            {
                this.SelectedIndex = index;
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                this.selectedIndex = e.Index;
            }
            base.OnDrawItem(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            try
            {
                base.OnHandleDestroyed(e);
            }
            catch
            {
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (!this.VerticalScrollbar)
            {
                if (e.Delta > 0)
                {
                    if ((this.Count > 0) && (base.TopIndex > 0))
                    {
                        base.TopIndex--;
                    }
                }
                else if (((e.Delta < 0) && (this.Count > 0)) && (base.TopIndex < (this.Count - 1)))
                {
                    base.TopIndex++;
                }
            }
        }

        public int Count
        {
            get
            {
                return Win32Helper.SendMessage(base.Handle, 0x18b, IntPtr.Zero, IntPtr.Zero);
            }
            set
            {
                Win32Helper.SendMessage(base.Handle, 0x1a7, (IntPtr) value, IntPtr.Zero);
                int selectedIndex = this.SelectedIndex;
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams createParams = base.CreateParams;
                if (!this.verticalScrollbar)
                {
                    createParams.Style &= -2097153;
                }
                createParams.Style &= -65;
                createParams.Style &= -3;
                createParams.Style = (createParams.Style | 0x10) | 0x2000;
                return createParams;
            }
        }

        public int SelectedIndex
        {
            get
            {
                this.selectedIndex = -1;
                if (this.SelectionMode == SelectionMode.One)
                {
                    this.selectedIndex = Win32Helper.SendMessage(base.Handle, 0x188, IntPtr.Zero, IntPtr.Zero);
                }
                return this.selectedIndex;
            }
            set
            {
                if (this.SelectionMode == SelectionMode.One)
                {
                    this.selectedIndex = Win32Helper.SendMessage(base.Handle, 390, (IntPtr) value, IntPtr.Zero);
                }
            }
        }

        public virtual bool VerticalScrollbar
        {
            get
            {
                return this.verticalScrollbar;
            }
            set
            {
                if (this.verticalScrollbar != value)
                {
                    this.verticalScrollbar = value;
                    base.RecreateHandle();
                }
            }
        }
    }
}

