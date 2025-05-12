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

        bool isNight = false;
        Color petalColor = Color.Pink;

        public Form1()
        {
            InitializeComponent();

            this.Width = 1250;
            this.Height = 800;
            picDisplay.Width = 1200;
            picDisplay.Height = 650;
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            grass = new Grass(picDisplay.Width, picDisplay.Height, 60);

            // 3 цветка, красиво распределены
            for (int i = 0; i < 3; i++)
            {
                float x = 300 + i * 300;
                float y = picDisplay.Height - 70;
                flowers.Add(new Flower(x, y, petalColor));
            }

            // Привязка трекбаров и лейблов (если есть)
            rainParticlesPerTick = tbParticlesPerTick.Value;
            rainSpeed = tbRainSpeed.Value;
            lblParticlesCount.Text = $"Капель за тик: {rainParticlesPerTick}";
            lblRainSpeed.Text = $"Скорость дождя: {rainSpeed}";
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
                rain[i].Update(grass, flowers);

                // Проверка столкновения с травой (капля исчезает красиво)
                if (rain[i].Y + rain[i].Length >= grass.Area.Top)
                    rain[i].StartFade();

                // Проверка столкновения с сердцевиной цветка (рост всех лепестков)
                foreach (var flower in flowers)
                {
                    if (flower.IsRainHit(rain[i].X, rain[i].Y))
                    {
                        rain[i].StartFade();
                        break;
                    }
                }

                // Удаляем каплю, если она полностью исчезла
                if (rain[i].Opacity <= 0)
                    rain.RemoveAt(i);
            }

            // Обновление цветков (лепестки растут, падают, растворяются)
            foreach (var flower in flowers)
                flower.UpdatePetals(grass.Area.Top);

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
                g.Clear(isNight ? Color.FromArgb(30, 30, 60) : Color.White);

                // Солнце или луна
                if (!isNight)
                    DrawSun(g);
                else
                    DrawMoon(g);

                grass.Draw(g);
                foreach (var flower in flowers)
                    flower.Draw(g, isNight);
                foreach (var drop in rain)
                    drop.Draw(g);
            }
        }

        private void DrawSun(Graphics g)
        {
            int sunX = 90, sunY = 90, sunR = 60;
            g.FillEllipse(Brushes.Gold, sunX - sunR, sunY - sunR, sunR * 2, sunR * 2);
            for (int i = 0; i < 12; i++)
            {
                double angle = i * Math.PI / 6;
                int x1 = sunX + (int)(Math.Cos(angle) * sunR * 1.1);
                int y1 = sunY + (int)(Math.Sin(angle) * sunR * 1.1);
                int x2 = sunX + (int)(Math.Cos(angle) * sunR * 1.5);
                int y2 = sunY + (int)(Math.Sin(angle) * sunR * 1.5);
                g.DrawLine(new Pen(Color.Gold, 4), x1, y1, x2, y2);
            }
        }

        private void DrawMoon(Graphics g)
        {
            int moonX = 90, moonY = 90, moonR = 50;
            g.FillEllipse(Brushes.LightGray, moonX - moonR, moonY - moonR, moonR * 2, moonR * 2);
            g.FillEllipse(new SolidBrush(Color.FromArgb(30, 30, 60)), moonX - moonR / 2, moonY - moonR, moonR * 2, moonR * 2);
        }

        private void btnToggleRain_Click(object sender, EventArgs e)
        {
            rainEnabled = !rainEnabled;
            btnToggleRain.Text = rainEnabled ? "Выключить дождик" : "Включить дождик";
        }

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

        private void btnNight_Click(object sender, EventArgs e)
        {
            isNight = !isNight;
            btnNight.Text = isNight ? "Вернуть день" : "Наступила ночь";
            DrawAll();
            picDisplay.Invalidate();
        }

        private void btnPetalColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = petalColor;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    petalColor = dlg.Color;
                    foreach (var flower in flowers)
                        flower.SetPetalColor(petalColor);
                    DrawAll();
                    picDisplay.Invalidate();
                }
            }
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }

        private void lblRainSpeed_Click(object sender, EventArgs e)
        {

        }

        private void btnNight_Click_1(object sender, EventArgs e)
        {
            isNight = !isNight;
            btnNight.Text = isNight ? "Вернуть день" : "Наступила ночь";
            DrawAll();
            picDisplay.Invalidate();
        }
    }
}
