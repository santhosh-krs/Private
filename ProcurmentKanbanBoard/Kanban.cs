namespace ProcurmentKanbanBoard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    [Serializable]
    public sealed class Kanban
    {
        [XmlIgnore]
        public static readonly Guid AnyId = new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff");
        private static volatile Kanban instance;
        private static readonly object syncRoot = new object();

        private Kanban()
        {
            Topics = new List<Topic>();
            Persons = new List<Person>();
            Columns = new List<Column>();
            Views = new List<View>();
            ApplicationSettings = new ApplicationSettings();
        }

        public void CheckConsistency()
        {
            Predicate<Guid> match = null;
            Predicate<Guid> predicate3 = null;
            Predicate<Guid> predicate4 = null;
            foreach (Column column in Columns)
            {
                foreach (Task task in column.Tasks)
                {
                    if ((task.TopicId != Guid.Empty) && (FindTopicById(task.TopicId) == null))
                    {
                        Trace.WriteLine("Tasks - removing topic: " + task.TopicId);
                        task.TopicId = Guid.Empty;
                    }
                    if ((task.PersonId != Guid.Empty) && (FindPersonById(task.PersonId) == null))
                    {
                        Trace.WriteLine("Tasks - removing person: " + task.PersonId);
                        task.PersonId = Guid.Empty;
                    }
                }
            }
            foreach (View view in Views)
            {
                if (match == null)
                {
                    Predicate<Guid> predicate5 = id => ((id != Guid.Empty) && (id != AnyId)) && (FindColumnById(id) == null);
                    match = predicate5;
                }
                view.Columns.RemoveAll(match);
                if (predicate3 == null)
                {
                    Predicate<Guid> predicate6 = id => ((id != Guid.Empty) && (id != AnyId)) && (FindTopicById(id) == null);
                    predicate3 = predicate6;
                }
                view.Topics.RemoveAll(predicate3);
                if (predicate4 == null)
                {
                    Predicate<Guid> predicate7 = id => ((id != Guid.Empty) && (id != AnyId)) && (FindPersonById(id) == null);
                    predicate4 = predicate7;
                }
                view.Persons.RemoveAll(predicate4);
            }
            bool flag = false;
            foreach (View view2 in Views)
            {
                if (view2.Id == ApplicationSettings.CurrentView)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                Trace.WriteLine("Views: resetting current view");
                if (Views.Count > 0)
                {
                    ApplicationSettings.CurrentView = Views[0].Id;
                }
                else
                {
                    ApplicationSettings.CurrentView = AnyId;
                }
            }
        }

        public static int CompareTasks(Task x, Task y, Kanban kanban, string parameter, bool desc)
        {
            int num;
            switch (parameter)
            {
                case "Text":
                    num = string.CompareOrdinal(x.Text, y.Text);
                    if (desc)
                    {
                        return -num;
                    }
                    return num;

                case "Topic":
                {
                    Topic topic = kanban.FindTopicById(x.TopicId);
                    Topic topic2 = kanban.FindTopicById(y.TopicId);
                    if ((topic == null) || (topic2 != null))
                    {
                        if ((topic == null) && (topic2 != null))
                        {
                            num = -1;
                            if (!desc)
                            {
                                return num;
                            }
                            return -num;
                        }
                        if ((topic == null))
                        {
                            return 0;
                        }
                        if (topic.Id == topic2.Id)
                        {
                            return 0;
                        }
                        num = string.CompareOrdinal(topic.Name, topic2.Name);
                        if (!desc)
                        {
                            return num;
                        }
                        return -num;
                    }
                    num = 1;
                    if (desc)
                    {
                        return -num;
                    }
                    return num;
                }
            }
            return 0;
        }

        public Column FindColumnById(Guid id)
        {
            if (id != Guid.Empty)
            {
                foreach (Column column in Columns)
                {
                    if (column.Id.CompareTo(id) == 0)
                    {
                        return column;
                    }
                }
            }
            return null;
        }

        public Person FindPersonById(Guid id)
        {
            if (id != Guid.Empty)
            {
                foreach (Person person in Persons)
                {
                    if (person.Id.CompareTo(id) == 0)
                    {
                        return person;
                    }
                }
            }
            return null;
        }

        public Task FindTaskById(Guid id)
        {
            if (id != Guid.Empty)
            {
                foreach (Column column in Columns)
                {
                    foreach (Task task in column.Tasks)
                    {
                        if (task.Id.CompareTo(id) == 0)
                        {
                            return task;
                        }
                    }
                }
            }
            return null;
        }

        public Task FindTaskById(Guid id, out Column column)
        {
            column = null;
            if (id != Guid.Empty)
            {
                foreach (Column column2 in Columns)
                {
                    foreach (Task task in column2.Tasks)
                    {
                        if (task.Id.CompareTo(id) == 0)
                        {
                            column = column2;
                            return task;
                        }
                    }
                }
            }
            return null;
        }

        public Task FindTaskById(Guid id, ref Column column, out Topic topic, out Person person)
        {
            topic = null;
            person = null;
            if (id != Guid.Empty)
            {
                foreach (Column column2 in Columns)
                {
                    foreach (Task task in column2.Tasks)
                    {
                        if (task.Id.CompareTo(id) == 0)
                        {
                            column = column2;
                            topic = FindTopicById(task.TopicId);
                            person = FindPersonById(task.PersonId);
                            return task;
                        }
                    }
                }
            }
            return null;
        }

        public Topic FindTopicById(Guid id)
        {
            if (id != Guid.Empty)
            {
                foreach (Topic topic in Topics)
                {
                    if (topic.Id.CompareTo(id) == 0)
                    {
                        return topic;
                    }
                }
            }
            return null;
        }

        private static Kanban Load()
        {
            Kanban kanban = null;
            StreamReader textReader = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Kanban));
                textReader = new StreamReader(DataPath, Encoding.UTF8);
                kanban = (Kanban) serializer.Deserialize(textReader);
                textReader.Close();
                kanban.CheckConsistency();
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Failed to load data from file: """ + DataPath + "\"\n\n" + exception.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                if (textReader != null)
                {
                    textReader.Close();
                }
            }
            return kanban;
        }

        public bool Save()
        {
            StreamWriter writer = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Kanban));
                writer = new StreamWriter(DataPath, false, Encoding.UTF8);
                serializer.Serialize((TextWriter) writer, this);
                writer.Close();
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Failed to save data to file: """ + DataPath + "\"\n\n" + exception.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                if (writer != null)
                {
                    writer.Close();
                }
                return false;
            }
        }

        public ApplicationSettings ApplicationSettings { get; set; }

        [XmlIgnore]
        public List<Column> Columns { get; set; }

        [XmlIgnore]
        public List<Column> CurrentView
        {
            get
            {
                View view = null;
                foreach (View view2 in Views)
                {
                    if (view2.Id == ApplicationSettings.CurrentView)
                    {
                        view = view2;
                        break;
                    }
                }
                if (view == null)
                {
                    return Columns;
                }
                List<Column> list = new List<Column>();
                foreach (Column column in Instance.Columns)
                {
                    if (view.Columns.Contains(AnyId) || view.Columns.Contains(column.Id))
                    {
                        Column item = new Column {
                            Id = column.Id,
                            Name = column.Name
                        };
                        list.Add(item);
                        foreach (Task task in column.Tasks)
                        {
                            if ((view.Topics.Contains(AnyId) || view.Topics.Contains(task.TopicId)) && (view.Persons.Contains(AnyId) || view.Persons.Contains(task.PersonId)))
                            {
                                item.Tasks.Add(task);
                            }
                        }
                    }
                }
                return list;
            }
        }

        [XmlIgnore]
        public static string DataPath => (Application.StartupPath + @"\" + Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".xml");

        public static Kanban Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = Load();
                        if (instance == null)
                        {
                            instance = new Kanban();
                        }
                    }
                }
                return instance;
            }
        }

        [XmlIgnore]
        public List<Person> Persons { get; set; }

        [XmlIgnore]
        public List<Topic> Topics { get; set; }

        [XmlIgnore]
        public List<View> Views { get; set; }

        [Serializable]
        public sealed class Column
        {
            public Column()
            {
                Id = Guid.NewGuid();
                Name = "New Column";
                Tasks = new List<Task>();
            }

            public Guid Id { get; set; }

            public string Name { get; set; }

            public List<Task> Tasks { get; set; }
        }

        [Serializable]
        public sealed class Person
        {
            public Person()
            {
                Id = Guid.NewGuid();
                Name = "New Person";
                DisplayName = "user1";
                Cluster = "Cluster1";
            }

            public string Cluster { get; set; }

            public string DisplayName { get; set; }

            public Guid Id { get; set; }

            public string Name { get; set; }
        }

        [Serializable]
        public sealed class Task
        {
            public Task()
            {
                Id = Guid.NewGuid();
                TopicId = Guid.Empty;
                PersonId = Guid.Empty;
                Text = "New Task";
            }

            public Guid Id { get; set; }

            public Guid PersonId { get; set; }

            [XmlIgnore]
            public string Text { get; set; }

            public Guid TopicId { get; set; }
        }

        [Serializable]
        public sealed class Topic
        {
            public Topic()
            {
                Id = Guid.NewGuid();
                Name = "New Topic";
                TaskForeColor = Color.WhiteSmoke;
                TaskBackColor = Color.Gray;
                Default = false;
            }

            public bool Default { get; set; }

            public Guid Id { get; set; }

            public string Name { get; set; }

            [XmlIgnore, DisplayName("Background Color")]
            public Color TaskBackColor { get; set; }

            [Browsable(false), XmlElement("TaskBackColor")]
            public string TaskBackColorHtml
            {
                get
                {
                    return ColorTranslator.ToHtml(TaskBackColor);
                }
                set
                {
                    TaskBackColor = ColorTranslator.FromHtml(value);
                }
            }

            [XmlIgnore, DisplayName("Text Color")]
            public Color TaskForeColor { get; set; }

            [Browsable(false), XmlElement("TaskForeColor")]
            public string TaskForeColorHtml
            {
                get
                {
                    return ColorTranslator.ToHtml(TaskForeColor);
                }
                set
                {
                    TaskForeColor = ColorTranslator.FromHtml(value);
                }
            }
        }

       
    }
}

