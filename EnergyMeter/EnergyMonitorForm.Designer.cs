namespace EnergyMonitor
{
    partial class EnergyMonitorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxReadings = new System.Windows.Forms.ListBox();
            this.listBoxPrices = new System.Windows.Forms.ListBox();
            this.buttonAddReading = new System.Windows.Forms.Button();
            this.buttonEditReading = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRemoveReading = new System.Windows.Forms.Button();
            this.buttonRemovePrice = new System.Windows.Forms.Button();
            this.buttonEditPrice = new System.Windows.Forms.Button();
            this.buttonNewPrice = new System.Windows.Forms.Button();
            this.buttonRemoveTax = new System.Windows.Forms.Button();
            this.buttonEditTax = new System.Windows.Forms.Button();
            this.buttonNewTax = new System.Windows.Forms.Button();
            this.listBoxTaxes = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxReadings
            // 
            this.listBoxReadings.FormattingEnabled = true;
            this.listBoxReadings.ItemHeight = 15;
            this.listBoxReadings.Location = new System.Drawing.Point(12, 37);
            this.listBoxReadings.Name = "listBoxReadings";
            this.listBoxReadings.Size = new System.Drawing.Size(268, 199);
            this.listBoxReadings.TabIndex = 0;
            // 
            // listBoxPrices
            // 
            this.listBoxPrices.FormattingEnabled = true;
            this.listBoxPrices.ItemHeight = 15;
            this.listBoxPrices.Location = new System.Drawing.Point(286, 37);
            this.listBoxPrices.Name = "listBoxPrices";
            this.listBoxPrices.Size = new System.Drawing.Size(268, 199);
            this.listBoxPrices.TabIndex = 1;
            // 
            // buttonAddReading
            // 
            this.buttonAddReading.Location = new System.Drawing.Point(166, 242);
            this.buttonAddReading.Name = "buttonAddReading";
            this.buttonAddReading.Size = new System.Drawing.Size(114, 43);
            this.buttonAddReading.TabIndex = 2;
            this.buttonAddReading.Text = "Nový odečet";
            this.buttonAddReading.UseVisualStyleBackColor = true;
            this.buttonAddReading.Click += new System.EventHandler(this.ButtonAddReading_Click);
            // 
            // buttonEditReading
            // 
            this.buttonEditReading.Location = new System.Drawing.Point(89, 242);
            this.buttonEditReading.MaximumSize = new System.Drawing.Size(300, 0);
            this.buttonEditReading.Name = "buttonEditReading";
            this.buttonEditReading.Size = new System.Drawing.Size(71, 43);
            this.buttonEditReading.TabIndex = 3;
            this.buttonEditReading.Text = "Upravit odečet";
            this.buttonEditReading.UseVisualStyleBackColor = true;
            this.buttonEditReading.Click += new System.EventHandler(this.ButtonEditReading_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "ODEČTY";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(286, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(268, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "CENÍK";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonRemoveReading
            // 
            this.buttonRemoveReading.Location = new System.Drawing.Point(12, 242);
            this.buttonRemoveReading.MaximumSize = new System.Drawing.Size(300, 0);
            this.buttonRemoveReading.Name = "buttonRemoveReading";
            this.buttonRemoveReading.Size = new System.Drawing.Size(71, 43);
            this.buttonRemoveReading.TabIndex = 8;
            this.buttonRemoveReading.Text = "Odebrat odečet";
            this.buttonRemoveReading.UseVisualStyleBackColor = true;
            this.buttonRemoveReading.Click += new System.EventHandler(this.ButtonRemoveReading_Click);
            // 
            // buttonRemovePrice
            // 
            this.buttonRemovePrice.Location = new System.Drawing.Point(286, 242);
            this.buttonRemovePrice.MaximumSize = new System.Drawing.Size(300, 0);
            this.buttonRemovePrice.Name = "buttonRemovePrice";
            this.buttonRemovePrice.Size = new System.Drawing.Size(71, 43);
            this.buttonRemovePrice.TabIndex = 11;
            this.buttonRemovePrice.Text = "Odebrat cenu";
            this.buttonRemovePrice.UseVisualStyleBackColor = true;
            this.buttonRemovePrice.Click += new System.EventHandler(this.ButtonRemovePrice_Click);
            // 
            // buttonEditPrice
            // 
            this.buttonEditPrice.Location = new System.Drawing.Point(363, 242);
            this.buttonEditPrice.MaximumSize = new System.Drawing.Size(300, 0);
            this.buttonEditPrice.Name = "buttonEditPrice";
            this.buttonEditPrice.Size = new System.Drawing.Size(71, 43);
            this.buttonEditPrice.TabIndex = 10;
            this.buttonEditPrice.Text = "Upravit cenu";
            this.buttonEditPrice.UseVisualStyleBackColor = true;
            this.buttonEditPrice.Click += new System.EventHandler(this.ButtonEditPrice_Click);
            // 
            // buttonNewPrice
            // 
            this.buttonNewPrice.Location = new System.Drawing.Point(440, 242);
            this.buttonNewPrice.Name = "buttonNewPrice";
            this.buttonNewPrice.Size = new System.Drawing.Size(114, 43);
            this.buttonNewPrice.TabIndex = 9;
            this.buttonNewPrice.Text = "Nová cena";
            this.buttonNewPrice.UseVisualStyleBackColor = true;
            this.buttonNewPrice.Click += new System.EventHandler(this.ButtonNewPrice_Click);
            // 
            // buttonRemoveTax
            // 
            this.buttonRemoveTax.Location = new System.Drawing.Point(560, 242);
            this.buttonRemoveTax.MaximumSize = new System.Drawing.Size(300, 0);
            this.buttonRemoveTax.Name = "buttonRemoveTax";
            this.buttonRemoveTax.Size = new System.Drawing.Size(71, 43);
            this.buttonRemoveTax.TabIndex = 14;
            this.buttonRemoveTax.Text = "Odebrat poplatek";
            this.buttonRemoveTax.UseVisualStyleBackColor = true;
            this.buttonRemoveTax.Click += new System.EventHandler(this.ButtonRemoveTax_Click);
            // 
            // buttonEditTax
            // 
            this.buttonEditTax.Location = new System.Drawing.Point(637, 242);
            this.buttonEditTax.MaximumSize = new System.Drawing.Size(300, 0);
            this.buttonEditTax.Name = "buttonEditTax";
            this.buttonEditTax.Size = new System.Drawing.Size(71, 43);
            this.buttonEditTax.TabIndex = 13;
            this.buttonEditTax.Text = "Upravit poplatek";
            this.buttonEditTax.UseVisualStyleBackColor = true;
            this.buttonEditTax.Click += new System.EventHandler(this.ButtonEditTax_Click);
            // 
            // buttonNewTax
            // 
            this.buttonNewTax.Location = new System.Drawing.Point(814, 242);
            this.buttonNewTax.Name = "buttonNewTax";
            this.buttonNewTax.Size = new System.Drawing.Size(114, 43);
            this.buttonNewTax.TabIndex = 12;
            this.buttonNewTax.Text = "Nový poplatek";
            this.buttonNewTax.UseVisualStyleBackColor = true;
            this.buttonNewTax.Click += new System.EventHandler(this.ButtonNewTax_Click);
            // 
            // listBoxTaxes
            // 
            this.listBoxTaxes.FormattingEnabled = true;
            this.listBoxTaxes.ItemHeight = 15;
            this.listBoxTaxes.Location = new System.Drawing.Point(560, 37);
            this.listBoxTaxes.Name = "listBoxTaxes";
            this.listBoxTaxes.Size = new System.Drawing.Size(368, 199);
            this.listBoxTaxes.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(560, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(368, 23);
            this.label3.TabIndex = 16;
            this.label3.Text = "POPLATKY";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(814, 638);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 28);
            this.buttonSave.TabIndex = 17;
            this.buttonSave.Text = "Uložit";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(694, 638);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(114, 28);
            this.buttonLoad.TabIndex = 18;
            this.buttonLoad.Text = "Načíst";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // EnergyMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 678);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxTaxes);
            this.Controls.Add(this.buttonRemoveTax);
            this.Controls.Add(this.buttonEditTax);
            this.Controls.Add(this.buttonNewTax);
            this.Controls.Add(this.buttonRemovePrice);
            this.Controls.Add(this.buttonEditPrice);
            this.Controls.Add(this.buttonNewPrice);
            this.Controls.Add(this.buttonRemoveReading);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonEditReading);
            this.Controls.Add(this.buttonAddReading);
            this.Controls.Add(this.listBoxPrices);
            this.Controls.Add(this.listBoxReadings);
            this.MaximizeBox = false;
            this.Name = "EnergyMonitorForm";
            this.Text = "Energy Monitor";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listBoxReadings;
        private ListBox listBoxPrices;
        private Button buttonAddReading;
        private Button buttonEditReading;
        private Label label1;
        private Label label2;
        private Button buttonRemoveReading;
        private Button buttonRemovePrice;
        private Button buttonEditPrice;
        private Button buttonNewPrice;
        private Button buttonRemoveTax;
        private Button buttonEditTax;
        private Button buttonNewTax;
        private ListBox listBoxTaxes;
        private Label label3;
        private Button buttonSave;
        private Button buttonLoad;
    }
}