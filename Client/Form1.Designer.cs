namespace WinSrvMonitor.Client
{
    partial class Form1
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
            this.metricViewItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnServer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCpu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCpuAvg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnMemory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnMemoryAvg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnRequestsPerSec = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnRequestsPerSecAvg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPackagesPerSec = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPackagesPerSecAvg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnBytesPerSec = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnBytesPerSecAvg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnError = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnActiveConnections = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnActiveConnectionsAvg = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.metricViewItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Location = new System.Drawing.Point(558, 420);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 25);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(13, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1132, 403);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnGroup,
            this.gridColumnServer,
            this.gridColumnCpu,
            this.gridColumnCpuAvg,
            this.gridColumnMemory,
            this.gridColumnMemoryAvg,
            this.gridColumnRequestsPerSec,
            this.gridColumnRequestsPerSecAvg,
            this.gridColumnPackagesPerSec,
            this.gridColumnPackagesPerSecAvg,
            this.gridColumnBytesPerSec,
            this.gridColumnBytesPerSecAvg,
            this.gridColumnActiveConnections,
            this.gridColumnActiveConnectionsAvg,
            this.gridColumnError});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Average, "Cpu.Value", this.gridColumnCpu, "{0:n2} %"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Average, "Cpu.MovingAverage", this.gridColumnCpuAvg, "{0:n2} %"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Average, "Memory.Value", this.gridColumnMemory, "{0:n2} %"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Average, "Memory.MovingAverage", this.gridColumnMemoryAvg, "{0:n2} %"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RequestsPerSec.Value", this.gridColumnRequestsPerSec, "{0:n0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RequestsPerSec.MovingAverage", this.gridColumnRequestsPerSecAvg, "{0:n0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PackagesPerSec.Value", this.gridColumnPackagesPerSec, "{0:n0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PackagesPerSec.MovingAverage", this.gridColumnPackagesPerSecAvg, "{0:n0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BytesPerSec.Value", this.gridColumnBytesPerSec, "{0:n0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BytesPerSec.MovingAverage", this.gridColumnBytesPerSecAvg, "{0:n0}")});
            this.gridView1.Name = "gridView1";
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnGroup, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            // 
            // gridColumnGroup
            // 
            this.gridColumnGroup.Caption = "Group";
            this.gridColumnGroup.FieldName = "Group";
            this.gridColumnGroup.Name = "gridColumnGroup";
            this.gridColumnGroup.Visible = true;
            this.gridColumnGroup.VisibleIndex = 0;
            // 
            // gridColumnServer
            // 
            this.gridColumnServer.Caption = "Server";
            this.gridColumnServer.FieldName = "Name";
            this.gridColumnServer.Name = "gridColumnServer";
            this.gridColumnServer.Visible = true;
            this.gridColumnServer.VisibleIndex = 0;
            this.gridColumnServer.Width = 150;
            // 
            // gridColumnCpu
            // 
            this.gridColumnCpu.Caption = "Cpu";
            this.gridColumnCpu.DisplayFormat.FormatString = "n2";
            this.gridColumnCpu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnCpu.FieldName = "Cpu.Value";
            this.gridColumnCpu.Name = "gridColumnCpu";
            this.gridColumnCpu.Visible = true;
            this.gridColumnCpu.VisibleIndex = 1;
            this.gridColumnCpu.Width = 70;
            // 
            // gridColumnCpuAvg
            // 
            this.gridColumnCpuAvg.Caption = "Cpu Avg.";
            this.gridColumnCpuAvg.DisplayFormat.FormatString = "n2";
            this.gridColumnCpuAvg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnCpuAvg.FieldName = "Cpu.MovingAverage";
            this.gridColumnCpuAvg.Name = "gridColumnCpuAvg";
            this.gridColumnCpuAvg.Visible = true;
            this.gridColumnCpuAvg.VisibleIndex = 2;
            this.gridColumnCpuAvg.Width = 70;
            // 
            // gridColumnMemory
            // 
            this.gridColumnMemory.Caption = "Memory";
            this.gridColumnMemory.DisplayFormat.FormatString = "n2";
            this.gridColumnMemory.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnMemory.FieldName = "Memory.Value";
            this.gridColumnMemory.Name = "gridColumnMemory";
            this.gridColumnMemory.Visible = true;
            this.gridColumnMemory.VisibleIndex = 3;
            this.gridColumnMemory.Width = 70;
            // 
            // gridColumnMemoryAvg
            // 
            this.gridColumnMemoryAvg.Caption = "Memory Avg.";
            this.gridColumnMemoryAvg.DisplayFormat.FormatString = "n2";
            this.gridColumnMemoryAvg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnMemoryAvg.FieldName = "Memory.MovingAverage";
            this.gridColumnMemoryAvg.Name = "gridColumnMemoryAvg";
            this.gridColumnMemoryAvg.Visible = true;
            this.gridColumnMemoryAvg.VisibleIndex = 4;
            this.gridColumnMemoryAvg.Width = 70;
            // 
            // gridColumnRequestsPerSec
            // 
            this.gridColumnRequestsPerSec.Caption = "Requests / sec";
            this.gridColumnRequestsPerSec.DisplayFormat.FormatString = "n0";
            this.gridColumnRequestsPerSec.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnRequestsPerSec.FieldName = "RequestsPerSec.Value";
            this.gridColumnRequestsPerSec.Name = "gridColumnRequestsPerSec";
            this.gridColumnRequestsPerSec.Visible = true;
            this.gridColumnRequestsPerSec.VisibleIndex = 5;
            this.gridColumnRequestsPerSec.Width = 118;
            // 
            // gridColumnRequestsPerSecAvg
            // 
            this.gridColumnRequestsPerSecAvg.Caption = "Requests / sec Avg.";
            this.gridColumnRequestsPerSecAvg.DisplayFormat.FormatString = "n0";
            this.gridColumnRequestsPerSecAvg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnRequestsPerSecAvg.FieldName = "RequestsPerSec.MovingAverage";
            this.gridColumnRequestsPerSecAvg.Name = "gridColumnRequestsPerSecAvg";
            this.gridColumnRequestsPerSecAvg.Visible = true;
            this.gridColumnRequestsPerSecAvg.VisibleIndex = 6;
            this.gridColumnRequestsPerSecAvg.Width = 118;
            // 
            // gridColumnPackagesPerSec
            // 
            this.gridColumnPackagesPerSec.Caption = "Packages / sec";
            this.gridColumnPackagesPerSec.DisplayFormat.FormatString = "n0";
            this.gridColumnPackagesPerSec.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnPackagesPerSec.FieldName = "PackagesPerSec.Value";
            this.gridColumnPackagesPerSec.Name = "gridColumnPackagesPerSec";
            this.gridColumnPackagesPerSec.Visible = true;
            this.gridColumnPackagesPerSec.VisibleIndex = 7;
            this.gridColumnPackagesPerSec.Width = 118;
            // 
            // gridColumnPackagesPerSecAvg
            // 
            this.gridColumnPackagesPerSecAvg.Caption = "Packages / sec Avg";
            this.gridColumnPackagesPerSecAvg.DisplayFormat.FormatString = "n0";
            this.gridColumnPackagesPerSecAvg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnPackagesPerSecAvg.FieldName = "PackagesPerSec.MovingAverage";
            this.gridColumnPackagesPerSecAvg.Name = "gridColumnPackagesPerSecAvg";
            this.gridColumnPackagesPerSecAvg.Visible = true;
            this.gridColumnPackagesPerSecAvg.VisibleIndex = 8;
            this.gridColumnPackagesPerSecAvg.Width = 118;
            // 
            // gridColumnBytesPerSec
            // 
            this.gridColumnBytesPerSec.Caption = "Mbit/s";
            this.gridColumnBytesPerSec.DisplayFormat.FormatString = "n0";
            this.gridColumnBytesPerSec.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnBytesPerSec.FieldName = "BytesPerSec.Value";
            this.gridColumnBytesPerSec.Name = "gridColumnBytesPerSec";
            this.gridColumnBytesPerSec.Visible = true;
            this.gridColumnBytesPerSec.VisibleIndex = 9;
            // 
            // gridColumnBytesPerSecAvg
            // 
            this.gridColumnBytesPerSecAvg.Caption = "Mbit/s Avg.";
            this.gridColumnBytesPerSecAvg.DisplayFormat.FormatString = "n0";
            this.gridColumnBytesPerSecAvg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnBytesPerSecAvg.FieldName = "BytesPerSec.MovingAverage";
            this.gridColumnBytesPerSecAvg.Name = "gridColumnBytesPerSecAvg";
            this.gridColumnBytesPerSecAvg.Visible = true;
            this.gridColumnBytesPerSecAvg.VisibleIndex = 10;
            // 
            // gridColumnError
            // 
            this.gridColumnError.Caption = "Error";
            this.gridColumnError.FieldName = "Error";
            this.gridColumnError.Name = "gridColumnError";
            this.gridColumnError.Visible = true;
            this.gridColumnError.VisibleIndex = 13;
            this.gridColumnError.Width = 139;
            // 
            // gridColumnActiveConnections
            // 
            this.gridColumnActiveConnections.Caption = "Active conns";
            this.gridColumnActiveConnections.DisplayFormat.FormatString = "Numeric \"n0\"";
            this.gridColumnActiveConnections.FieldName = "ActiveConnections.Value";
            this.gridColumnActiveConnections.Name = "gridColumnActiveConnections";
            this.gridColumnActiveConnections.Visible = true;
            this.gridColumnActiveConnections.VisibleIndex = 11;
            // 
            // gridColumnActiveConnectionsAvg
            // 
            this.gridColumnActiveConnectionsAvg.Caption = "Active conns Avg.";
            this.gridColumnActiveConnectionsAvg.DisplayFormat.FormatString = "Numeric \"n0\"";
            this.gridColumnActiveConnectionsAvg.FieldName = "ActiveConnections.MovingAverage";
            this.gridColumnActiveConnectionsAvg.Name = "gridColumnActiveConnectionsAvg";
            this.gridColumnActiveConnectionsAvg.Visible = true;
            this.gridColumnActiveConnectionsAvg.VisibleIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 455);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btnClose);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Windows Server Monitor Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metricViewItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource metricViewItemBindingSource;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn metricDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnServer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCpu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCpuAvg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnMemory;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnMemoryAvg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnError;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRequestsPerSec;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRequestsPerSecAvg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPackagesPerSec;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPackagesPerSecAvg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBytesPerSec;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBytesPerSecAvg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnActiveConnections;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnActiveConnectionsAvg;
    }
}

