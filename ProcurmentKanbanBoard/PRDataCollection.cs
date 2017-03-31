namespace ProcurmentKanbanBoard
{
    using Microsoft.Office.Interop.Excel;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PRDataCollection
    {
        private static volatile PRDataCollection instance;
        public List<PRData> PRDataEntries = new List<PRData>();
        private static object syncRoot = new object();

        private static PRData ConvertToStringArray(Array values)
        {
            //if (values.GetValue(1, 7) == null)
            //{
            //    return null;
            //}
            PRData data = new PRData();
            string[] strArray = new string[values.Length];
            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(1, i) == null)
                {
                    continue;
                }
                Type type = values.GetValue(1, i).GetType();
                    int num3 = i;
                switch (num3)
                {

                    case 5:
                        data.PRNumber = values.GetValue(1, i).ToString();
                        break;

                    case 8:
                        data.SWHW = values.GetValue(1, i).ToString();
                        break;

                    case 9:
                        data.Description = values.GetValue(1, i).ToString();
                        break;

                    case 14:
                        data.PONumber = values.GetValue(1, i).ToString();
                        break;

                    case 20:
                        data.Project = values.GetValue(1, i).ToString();
                        break;

                    case 21:
                        data.ManagerName = values.GetValue(1, i).ToString();
                        break;

                    case 22:
                        data.Status = values.GetValue(1, i).ToString();
                        break;

                    case 7:
                    {

                        if (type.Name.Equals("Double"))
                        {
                            data.Quantity = Convert.ToDouble(values.GetValue(1, i).ToString());

                        }

                        break;
                    }

                   
                }
                if (type.Name.Equals("DateTime"))
                {
                    switch (i)
                    {
                        case 4:
                            data.SmPmRequestedDate = Convert.ToDateTime(values.GetValue(1, i));
                            break;

                        //case 3:
                        //    data.QuotationRequestedDate = Convert.ToDateTime(values.GetValue(1, i));
                        //    break;

                        //case 4:
                        //    data.QuotationReceivedDate = Convert.ToDateTime(values.GetValue(1, i));
                        //    break;

                        //case 4:
                        //    data.QuotationApprovedDate = Convert.ToDateTime(values.GetValue(1, i));
                        //    break;

                        case 6:
                            data.PRCreatedDate = Convert.ToDateTime(values.GetValue(1, i));
                            break;

                        case 10:
                            data.SCMApprovedDate = Convert.ToDateTime(values.GetValue(1, i));//SCM
                            break;

                        case 11:
                            data.ReportingManagerApproved = Convert.ToDateTime(values.GetValue(1, i));// Reporting manager
                            break;
                        case 12:
                            data.IT_CoordinatorApproved  = Convert.ToDateTime(values.GetValue(1, i));// Reporting manager
                            break;
                        case 13:
                            data.LOBApproved  = Convert.ToDateTime(values.GetValue(1, i));// Reporting manager
                            break;
                        //It Coordinator and Lob HEad date 
                        case 15:
                            data.POReleasedDate = Convert.ToDateTime(values.GetValue(1, i));
                            break;

                        case 16:
                            data.ExpectedDate = Convert.ToDateTime(values.GetValue(1, i));
                            break;

                        case 23:
                            data.PRRecivedDate = Convert.ToDateTime(values.GetValue(1, i));
                            break;
                    }
                }
            }
            return data;
        }

        internal PRDataCollection ReadDataFromExcel(string DataPath, string sheetname)
        {
            Application o = new ApplicationClass();
            Workbook workbook = o.Workbooks.Open(DataPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Worksheet worksheet = (Worksheet) workbook.Worksheets.get_Item(1);
            Worksheet worksheet2 = (Worksheet) workbook.Sheets[sheetname];
            Range usedRange = worksheet2.UsedRange;
            foreach (Range range2 in usedRange.Rows)
            {
                int row = range2.Row;
                if (row != 1)
                {
                    Array values = (Array) worksheet2.get_Range(string.Concat(new object[] { "A", row, ":Z", row }), Type.Missing).Cells.get_Value(Missing.Value);
                    PRData item = ConvertToStringArray(values);
                   
                    if (item != null)
                    {
                        item.SerialNo = row.ToString();
                        this.PRDataEntries.Add(item);
                    }
                }
            }
            o.Quit();
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(o);
            o = null;
            return this;
        }

        public static PRDataCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if ((instance == null) && (instance == null))
                        {
                            instance = new PRDataCollection();
                        }
                    }
                }
                return instance;
            }
        }
    }
}

