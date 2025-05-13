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
        public float CenterRadius = 38;
        public List<Petal> Petals = new List<Petal>();
        public Color PetalColor = Color.Pink;

        private Random rnd = new Random();

        public Flower(float x, float y, Color petalColor)
        {
            X = x;
            Y = y;
            PetalColor = petalColor;
            for (int i = 0; i < PetalCount; i++)
            {
                float angle = 360f / PetalCount * i;
                Petals.Add(new Petal(angle, X, Y, StemHeight, CenterRadius, PetalColor));
            }
        }

        // Проверка попадания дождя по сердцевине
        public bool IsRainHit(float px, float py)
        {
            float dx = px - X;
            float dy = py - (Y - StemHeight);
            if (dx * dx + dy * dy <= CenterRadius * CenterRadius)
            {
                GrowAllPetals();
                return true;
            }
            return false;
        }

        // Обновление лепестков (рост, падение, растворение)
        public void UpdatePetals(float groundY)
        {
            foreach (var petal in Petals)
                petal.Update(groundY);

            // Если все лепестки полностью выросли - можно запускать падение
            bool allGrown = Petals.TrueForAll(p => p.IsFullyGrown && !p.IsFalling);
            if (allGrown)
            {
                foreach (var petal in Petals)
                    petal.StartFalling();
            }

            // Если все лепестки растворились - появляются новые
            bool allDead = Petals.TrueForAll(p => p.IsDead);
            if (allDead)
            {
                for (int i = 0; i < Petals.Count; i++)
                {
                    float angle = Petals[i].Angle;
                    Petals[i] = new Petal(angle, X, Y, StemHeight, CenterRadius, PetalColor);
                }
            }
        }

        // Одновременный рост всех лепестков
        public void GrowAllPetals()
        {
            foreach (var petal in Petals)
                petal.Grow();
        }

        // Одновременное падение всех лепестков
        public void DropAllPetals()
        {
            foreach (var petal in Petals)
                petal.StartFalling();
        }

        // Сброс лепестков (если нужно вручную)
        public void ResetAllPetals()
        {
            for (int i = 0; i < Petals.Count; i++)
            {
                float angle = Petals[i].Angle;
                Petals[i] = new Petal(angle, X, Y, StemHeight, CenterRadius, PetalColor);
            }
        }

        // Смена цвета лепестков
        public void SetPetalColor(Color color)
        {
            PetalColor = color;
            foreach (var petal in Petals)
                petal.PetalColor = color;
        }

        public void Draw(Graphics g, bool isNight = false)
        {
            // Стебель
            g.FillRectangle(Brushes.Green, X - StemWidth / 2, Y - StemHeight, StemWidth, StemHeight);

            // Листочек
            float leafX = X + 40;
            float leafY = Y - StemHeight / 2 + 20;
            g.FillEllipse(Brushes.Green, leafX - 18, leafY - 12, 36, 24);

            // Лепестки
            foreach (var petal in Petals)
                petal.Draw(g, X, Y, StemHeight, CenterRadius);

            // Сердцевина
            g.FillEllipse(Brushes.Yellow, X - CenterRadius, Y - StemHeight - CenterRadius, CenterRadius * 2, CenterRadius * 2);

            // Мордочка: глазки и улыбка/сон
            float faceX = X;
            float faceY = Y - StemHeight;

            if (!isNight)
            {
                // Глазки открыты
                g.FillEllipse(Brushes.Black, faceX - 12, faceY - 10, 7, 7);
                g.FillEllipse(Brushes.Black, faceX + 5, faceY - 10, 7, 7);
                g.DrawArc(Pens.Black, faceX - 10, faceY - 2, 20, 14, 20, 140); // улыбка
            }
            else
            {
                // Сонные глазки и сонная улыбка
                g.DrawArc(Pens.Black, faceX - 12, faceY - 7, 7, 7, 0, 180);
                g.DrawArc(Pens.Black, faceX + 5, faceY - 7, 7, 7, 0, 180);
                g.DrawArc(Pens.Black, faceX - 10, faceY + 2, 20, 10, 20, 140);
                g.DrawString("Z", new Font("Arial", 12), Brushes.MediumSlateBlue, faceX + 18, faceY - 25);
            }
        }
    }
}


