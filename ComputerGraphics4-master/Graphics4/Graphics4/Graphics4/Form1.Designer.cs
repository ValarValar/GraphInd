namespace Graphics4
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.DrawButton = new System.Windows.Forms.Button();
            this.TransformationButton = new System.Windows.Forms.Button();
            this.DefinitionButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(930, 466);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(949, 276);
            this.ResultLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(129, 19);
            this.ResultLabel.TabIndex = 9;
            this.ResultLabel.Text = "Точка не выбрана";
            // 
            // DrawButton
            // 
            this.DrawButton.Location = new System.Drawing.Point(948, 12);
            this.DrawButton.Name = "DrawButton";
            this.DrawButton.Size = new System.Drawing.Size(184, 83);
            this.DrawButton.TabIndex = 12;
            this.DrawButton.Text = "Рисование";
            this.DrawButton.UseVisualStyleBackColor = true;
            this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // TransformationButton
            // 
            this.TransformationButton.Location = new System.Drawing.Point(948, 101);
            this.TransformationButton.Name = "TransformationButton";
            this.TransformationButton.Size = new System.Drawing.Size(184, 83);
            this.TransformationButton.TabIndex = 14;
            this.TransformationButton.Text = "Преобразование";
            this.TransformationButton.UseVisualStyleBackColor = true;
            this.TransformationButton.Click += new System.EventHandler(this.TransformationButton_Click);
            // 
            // DefinitionButton
            // 
            this.DefinitionButton.Location = new System.Drawing.Point(948, 190);
            this.DefinitionButton.Name = "DefinitionButton";
            this.DefinitionButton.Size = new System.Drawing.Size(184, 83);
            this.DefinitionButton.TabIndex = 15;
            this.DefinitionButton.Text = "Определение";
            this.DefinitionButton.UseVisualStyleBackColor = true;
            this.DefinitionButton.Click += new System.EventHandler(this.DefinitionButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Смещение",
            "Поворот примитива",
            "Поворот ребра",
            "Масштабирование",
            "Поиск точки пересечения"});
            this.comboBox1.Location = new System.Drawing.Point(948, 298);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(184, 27);
            this.comboBox1.TabIndex = 16;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 503);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 17;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(16, 554);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 26);
            this.textBox2.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 481);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 19;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 532);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 19);
            this.label2.TabIndex = 20;
            this.label2.Text = "label2";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(12, 586);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(184, 83);
            this.OkButton.TabIndex = 21;
            this.OkButton.Text = "Применить";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.DefinitionButton);
            this.Controls.Add(this.TransformationButton);
            this.Controls.Add(this.DrawButton);
            this.Controls.Add(this.ResultLabel);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.Button DrawButton;
        private System.Windows.Forms.Button TransformationButton;
        private System.Windows.Forms.Button DefinitionButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OkButton;
    }
}

