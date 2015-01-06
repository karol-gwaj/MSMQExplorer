using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MSMQExplorer
{
    public partial class MainForm : Form
    {
        private MessageQueue[] PrivateQueues;
        private MessageQueue[] PublicQueues;
        private MessageQueue JournalQueue;
        private MessageQueue DeadLetterQueue;
        private MessageQueue TransactionalDeadLetterQueue;
        private Dictionary<string, int> QueueMessagesCount;
        private List<string> SelectedPath;

        #region .ctor
        public MainForm()
        {
            InitializeComponent();

            Task.Factory.StartNew(
                delegate()
                {
                    ReloadQueues(ServerName.Text);
                    Invoke(new Action(() => RefreshQueuesTree()));
                });
        }
        #endregion

        #region RefreshQueuesTree
        private void RefreshQueuesTree()
        {
            QueuesTree.BeginUpdate();
            try
            {
                QueuesTree.Nodes.Clear();

                var node = GetPrivateQueuesTreeNode();
                if (node != null) QueuesTree.Nodes.Add(node);

                node = GetPublicQueuesTreeNode();
                if (node != null) QueuesTree.Nodes.Add(node);                

                node = GetSystemQueuesTreeNode();
                if (node != null) QueuesTree.Nodes.Add(node);     
           
                if (SelectedPath != null)
                {
                    TreeNode selected = null;

                    var nodes = QueuesTree.Nodes;
                    foreach (var key in SelectedPath.AsEnumerable().Reverse())
                    {
                        var s = nodes.OfType<TreeNode>().Where(n => string.Equals(n.Name, key) || string.Equals(n.Text, key)).FirstOrDefault();
                        if (s != null)
                        {
                            selected = s;
                            selected.Expand();
                            nodes = selected.Nodes;
                        }
                    }

                    if (selected != null)
                    {
                        QueuesTree.SelectedNode = selected;
                        selected.EnsureVisible();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                QueuesTree.EndUpdate();
            }
        }
        #endregion

        #region GetPublicQueuesTreeNode
        private TreeNode GetPublicQueuesTreeNode()
        {
            if (PublicQueues == null) return null;

            var node = new TreeNode("Public Queues");
            node.ImageIndex = node.SelectedImageIndex = 1;

            foreach (var mq in PublicQueues)
            {
                var n = new TreeNode(mq.QueueName.Substring(8));
                n.ImageIndex = n.SelectedImageIndex = 3;
                n.Tag = mq;
                n.Name = mq.FormatName;
                node.Nodes.Add(n);
            }

            return node;
        }
        #endregion

        #region GetPrivateQueuesTreeNode
        private TreeNode GetPrivateQueuesTreeNode()
        {
            if (PrivateQueues == null) return null;

            var node = new TreeNode("Private Queues");
            node.ImageIndex = node.SelectedImageIndex = 1;

            foreach (var mq in PrivateQueues)
            {
                var mqj = new MessageQueue(string.Concat("FormatName:", mq.FormatName, ";JOURNAL"), QueueAccessMode.Peek);
                var mqp = new MessageQueue(string.Concat("FormatName:", mq.FormatName, ";POISON"), QueueAccessMode.Peek);
                var mqr = new MessageQueue(string.Concat("FormatName:", mq.FormatName, ";RETRY"), QueueAccessMode.Peek);

                var n = new TreeNode(mq.QueueName.Substring(9));
                n.ImageIndex = n.SelectedImageIndex = 2;
                n.Tag = mq;

                var qn = new TreeNode("Queue messages");
                qn.ImageIndex = qn.SelectedImageIndex = 4;
                qn.Tag = mq;
                qn.Name = mq.FormatName;
                qn.Nodes.Add(new TreeNode("loading...") { Name = "dummy" });
                n.Nodes.Add(qn);

                var jn = new TreeNode("Journal messages");
                jn.ImageIndex = jn.SelectedImageIndex = 5;
                jn.Tag = mqj;
                jn.Name = mqj.FormatName;
                jn.Nodes.Add(new TreeNode("loading...") { Name = "dummy" });
                n.Nodes.Add(jn);

                var pn = new TreeNode("Poison messages");
                pn.ImageIndex = pn.SelectedImageIndex = 4;
                pn.Tag = mqp;
                pn.Name = mqp.FormatName;
                pn.Nodes.Add(new TreeNode("loading...") { Name = "dummy" });
                n.Nodes.Add(pn);

                var rn = new TreeNode("Retry messages");
                rn.ImageIndex = rn.SelectedImageIndex = 4;
                rn.Tag = mqr;
                rn.Name = mqr.FormatName;
                rn.Nodes.Add(new TreeNode("loading...") { Name = "dummy" });
                n.Nodes.Add(rn);

                node.Nodes.Add(n);
            }

            return node;
        }
        #endregion

        #region GetSystemQueuesTreeNodes
        private TreeNode GetSystemQueuesTreeNode()
        {
            var node = new TreeNode("System Queues");
            node.ImageIndex = node.SelectedImageIndex = 1;

            if (JournalQueue != null)
            {
                var n = new TreeNode("Journal messages");
                n.ImageIndex = n.SelectedImageIndex = 5;
                n.Tag = JournalQueue;
                n.Name = JournalQueue.FormatName;
                node.Nodes.Add(n);
            }

            if (DeadLetterQueue != null)
            {
                var n = new TreeNode("Dead-letter messages");
                n.ImageIndex = n.SelectedImageIndex = 6;
                n.Tag = DeadLetterQueue;
                n.Name = DeadLetterQueue.FormatName;
                node.Nodes.Add(n);
            }

            if (TransactionalDeadLetterQueue != null)
            {
                var n = new TreeNode("Transactional dead-letter messages");
                n.ImageIndex = n.SelectedImageIndex = 6;
                n.Tag = TransactionalDeadLetterQueue;
                n.Name = TransactionalDeadLetterQueue.FormatName;
                node.Nodes.Add(n);
            }

            return node;
        }
        #endregion

        #region ReloadQueues
        private void ReloadQueues(string server)
        {
            if (string.IsNullOrWhiteSpace(server)) server = "localhost";

            Parallel.Invoke(
                delegate()
                {
                    PrivateQueues = MessageQueue.GetPrivateQueuesByMachine(server);
                    Debug.WriteLine("private queues loaded");
                },
                delegate()
                {
                    try
                    {
                        PublicQueues = MessageQueue.GetPublicQueuesByMachine(server);
                        Debug.WriteLine("public queues loaded");
                    }
                    catch (MessageQueueException ex)
                    {
                        if (ex.MessageQueueErrorCode == MessageQueueErrorCode.UnsupportedOperation)
                        {
                            Debug.WriteLine("no public queues");
                            PublicQueues = null; // public queues not supported (msmq not installed in AD mode)
                        }
                        else throw;
                    }
                },
                delegate()
                {
                    JournalQueue = new MessageQueue(string.Concat("FormatName:DIRECT=OS:", server, @"\system$;JOURNAL"), QueueAccessMode.Receive);
                    DeadLetterQueue = new MessageQueue(string.Concat("FormatName:DIRECT=OS:", server, @"\system$;DEADLETTER"), QueueAccessMode.Receive);
                    TransactionalDeadLetterQueue = new MessageQueue(string.Concat("FormatName:DIRECT=OS:", server, @"\system$;DEADXACT"), QueueAccessMode.Receive);
                    Debug.WriteLine("system queues loaded");
                },
                delegate()
                {
                    var dict = new Dictionary<string, int>();
                    var category = new PerformanceCounterCategory("MSMQ Queue");
                    category.MachineName = server;
                    using (var counter = new PerformanceCounter())
                    {
                        counter.MachineName = server;
                        counter.CategoryName = "MSMQ Queue";

                        counter.CounterName = "Messages in Queue";
                        foreach (string inst in category.GetInstanceNames())
                        {
                            if (inst.EndsWith("$")) continue;

                            counter.InstanceName = inst;

                            var idx = inst.IndexOf("private$");
                            if (idx < 0) continue;

                            var name = inst.Substring(idx);
                            var count = (int)counter.NextValue();

                            dict[NormalizeQueueName(name)] = count;
                        }

                        counter.CounterName = "Messages in Journal Queue";
                        foreach (string inst in category.GetInstanceNames())
                        {
                            if (inst.EndsWith("$")) continue;

                            counter.InstanceName = inst;

                            var idx = inst.IndexOf("private$");
                            if (idx < 0) continue;

                            var name = string.Concat(inst.Substring(idx), ";JOURNAL");
                            var count = (int)counter.NextValue();

                            dict[NormalizeQueueName(name)] = count;
                        }
                    }

                    QueueMessagesCount = dict;
                    Debug.WriteLine("counters loaded");
                });
        }
        #endregion

        #region GetMessageCount
        private int GetMessageCount(MessageQueue q)
        {
            try
            {
                var name = NormalizeQueueName(q.Path);
                if (QueueMessagesCount.ContainsKey(name)) return QueueMessagesCount[name];
            }
            catch { }

            int count = 0;

            q.MessageReadPropertyFilter.SetDefaults();
            q.MessageReadPropertyFilter.Body = false;

            var enumerator = q.GetMessageEnumerator2();
            while (enumerator.MoveNext())
            {
                count++;
            }

            return count;
        }
        #endregion

        #region NormalizeQueueName
        private string NormalizeQueueName(string path)
        {
            if (path.StartsWith("FormatName:"))
            {
                var idx = path.IndexOf("private$");
                if (idx >= 0)
                {
                    path = path.Substring(idx);
                }
            }

            return path.Replace("\\", "/");
        }
        #endregion

        #region EnsureNodeLoaded
        private void EnsureNodeLoaded(TreeNode node)
        {
            if (node == null) return;
            if (string.IsNullOrWhiteSpace(node.Name) || (node.Nodes.Count != 1) || !string.Equals(node.Nodes[0].Name, "dummy")) return;

            node.Nodes.Clear();

            var mq = node.Tag as MessageQueue;
            if (mq != null)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    mq.MessageReadPropertyFilter.SetDefaults();
                    mq.MessageReadPropertyFilter.AcknowledgeType = true;
                    mq.MessageReadPropertyFilter.ArrivedTime = true;
                    mq.MessageReadPropertyFilter.Authenticated = true;
                    mq.MessageReadPropertyFilter.MessageType = true;
                    mq.MessageReadPropertyFilter.Priority = true;
                    mq.MessageReadPropertyFilter.Recoverable = true;
                    mq.MessageReadPropertyFilter.SentTime = true;
                    mq.MessageReadPropertyFilter.TimeToBeReceived = true;
                    mq.MessageReadPropertyFilter.TimeToReachQueue = true;
                    mq.MessageReadPropertyFilter.UseAuthentication = true;
                    mq.MessageReadPropertyFilter.UseDeadLetterQueue = true;
                    mq.MessageReadPropertyFilter.UseEncryption = true;
                    mq.MessageReadPropertyFilter.UseJournalQueue = true;
                    mq.MessageReadPropertyFilter.UseTracing = true;

                    var enumerator = mq.GetMessageEnumerator2();                   
                    while (enumerator.MoveNext())
                    {
                        var m = enumerator.Current;
                        
                        var name = m.Label;
                        if (string.IsNullOrWhiteSpace(name)) name = m.Id;

                        var n = new TreeNode(name);
                        n.ImageIndex = n.SelectedImageIndex = 0;
                        n.Tag = m;
                        n.Name = m.Id;

                        node.Nodes.Add(n);
                    }
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        #region ConvertMessageToXMLDoc
        private System.Xml.XmlDocument ConvertMessageToXMLDoc(System.Messaging.Message msg)
        {
            try
            {
                msg.BodyStream.Position = 0;

                byte[] buffer = new byte[msg.BodyStream.Length];
                msg.BodyStream.Read(buffer, 0, (int)msg.BodyStream.Length);

                int envelopeStart = FindEnvolopeStart(buffer);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer, envelopeStart, buffer.Length - envelopeStart);

                System.ServiceModel.Channels.BinaryMessageEncodingBindingElement elm = new System.ServiceModel.Channels.BinaryMessageEncodingBindingElement();
                System.ServiceModel.Channels.Message msg1 = elm.CreateMessageEncoderFactory().Encoder.ReadMessage(stream, Int32.MaxValue);

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(msg1.GetReaderAtBodyContents());
                msg.BodyStream.Position = 0;
                return doc;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region FindEnvolopeStart
        private int FindEnvolopeStart(byte[] stream)
        {
            byte knownEncodingRecordType = (byte)0x03;
            byte binaryEncodingType = (byte)0x07;
            byte canonicalizationMethod = (byte)0x56;
            byte envelopeStart = (byte)0x02;

            int i = 0;
            byte prevByte = stream[i];
            byte curByte = (byte)0;
            bool foundEnvelopeStart = false;
            for (i = 0; i < stream.Length; i++)
            {
                curByte = stream[i];
                if (curByte == envelopeStart &&
                    prevByte == canonicalizationMethod)
                {
                    int indexCheck = i - 2;
                    if (indexCheck > 1)
                    {
                        if (stream[indexCheck] == binaryEncodingType &&
                            stream[indexCheck - 1] == knownEncodingRecordType)
                        {
                            foundEnvelopeStart = true;
                            break;
                        }
                    }
                }

                prevByte = curByte;
            }

            return foundEnvelopeStart ? (i - 1) : -1;
        }
        #endregion

        #region ToString
        private string ToString(XmlDocument doc)
        {
            var builder = new StringBuilder();
            
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.NewLineChars = "\r\n";
            settings.NewLineHandling = NewLineHandling.Replace;

            using (XmlWriter writer = XmlWriter.Create(builder, settings))
            {
                doc.Save(writer);
            }
            
            return builder.ToString();
        }
        #endregion

        #region RefreshButton_Click
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            ReloadQueues(ServerName.Text);
            RefreshQueuesTree();
        }
        #endregion

        #region QueuesTree_AfterSelect
        private void QueuesTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var sn = e.Node;
            SelectedPath = new List<string>();
            while (sn != null)
            {
                var key = sn.Name;
                if (string.IsNullOrWhiteSpace(key)) key = sn.Text;

                if (!string.IsNullOrWhiteSpace(key)) SelectedPath.Add(key);

                sn = sn.Parent;
            }

            MessagesList.BeginUpdate();
            try
            {
                MessagesList.Items.Clear();
                MessagesList.Columns.Clear();

                MessageEditor.IsReadOnly = false;
                MessageEditor.Scrolling.ScrollBy(-1, -1);
                MessageEditor.Text = "";

                if (e.Node == null) return;

                if (e.Node.Tag is System.Messaging.Message)
                {
                    var m = e.Node.Tag as System.Messaging.Message;

                    var doc = ConvertMessageToXMLDoc(m);
                    if (doc != null)
                    {
                        MessageEditor.ConfigurationManager.Language = "xml";
                        MessageEditor.Text = ToString(doc);
                    }
                    else
                    {
                        MessageEditor.ConfigurationManager.Language = "";
                        m.BodyStream.Position = 0;
                        MessageEditor.Text = new StreamReader(m.BodyStream).ReadToEnd();
                    }

                    MessageEditor.Refresh();

                    MessagesList.Columns.Add(new ColumnHeader() { Text = "Name" });
                    MessagesList.Columns.Add(new ColumnHeader() { Text = "Value" });
                    
                    MessagesList.Items.Add(new ListViewItem(new string[] { "AcknowledgeType", Convert.ToString(m.AcknowledgeType) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "ArrivedTime", Convert.ToString(m.ArrivedTime) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "Authenticated", Convert.ToString(m.Authenticated) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "BodyLength", Convert.ToString(m.BodyStream.Length) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "MessageId", Convert.ToString(m.Id) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "Label", Convert.ToString(m.Label) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "MessageType", Convert.ToString(m.MessageType) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "Priority", Convert.ToString(m.Priority) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "Recoverable", Convert.ToString(m.Recoverable) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "SentTime", Convert.ToString(m.SentTime) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "TimeToBeReceived", Convert.ToString(m.TimeToBeReceived) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "TimeToReachQueue", Convert.ToString(m.TimeToReachQueue) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "UseAuthentication", Convert.ToString(m.UseAuthentication) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "UseDeadLetterQueue", Convert.ToString(m.UseDeadLetterQueue) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "UseEncryption", Convert.ToString(m.UseEncryption) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "UseJournalQueue", Convert.ToString(m.UseJournalQueue) }));
                    MessagesList.Items.Add(new ListViewItem(new string[] { "UseTracing", Convert.ToString(m.UseTracing) }));



                    MessagesList.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    MessagesList.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                else if (string.IsNullOrWhiteSpace(e.Node.Name))
                {

                    MessagesList.Columns.Add(new ColumnHeader() { Text = "Name" });
                    MessagesList.Columns.Add(new ColumnHeader() { Text = "Number of Messages" });

                    foreach (TreeNode n in e.Node.Nodes)
                    {
                        var mq = n.Tag as MessageQueue;

                        var li = new ListViewItem();
                        li.Tag = mq;
                        li.Text = n.Text;
                        li.ImageIndex = n.ImageIndex;

                        if (mq != null)
                        {
                            try
                            {
                                li.SubItems.Add(new ListViewItem.ListViewSubItem(li, GetMessageCount(mq).ToString()));
                            }
                            catch (Exception ex)
                            {
                                li.SubItems.Add(new ListViewItem.ListViewSubItem(li, ex.Message) { ForeColor = Color.Red });
                            }
                        }

                        MessagesList.Items.Add(li);
                    }

                    MessagesList.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    MessagesList.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    if (MessagesList.Columns[1].Width < 60) MessagesList.Columns[1].Width = 60;
                    else if (MessagesList.Columns[1].Width > 500) MessagesList.Columns[1].Width = 500;
                }
                else
                {
                    EnsureNodeLoaded(e.Node);

                    MessagesList.Columns.Add(new ColumnHeader() { Text = "Label" });
                    MessagesList.Columns.Add(new ColumnHeader() { Text = "Size" });

                    foreach (TreeNode n in e.Node.Nodes)
                    {
                        var m = n.Tag as System.Messaging.Message;

                        var li = new ListViewItem();
                        li.Tag = n;
                        li.Text = n.Text;
                        li.ImageIndex = n.ImageIndex;

                        if (m != null)
                        {
                            try
                            {
                                li.SubItems.Add(new ListViewItem.ListViewSubItem(li, m.BodyStream.Length.ToString()));
                            }
                            catch (Exception ex)
                            {
                                li.SubItems.Add(new ListViewItem.ListViewSubItem(li, ex.Message) { ForeColor = Color.Red });
                            }
                        }

                        MessagesList.Items.Add(li);
                    }

                    MessagesList.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    MessagesList.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    if (MessagesList.Columns[1].Width < 60) MessagesList.Columns[1].Width = 60;
                    else if (MessagesList.Columns[1].Width > 500) MessagesList.Columns[1].Width = 500;
                }
            }
            finally
            {
                MessagesList.EndUpdate();
                MessageEditor.IsReadOnly = true;
            }
        }
        #endregion

        #region QueuesTree_BeforeExpand
        private void QueuesTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            EnsureNodeLoaded(e.Node);
        }
        #endregion

        #region MessagesList_DoubleClick
        private void MessagesList_DoubleClick(object sender, EventArgs e)
        {
            if (MessagesList.SelectedItems.Count == 0) return;

            var li = MessagesList.SelectedItems[0];
            var node = li.Tag as TreeNode;
            if (node == null) return;

            QueuesTree.SelectedNode = node;
            QueuesTree.Focus();

        }
        #endregion
    }
}