public class Petal
{
    public float Angle; // угол (где расположен на цветке)
    public float Length; // текущая длина
    public float Width; // ширина
    public float MinLength; // минимальная длина (маленький лепесток)
    public float MaxLength; // максимальная длина (полный рост)
    public float GrowStep; // шаг роста
    public bool IsFalling; // падает ли лепесток
    public float FallX, FallY; // координаты падения (если падает)
    public float FallVX, FallVY; // скорость падения
    public float Opacity; // прозрачность (для растворения на земле)
    public bool OnGround; // уже на земле
    public Color PetalColor = Color.Pink;
    public float FlowerOffsetX = 0;
    public float FlowerOffsetY = 0;

    private float flowerX, flowerY, stemHeight, centerRadius;
    private static Random rnd = new Random();

    public Petal(float angle, float flowerX, float flowerY, float stemHeight, float centerRadius, Color petalColor)
    {
        this.Angle = angle;
        this.MinLength = 18 + rnd.Next(4);
        this.MaxLength = 70 + rnd.Next(8);
        this.Length = MinLength;
        this.Width = 28 + rnd.Next(6);
        this.GrowStep = 0.7f + (float)rnd.NextDouble() * 0.3f;
        this.IsFalling = false;
        this.Opacity = 1.0f;
        this.OnGround = false;
        this.flowerX = flowerX;
        this.flowerY = flowerY;
        this.stemHeight = stemHeight;
        this.centerRadius = centerRadius;
        this.PetalColor = petalColor;
    }

    public bool IsFullyGrown => !IsFalling && Length >= MaxLength - 10.1f;
    public bool IsDead => OnGround && Opacity <= 0.01f;

    public void Grow()
    {
        if (!IsFalling && Length < MaxLength)
        {
            Length += GrowStep;
            if (Length > MaxLength) Length = MaxLength;
        }
    }

    public void StartFalling()
    {
        if (IsFalling) return;
        IsFalling = true;
        float angleRad = Angle * (float)Math.PI / 180f;
        FallX = flowerX + (float)Math.Cos(angleRad) * (centerRadius + Length / 2);
        FallY = flowerY - stemHeight + (float)Math.Sin(angleRad) * (centerRadius + Length / 2);
        FallVX = (float)(rnd.NextDouble() - 0.5) * 2.5f;
        FallVY = 2.5f + (float)rnd.NextDouble() * 1.5f;
    }

    public void Update(float groundY)
    {
        if (IsFalling && !OnGround)
        {
            FallX += FallVX;
            FallY += FallVY;
            FallVY += 0.13f; // гравитация

            if (FallY >= groundY - Width / 2)
            {
                FallY = groundY - Width / 2;
                OnGround = true;
            }
        }
        else if (OnGround)
        {
            Opacity -= 0.025f;
            if (Opacity < 0) Opacity = 0;
        }
    }
    public float GetCurrentX()
    {
        float angleRad = Angle * (float)Math.PI / 180f;
        return flowerX + (float)Math.Cos(angleRad) * (centerRadius + Length / 2) + FlowerOffsetX;
    }

    public float GetCurrentY()
    {
        float angleRad = Angle * (float)Math.PI / 180f;
        return flowerY - stemHeight + (float)Math.Sin(angleRad) * (centerRadius + Length / 2) + FlowerOffsetY;
    }

    public void Draw(Graphics g, float flowerX, float flowerY, float stemHeight, float centerRadius)
    {
        if (!IsFalling)
        {
            float angleRad = Angle * (float)Math.PI / 180f;
            float px = flowerX + (float)Math.Cos(angleRad) * (centerRadius + Length / 2);
            float py = flowerY - stemHeight + (float)Math.Sin(angleRad) * (centerRadius + Length / 2);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb((int)(255 * Opacity), PetalColor)))
            {
                g.FillEllipse(brush, px - Width / 2, py - Length / 2, Width, Length);
            }
        }
        else
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb((int)(255 * Opacity), PetalColor)))
            {
                g.FillEllipse(brush, FallX - Width / 2, FallY - Length / 2, Width, Length);
            }
        }
    }
}








