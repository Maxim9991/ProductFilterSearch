
namespace ProductFiltetSearch
{
    partial class AddNameFilter
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
            this.lblAddTo = new System.Windows.Forms.Label();
            this.cbAddTo = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tbAdd = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAddTo
            // 
            this.lblAddTo.AutoSize = true;
            this.lblAddTo.Location = new System.Drawing.Point(12, 43);
            this.lblAddTo.Name = "lblAddTo";
            this.lblAddTo.Size = new System.Drawing.Size(41, 15);
            this.lblAddTo.TabIndex = 0;
            this.lblAddTo.Text = "AddTo";
            // 
            // cbAddTo
            // 
            this.cbAddTo.FormattingEnabled = true;
            this.cbAddTo.Items.AddRange(new object[] {
            "FilterName",
            "FilterValue"});
            this.cbAddTo.Location = new System.Drawing.Point(74, 40);
            this.cbAddTo.Name = "cbAddTo";
            this.cbAddTo.Size = new System.Drawing.Size(185, 23);
            this.cbAddTo.TabIndex = 1;
            this.cbAddTo.SelectedIndexChanged += new System.EventHandler(this.cbAddTo_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 92);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name";
            // 
            // tbAdd
            // 
            this.tbAdd.Location = new System.Drawing.Point(74, 89);
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(185, 23);
            this.tbAdd.TabIndex = 3;
            this.tbAdd.TextChanged += new System.EventHandler(this.tbAdd_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(74, 132);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 41);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Додати";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // AddNameFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 187);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbAdd);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cbAddTo);
            this.Controls.Add(this.lblAddTo);
            this.Name = "AddNameFilter";
            this.Text = "AddNameFilter";
            this.Load += new System.EventHandler(this.AddNameFilter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddTo;
        private System.Windows.Forms.ComboBox cbAddTo;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbAdd;
        private System.Windows.Forms.Button btnSave;
    }
}