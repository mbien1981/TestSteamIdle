namespace TestSteamIdleGui
{
    partial class MainWindow
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
            this.StartButton = new System.Windows.Forms.Button();
            this.OutputBox = new System.Windows.Forms.RichTextBox();
            this.AppList = new System.Windows.Forms.CheckedListBox();
            this.StopButton = new System.Windows.Forms.Button();
            this.InputField = new System.Windows.Forms.TextBox();
            this.AddAppButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AppCounter = new System.Windows.Forms.Label();
            this.ClearLogButton = new System.Windows.Forms.Button();
            this.RemoveAppButton = new System.Windows.Forms.Button();
            this.SetInvisibleButton = new System.Windows.Forms.Button();
            this.SetOnlineButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartButton.ForeColor = System.Drawing.Color.Silver;
            this.StartButton.Location = new System.Drawing.Point(13, 12);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(256, 56);
            this.StartButton.TabIndex = 0;
            this.StartButton.TabStop = false;
            this.StartButton.Text = "Start Idle";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // OutputBox
            // 
            this.OutputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OutputBox.ForeColor = System.Drawing.Color.White;
            this.OutputBox.Location = new System.Drawing.Point(13, 89);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(256, 330);
            this.OutputBox.TabIndex = 0;
            this.OutputBox.TabStop = false;
            this.OutputBox.Text = "";
            this.OutputBox.WordWrap = false;
            // 
            // AppList
            // 
            this.AppList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.AppList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppList.ForeColor = System.Drawing.Color.Silver;
            this.AppList.FormattingEnabled = true;
            this.AppList.Location = new System.Drawing.Point(275, 89);
            this.AppList.Name = "AppList";
            this.AppList.Size = new System.Drawing.Size(254, 292);
            this.AppList.TabIndex = 2;
            this.AppList.TabStop = false;
            // 
            // StopButton
            // 
            this.StopButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.StopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopButton.ForeColor = System.Drawing.Color.Silver;
            this.StopButton.Location = new System.Drawing.Point(275, 12);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(254, 56);
            this.StopButton.TabIndex = 3;
            this.StopButton.TabStop = false;
            this.StopButton.Text = "Stop Idle";
            this.StopButton.UseVisualStyleBackColor = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // InputField
            // 
            this.InputField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.InputField.ForeColor = System.Drawing.Color.Silver;
            this.InputField.Location = new System.Drawing.Point(275, 399);
            this.InputField.MinimumSize = new System.Drawing.Size(4, 20);
            this.InputField.Name = "InputField";
            this.InputField.Size = new System.Drawing.Size(254, 20);
            this.InputField.TabIndex = 4;
            this.InputField.TabStop = false;
            this.InputField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputField_KeyPress);
            // 
            // AddAppButton
            // 
            this.AddAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddAppButton.ForeColor = System.Drawing.Color.Silver;
            this.AddAppButton.Location = new System.Drawing.Point(275, 425);
            this.AddAppButton.Name = "AddAppButton";
            this.AddAppButton.Size = new System.Drawing.Size(122, 25);
            this.AddAppButton.TabIndex = 5;
            this.AddAppButton.TabStop = false;
            this.AddAppButton.Text = "Add App";
            this.AddAppButton.UseVisualStyleBackColor = true;
            this.AddAppButton.Click += new System.EventHandler(this.AddAppButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Log";
            // 
            // AppCounter
            // 
            this.AppCounter.AutoSize = true;
            this.AppCounter.ForeColor = System.Drawing.Color.Silver;
            this.AppCounter.Location = new System.Drawing.Point(272, 71);
            this.AppCounter.Name = "AppCounter";
            this.AppCounter.Size = new System.Drawing.Size(43, 13);
            this.AppCounter.TabIndex = 7;
            this.AppCounter.Text = "Apps: 0";
            // 
            // ClearLogButton
            // 
            this.ClearLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearLogButton.ForeColor = System.Drawing.Color.Silver;
            this.ClearLogButton.Location = new System.Drawing.Point(13, 425);
            this.ClearLogButton.Name = "ClearLogButton";
            this.ClearLogButton.Size = new System.Drawing.Size(66, 25);
            this.ClearLogButton.TabIndex = 8;
            this.ClearLogButton.TabStop = false;
            this.ClearLogButton.Text = "Clear log";
            this.ClearLogButton.UseVisualStyleBackColor = true;
            this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButton_Click);
            // 
            // RemoveAppButton
            // 
            this.RemoveAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveAppButton.ForeColor = System.Drawing.Color.Silver;
            this.RemoveAppButton.Location = new System.Drawing.Point(407, 425);
            this.RemoveAppButton.Name = "RemoveAppButton";
            this.RemoveAppButton.Size = new System.Drawing.Size(121, 25);
            this.RemoveAppButton.TabIndex = 9;
            this.RemoveAppButton.TabStop = false;
            this.RemoveAppButton.Text = "Remove App";
            this.RemoveAppButton.UseVisualStyleBackColor = true;
            this.RemoveAppButton.Click += new System.EventHandler(this.RemoveAppButton_Click);
            // 
            // SetInvisibleButton
            // 
            this.SetInvisibleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SetInvisibleButton.ForeColor = System.Drawing.Color.Silver;
            this.SetInvisibleButton.Location = new System.Drawing.Point(85, 425);
            this.SetInvisibleButton.Name = "SetInvisibleButton";
            this.SetInvisibleButton.Size = new System.Drawing.Size(112, 25);
            this.SetInvisibleButton.TabIndex = 11;
            this.SetInvisibleButton.TabStop = false;
            this.SetInvisibleButton.Text = "Set Invisible";
            this.SetInvisibleButton.UseVisualStyleBackColor = true;
            this.SetInvisibleButton.Click += new System.EventHandler(this.SetInvisibleButton_Click);
            // 
            // SetOnlineButton
            // 
            this.SetOnlineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SetOnlineButton.ForeColor = System.Drawing.Color.Silver;
            this.SetOnlineButton.Location = new System.Drawing.Point(203, 425);
            this.SetOnlineButton.Name = "SetOnlineButton";
            this.SetOnlineButton.Size = new System.Drawing.Size(66, 25);
            this.SetOnlineButton.TabIndex = 12;
            this.SetOnlineButton.TabStop = false;
            this.SetOnlineButton.Text = "Set online";
            this.SetOnlineButton.UseVisualStyleBackColor = true;
            this.SetOnlineButton.Click += new System.EventHandler(this.SetOnlineButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(540, 462);
            this.Controls.Add(this.SetOnlineButton);
            this.Controls.Add(this.SetInvisibleButton);
            this.Controls.Add(this.RemoveAppButton);
            this.Controls.Add(this.ClearLogButton);
            this.Controls.Add(this.AppCounter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddAppButton);
            this.Controls.Add(this.InputField);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.AppList);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.StartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "TestSteamIdle";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.RichTextBox OutputBox;
        private System.Windows.Forms.CheckedListBox AppList;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TextBox InputField;
        private System.Windows.Forms.Button AddAppButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label AppCounter;
        private System.Windows.Forms.Button ClearLogButton;
        private System.Windows.Forms.Button RemoveAppButton;
        private System.Windows.Forms.Button SetInvisibleButton;
        private System.Windows.Forms.Button SetOnlineButton;
    }
}

