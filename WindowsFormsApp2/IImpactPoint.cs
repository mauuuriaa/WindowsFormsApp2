using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public abstract class IImpactPoint
    {
        public int X;
        public int Y;
        //public int Power = 100;
        public abstract void ImpactParticle(Petal petal);

        public virtual void Render(Graphics g)
        {
            g.DrawEllipse(Pens.Red, X - 5, Y - 5, 10, 10);
        }
    }

    public class HurricanePoint : IImpactPoint
    {
        public int Power = 500;               // Сила притяжения
        public float RotationSpeed = 0.3f;   // Скорость вращения лепестков
        public float EjectionPower = 12f;    // Сила выброса лепестков
        public bool IsActive = false;

        private float[] circleAngles = new float[3]; // Углы вращения для трёх кругов
        private int[] radii = new int[] { 120, 160, 200 }; // Радиусы трёх кругов

        // Границы экрана / PictureBox, чтобы удалять лепестки
        public int ScreenWidth = 1100;
        public int ScreenHeight = 750;

        public override void ImpactParticle(Petal petal)
        {
            if (!IsActive || petal.OnGround)
                return;

            // Координаты лепестка
            float px = petal.IsFalling ? petal.FallX : petal.GetCurrentX();
            float py = petal.IsFalling ? petal.FallY : petal.GetCurrentY();

            // Вектор от лепестка к центру урагана (X, Y)
            float dx = X - px;
            float dy = Y - py;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance < 1) distance = 1; // чтобы избежать деления на ноль

            // Притяжение по спирали: притягиваем к центру + добавляем вращение
            float attractionForce = Power / distance;

            // Направление к центру (нормализуем)
            float dirX = dx / distance;
            float dirY = dy / distance;

            // Угол для вращения (по касательной к центру)
            float angleToCenter = (float)Math.Atan2(dy, dx);
            float tangentAngle = angleToCenter + (float)(Math.PI / 2);

            // Сила вращения (чем ближе к центру, тем сильнее)
            float rotationForce = RotationSpeed * Power / distance;

            if (petal.IsFalling)
            {
                // Добавляем притяжение к центру
                petal.FallVX += dirX * attractionForce * 0.2f;
                petal.FallVY += dirY * attractionForce * 0.2f;

                // Добавляем вращение по касательной
                petal.FallVX += (float)Math.Cos(tangentAngle) * rotationForce;
                petal.FallVY += (float)Math.Sin(tangentAngle) * rotationForce;

                // Если лепесток очень близко к центру - выбрасываем наружу
                if (distance < 30)
                {
                    float ejectAngle = angleToCenter + (float)Math.PI;
                    petal.FallVX = (float)Math.Cos(ejectAngle) * EjectionPower;
                    petal.FallVY = (float)Math.Sin(ejectAngle) * EjectionPower;
                    petal.StartFalling();
                }

                // Удаляем лепесток, если он вышел за пределы экрана
                if (petal.FallX < 0 || petal.FallX > ScreenWidth || petal.FallY < 0 || petal.FallY > ScreenHeight)
                {
                    petal.OnGround = true; // или другой способ удалить лепесток из системы
                }
            }
            else
            {
                // Для лепестков, которые ещё не падают, просто слегка притягиваем к центру (без вращения)
                petal.FlowerOffsetX += dirX * attractionForce * 0.1f;
                petal.FlowerOffsetY += dirY * attractionForce * 0.1f;
            }
        }

        public override void Render(Graphics g)
        {
            if (!IsActive) return;

            // Рисуем три прерывистых круга с разной скоростью вращения
            for (int i = 0; i < radii.Length; i++)
            {
                int radius = radii[i];
                float angle = circleAngles[i];

                using (Pen pen = new Pen(Color.MediumSlateBlue, 3))
                {
                    pen.DashPattern = new float[] { 6, 6 }; // Прерывистая линия

                    var state = g.Save();

                    // Центр урагана без смещения по Y
                    g.TranslateTransform(X, Y);

                    // Вращаем круг
                    g.RotateTransform(angle * 180 / (float)Math.PI);

                    // Рисуем круг
                    g.DrawEllipse(pen, -radius / 2, -radius / 2, radius, radius);

                    g.Restore(state);
                }

                // Обновляем угол для вращения каждого круга
                circleAngles[i] -= 0.04f * (1 + i * 0.5f);
                if (circleAngles[i] > 2 * Math.PI) circleAngles[i] -= 2 * (float)Math.PI;
            }

            // Надпись в центре урагана
            string text = "Я ураган";
            using (Font font = new Font("Arial", 16, FontStyle.Bold))
            {
                SizeF textSize = g.MeasureString(text, font);
                float textX = X - textSize.Width / 2;
                float textY = Y - textSize.Height / 2;
                g.DrawString(text, font, Brushes.MediumSlateBlue, textX, textY);
            }
        }
    }
}
