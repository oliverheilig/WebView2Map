namespace WebView2Map
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.waypointGrid = new System.Windows.Forms.DataGridView();
            this.routePanel = new System.Windows.Forms.Panel();
            this.getRouteButton = new System.Windows.Forms.Button();
            this.travelTimeLabel = new System.Windows.Forms.Label();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.latitudeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longitudeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waypointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waypointGrid)).BeginInit();
            this.routePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waypointBindingSource)).BeginInit();
            this.SuspendLayout();
            //
            // splitContainer
            //
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            //
            // splitContainer.Panel1
            //
            this.splitContainer.Panel1.Controls.Add(this.waypointGrid);
            this.splitContainer.Panel1.Controls.Add(this.routePanel);
            //
            // splitContainer.Panel2
            //
            this.splitContainer.Panel2.Controls.Add(this.webView);
            this.splitContainer.Size = new System.Drawing.Size(1054, 450);
            this.splitContainer.SplitterDistance = 310;
            this.splitContainer.TabIndex = 0;
            //
            // waypointGrid
            //
            this.waypointGrid.AutoGenerateColumns = false;
            this.waypointGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.waypointGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.latitudeColumn,
            this.longitudeColumn});
            this.waypointGrid.DataSource = this.waypointBindingSource;
            this.waypointGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waypointGrid.Location = new System.Drawing.Point(0, 100);
            this.waypointGrid.Name = "waypointGrid";
            this.waypointGrid.RowHeadersWidth = 51;
            this.waypointGrid.RowTemplate.Height = 24;
            this.waypointGrid.Size = new System.Drawing.Size(310, 350);
            this.waypointGrid.TabIndex = 1;
            //
            // routePanel
            //
            this.routePanel.Controls.Add(this.getRouteButton);
            this.routePanel.Controls.Add(this.travelTimeLabel);
            this.routePanel.Controls.Add(this.distanceLabel);
            this.routePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.routePanel.Location = new System.Drawing.Point(0, 0);
            this.routePanel.Name = "routePanel";
            this.routePanel.Size = new System.Drawing.Size(310, 100);
            this.routePanel.TabIndex = 4;
            //
            // getRouteButton
            //
            this.getRouteButton.Enabled = false;
            this.getRouteButton.Location = new System.Drawing.Point(100, 74);
            this.getRouteButton.Name = "getRouteButton";
            this.getRouteButton.Size = new System.Drawing.Size(97, 23);
            this.getRouteButton.TabIndex = 0;
            this.getRouteButton.Text = "Get Route";
            this.getRouteButton.UseVisualStyleBackColor = true;
            this.getRouteButton.Click += new System.EventHandler(this.getRouteButton_Click);
            //
            // travelTimeLabel
            //
            this.travelTimeLabel.AutoSize = true;
            this.travelTimeLabel.Location = new System.Drawing.Point(17, 12);
            this.travelTimeLabel.Name = "travelTimeLabel";
            this.travelTimeLabel.Size = new System.Drawing.Size(83, 16);
            this.travelTimeLabel.TabIndex = 2;
            this.travelTimeLabel.Text = "Travel Time:";
            //
            // distanceLabel
            //
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Location = new System.Drawing.Point(17, 40);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(63, 16);
            this.distanceLabel.TabIndex = 3;
            this.distanceLabel.Text = "Distance:";
            //
            // webView
            //
            this.webView.AllowExternalDrop = true;
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(0, 0);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(740, 450);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            this.webView.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.webView_NavigationCompleted);
            //
            // latitudeColumn
            //
            this.latitudeColumn.DataPropertyName = "Latitude";
            this.latitudeColumn.HeaderText = "Latitude";
            this.latitudeColumn.MinimumWidth = 6;
            this.latitudeColumn.Name = "latitudeColumn";
            this.latitudeColumn.Width = 125;
            //
            // longitudeColumn
            //
            this.longitudeColumn.DataPropertyName = "Longitude";
            this.longitudeColumn.HeaderText = "Longitude";
            this.longitudeColumn.MinimumWidth = 6;
            this.longitudeColumn.Name = "longitudeColumn";
            this.longitudeColumn.Width = 125;
            //
            // waypointBindingSource
            //
            this.waypointBindingSource.DataSource = typeof(WebView2Map.Waypoint);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 450);
            this.Controls.Add(this.splitContainer);
            this.Name = "MainForm";
            this.Text = "WebView2Map";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.waypointGrid)).EndInit();
            this.routePanel.ResumeLayout(false);
            this.routePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waypointBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private System.Windows.Forms.Button getRouteButton;
        private System.Windows.Forms.DataGridView waypointGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn latitudeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longitudeColumn;
        private System.Windows.Forms.BindingSource waypointBindingSource;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.Label travelTimeLabel;
        private System.Windows.Forms.Panel routePanel;
    }
}

