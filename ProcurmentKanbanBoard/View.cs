using System;
using System.Collections.Generic;

namespace ProcurmentKanbanBoard
{
    [Serializable]
    public sealed class View
    {
        public View()
        {
            this.Id = Guid.NewGuid();
            this.Name = "New View";
            this.Columns = new List<Guid>();
            this.Topics = new List<Guid>();
            this.Persons = new List<Guid>();
        }

        internal List<Guid> Columns
        {
            get; set;
        }

        internal Guid Id
        {
            get; set;
        }

        internal string Name
        {
            get; set;
        }

        internal List<Guid> Persons
        {
            get; set;
        }

        internal List<Guid> Topics
        {
            get; set;
        }
    }
}