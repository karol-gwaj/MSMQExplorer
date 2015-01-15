namespace MSMQExplorer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.QueuesTree = new System.Windows.Forms.TreeView();
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.purgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Images = new System.Windows.Forms.ImageList(this.components);
            this.MessagesList = new System.Windows.Forms.ListView();
            this.MessageEditor = new ScintillaNET.Scintilla();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.SelectedQueuePath = new System.Windows.Forms.ToolStripStatusLabel();
            this.SelectedMessageText = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ServerName = new System.Windows.Forms.ToolStripTextBox();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Help = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.ContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MessageEditor)).BeginInit();
            this.StatusBar.SuspendLayout();
            this.ToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.MessageEditor);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.splitContainer1.Size = new System.Drawing.Size(876, 478);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(4, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.QueuesTree);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.MessagesList);
            this.splitContainer2.Size = new System.Drawing.Size(287, 478);
            this.splitContainer2.SplitterDistance = 300;
            this.splitContainer2.TabIndex = 1;
            // 
            // QueuesTree
            // 
            this.QueuesTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.QueuesTree.ContextMenuStrip = this.ContextMenu;
            this.QueuesTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueuesTree.HideSelection = false;
            this.QueuesTree.ImageIndex = 0;
            this.QueuesTree.ImageList = this.Images;
            this.QueuesTree.Location = new System.Drawing.Point(0, 0);
            this.QueuesTree.Name = "QueuesTree";
            this.QueuesTree.SelectedImageIndex = 0;
            this.QueuesTree.Size = new System.Drawing.Size(287, 300);
            this.QueuesTree.TabIndex = 0;
            this.QueuesTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.QueuesTree_BeforeExpand);
            this.QueuesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.QueuesTree_AfterSelect);
            this.QueuesTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.QueuesTree_MouseDown);
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.purgeToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator2,
            this.createToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.newToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.ContextMenu.Name = "ContextMenu";
            this.ContextMenu.Size = new System.Drawing.Size(153, 208);
            this.ContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
            // 
            // purgeToolStripMenuItem
            // 
            this.purgeToolStripMenuItem.Name = "purgeToolStripMenuItem";
            this.purgeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.purgeToolStripMenuItem.Text = "Purge";
            this.purgeToolStripMenuItem.Click += new System.EventHandler(this.purgeToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // Images
            // 
            this.Images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Images.ImageStream")));
            this.Images.TransparentColor = System.Drawing.Color.Transparent;
            this.Images.Images.SetKeyName(0, "message.png");
            this.Images.Images.SetKeyName(1, "Folder-002.png");
            this.Images.Images.SetKeyName(2, "PrivateQueue-002.png");
            this.Images.Images.SetKeyName(3, "PublicQueue-002.png");
            this.Images.Images.SetKeyName(4, "Messages-002.png");
            this.Images.Images.SetKeyName(5, "JournalMessages-002.png");
            this.Images.Images.SetKeyName(6, "DeadLetterMessages-002.png");
            this.Images.Images.SetKeyName(7, "MessageQueuing-002.png");
            // 
            // MessagesList
            // 
            this.MessagesList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MessagesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessagesList.FullRowSelect = true;
            this.MessagesList.LabelWrap = false;
            this.MessagesList.Location = new System.Drawing.Point(0, 0);
            this.MessagesList.MultiSelect = false;
            this.MessagesList.Name = "MessagesList";
            this.MessagesList.Size = new System.Drawing.Size(287, 174);
            this.MessagesList.SmallImageList = this.Images;
            this.MessagesList.TabIndex = 0;
            this.MessagesList.UseCompatibleStateImageBehavior = false;
            this.MessagesList.View = System.Windows.Forms.View.Details;
            this.MessagesList.SelectedIndexChanged += new System.EventHandler(this.MessagesList_SelectedIndexChanged);
            this.MessagesList.DoubleClick += new System.EventHandler(this.MessagesList_DoubleClick);
            // 
            // MessageEditor
            // 
            this.MessageEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MessageEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageEditor.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageEditor.IsReadOnly = true;
            this.MessageEditor.Location = new System.Drawing.Point(0, 0);
            this.MessageEditor.Name = "MessageEditor";
            this.MessageEditor.Scrolling.ScrollPastEnd = true;
            this.MessageEditor.Size = new System.Drawing.Size(577, 478);
            this.MessageEditor.Styles.BraceBad.FontName = "Verdana\0";
            this.MessageEditor.Styles.BraceBad.Size = 9F;
            this.MessageEditor.Styles.BraceLight.FontName = "Verdana\0";
            this.MessageEditor.Styles.BraceLight.Size = 9F;
            this.MessageEditor.Styles.ControlChar.FontName = "Verdana\0";
            this.MessageEditor.Styles.ControlChar.Size = 9F;
            this.MessageEditor.Styles.Default.BackColor = System.Drawing.SystemColors.Window;
            this.MessageEditor.Styles.Default.FontName = "Verdana\0";
            this.MessageEditor.Styles.Default.Size = 9F;
            this.MessageEditor.Styles.IndentGuide.FontName = "Verdana\0";
            this.MessageEditor.Styles.IndentGuide.Size = 9F;
            this.MessageEditor.Styles.LastPredefined.FontName = "Verdana\0";
            this.MessageEditor.Styles.LastPredefined.Size = 9F;
            this.MessageEditor.Styles.LineNumber.FontName = "Verdana\0";
            this.MessageEditor.Styles.LineNumber.Size = 9F;
            this.MessageEditor.Styles.Max.FontName = "Verdana\0";
            this.MessageEditor.Styles.Max.Size = 9F;
            this.MessageEditor.TabIndex = 0;
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectedQueuePath,
            this.SelectedMessageText,
            this.ProgressBar});
            this.StatusBar.Location = new System.Drawing.Point(0, 503);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(876, 22);
            this.StatusBar.TabIndex = 1;
            this.StatusBar.Text = "statusStrip1";
            // 
            // SelectedQueuePath
            // 
            this.SelectedQueuePath.Image = global::MSMQExplorer.Properties.Resources.Messages_002;
            this.SelectedQueuePath.Margin = new System.Windows.Forms.Padding(4, 3, 0, 2);
            this.SelectedQueuePath.Name = "SelectedQueuePath";
            this.SelectedQueuePath.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.SelectedQueuePath.Size = new System.Drawing.Size(102, 17);
            this.SelectedQueuePath.Text = "SelectedQueue";
            this.SelectedQueuePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectedQueuePath.Visible = false;
            // 
            // SelectedMessageText
            // 
            this.SelectedMessageText.Image = global::MSMQExplorer.Properties.Resources.message;
            this.SelectedMessageText.Margin = new System.Windows.Forms.Padding(6, 3, 0, 2);
            this.SelectedMessageText.Name = "SelectedMessageText";
            this.SelectedMessageText.Size = new System.Drawing.Size(134, 17);
            this.SelectedMessageText.Text = "toolStripStatusLabel1";
            this.SelectedMessageText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectedMessageText.Visible = false;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            this.ProgressBar.Visible = false;
            // 
            // ToolBar
            // 
            this.ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.ServerName,
            this.RefreshButton,
            this.toolStripSeparator1,
            this.Help});
            this.ToolBar.Location = new System.Drawing.Point(0, 0);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.Size = new System.Drawing.Size(876, 25);
            this.ToolBar.TabIndex = 2;
            this.ToolBar.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel1.Text = "Host name:";
            // 
            // ServerName
            // 
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(152, 25);
            this.ServerName.ToolTipText = "Fully qualified host name of target machine.";
            this.ServerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ServerName_KeyPress);
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshButton.Image = global::MSMQExplorer.Properties.Resources.icon_refresh;
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(23, 22);
            this.RefreshButton.Text = "toolStripButton2";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Help
            // 
            this.Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Help.Image = global::MSMQExplorer.Properties.Resources._109_AllAnnotations_Help_16x16_72;
            this.Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(23, 22);
            this.Help.Text = "toolStripButton1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 525);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.StatusBar);
            this.Name = "MainForm";
            this.Text = "MSMQ Explorer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MessageEditor)).EndInit();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ToolBar.ResumeLayout(false);
            this.ToolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView QueuesTree;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStrip ToolBar;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ScintillaNET.Scintilla MessageEditor;
        private System.Windows.Forms.ListView MessagesList;
        private System.Windows.Forms.ToolStripTextBox ServerName;
        private System.Windows.Forms.ToolStripButton RefreshButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ImageList Images;
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem purgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton Help;
        private System.Windows.Forms.ToolStripStatusLabel SelectedQueuePath;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel SelectedMessageText;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    }
}

