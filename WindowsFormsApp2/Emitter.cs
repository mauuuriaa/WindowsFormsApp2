using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    /*public class Emitter
    {
        public List<Particle> particles = new List<Particle>();
        public int MousePositionX;
        public int MousePositionY;

        public float GravitationX = 0;
        public float GravitationY = 1;

        public int X;
        public int Y;
        public int Direction = 0;
        public int Spreading = 360;
        public int SpeedMin = 1;
        public int SpeedMax = 10;
        public int RadiusMin = 2;
        public int RadiusMax = 10;

        public int ParticlesPerTick = 1;
        public float Opacity = 1.0f; // прозрачность частиц

        public Color ColorFrom = Color.White;
        public Color ColorTo = Color.FromArgb(0, Color.Black);

        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();

        public virtual Particle CreateParticle()
        {
            var particle = new ParticleColorful
            {
                FromColor = ColorFrom,
                ToColor = ColorTo,
                Opacity = this.Opacity
            };

            return particle;
        }

        public void UpdateState()
        {
            int particlesToCreate = ParticlesPerTick;

            foreach (var particle in particles)
            {
                foreach (var point in impactPoints)
                {
                    point.ImpactParticle(particle);
                }

                particle.SpeedX += GravitationX;
                particle.SpeedY += GravitationY;

                particle.X += particle.SpeedX;
                particle.Y += particle.SpeedY;
            }

            while (particlesToCreate > 0)
            {
                particlesToCreate--;
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }
        }

        public void Render(Graphics g)
        {
            foreach (var particle in particles)
                particle.Draw(g);

            foreach (var point in impactPoints)
                point.Render(g);
        }

        public virtual void ResetParticle(Particle particle)
        {
            particle.X = X;
            particle.Y = Y;

            var direction = Direction + (double)Particle.rand.Next(Spreading) - Spreading / 2;
            var speed = Particle.rand.Next(SpeedMin, SpeedMax);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle.Radius = Particle.rand.Next(RadiusMin, RadiusMax);
            particle.Opacity = this.Opacity;
        }
    }

    public class TopEmitter : Emitter
    {
        public int Width;

        public override void ResetParticle(Particle particle)
        {
            base.ResetParticle(particle);

            particle.X = Particle.rand.Next(Width);
            particle.Y = 0;
            particle.SpeedY = 1;
            particle.SpeedX = Particle.rand.Next(-2, 3);
        }
    }*/
}
