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
        List<Emitter> emitters = new List<Emitter>();
        TopEmitter emitter;

        GravityPoint sink; // статичный гравитрон-притягиватель
        SourceGravityPoint source; // двигающийся гравитрон-источник

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            // Создаем верхний эмиттер
            emitter = new TopEmitter
            {
                Direction = 180,
                Spreading = 30,
                SpeedMin = 5,
                SpeedMax = 10,
                ColorFrom = Color.Cyan,
                ColorTo = Color.FromArgb(0, Color.Blue),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = 0,
                Width = picDisplay.Width
            };
            emitters.Add(emitter);

            tbParticlesPerTick.Value = emitter.ParticlesPerTick;
            tbParticlesPerTick.Scroll += tbParticlesPerTick_Scroll;
            lblParticlesCount.Text = $"Частиц за тик: {tbParticlesPerTick.Value}";
            lblDegreeCount.Text = $"Угол поворота: {theDirection.Value}";
            lblOpacityCount.Text = $"Прозрачность: {TbOpacity.Value}";

            // Статичный гравитрон-поглотитель (внизу экрана)
            sink = new GravityPoint
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height - 50,
                Power = 200
            };
            emitter.impactPoints.Add(sink);

            // Двигающийся гравитрон-источник
            source = new SourceGravityPoint
            {
                X = emitter.X,
                Y = emitter.Y,
                Power = 50
            };
            emitter.impactPoints.Add(source);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
            }

            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            emitter.MousePositionX = e.X;
            emitter.MousePositionY = e.Y;

            // Перемещаем гравитрон-источник к мыши
            source.X = e.X;
            source.Y = e.Y;
        }

        private void theDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = theDirection.Value;
            lblDegreeCount.Text = $"Угол поворота: {theDirection.Value}";
        }

        private void tbParticlesPerTick_Scroll(object sender, EventArgs e)
        {
            emitter.ParticlesPerTick = tbParticlesPerTick.Value;
            lblParticlesCount.Text = $"Частиц за тик: {tbParticlesPerTick.Value}";
        }

        private void TbOpacity_Scroll(object sender, EventArgs e)
        {
            emitter.Opacity = TbOpacity.Value / 100f;
            lblOpacityCount.Text = $"Прозрачность: {TbOpacity.Value}";
        }

        private void btnToggleRain_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e) { }
    }

        
        
    }
