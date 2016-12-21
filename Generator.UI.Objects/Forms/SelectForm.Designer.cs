namespace Generator.UI.Objects.Forms
{
    partial class SelectForm
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
            if(disposing && (components != null))
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
            this.lvFields = new System.Windows.Forms.ListView();
            this.Campo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvTables = new System.Windows.Forms.ListView();
            this.Tabla = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkFields = new System.Windows.Forms.CheckBox();
            this.chkAllTables = new System.Windows.Forms.CheckBox();
            this.lblListaCampos = new System.Windows.Forms.Label();
            this.lblTablas = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvFields
            // 
            this.lvFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvFields.CheckBoxes = true;
            this.lvFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Campo,
            this.Tipo});
            this.lvFields.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvFields.FullRowSelect = true;
            this.lvFields.GridLines = true;
            this.lvFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvFields.Location = new System.Drawing.Point(12, 294);
            this.lvFields.MultiSelect = false;
            this.lvFields.Name = "lvFields";
            this.lvFields.Size = new System.Drawing.Size(343, 151);
            this.lvFields.TabIndex = 1;
            this.lvFields.UseCompatibleStateImageBehavior = false;
            this.lvFields.View = System.Windows.Forms.View.Details;
            // 
            // Campo
            // 
            this.Campo.Text = "Campo";
            this.Campo.Width = 184;
            // 
            // Tipo
            // 
            this.Tipo.Text = "Tipo";
            this.Tipo.Width = 80;
            // 
            // lvTables
            // 
            this.lvTables.CheckBoxes = true;
            this.lvTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Tabla});
            this.lvTables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvTables.FullRowSelect = true;
            this.lvTables.GridLines = true;
            this.lvTables.Location = new System.Drawing.Point(12, 42);
            this.lvTables.MultiSelect = false;
            this.lvTables.Name = "lvTables";
            this.lvTables.ShowGroups = false;
            this.lvTables.Size = new System.Drawing.Size(343, 194);
            this.lvTables.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvTables.TabIndex = 2;
            this.lvTables.UseCompatibleStateImageBehavior = false;
            this.lvTables.View = System.Windows.Forms.View.Details;
            this.lvTables.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTables_ItemChecked);
            this.lvTables.SelectedIndexChanged += new System.EventHandler(this.lvTables_SelectedIndexChanged);
            // 
            // Tabla
            // 
            this.Tabla.Text = "Tabla";
            this.Tabla.Width = 260;
            // 
            // chkFields
            // 
            this.chkFields.AutoSize = true;
            this.chkFields.Checked = true;
            this.chkFields.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFields.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkFields.Location = new System.Drawing.Point(244, 271);
            this.chkFields.Name = "chkFields";
            this.chkFields.Size = new System.Drawing.Size(111, 17);
            this.chkFields.TabIndex = 18;
            this.chkFields.Text = "Seleccionar todos";
            this.chkFields.UseVisualStyleBackColor = true;
            this.chkFields.CheckedChanged += new System.EventHandler(this.chkFields_CheckedChanged);
            // 
            // chkAllTables
            // 
            this.chkAllTables.AutoSize = true;
            this.chkAllTables.Checked = true;
            this.chkAllTables.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllTables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAllTables.Location = new System.Drawing.Point(244, 19);
            this.chkAllTables.Name = "chkAllTables";
            this.chkAllTables.Size = new System.Drawing.Size(111, 17);
            this.chkAllTables.TabIndex = 17;
            this.chkAllTables.Text = "Seleccionar todas";
            this.chkAllTables.UseVisualStyleBackColor = true;
            this.chkAllTables.CheckedChanged += new System.EventHandler(this.chkAllTables_CheckedChanged);
            // 
            // lblListaCampos
            // 
            this.lblListaCampos.AutoSize = true;
            this.lblListaCampos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListaCampos.Location = new System.Drawing.Point(12, 272);
            this.lblListaCampos.Name = "lblListaCampos";
            this.lblListaCampos.Size = new System.Drawing.Size(99, 13);
            this.lblListaCampos.TabIndex = 20;
            this.lblListaCampos.Text = "Lista de campos";
            // 
            // lblTablas
            // 
            this.lblTablas.AutoSize = true;
            this.lblTablas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTablas.Location = new System.Drawing.Point(12, 20);
            this.lblTablas.Name = "lblTablas";
            this.lblTablas.Size = new System.Drawing.Size(90, 13);
            this.lblTablas.TabIndex = 19;
            this.lblTablas.Text = "Lista de tablas";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(267, 462);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(87, 29);
            this.btnGenerate.TabIndex = 21;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // SelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 503);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblListaCampos);
            this.Controls.Add(this.lblTablas);
            this.Controls.Add(this.chkFields);
            this.Controls.Add(this.chkAllTables);
            this.Controls.Add(this.lvFields);
            this.Controls.Add(this.lvTables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvFields;
        private System.Windows.Forms.ColumnHeader Campo;
        private System.Windows.Forms.ColumnHeader Tipo;
        private System.Windows.Forms.ListView lvTables;
        private System.Windows.Forms.ColumnHeader Tabla;
        private System.Windows.Forms.CheckBox chkFields;
        private System.Windows.Forms.CheckBox chkAllTables;
        private System.Windows.Forms.Label lblListaCampos;
        private System.Windows.Forms.Label lblTablas;
        private System.Windows.Forms.Button btnGenerate;
    }
}