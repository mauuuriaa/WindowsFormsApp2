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

        // Система частиц дождя
        TopEmitter emitter;
        int particlesPerTickBeforePause;

        public Form1()
        {
            InitializeComponent();
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
                ParticlesPerTick = 10,
                X = this.Width / 2,
                Y = 0,
                Width = this.Width
            };

            // Создать траву
            grass = new Grass(this.Width, this.Height, 60);

            // Расположить 5 цветков
            for (int i = 0; i < 5; i++)
            {
                float x = 120 + i * 160;
                float y = this.Height - 70;
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

            // Обновление цветков
            foreach (var flower in flowers)
                flower.UpdatePetals();

            this.Invalidate();
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
                particlesPerTickBeforePause = emitter.ParticlesPerTick;
                emitter.ParticlesPerTick = 0;
                btnToggleRain.Text = "Включить дождик";
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            grass.Draw(e.Graphics);
            foreach (var flower in flowers)
                flower.Draw(e.Graphics);

            // Отрисовка частиц дождя
            using (var bmp = new Bitmap(picDisplay.Width, picDisplay.Height))
            using (var g = Graphics.FromImage(bmp))
            {
                emitter.Render(g);
                e.Graphics.DrawImage(bmp, 0, 0);
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
