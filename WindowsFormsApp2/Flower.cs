using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class Flower
    {
        public float X, Y;
        public float StemHeight = 130;
        public float StemWidth = 12;
        public int PetalCount = 7;
        public float PetalLength = 60;
        public float PetalWidth = 40;
        public float PetalGrowStep = 1.0f; // Уменьшено для медленного роста
        public float MaxPetalLength = 90;
        public float MinPetalLength = 40;
        public float CenterRadius = 38;
        public bool PetalsFalling = false;
        public int PetalsToFall = 0;
        public List<float> PetalAngles = new List<float>();
        private Random rnd = new Random();

        public Flower(float x, float y)
        {
            X = x;
            Y = y;
            for (int i = 0; i < PetalCount; i++)
                PetalAngles.Add(360f / PetalCount * i);
        }

        public bool IsRainHit(float px, float py)
        {
            float dx = px - X;
            float dy = py - (Y - StemHeight);
            float r = (float)Math.Sqrt(dx * dx + dy * dy);
            return r > CenterRadius && r < CenterRadius + PetalLength && Math.Abs(dy) < PetalLength + 10;
        }

        public void Grow()
        {
            if (!PetalsFalling && PetalLength < MaxPetalLength)
            {
                PetalLength += PetalGrowStep;
                if (PetalLength >= MaxPetalLength)
                {
                    PetalLength = MaxPetalLength;
                    PetalsFalling = true;
                    PetalsToFall = PetalCount;
                }
            }
        }

        public void UpdatePetals()
        {
            if (PetalsFalling && PetalsToFall > 0)
            {
                PetalLength -= PetalGrowStep * 2;
                if (PetalLength <= MinPetalLength)
                {
                    PetalsToFall--;
                    PetalLength = MinPetalLength;
                    if (PetalsToFall == 0)
                    {
                        PetalsFalling = false;
                        PetalLength = 50 + rnd.Next(10, 25);
                    }
                }
            }
        }

        public void Draw(Graphics g)
        {
            // Стебель
            g.FillRectangle(Brushes.Green, X - StemWidth / 2, Y - StemHeight, StemWidth, StemHeight);

            // Листочек
            float leafX = X + 40;
            float leafY = Y - StemHeight / 2 + 20;
            g.FillEllipse(Brushes.Green, leafX - 18, leafY - 12, 36, 24);

            // Лепестки
            for (int i = 0; i < PetalCount; i++)
            {
                float angle = PetalAngles[i] * (float)Math.PI / 180;
                float px = X + (float)Math.Cos(angle) * (CenterRadius + PetalLength / 2);
                float py = Y - StemHeight + (float)Math.Sin(angle) * (CenterRadius + PetalLength / 2);
                g.FillEllipse(Brushes.Pink, px - PetalWidth / 2, py - PetalLength / 2, PetalWidth, PetalLength);
            }

            // Сердцевина
            g.FillEllipse(Brushes.Yellow, X - CenterRadius, Y - StemHeight - CenterRadius, CenterRadius * 2, CenterRadius * 2);

            // Мордочка: глазки и улыбка
            float faceX = X;
            float faceY = Y - StemHeight;
            g.FillEllipse(Brushes.Black, faceX - 12, faceY - 10, 7, 7);
            g.FillEllipse(Brushes.Black, faceX + 5, faceY - 10, 7, 7);
            g.DrawArc(Pens.Black, faceX - 10, faceY - 2, 20, 14, 20, 140);
        }
    }



}
