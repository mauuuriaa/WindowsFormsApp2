using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        List<Flower> flowers = new List<Flower>();
        Grass grass;
        List<ParticleRain> rain = new List<ParticleRain>();
        Random rnd = new Random();

        bool rainEnabled = false;
        int rainParticlesPerTick = 10;
        int rainSpeed = 10;

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            
            // Размер формы и PictureBox
            this.Width = 1200;
            this.Height = 650;
            picDisplay.Width = 1200;


            // Создать траву
            grass = new Grass(picDisplay.Width, picDisplay.Height, 60);

            // Расположить 5 цветков
            for (int i = 0; i < 5; i++)
            {
                float x = 120 + i * 160;
                float y = picDisplay.Height - 70;
                flowers.Add(new Flower(x, y));
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Дождик
            if (rainEnabled)
            {
                for (int i = 0; i < rainParticlesPerTick; i++)
                {
                    float rx = rnd.Next(0, picDisplay.Width);
                    rain.Add(new ParticleRain(rx, 0, rainSpeed, 18, Color.DeepSkyBlue));
                }
            }

            // Обновление частиц дождя
            for (int i = rain.Count - 1; i >= 0; i--)
            {
                rain[i].Update();
                bool hit = false;
                foreach (var flower in flowers)
                {
                    if (flower.IsRainHit(rain[i].X, rain[i].Y))
                    {
                        flower.Grow();
                        hit = true;
                        break;
                    }
                }
                if (hit || rain[i].Y > picDisplay.Height)
                    rain.RemoveAt(i);
            }

            // Обновление цветков
            foreach (var flower in flowers)
                flower.UpdatePetals();

            // Рисуем всё
            DrawAll();
            picDisplay.Invalidate();
        }

        private void DrawAll()
        {
            Bitmap bmp = picDisplay.Image as Bitmap;
            if (bmp == null) return;
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                grass.Draw(g);
                foreach (var flower in flowers)
                    flower.Draw(g);
                foreach (var drop in rain)
                    drop.Draw(g);
            }

        }

        private void btnToggleRain_Click(object sender, EventArgs e)
        {
            rainEnabled = !rainEnabled;
            btnToggleRain.Text = rainEnabled ? "Выключить дождик" : "Включить дождик";
        
        }

        

        // Остальные обработчики оставляем как есть
        private void picDisplay_MouseMove(object sender, MouseEventArgs e) { }
       
        private void Form1_Load(object sender, EventArgs e) { }

        private void tbParticlesPerTick_Scroll(object sender, EventArgs e)
        {
            rainParticlesPerTick = tbParticlesPerTick.Value;
            lblParticlesCount.Text = $"Капель за тик: {rainParticlesPerTick}";
        }

        private void tbRainSpeed_Scroll(object sender, EventArgs e)
        {
            rainSpeed = tbRainSpeed.Value;
            lblRainSpeed.Text = $"Скорость дождя: {rainSpeed}";
        }
    }

}
