namespace CustomSuggest
{
    partial class Form1
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
            TextBox得意先 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuggestTextBox得意先 = new ImeSuggestTextBox();
            label3 = new Label();
            Cmb得意先 = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // TextBox得意先
            // 
            TextBox得意先.Location = new Point(146, 95);
            TextBox得意先.Name = "TextBox得意先";
            TextBox得意先.Size = new Size(147, 23);
            TextBox得意先.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 98);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 1;
            label1.Text = "通常のサジェスト";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 157);
            label2.Name = "label2";
            label2.Size = new Size(99, 15);
            label2.TabIndex = 3;
            label2.Text = "中間一致サジェスト";
            // 
            // SuggestTextBox得意先
            // 
            SuggestTextBox得意先.Location = new Point(146, 136);
            SuggestTextBox得意先.Name = "SuggestTextBox得意先";
            SuggestTextBox得意先.Placeholder = "(名称などで中間一致検索)";
            SuggestTextBox得意先.Size = new Size(286, 191);
            SuggestTextBox得意先.TabIndex = 3;
            SuggestTextBox得意先.TextWidth = 147;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 42);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 6;
            label3.Text = "コンボボックス";
            // 
            // Cmb得意先
            // 
            Cmb得意先.FormattingEnabled = true;
            Cmb得意先.Location = new Point(146, 39);
            Cmb得意先.Name = "Cmb得意先";
            Cmb得意先.Size = new Size(147, 23);
            Cmb得意先.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Red;
            label4.Location = new Point(320, 42);
            label4.Name = "label4";
            label4.Size = new Size(85, 15);
            label4.TabIndex = 8;
            label4.Text = "(F4キーでOPEN)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Red;
            label5.Location = new Point(320, 98);
            label5.Name = "label5";
            label5.Size = new Size(154, 15);
            label5.TabIndex = 9;
            label5.Text = "(前方一致でしか検索できない)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.Red;
            label6.Location = new Point(320, 157);
            label6.Name = "label6";
            label6.Size = new Size(140, 15);
            label6.TabIndex = 10;
            label6.Text = "(中間一致で絞り込み可能)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(Cmb得意先);
            Controls.Add(label3);
            Controls.Add(SuggestTextBox得意先);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TextBox得意先);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TextBox得意先;
        private Label label1;
        private Label label2;
        private ImeSuggestTextBox SuggestTextBox得意先;
        private Label label3;
        private ComboBox Cmb得意先;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}
