namespace WindowsFormsApp2
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
            this.components = new System.ComponentModel.Container();
            this.picDisplay = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.theDirection = new System.Windows.Forms.TrackBar();
            this.lblParticlesCount = new System.Windows.Forms.Label();
            this.tbParticlesPerTick = new System.Windows.Forms.TrackBar();
            this.lblDegreeCount = new System.Windows.Forms.Label();
            this.TbOpacity = new System.Windows.Forms.TrackBar();
            this.lblOpacityCount = new System.Windows.Forms.Label();
            this.btnToggleRain = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.theDirection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbParticlesPerTick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TbOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // picDisplay
            // 
            this.picDisplay.Location = new System.Drawing.Point(12, 12);
            this.picDisplay.Name = "picDisplay";
            this.picDisplay.Size = new System.Drawing.Size(740, 375);
            this.picDisplay.TabIndex = 0;
            this.picDisplay.TabStop = false;
            this.picDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picDisplay_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // theDirection
            // 
            this.theDirection.Location = new System.Drawing.Point(761, 85);
            this.theDirection.Maximum = 360;
            this.theDirection.Minimum = 180;
            this.theDirection.Name = "theDirection";
            this.theDirection.Size = new System.Drawing.Size(141, 58);
            this.theDirection.TabIndex = 1;
            this.theDirection.Value = 180;
            this.theDirection.Scroll += new System.EventHandler(this.theDirection_Scroll);
            // 
            // lblParticlesCount
            // 
            this.lblParticlesCount.AutoSize = true;
            this.lblParticlesCount.Location = new System.Drawing.Point(909, 12);
            this.lblParticlesCount.Name = "lblParticlesCount";
            this.lblParticlesCount.Size = new System.Drawing.Size(0, 16);
            this.lblParticlesCount.TabIndex = 4;
            // 
            // tbParticlesPerTick
            // 
            this.tbParticlesPerTick.Location = new System.Drawing.Point(758, 12);
            this.tbParticlesPerTick.Maximum = 100;
            this.tbParticlesPerTick.Minimum = 1;
            this.tbParticlesPerTick.Name = "tbParticlesPerTick";
            this.tbParticlesPerTick.Size = new System.Drawing.Size(144, 58);
            this.tbParticlesPerTick.TabIndex = 5;
            this.tbParticlesPerTick.Value = 1;
            this.tbParticlesPerTick.Scroll += new System.EventHandler(this.tbParticlesPerTick_Scroll);
            // 
            // lblDegreeCount
            // 
            this.lblDegreeCount.AutoSize = true;
            this.lblDegreeCount.Location = new System.Drawing.Point(909, 85);
            this.lblDegreeCount.Name = "lblDegreeCount";
            this.lblDegreeCount.Size = new System.Drawing.Size(0, 16);
            this.lblDegreeCount.TabIndex = 6;
            // 
            // TbOpacity
            // 
            this.TbOpacity.Location = new System.Drawing.Point(761, 149);
            this.TbOpacity.Maximum = 100;
            this.TbOpacity.Minimum = 10;
            this.TbOpacity.Name = "TbOpacity";
            this.TbOpacity.Size = new System.Drawing.Size(141, 58);
            this.TbOpacity.TabIndex = 7;
            this.TbOpacity.Value = 100;
            this.TbOpacity.Scroll += new System.EventHandler(this.TbOpacity_Scroll);
            // 
            // lblOpacityCount
            // 
            this.lblOpacityCount.AutoSize = true;
            this.lblOpacityCount.Location = new System.Drawing.Point(912, 149);
            this.lblOpacityCount.Name = "lblOpacityCount";
            this.lblOpacityCount.Size = new System.Drawing.Size(0, 16);
            this.lblOpacityCount.TabIndex = 8;
            // 
            // btnToggleRain
            // 
            this.btnToggleRain.Location = new System.Drawing.Point(52, 403);
            this.btnToggleRain.Name = "btnToggleRain";
            this.btnToggleRain.Size = new System.Drawing.Size(141, 45);
            this.btnToggleRain.TabIndex = 9;
            this.btnToggleRain.Text = "Начать дождик";
            this.btnToggleRain.UseVisualStyleBackColor = true;
            this.btnToggleRain.Click += new System.EventHandler(this.btnToggleRain_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 481);
            this.Controls.Add(this.btnToggleRain);
            this.Controls.Add(this.lblOpacityCount);
            this.Controls.Add(this.TbOpacity);
            this.Controls.Add(this.lblDegreeCount);
            this.Controls.Add(this.tbParticlesPerTick);
            this.Controls.Add(this.lblParticlesCount);
            this.Controls.Add(this.theDirection);
            this.Controls.Add(this.picDisplay);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.theDirection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbParticlesPerTick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TbOpacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDisplay;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar theDirection;
        private System.Windows.Forms.Label lblParticlesCount;
        private System.Windows.Forms.TrackBar tbParticlesPerTick;
        private System.Windows.Forms.Label lblDegreeCount;
        private System.Windows.Forms.TrackBar TbOpacity;
        private System.Windows.Forms.Label lblOpacityCount;
        private System.Windows.Forms.Button btnToggleRain;
    }
}

