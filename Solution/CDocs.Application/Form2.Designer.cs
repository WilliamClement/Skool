namespace CDocs.Application
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fr = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ViolationSource = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.responsible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notifed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.done = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(776, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fr,
            this.ViolationSource,
            this.Number,
            this.responsible,
            this.notifed,
            this.done});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(776, 368);
            this.dataGridView1.TabIndex = 2;
            // 
            // fr
            // 
            this.fr.Frozen = true;
            this.fr.HeaderText = "Отчет";
            this.fr.Name = "fr";
            this.fr.ReadOnly = true;
            this.fr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.fr.Width = 200;
            // 
            // ViolationSource
            // 
            this.ViolationSource.HeaderText = "Источник";
            this.ViolationSource.Name = "ViolationSource";
            this.ViolationSource.ReadOnly = true;
            this.ViolationSource.Width = 200;
            // 
            // Number
            // 
            this.Number.HeaderText = "№";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Width = 50;
            // 
            // responsible
            // 
            this.responsible.HeaderText = "исполнитель";
            this.responsible.Name = "responsible";
            this.responsible.ReadOnly = true;
            // 
            // notifed
            // 
            this.notifed.HeaderText = "оповещен";
            this.notifed.Name = "notifed";
            this.notifed.ReadOnly = true;
            this.notifed.Width = 75;
            // 
            // done
            // 
            this.done.HeaderText = "устранено";
            this.done.Name = "done";
            this.done.ReadOnly = true;
            this.done.Width = 75;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 386);
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(776, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Состояние устранения";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button button1;
        private DataGridView dataGridView1;
        private DataGridViewLinkColumn fr;
        private DataGridViewLinkColumn ViolationSource;
        private DataGridViewTextBoxColumn Number;
        private DataGridViewTextBoxColumn responsible;
        private DataGridViewCheckBoxColumn notifed;
        private DataGridViewCheckBoxColumn done;
        private ProgressBar progressBar1;
    }
}