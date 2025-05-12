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
            this.btnToggleRain = new System.Windows.Forms.Button();
            this.tbParticlesPerTick = new System.Windows.Forms.TrackBar();
            this.lblParticlesCount = new System.Windows.Forms.Label();
            this.lblRainSpeed = new System.Windows.Forms.Label();
            this.tbRainSpeed = new System.Windows.Forms.TrackBar();
            this.btnNight = new System.Windows.Forms.Button();
            this.btnPetalColor = new System.Windows.Forms.Button();
            this.btnWind = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbParticlesPerTick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRainSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // picDisplay
            // 
            this.picDisplay.Location = new System.Drawing.Point(12, 12);
            this.picDisplay.Name = "picDisplay";
            this.picDisplay.Size = new System.Drawing.Size(1017, 717);
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
            // btnToggleRain
            // 
            this.btnToggleRain.Location = new System.Drawing.Point(1050, 16);
            this.btnToggleRain.Name = "btnToggleRain";
            this.btnToggleRain.Size = new System.Drawing.Size(141, 45);
            this.btnToggleRain.TabIndex = 9;
            this.btnToggleRain.Text = "Начать дождик";
            this.btnToggleRain.UseVisualStyleBackColor = true;
            this.btnToggleRain.Click += new System.EventHandler(this.btnToggleRain_Click);
            // 
            // tbParticlesPerTick
            // 
            this.tbParticlesPerTick.Location = new System.Drawing.Point(1050, 100);
            this.tbParticlesPerTick.Maximum = 100;
            this.tbParticlesPerTick.Minimum = 1;
            this.tbParticlesPerTick.Name = "tbParticlesPerTick";
            this.tbParticlesPerTick.Size = new System.Drawing.Size(151, 58);
            this.tbParticlesPerTick.TabIndex = 10;
            this.tbParticlesPerTick.Value = 1;
            this.tbParticlesPerTick.Scroll += new System.EventHandler(this.tbParticlesPerTick_Scroll);
            // 
            // lblParticlesCount
            // 
            this.lblParticlesCount.AutoSize = true;
            this.lblParticlesCount.Location = new System.Drawing.Point(1061, 81);
            this.lblParticlesCount.Name = "lblParticlesCount";
            this.lblParticlesCount.Size = new System.Drawing.Size(98, 16);
            this.lblParticlesCount.TabIndex = 11;
            this.lblParticlesCount.Text = "Капель за тик";
            // 
            // lblRainSpeed
            // 
            this.lblRainSpeed.AutoSize = true;
            this.lblRainSpeed.Location = new System.Drawing.Point(1061, 166);
            this.lblRainSpeed.Name = "lblRainSpeed";
            this.lblRainSpeed.Size = new System.Drawing.Size(111, 16);
            this.lblRainSpeed.TabIndex = 13;
            this.lblRainSpeed.Text = "Скорость дождя";
            this.lblRainSpeed.Click += new System.EventHandler(this.lblRainSpeed_Click);
            // 
            // tbRainSpeed
            // 
            this.tbRainSpeed.Location = new System.Drawing.Point(1050, 185);
            this.tbRainSpeed.Maximum = 30;
            this.tbRainSpeed.Minimum = 2;
            this.tbRainSpeed.Name = "tbRainSpeed";
            this.tbRainSpeed.Size = new System.Drawing.Size(151, 58);
            this.tbRainSpeed.TabIndex = 12;
            this.tbRainSpeed.Value = 2;
            this.tbRainSpeed.Scroll += new System.EventHandler(this.tbRainSpeed_Scroll);
            // 
            // btnNight
            // 
            this.btnNight.Location = new System.Drawing.Point(1050, 249);
            this.btnNight.Name = "btnNight";
            this.btnNight.Size = new System.Drawing.Size(141, 45);
            this.btnNight.TabIndex = 14;
            this.btnNight.Text = "Наступила ночь";
            this.btnNight.UseVisualStyleBackColor = true;
            this.btnNight.Click += new System.EventHandler(this.btnNight_Click_1);
            // 
            // btnPetalColor
            // 
            this.btnPetalColor.Location = new System.Drawing.Point(1050, 331);
            this.btnPetalColor.Name = "btnPetalColor";
            this.btnPetalColor.Size = new System.Drawing.Size(141, 45);
            this.btnPetalColor.TabIndex = 15;
            this.btnPetalColor.Text = "Цвет цветочка";
            this.btnPetalColor.UseVisualStyleBackColor = true;
            this.btnPetalColor.Click += new System.EventHandler(this.btnPetalColor_Click_1);
            // 
            // btnWind
            // 
            this.btnWind.Location = new System.Drawing.Point(1050, 409);
            this.btnWind.Name = "btnWind";
            this.btnWind.Size = new System.Drawing.Size(141, 45);
            this.btnWind.TabIndex = 16;
            this.btnWind.Text = "Включить ветер";
            this.btnWind.UseVisualStyleBackColor = true;
            this.btnWind.Click += new System.EventHandler(this.btnWind_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 779);
            this.Controls.Add(this.btnWind);
            this.Controls.Add(this.btnPetalColor);
            this.Controls.Add(this.btnNight);
            this.Controls.Add(this.lblRainSpeed);
            this.Controls.Add(this.tbRainSpeed);
            this.Controls.Add(this.lblParticlesCount);
            this.Controls.Add(this.tbParticlesPerTick);
            this.Controls.Add(this.btnToggleRain);
            this.Controls.Add(this.picDisplay);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbParticlesPerTick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRainSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDisplay;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnToggleRain;
        private System.Windows.Forms.TrackBar tbParticlesPerTick;
        private System.Windows.Forms.Label lblParticlesCount;
        private System.Windows.Forms.Label lblRainSpeed;
        private System.Windows.Forms.TrackBar tbRainSpeed;
        private System.Windows.Forms.Button btnNight;
        private System.Windows.Forms.Button btnPetalColor;
        private System.Windows.Forms.Button btnWind;
    }
}

