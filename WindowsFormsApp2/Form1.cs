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
        bool rainEnabled = false;
        Random rnd = new Random();

        TopEmitter emitter;
        int particlesPerTickBeforePause = 10; // сколько частиц вкл/выкл

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            this.DoubleBuffered = true;
            this.Width = 900;
            this.Height = 650;

            // Инициализация эмиттера дождя
            emitter = new TopEmitter
            {
                Direction = 180,
                Spreading = 30,
                SpeedMin = 5,
                SpeedMax = 10,
                ColorFrom = Color.Cyan,
                ColorTo = Color.FromArgb(0, Color.Blue),
                ParticlesPerTick = 0, // <--- Дождик выключен по умолчанию!
                X = picDisplay.Width / 2,
                Y = 0,
                Width = picDisplay.Width
            };

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
            emitter.UpdateState();

            // Обработка столкновений частиц дождя с цветами
            foreach (var particle in emitter.particles.ToList())
            {
                foreach (var flower in flowers)
                {
                    if (flower.IsRainHit(particle.X, particle.Y))
                    {
                        flower.Grow();
                        emitter.particles.Remove(particle); // Удаляем частицу при попадании
                        break;
                    }
                }
            }

            // Обновление цветков (опадание лепестков)
            foreach (var flower in flowers)
                flower.UpdatePetals();

            // Рисуем всё на Bitmap
            DrawAll();

            // Обновляем PictureBox
            picDisplay.Invalidate();
        }

        private void DrawAll()
        {
            // Получаем bitmap из picDisplay
            Bitmap bmp = picDisplay.Image as Bitmap;
            if (bmp == null) return;
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                grass.Draw(g);
                foreach (var flower in flowers)
                    flower.Draw(g);
                emitter.Render(g);
            }

        }

        private void btnToggleRain_Click(object sender, EventArgs e)
        {
            rainEnabled = !rainEnabled;

            if (rainEnabled)
            {
                emitter.ParticlesPerTick = particlesPerTickBeforePause;
                btnToggleRain.Text = "Выключить дождик";
            }
            else
            {
                particlesPerTickBeforePause = emitter.ParticlesPerTick > 0 ? emitter.ParticlesPerTick : particlesPerTickBeforePause;
                emitter.ParticlesPerTick = 0;
                btnToggleRain.Text = "Включить дождик";
            }
        }

        

        // Остальные обработчики оставляем как есть
        private void picDisplay_MouseMove(object sender, MouseEventArgs e) { }
        private void theDirection_Scroll(object sender, EventArgs e) { }
        private void tbParticlesPerTick_Scroll(object sender, EventArgs e) { }
        private void TbOpacity_Scroll(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
    }

}
