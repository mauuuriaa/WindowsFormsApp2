using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{


    public class AntiGravityPoint : IImpactPoint
    {
        public int Power = 100;

        public override void ImpactParticle(Petal petal)
        {
            //if (!petal.IsFalling || petal.IsGone) return;

            float gX = X - petal.FallX;
            float gY = Y - petal.FallY;
            float r2 = (float)System.Math.Max(100, gX * gX + gY * gY);

            petal.FallVX -= gX * Power / r2;
            petal.FallVY -= gY * Power / r2;
        }

        public override void Render(Graphics g)
        {
            g.DrawEllipse(System.Drawing.Pens.DeepSkyBlue, X - 30, Y - 30, 60, 60);
            g.DrawString("Гравитрон", new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.DeepSkyBlue, X + 10, Y - 30);
        }
    }
}