namespace ProcurmentKanbanBoard
{
    using System;
    using System.Runtime.CompilerServices;

    public class PRData
    {
        // Fields
        private DateTime defaultdate = new DateTime();

        // Methods
        public PRData()
        {
            this.QuotationRequestedDate = this.Defaultdate;
            this.QuotationReceivedDate = this.Defaultdate;
            this.QuotationApprovedDate = this.Defaultdate;
            this.SmPmRequestedDate = this.Defaultdate;
            this.CurrentDate = DateTime.Now;
            this.PRCreatedDate = this.Defaultdate;
            this.ExpectedDate = this.Defaultdate;
            this.ReportingManagerApproved = this.Defaultdate;
            this.SCMApprovedDate = this.Defaultdate;
            this.POReleasedDate = this.Defaultdate;
            this.PRRecivedDate = this.Defaultdate;
            this.ManagerName = string.Empty;
            this.Project = string.Empty;
        }
        public Guid Guid
        {
            get; set;
        }

        public string SerialNo
        {
            get; set;
        }
        // Properties
        public DateTime ReportingManagerApproved { get; set; }
        public DateTime IT_CoordinatorApproved
        {
            get; set;
        }
        public DateTime LOBApproved
        {
            get; set;
        }
        public DateTime CurrentDate { get; set; }

        public DateTime Defaultdate
        {
            get { return this.defaultdate; }
            set { this.defaultdate = value; }
        }

        public string Description { get; set; }

        public DateTime ExpectedDate { get; set; }

        public DateTime SCMApprovedDate { get; set; }

        public string ManagerName { get; set; }

        public DateTime PRCreatedDate { get; set; }

        public string PONumber { get; set; }

        public DateTime PRRecivedDate { get; set; }

        public DateTime POReleasedDate { get; set; }

        public string PRNumber { get; set; }

        public string Project { get; set; }

        public double Quantity { get; set; }

        public DateTime QuotationApprovedDate { get; set; }

        public DateTime QuotationReceivedDate { get; set; }

        public DateTime QuotationRequestedDate { get; set; }

        public DateTime SmPmRequestedDate { get; set; }

        public string Status { get; set; }

        public string SWHW { get; set; }
    }



}

