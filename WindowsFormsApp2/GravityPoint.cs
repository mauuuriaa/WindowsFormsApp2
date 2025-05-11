using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class GravityPoint : IImpactPoint
    {
        public int Power = 100;

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            float r2 = Math.Max(100, gX * gX + gY * gY);

            particle.SpeedX += gX * Power / r2;
            particle.SpeedY += gY * Power / r2;
        }
    }

    // Гравитрон-поглотитель с телепортацией
    public class SinkGravityPoint : GravityPoint
    {
        public SourceGravityPoint SourceTarget;

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            float r2 = Math.Max(100, gX * gX + gY * gY);

            particle.SpeedX += gX * Power / r2;
            particle.SpeedY += gY * Power / r2;

            // Если частица близко - телепортируем её к источнику
            float distance = (float)Math.Sqrt(gX * gX + gY * gY);
            if (distance < 10)
            {
                particle.X = SourceTarget.X;
                particle.Y = SourceTarget.Y;
                particle.SpeedX = 0;
                particle.SpeedY = 0;
            }
        }
    }

    // Гравитрон-источник, из которого частицы вылетают
    public class SourceGravityPoint : IImpactPoint
    {
        public int Power = 50;

        public override void ImpactParticle(Particle particle)
        {
            // Можно добавить эффект отталкивания, если нужно
        }
    }
}