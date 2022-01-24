
namespace EmbossingFilter
{
    partial class Form1
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
            this.loadButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.threadsCounter = new System.Windows.Forms.TrackBar();
            this.filterButton = new System.Windows.Forms.Button();
            this.threadsLabel = new System.Windows.Forms.Label();
            this.originalPathLabel = new System.Windows.Forms.Label();
            this.cppCheckbox = new System.Windows.Forms.CheckBox();
            this.asmCheckbox = new System.Windows.Forms.CheckBox();
            this.cppTime = new System.Windows.Forms.Label();
            this.asmTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadsCounter)).BeginInit();
            this.SuspendLayout();
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(12, 12);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Load bitmap";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 207);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(571, 334);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(683, 207);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(589, 334);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // threadsCounter
            // 
            this.threadsCounter.Location = new System.Drawing.Point(131, 141);
            this.threadsCounter.Maximum = 64;
            this.threadsCounter.Minimum = 1;
            this.threadsCounter.Name = "threadsCounter";
            this.threadsCounter.Size = new System.Drawing.Size(222, 45);
            this.threadsCounter.TabIndex = 4;
            this.threadsCounter.Value = 1;
            this.threadsCounter.Visible = false;
            this.threadsCounter.Scroll += new System.EventHandler(this.threadsCounter_Scroll);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(131, 12);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(103, 23);
            this.filterButton.TabIndex = 5;
            this.filterButton.Text = "Filter the image";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Visible = false;
            this.filterButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // threadsLabel
            // 
            this.threadsLabel.AutoSize = true;
            this.threadsLabel.Location = new System.Drawing.Point(12, 141);
            this.threadsLabel.Name = "threadsLabel";
            this.threadsLabel.Size = new System.Drawing.Size(106, 13);
            this.threadsLabel.TabIndex = 6;
            this.threadsLabel.Text = "Number of threads: 1";
            this.threadsLabel.Visible = false;
            // 
            // originalPathLabel
            // 
            this.originalPathLabel.AutoSize = true;
            this.originalPathLabel.Location = new System.Drawing.Point(12, 544);
            this.originalPathLabel.Name = "originalPathLabel";
            this.originalPathLabel.Size = new System.Drawing.Size(0, 13);
            this.originalPathLabel.TabIndex = 7;
            // 
            // cppCheckbox
            // 
            this.cppCheckbox.AutoCheck = false;
            this.cppCheckbox.AutoSize = true;
            this.cppCheckbox.Checked = true;
            this.cppCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cppCheckbox.Location = new System.Drawing.Point(15, 59);
            this.cppCheckbox.Name = "cppCheckbox";
            this.cppCheckbox.Size = new System.Drawing.Size(48, 17);
            this.cppCheckbox.TabIndex = 8;
            this.cppCheckbox.Text = "C++ ";
            this.cppCheckbox.UseVisualStyleBackColor = true;
            this.cppCheckbox.Visible = false;
            this.cppCheckbox.Click += new System.EventHandler(this.cppCheckbox_OnClick);
            // 
            // asmCheckbox
            // 
            this.asmCheckbox.AutoCheck = false;
            this.asmCheckbox.AutoSize = true;
            this.asmCheckbox.Location = new System.Drawing.Point(15, 82);
            this.asmCheckbox.Name = "asmCheckbox";
            this.asmCheckbox.Size = new System.Drawing.Size(49, 17);
            this.asmCheckbox.TabIndex = 9;
            this.asmCheckbox.Text = "ASM";
            this.asmCheckbox.UseVisualStyleBackColor = true;
            this.asmCheckbox.Visible = false;
            this.asmCheckbox.Click += new System.EventHandler(this.asmCheckbox_OnClick);
            // 
            // cppTime
            // 
            this.cppTime.AutoSize = true;
            this.cppTime.Location = new System.Drawing.Point(69, 60);
            this.cppTime.Name = "cppTime";
            this.cppTime.Size = new System.Drawing.Size(0, 13);
            this.cppTime.TabIndex = 10;
            // 
            // asmTime
            // 
            this.asmTime.AutoSize = true;
            this.asmTime.Location = new System.Drawing.Point(69, 82);
            this.asmTime.Name = "asmTime";
            this.asmTime.Size = new System.Drawing.Size(0, 13);
            this.asmTime.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 654);
            this.Controls.Add(this.asmTime);
            this.Controls.Add(this.cppTime);
            this.Controls.Add(this.asmCheckbox);
            this.Controls.Add(this.cppCheckbox);
            this.Controls.Add(this.originalPathLabel);
            this.Controls.Add(this.threadsLabel);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.threadsCounter);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.loadButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadsCounter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TrackBar threadsCounter;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Label threadsLabel;
        private System.Windows.Forms.Label originalPathLabel;
        private System.Windows.Forms.CheckBox cppCheckbox;
        private System.Windows.Forms.CheckBox asmCheckbox;
        private System.Windows.Forms.Label cppTime;
        private System.Windows.Forms.Label asmTime;
    }
}

