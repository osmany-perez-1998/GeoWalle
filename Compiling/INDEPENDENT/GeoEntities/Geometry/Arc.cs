using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Compiling
{
    public class Arc:IGeometry,IPaintable,IIntersectable
    {
        public double Radius { get; private set; }
       
        public double Angle { get; private set; }

        public GeoPoint Center { get; private set; }

        public Ray Ray1 { get; private set; }
        public Ray Ray2 { get; private set; }

        public GeoPoint P1 { get; private set; }
        public GeoPoint P2 { get; private set; }
        
        public Arc(GeoPoint Center,GeoPoint PointInRay1,GeoPoint PointInRay2,Measure Radio)
        {
            this.Center = Center;            
            Angle = FindAngle(Center, PointInRay1, PointInRay2);
            this.Radius = Radio.Value;
            Ray1 = new Ray(Center, PointInRay1);
            Ray2 = new Ray(Center, PointInRay2);
            P1 = PointInRay1;
            P2 = PointInRay2;
        }

        public double FindAngle(GeoPoint Center,GeoPoint PointInRay1,GeoPoint PointInRay2)
        {
            return 1;
            //if (Center.Y == PointInRay1.Y && Center.Y == PointInRay2.Y) return Math.PI;

            //else if(Center.Y == PointInRay1.Y)
            //{
            //    double auxiliar = (Center.Y - PointInRay2.Y) / (Center.X - PointInRay2.Y);
            //    double m = (double)Math.Sqrt(Math.Pow(auxiliar, 2));
            //    double angle = (Math.PI / 2) - Math.Atan(m);
            //    return angle;
            //}
            //else if (Center.Y == PointInRay2.Y)
            //{
            //    double auxiliar = (Center.Y - PointInRay1.Y) / (Center.X - PointInRay1.Y);
            //    double m = (double)Math.Sqrt(Math.Pow(auxiliar, 2));
            //    double angle = (Math.PI / 2) - Math.Atan(m);
            //    return angle;
            //}
            //else
            //{
            //    double auxiliar1 = (Center.Y - PointInRay2.Y) / (Center.X - PointInRay2.Y);
            //    double m1 = (double)Math.Sqrt(Math.Pow(auxiliar1, 2));
            //    double angle1 = (Math.PI / 2) - Math.Atan(m1);

            //    double auxiliar2 = (Center.Y - PointInRay1.Y) / (Center.X - PointInRay1.Y);
            //    double m2 = (double)Math.Sqrt(Math.Pow(auxiliar2, 2));
            //    double angle2 = (Math.PI / 2) - Math.Atan(m2);

            //    return Math.Sqrt(Math.Pow(angle1 - angle2, 2));
            //}


        }
        public string Type() { return "arc"; }
       
    }
}
