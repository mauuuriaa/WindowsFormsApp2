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
        public abstract void ImpactParticle(Petal petal) ;

        public virtual void Render(Graphics g)
        {
            g.DrawEllipse(Pens.Red, X - 5, Y - 5, 10, 10);
        }
    }

    public class WindPoint : IImpactPoint
    {
        public int Power = 300;
        public bool IsActive = false;

        public override void ImpactParticle(Petal petal)
        {
            if (!IsActive) return;

            float dx = petal.FallX - X;
            float dy = petal.FallY - Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance < 100) // Зона действия ветра
            {
                float force = Power / Math.Max(1, distance);
                petal.FallVX += (dx / distance) * force;
                petal.FallVY += (dy / distance) * force;
            }
        }

        public override void Render(Graphics g)
        {
            if (!IsActive) return;

            g.DrawEllipse(Pens.CornflowerBlue, X - 50, Y - 50, 100, 100);
            g.DrawString("Ветер", new Font("Arial", 10, FontStyle.Bold), Brushes.CornflowerBlue, X + 20, Y - 40);
        }
    }

    public class HurricanePoint : IImpactPoint
    {
        public int Power = 250;
        public float RotationSpeed = 0.15f; // Скорость вращения лепестков
        public float EjectionPower = 8f; // Сила выброса лепестков
        public bool IsActive = false;

        private float angleOffset = 0;

        public override void ImpactParticle(Petal petal)
        {
            if (!IsActive || petal.OnGround || !petal.IsFalling) return;

            // Направление к центру урагана
            float dx = X - petal.FallX;
            float dy = Y - petal.FallY;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            // Притягиваем лепесток
            if (distance > 10) // не втягиваем слишком близко
            {
                float attractionForce = Power / distance;
                petal.FallVX += (dx / distance) * attractionForce * 0.2f;
                petal.FallVY += (dy / distance) * attractionForce * 0.2f;
            }

            // Закручиваем лепесток
            float angle = (float)Math.Atan2(dy, dx) + angleOffset;
            float rotationForce = Power / (distance + 1) * RotationSpeed;
            petal.FallVX += (float)Math.Cos(angle) * rotationForce;
            petal.FallVY += (float)Math.Sin(angle) * rotationForce;

            // Если лепесток достиг центра, выбрасываем его
            if (distance < 30)
            {
                float ejectAngle = (float)(Math.Atan2(dy, dx) + Math.PI);
                petal.FallVX = (float)Math.Cos(ejectAngle) * EjectionPower;
                petal.FallVY = (float)Math.Sin(ejectAngle) * EjectionPower;
            }
        }

        public override void Render(Graphics g)
        {
            if (!IsActive) return;

            // Рисуем центральную воронку
            int size = 120;
            g.FillEllipse(new SolidBrush(Color.FromArgb(150, Color.SlateBlue)), X - size / 2, Y - size / 2, size, size);

            // Вращающийся эффект
            for (int i = 0; i < 8; i++)
            {
                float angle = i * (float)Math.PI / 4 + angleOffset;
                float length = size / 2;
                float x1 = X + (float)Math.Cos(angle) * 20;
                float y1 = Y + (float)Math.Sin(angle) * 20;
                float x2 = X + (float)Math.Cos(angle) * length;
                float y2 = Y + (float)Math.Sin(angle) * length;

                g.DrawLine(new Pen(Color.CornflowerBlue, 2), x1, y1, x2, y2);
            }

            // Медленно увеличиваем угол для эффекта вращения
            angleOffset += 0.05f;
            if (angleOffset > 2 * Math.PI) angleOffset -= 2 * (float)Math.PI;
        }
    }


}
    
