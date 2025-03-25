namespace Fesco_Inventory
{
    partial class InventryItem_AdminSup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventryItem_AdminSup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_back = new Guna.UI2.WinForms.Guna2CircleButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox_searchBy = new System.Windows.Forms.ComboBox();
            this.textbox_search = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Del = new System.Windows.Forms.DataGridViewImageColumn();
            this.EditCol = new System.Windows.Forms.DataGridViewImageColumn();
            this.Stock = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDiscard = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnItem_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit_of_Measure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAddedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_OutofStock = new Guna.UI2.WinForms.Guna2Button();
            this.btn_addItem = new Guna.UI2.WinForms.Guna2Button();
            this.btn_reports = new Guna.UI2.WinForms.Guna2Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel1.Controls.Add(this.btn_back, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_reports, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.16483F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.48351F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.351648F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1111, 455);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btn_back
            // 
            this.btn_back.Animated = true;
            this.btn_back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_back.BackgroundImage")));
            this.btn_back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_back.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_back.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_back.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_back.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_back.FillColor = System.Drawing.Color.Transparent;
            this.btn_back.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_back.ForeColor = System.Drawing.Color.White;
            this.btn_back.Location = new System.Drawing.Point(3, 3);
            this.btn_back.Name = "btn_back";
            this.btn_back.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btn_back.Size = new System.Drawing.Size(44, 39);
            this.btn_back.TabIndex = 3;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.comboBox_searchBy, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textbox_search, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(147, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(816, 62);
            this.tableLayoutPanel2.TabIndex = 58;
            // 
            // comboBox_searchBy
            // 
            this.comboBox_searchBy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.comboBox_searchBy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_searchBy.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_searchBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.comboBox_searchBy.FormattingEnabled = true;
            this.comboBox_searchBy.Items.AddRange(new object[] {
            "Item Number",
            "Item Description"});
            this.comboBox_searchBy.Location = new System.Drawing.Point(6, 8);
            this.comboBox_searchBy.Name = "comboBox_searchBy";
            this.comboBox_searchBy.Size = new System.Drawing.Size(399, 45);
            this.comboBox_searchBy.TabIndex = 58;
            this.comboBox_searchBy.Text = "Search By";
            this.comboBox_searchBy.SelectedValueChanged += new System.EventHandler(this.comboBox_searchBy_SelectedValueChanged);
            // 
            // textbox_search
            // 
            this.textbox_search.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textbox_search.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textbox_search.DefaultText = "Search";
            this.textbox_search.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textbox_search.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textbox_search.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_search.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textbox_search.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_search.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox_search.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textbox_search.Location = new System.Drawing.Point(412, 8);
            this.textbox_search.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_search.Name = "textbox_search";
            this.textbox_search.PasswordChar = '\0';
            this.textbox_search.PlaceholderText = "";
            this.textbox_search.SelectedText = "";
            this.textbox_search.Size = new System.Drawing.Size(397, 45);
            this.textbox_search.TabIndex = 57;
            this.textbox_search.TextChanged += new System.EventHandler(this.textbox_search_TextChanged);
            this.textbox_search.Click += new System.EventHandler(this.textbox_search_Click);
            this.textbox_search.Leave += new System.EventHandler(this.textbox_search_Leave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(147, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 342);
            this.panel1.TabIndex = 61;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Bisque;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Tan;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Tan;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 55;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Del,
            this.EditCol,
            this.Stock,
            this.colDiscard,
            this.ColumnItem_No,
            this.ColumnDesc,
            this.Unit_of_Measure,
            this.ColumnQuantity,
            this.ColumnAddedBy,
            this.AddedOn});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Goldenrod;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.RowTemplate.DividerHeight = 1;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(812, 338);
            this.dataGridView1.TabIndex = 60;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SizeChanged += new System.EventHandler(this.dataGridView1_SizeChanged);
            // 
            // Del
            // 
            this.Del.HeaderText = "Delete";
            this.Del.Image = ((System.Drawing.Image)(resources.GetObject("Del.Image")));
            this.Del.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Del.MinimumWidth = 150;
            this.Del.Name = "Del";
            this.Del.ReadOnly = true;
            this.Del.Width = 150;
            // 
            // EditCol
            // 
            this.EditCol.HeaderText = "Edit";
            this.EditCol.Image = ((System.Drawing.Image)(resources.GetObject("EditCol.Image")));
            this.EditCol.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.EditCol.MinimumWidth = 150;
            this.EditCol.Name = "EditCol";
            this.EditCol.ReadOnly = true;
            this.EditCol.Width = 150;
            // 
            // Stock
            // 
            this.Stock.HeaderText = "Increase Stock";
            this.Stock.Image = ((System.Drawing.Image)(resources.GetObject("Stock.Image")));
            this.Stock.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Stock.MinimumWidth = 150;
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Width = 150;
            // 
            // colDiscard
            // 
            this.colDiscard.HeaderText = "Discard";
            this.colDiscard.Image = ((System.Drawing.Image)(resources.GetObject("colDiscard.Image")));
            this.colDiscard.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colDiscard.MinimumWidth = 150;
            this.colDiscard.Name = "colDiscard";
            this.colDiscard.ReadOnly = true;
            this.colDiscard.Width = 150;
            // 
            // ColumnItem_No
            // 
            this.ColumnItem_No.DataPropertyName = "ItemNo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnItem_No.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnItem_No.HeaderText = "Item#";
            this.ColumnItem_No.MinimumWidth = 150;
            this.ColumnItem_No.Name = "ColumnItem_No";
            this.ColumnItem_No.ReadOnly = true;
            this.ColumnItem_No.Width = 150;
            // 
            // ColumnDesc
            // 
            this.ColumnDesc.DataPropertyName = "Item_Description";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnDesc.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnDesc.HeaderText = "Description";
            this.ColumnDesc.MinimumWidth = 150;
            this.ColumnDesc.Name = "ColumnDesc";
            this.ColumnDesc.ReadOnly = true;
            this.ColumnDesc.Width = 170;
            // 
            // Unit_of_Measure
            // 
            this.Unit_of_Measure.DataPropertyName = "Unit_of_Measure";
            this.Unit_of_Measure.HeaderText = "Unit";
            this.Unit_of_Measure.MinimumWidth = 150;
            this.Unit_of_Measure.Name = "Unit_of_Measure";
            this.Unit_of_Measure.ReadOnly = true;
            this.Unit_of_Measure.Width = 170;
            // 
            // ColumnQuantity
            // 
            this.ColumnQuantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnQuantity.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnQuantity.HeaderText = "Quantity";
            this.ColumnQuantity.MinimumWidth = 150;
            this.ColumnQuantity.Name = "ColumnQuantity";
            this.ColumnQuantity.ReadOnly = true;
            this.ColumnQuantity.Width = 150;
            // 
            // ColumnAddedBy
            // 
            this.ColumnAddedBy.DataPropertyName = "Added_By";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnAddedBy.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnAddedBy.HeaderText = "Added By";
            this.ColumnAddedBy.MinimumWidth = 150;
            this.ColumnAddedBy.Name = "ColumnAddedBy";
            this.ColumnAddedBy.ReadOnly = true;
            this.ColumnAddedBy.Width = 150;
            // 
            // AddedOn
            // 
            this.AddedOn.DataPropertyName = "Added_On";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AddedOn.DefaultCellStyle = dataGridViewCellStyle6;
            this.AddedOn.HeaderText = "Added On";
            this.AddedOn.MinimumWidth = 150;
            this.AddedOn.Name = "AddedOn";
            this.AddedOn.ReadOnly = true;
            this.AddedOn.Width = 150;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.btn_OutofStock, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btn_addItem, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 71);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(138, 342);
            this.tableLayoutPanel3.TabIndex = 62;
            // 
            // btn_OutofStock
            // 
            this.btn_OutofStock.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_OutofStock.Animated = true;
            this.btn_OutofStock.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_OutofStock.BorderRadius = 25;
            this.btn_OutofStock.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.btn_OutofStock.BorderThickness = 4;
            this.btn_OutofStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_OutofStock.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_OutofStock.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_OutofStock.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_OutofStock.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_OutofStock.FillColor = System.Drawing.Color.Goldenrod;
            this.btn_OutofStock.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OutofStock.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_OutofStock.Image = ((System.Drawing.Image)(resources.GetObject("btn_OutofStock.Image")));
            this.btn_OutofStock.ImageOffset = new System.Drawing.Point(22, -13);
            this.btn_OutofStock.ImageSize = new System.Drawing.Size(65, 65);
            this.btn_OutofStock.Location = new System.Drawing.Point(3, 174);
            this.btn_OutofStock.Name = "btn_OutofStock";
            this.btn_OutofStock.Size = new System.Drawing.Size(132, 164);
            this.btn_OutofStock.TabIndex = 55;
            this.btn_OutofStock.Text = "Stockouts";
            this.btn_OutofStock.TextOffset = new System.Drawing.Point(-17, 40);
            this.btn_OutofStock.Click += new System.EventHandler(this.btn_OutofStock_Click);
            // 
            // btn_addItem
            // 
            this.btn_addItem.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_addItem.Animated = true;
            this.btn_addItem.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_addItem.BorderRadius = 25;
            this.btn_addItem.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.btn_addItem.BorderThickness = 4;
            this.btn_addItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_addItem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_addItem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_addItem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_addItem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_addItem.FillColor = System.Drawing.Color.Goldenrod;
            this.btn_addItem.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addItem.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_addItem.Image = ((System.Drawing.Image)(resources.GetObject("btn_addItem.Image")));
            this.btn_addItem.ImageOffset = new System.Drawing.Point(20, -13);
            this.btn_addItem.ImageSize = new System.Drawing.Size(68, 68);
            this.btn_addItem.Location = new System.Drawing.Point(3, 3);
            this.btn_addItem.Name = "btn_addItem";
            this.btn_addItem.Size = new System.Drawing.Size(132, 165);
            this.btn_addItem.TabIndex = 54;
            this.btn_addItem.Text = "Add Item";
            this.btn_addItem.TextOffset = new System.Drawing.Point(-18, 40);
            this.btn_addItem.Click += new System.EventHandler(this.btn_addItem_Click);
            // 
            // btn_reports
            // 
            this.btn_reports.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_reports.Animated = true;
            this.btn_reports.AutoRoundedCorners = true;
            this.btn_reports.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_reports.BackgroundImage")));
            this.btn_reports.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_reports.BorderRadius = 30;
            this.btn_reports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_reports.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_reports.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_reports.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_reports.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_reports.FillColor = System.Drawing.Color.Transparent;
            this.btn_reports.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_reports.ForeColor = System.Drawing.Color.White;
            this.btn_reports.IndicateFocus = true;
            this.btn_reports.Location = new System.Drawing.Point(1043, 3);
            this.btn_reports.Name = "btn_reports";
            this.btn_reports.Size = new System.Drawing.Size(65, 62);
            this.btn_reports.TabIndex = 63;
            this.btn_reports.Click += new System.EventHandler(this.btn_reports_Click);
            // 
            // InventryItem_AdminSup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "InventryItem_AdminSup";
            this.Size = new System.Drawing.Size(1111, 455);
            this.Load += new System.EventHandler(this.InventryItem_AdminSup_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2CircleButton btn_back;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox comboBox_searchBy;
        private Guna.UI2.WinForms.Guna2TextBox textbox_search;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button btn_addItem;
        private Guna.UI2.WinForms.Guna2Button btn_OutofStock;
        private System.Windows.Forms.DataGridViewImageColumn Del;
        private System.Windows.Forms.DataGridViewImageColumn EditCol;
        private System.Windows.Forms.DataGridViewImageColumn Stock;
        private System.Windows.Forms.DataGridViewImageColumn colDiscard;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItem_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit_of_Measure;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddedOn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Guna.UI2.WinForms.Guna2Button btn_reports;
    }
}
