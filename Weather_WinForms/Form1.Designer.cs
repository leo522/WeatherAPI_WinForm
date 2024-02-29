namespace Weather_WinForms
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
            getData_btn = new Button();
            lbl_DateTime = new Label();
            listView_Weather = new ListView();
            SuspendLayout();
            // 
            // getData_btn
            // 
            getData_btn.Location = new Point(613, 332);
            getData_btn.Name = "getData_btn";
            getData_btn.Size = new Size(135, 56);
            getData_btn.TabIndex = 0;
            getData_btn.Text = "取得天氣資料";
            getData_btn.UseVisualStyleBackColor = true;
            getData_btn.Click += getData_btn_Click;
            // 
            // lbl_DateTime
            // 
            lbl_DateTime.AutoSize = true;
            lbl_DateTime.Location = new Point(81, 51);
            lbl_DateTime.Name = "lbl_DateTime";
            lbl_DateTime.Size = new Size(99, 19);
            lbl_DateTime.TabIndex = 2;
            lbl_DateTime.Text = "當前日期時間";
            // 
            // listView_Weather
            // 
            listView_Weather.Location = new Point(70, 92);
            listView_Weather.Name = "listView_Weather";
            listView_Weather.Size = new Size(414, 601);
            listView_Weather.TabIndex = 3;
            listView_Weather.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1444, 705);
            Controls.Add(listView_Weather);
            Controls.Add(lbl_DateTime);
            Controls.Add(getData_btn);
            Name = "Form1";
            Text = "WeatherInfo_TW";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button getData_btn;
        private Label lbl_DateTime;
        private ListView listView_Weather;
    }
}
