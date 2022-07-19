using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE
{
    public class SequenceDrawer : ISubDrawer
    {
        public SequenceDrawer(Drawer drawer)
        {
            Register(typeof(Sequence), drawer);
        }
        public bool Paint(IGeoEntities entity, Canvas canvas, Color color, string label)
        {
            Sequence a = entity as Sequence;
            canvas.SetColor(color);
            if (a.InsideType == new GeoPoint().Type())
            {
                foreach (var item in a.FiniteStorage)
                {
                    GeoPoint aux = item as GeoPoint;
                    canvas.DrawPoint(new Point((int)aux.X, (int)aux.Y), label);
                    
                }
                canvas.Refresh();
                return true;
            }
            if (a.InsideType == new Circle(new GeoPoint(), new Measure()).Type())
            {
                foreach (var item in a.FiniteStorage)
                {
                    Circle aux = item as Circle;
                    canvas.DrawCircle(new Point((int)aux.Center.X, (int)aux.Center.Y), (int)aux.Radius.Value, label);
                    

                }
                canvas.Refresh();
                return true;
            }
            if(a.InsideType==new Line(new GeoPoint(),new GeoPoint()).Type())
            {
                foreach (var item in a.FiniteStorage)
                {
                    Line aux = item as Line;
                    canvas.DrawLine(new Point((int)aux.p1.X, (int)aux.p1.Y),new Point( (int)aux.p2.X,(int)aux.p2.Y), label);
                   
                }
                canvas.Refresh();
                return true;
            }
            if (a.InsideType == new Segment(new GeoPoint(), new GeoPoint()).Type())
            {
                foreach (var item in a.FiniteStorage)
                {
                    Segment aux = item as Segment;
                    canvas.DrawSegment(new Point((int)aux.Starts.X, (int)aux.Starts.Y), new Point((int)aux.End.X, (int)aux.End.Y), label);
                    

                }
                canvas.Refresh();
                return true;
            }
            if (a.InsideType == new Ray(new GeoPoint(), new GeoPoint()).Type())
            {
                foreach (var item in a.FiniteStorage)
                {
                    Ray aux = item as Ray;
                    canvas.DrawRay(new Point((int)aux.StarsFrom.X, (int)aux.StarsFrom.Y), new Point((int)aux.GivenPoint.X, (int)aux.GivenPoint.Y), label);


                }
                canvas.Refresh();
                return true;
            }


            canvas.Refresh();

            return false; ;
        }

        public void Register(Type geoentity, Drawer painter)
        {
            painter.RegisterDrawer(geoentity, this);
        }
    }
}
