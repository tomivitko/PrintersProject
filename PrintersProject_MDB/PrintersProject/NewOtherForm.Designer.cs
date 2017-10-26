namespace PrintersProject
{
    partial class NewOtherForm
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
            this.radioButtonDepartment = new System.Windows.Forms.RadioButton();
            this.radioButtonManufacturer = new System.Windows.Forms.RadioButton();
            this.radioButtonSupplie = new System.Windows.Forms.RadioButton();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radioButtonDepartment
            // 
            this.radioButtonDepartment.AutoSize = true;
            this.radioButtonDepartment.Location = new System.Drawing.Point(12, 12);
            this.radioButtonDepartment.Name = "radioButtonDepartment";
            this.radioButtonDepartment.Size = new System.Drawing.Size(49, 17);
            this.radioButtonDepartment.TabIndex = 0;
            this.radioButtonDepartment.TabStop = true;
            this.radioButtonDepartment.Text = "Odjel";
            this.radioButtonDepartment.UseVisualStyleBackColor = true;
            // 
            // radioButtonManufacturer
            // 
            this.radioButtonManufacturer.AutoSize = true;
            this.radioButtonManufacturer.Location = new System.Drawing.Point(12, 35);
            this.radioButtonManufacturer.Name = "radioButtonManufacturer";
            this.radioButtonManufacturer.Size = new System.Drawing.Size(79, 17);
            this.radioButtonManufacturer.TabIndex = 1;
            this.radioButtonManufacturer.TabStop = true;
            this.radioButtonManufacturer.Text = "Proizvođač";
            this.radioButtonManufacturer.UseVisualStyleBackColor = true;
            // 
            // radioButtonSupplie
            // 
            this.radioButtonSupplie.AutoSize = true;
            this.radioButtonSupplie.Location = new System.Drawing.Point(12, 58);
            this.radioButtonSupplie.Name = "radioButtonSupplie";
            this.radioButtonSupplie.Size = new System.Drawing.Size(64, 17);
            this.radioButtonSupplie.TabIndex = 2;
            this.radioButtonSupplie.TabStop = true;
            this.radioButtonSupplie.Text = "Materijal";
            this.radioButtonSupplie.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(82, 83);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 3;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 109);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Spremi";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(107, 109);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Odustani";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Naziv:";
            // 
            // NewOtherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 146);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.radioButtonSupplie);
            this.Controls.Add(this.radioButtonManufacturer);
            this.Controls.Add(this.radioButtonDepartment);
            this.Name = "NewOtherForm";
            this.Text = "Dodaj ostalo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonDepartment;
        private System.Windows.Forms.RadioButton radioButtonManufacturer;
        private System.Windows.Forms.RadioButton radioButtonSupplie;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
    }
}