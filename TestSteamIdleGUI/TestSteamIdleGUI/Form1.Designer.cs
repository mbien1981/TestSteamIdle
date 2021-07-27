
namespace TestSteamIdleGUI
{
    partial class mainWindow
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
            this.StartIdleButton = new System.Windows.Forms.Button();
            this.killIdleButton = new System.Windows.Forms.Button();
            this.OutputCase = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartIdleButton
            // 
            this.StartIdleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.StartIdleButton.ForeColor = System.Drawing.Color.White;
            this.StartIdleButton.Location = new System.Drawing.Point(13, 11);
            this.StartIdleButton.Name = "StartIdleButton";
            this.StartIdleButton.Size = new System.Drawing.Size(182, 60);
            this.StartIdleButton.TabIndex = 0;
            this.StartIdleButton.TabStop = false;
            this.StartIdleButton.Text = "Start Idle";
            this.StartIdleButton.UseVisualStyleBackColor = false;
            this.StartIdleButton.Click += new System.EventHandler(this.startIdle_Button);
            // 
            // killIdleButton
            // 
            this.killIdleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.killIdleButton.ForeColor = System.Drawing.Color.White;
            this.killIdleButton.Location = new System.Drawing.Point(201, 11);
            this.killIdleButton.Name = "killIdleButton";
            this.killIdleButton.Size = new System.Drawing.Size(183, 60);
            this.killIdleButton.TabIndex = 2;
            this.killIdleButton.TabStop = false;
            this.killIdleButton.Text = "Stop Idle";
            this.killIdleButton.UseVisualStyleBackColor = false;
            this.killIdleButton.Click += new System.EventHandler(this.killIdleButton_Click);
            // 
            // OutputCase
            // 
            this.OutputCase.AccessibleName = "OutputCase";
            this.OutputCase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OutputCase.ForeColor = System.Drawing.Color.White;
            this.OutputCase.Location = new System.Drawing.Point(13, 78);
            this.OutputCase.Name = "OutputCase";
            this.OutputCase.ReadOnly = true;
            this.OutputCase.Size = new System.Drawing.Size(371, 360);
            this.OutputCase.TabIndex = 3;
            this.OutputCase.Text = "";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(309, 78);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.TabStop = false;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(398, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.OutputCase);
            this.Controls.Add(this.killIdleButton);
            this.Controls.Add(this.StartIdleButton);
            this.Name = "mainWindow";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "TestSteamIdle GUI";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartIdleButton;
        private System.Windows.Forms.Button killIdleButton;
        private System.Windows.Forms.RichTextBox OutputCase;
        private System.Windows.Forms.Button button2;
    }
}

