namespace SystemProcessesApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView listViewProcesses;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem openDouMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listViewProcesses = new System.Windows.Forms.ListView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openDouMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            // Context menu
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDouMenuItem});
            this.openDouMenuItem.Text = "Open dou.ua";
            this.openDouMenuItem.Click += new System.EventHandler(this.OpenDouMenuItem_Click);

            // ListView
            this.listViewProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewProcesses.View = System.Windows.Forms.View.Details;
            this.listViewProcesses.Columns.Add("Process Name", 200);
            this.listViewProcesses.Columns.Add("PID", 100);
            this.listViewProcesses.ContextMenuStrip = this.contextMenu;

            // Form
            this.ClientSize = new System.Drawing.Size(400, 600);
            this.Controls.Add(this.listViewProcesses);
            this.Text = "System Processes";
            this.Load += new System.EventHandler(this.Form1_Load);
        }
    }
}