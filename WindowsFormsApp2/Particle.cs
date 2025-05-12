using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class Particle
    {
        public int Radius;
        public float X;
        public float Y;
        public float SpeedX;
        public float SpeedY;

        public float Opacity = 1.0f;

        public static Random rand = new Random();

        public Particle()
        {
            var direction = (double)rand.Next(360);
            var speed = 1 + rand.Next(10);

            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            Radius = 2 + rand.Next(10);
        }

        public virtual void Draw(Graphics g)
        {
            int alpha = (int)(Opacity * 255);
            var color = Color.FromArgb(alpha, Color.Black);
            var b = new SolidBrush(color);

            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            b.Dispose();
        }
    }

    public class ParticleColorful : Particle
    {
        public Color FromColor;
        public Color ToColor;

        public float MixFactor = 0.5f; // Коэффициент смешивания от 0 до 1

        public static Color MixColor(Color color1, Color color2, float k)
        {
            return Color.FromArgb(
                (int)(color2.A * k + color1.A * (1 - k)),
                (int)(color2.R * k + color1.R * (1 - k)),
                (int)(color2.G * k + color1.G * (1 - k)),
                (int)(color2.B * k + color1.B * (1 - k))
            );
        }

        public override void Draw(Graphics g)
        {
            int alpha = (int)(Opacity * 255);

            var mixedColor = MixColor(FromColor, ToColor, MixFactor);
            mixedColor = Color.FromArgb(alpha, mixedColor); // применяем прозрачность

            using (var b = new SolidBrush(mixedColor))
            {
                g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            }
        }
    }


    public class ParticleRain
    {
        public float X, Y;
        public float SpeedY;
        public float Length;
        public Color Color;
        public float Opacity;
        public bool IsAlive = true;

        public ParticleRain(float x, float y, float speedY, float length, Color color, float opacity = 0.8f)
        {
            X = x;
            Y = y;
            SpeedY = speedY;
            Length = length;
            Color = color;
            Opacity = opacity;
        }

        public void Update(Grass grass, List<Flower> flowers)
        {
            Y += SpeedY;

            // Проверка столкновения с травой
            if (Y + Length > grass.Area.Top)
                IsAlive = false;

            // Проверка столкновения с цветами
            foreach (var flower in flowers)
            {
                if (flower.IsRainHit(X, Y))
                {
                    flower.Grow();
                    IsAlive = false;
                    break;
                }
            }
        }

        public void Draw(Graphics g)
        {
            // Тело капли - вытянутый эллипс
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(160, Color)))
            {
                g.FillEllipse(brush, X - 2, Y, 4, Length);
            }

            // Светлый блик - маленький эллипс сверху (эффект мокроты)
            using (SolidBrush highlight = new SolidBrush(Color.FromArgb(180, Color.White)))
            {
                g.FillEllipse(highlight, X - 1, Y + Length * 0.15f, 2, 2);
            }

            // Контур - тонкая полупрозрачная линия
            using (Pen pen = new Pen(Color.FromArgb(120, Color), 1))
            {
                g.DrawEllipse(pen, X - 2, Y, 4, Length);
            }
        }
    }


    public class Grass
    {
        public RectangleF Area;

        public Grass(float width, float height, float grassHeight = 60)
        {
            Area = new RectangleF(0, height - grassHeight, width, grassHeight);
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.LawnGreen, Area);
        }
    }
}