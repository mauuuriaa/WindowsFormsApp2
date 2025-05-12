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


}
    
