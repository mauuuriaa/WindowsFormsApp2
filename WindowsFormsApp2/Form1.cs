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

        private WateringCan wateringCan = new WateringCan();
        private Umbrella umbrella = new Umbrella();




        private HurricanePoint hurricanePoint = new HurricanePoint();
        private bool hurricaneEnabled = false;

        public Form1()
        {
            InitializeComponent();


            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            grass = new Grass(picDisplay.Width, picDisplay.Height, 70);
            btnHurricane.Visible = false;
            

            // 3 цветка
            for (int i = 0; i < 3; i++)
            {
                float x = 100 + i * 250;
                float y = picDisplay.Height - 70;
                flowers.Add(new Flower(x, y, petalColor));
            }

            // Привязка трекбаров и лейблов 
            rainParticlesPerTick = tbParticlesPerTick.Value;
            rainSpeed = tbRainSpeed.Value;
            lblParticlesCount.Text = $"Капель за тик: {rainParticlesPerTick}";
            lblRainSpeed.Text = $"Скорость дождя: {rainSpeed}";
            btnWateringCan.Text = "Полить цветочки";
            btnUmbrella.Text = "Зонтик";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //  Обновляем позиции инструментов (лейка и зонтик следуют за курсором)
            Point cursor = PointToClient(Cursor.Position);
            wateringCan.X = cursor.X;
            wateringCan.Y = cursor.Y;
            umbrella.X = cursor.X;
            umbrella.Y = cursor.Y;

            // Генерируем дождевые капли, если дождь включен (и зонтик не активен)
            if (rainEnabled)
            {
                for (int i = 0; i < rainParticlesPerTick; i++)
                {
                    float rx = rnd.Next(0, picDisplay.Width);
                    rain.Add(new ParticleRain(rx, 0, rainSpeed, 18, Color.DeepSkyBlue));
                }
            }

            // Генерируем "лейку" — частицы воды под курсором, если лейка активна
            if (wateringCan.IsActive)
            {
                for (int i = 0; i < rainParticlesPerTick; i++)
                {
                    float rx = wateringCan.X + rnd.Next(-20, 20);
                    float ry = wateringCan.Y + rnd.Next(0, 16);
                    rain.Add(new ParticleRain(rx, ry, rainSpeed, 18, Color.DeepSkyBlue));
                }
            }

            // Обновляем все частицы дождя и воды
            for (int i = rain.Count - 1; i >= 0; i--)
            {
                // Если зонтик активен и частица попала под него — удаляем частицу
                if (umbrella.IsActive && umbrella.CheckCollision(rain[i].X, rain[i].Y))
                {
                    rain.RemoveAt(i);
                    continue;
                }

                // Обновляем состояние частицы
                rain[i].Update(grass, flowers);

                // Если частица достигла земли — начинаем её растворять
                if (rain[i].Y + rain[i].Length >= grass.Area.Top)
                    rain[i].StartFade();

                // Проверяем попадание по цветку (и для дождя, и для лейки)
                foreach (var flower in flowers)
                {
                    // Если лейка активна, поливаем только те цветы, которые под курсором
                    if (wateringCan.IsActive)
                    {
                        // Проверяем, что частица находится над цветком под курсором
                        if (flower.IsRainHit(rain[i].X, rain[i].Y))
                        {
                            rain[i].StartFade();
                            break;
                        }
                    }
                    else
                    {
                        // Обычный дождь: если частица попала по цветку — растворяем
                        if (flower.IsRainHit(rain[i].X, rain[i].Y))
                        {
                            rain[i].StartFade();
                            break;
                        }
                    }
                }

                // Удаляем растворившиеся частицы
                if (rain[i].Opacity <= 0)
                    rain.RemoveAt(i);
            }

            // Обновляем цветы и лепестки
            foreach (var flower in flowers)
            {
                flower.UpdatePetals(grass.Area.Top);

                foreach (var petal in flower.Petals)
                {
                    if (hurricaneEnabled)
                        hurricanePoint.ImpactParticle(petal);

                    if (petal.FallX < 0 || petal.FallX > picDisplay.Width || petal.FallY > picDisplay.Height)
                    {
                        petal.Opacity = 0;
                        petal.OnGround = true;
                    }
                }
            }

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

                if (!isNight)
                    DrawSun(g);
                else
                    DrawMoon(g);

                grass.Draw(g);
                foreach (var flower in flowers)
                    flower.Draw(g, isNight);
                foreach (var drop in rain)
                    drop.Draw(g);

                if (hurricaneEnabled)
                    hurricanePoint.Render(g);
                if (wateringCan.IsActive)
                    DrawWateringCan(g);

                if (umbrella.IsActive)
                    DrawUmbrella(g);
            }
        }

        private void DrawWateringCan(Graphics g)
        {
            // Простая векторная модель лейки
            Point[] canBody = {
            new Point(wateringCan.X - 15, wateringCan.Y - 40),
            new Point(wateringCan.X + 15, wateringCan.Y - 40),
            new Point(wateringCan.X + 10, wateringCan.Y),
            new Point(wateringCan.X - 10, wateringCan.Y)
        };
            g.FillPolygon(Brushes.SteelBlue, canBody);
            g.FillEllipse(Brushes.Silver, wateringCan.X - 20, wateringCan.Y - 50, 40, 20);
        }

        private void DrawUmbrella(Graphics g)
        {
            int domeRadius = umbrella.Radius * 3 / 2; // Купол больше, чем область коллизии
            int domeWidth = domeRadius;
            int domeHeight = domeRadius / 2;

            // Координаты купола (центрируется по X, верхняя точка по Y)
            int domeX = umbrella.X - domeWidth / 2;
            int domeY = umbrella.Y - domeHeight;

            // Рисуем купол зонтика (полуэллипс)
            Rectangle domeRect = new Rectangle(domeX, domeY, domeWidth, domeHeight * 2);
            g.FillPie(Brushes.Red, domeRect, 180, 180);
            g.DrawArc(Pens.Black, domeRect, 180, 180);

            // Рисуем палку зонтика из центра купола вниз
            int stickStartX = umbrella.X;
            int stickStartY = umbrella.Y;
            int stickEndX = umbrella.X;
            int stickEndY = umbrella.Y + 60;
            g.DrawLine(new Pen(Color.Black, 4), stickStartX, stickStartY, stickEndX, stickEndY);
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
            btnHurricane.Visible = rainEnabled;

            if (!rainEnabled)
            {
                hurricaneEnabled = false;
                hurricanePoint.IsActive = false;
                btnHurricane.Text = "Включить ураган";
            }
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




        private void btnPetalColor_Click_1(object sender, EventArgs e)
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





        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {

        }
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



        private void btnHurricane_Click(object sender, EventArgs e)
        {
            hurricaneEnabled = !hurricaneEnabled;
            hurricanePoint.IsActive = hurricaneEnabled;
            btnHurricane.Text = hurricaneEnabled ? "Выключить ураган" : "Включить ураган";

            if (hurricaneEnabled)
            {
                hurricanePoint.X = picDisplay.Width / 2;
                hurricanePoint.Y = picDisplay.Height / 2 - 70;
            }
        }



        private void btnUmbrella_Click(object sender, EventArgs e)
        {
            if (!umbrella.IsActive)
            {
                umbrella.IsActive = true;
                wateringCan.IsActive = false;
                btnUmbrella.Text = "Убрать зонтик";
                btnWateringCan.Text = "Полить цветочки";
            }
            else
            {
                umbrella.IsActive = false;
                btnUmbrella.Text = "Зонтик";
            }
        }

        private void btnWateringCan_Click(object sender, EventArgs e)
        {
            if (!wateringCan.IsActive)
            {
                wateringCan.IsActive = true;
                umbrella.IsActive = false;
                btnWateringCan.Text = "Выключить лейку";
                btnUmbrella.Text = "Зонтик";
            }
            else
            {
                wateringCan.IsActive = false;
                btnWateringCan.Text = "Полить цветочки";
            }
        }
    }
}



