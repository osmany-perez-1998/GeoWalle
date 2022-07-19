using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public static class Intersections
    {
        public static bool BelongToArc(Arc arc, GeoPoint point)
        {
            var circle = new Circle(arc.Center, new Measure(arc.Radius));

            var vector1 = new GeoPoint(arc.P1.X - arc.Center.X, arc.P1.Y - arc.Center.Y);
            var vector2 = new GeoPoint(arc.P2.X - arc.Center.X, arc.P2.Y - arc.Center.Y);
            var vectorCenter = new GeoPoint(point.X - arc.Center.X, point.Y - arc.Center.Y);

            var measure = new Measure(point, circle.Center);
            if (measure.Value != circle.Radius.Value) return false;
            var compZ1 = vector1.X * vectorCenter.Y - vector1.Y * vectorCenter.X;
            var compZ2 = vector2.X * vectorCenter.Y - vector2.Y * vectorCenter.X;
            if (compZ1 > 0)
            {
                if (compZ2 < 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static Double Angle(BehavesLikeLine l1, BehavesLikeLine l2)
        {
            if (l1.By == 0 && l2.By == 0) return 0;

            if (l1.By == 0)
            {
                return Math.PI / 2 - Math.Atan(Math.Sqrt(Math.Pow(l2.Pendiente, 2)));
            }
            if (l2.By == 0)
            {
                return Math.PI / 2 - Math.Atan(Math.Sqrt(Math.Pow(l2.Pendiente, 2)));
            }
            return Math.Atan(Math.Sqrt(Math.Pow((l1.Pendiente - l2.Pendiente) / (1 - l1.Pendiente * l2.Pendiente), 2)));
        }

        public static IGeoEntities Intersect(IGeoEntities a1, IGeoEntities a2)
        {
            switch (a1.Type())
            {
                case "point":
                    {
                        switch (a2.Type())
                        {
                            case "point":
                                {
                                    return Intersect(a1 as GeoPoint, a2 as GeoPoint);
                                }
                            case "segment":
                                {
                                    return Intersect(a1 as GeoPoint, a2 as Segment);
                                }
                            case "line":
                                {
                                    return Intersect(a1 as GeoPoint, a2 as Line);
                                }
                            case "ray":
                                {
                                    return Intersect(a1 as GeoPoint, a2 as Ray);
                                }
                            case "circle":
                                {
                                    return Intersect(a1 as GeoPoint, a2 as Circle);
                                }
                            case "arc":
                                {
                                    return Intersect(a1 as GeoPoint, a2 as Arc);
                                }

                        }
                        break;
                    }
                case "line":
                    {
                        switch (a2.Type())
                        {
                            case "point":
                                {
                                    return Intersect(a1 as Line, a2 as GeoPoint);
                                }
                            case "segment":
                                {
                                    return Intersect(a1 as Line, a2 as Segment);
                                }
                            case "line":
                                {
                                    return Intersect(a1 as Line, a2 as Line);
                                }
                            case "ray":
                                {
                                    return Intersect(a1 as Line, a2 as Ray);
                                }
                            case "circle":
                                {
                                    return Intersect(a1 as Line, a2 as Circle);
                                }
                            case "arc":
                                {
                                    return Intersect(a1 as Line, a2 as Arc);
                                }

                        }
                        break;
                    }
                case "segment":
                    {
                        switch (a2.Type())
                        {

                            case "point":
                                {
                                    return Intersect(a1 as Segment, a2 as GeoPoint);
                                }
                            case "segment":
                                {
                                    return Intersect(a1 as Segment, a2 as Segment);
                                }
                            case "line":
                                {
                                    return Intersect(a1 as Segment, a2 as Line);
                                }
                            case "ray":
                                {
                                    return Intersect(a1 as Segment, a2 as Ray);
                                }
                            case "circle":
                                {
                                    return Intersect(a1 as Segment, a2 as Circle);
                                }
                            case "arc":
                                {
                                    return Intersect(a1 as Segment, a2 as Arc);
                                }

                        }
                        break;
                    }
                case "ray":
                    {
                        switch (a2.Type())
                        {

                            case "point":
                                {
                                    return Intersect(a1 as Ray, a2 as GeoPoint);
                                }
                            case "segment":
                                {
                                    return Intersect(a1 as Ray, a2 as Segment);
                                }
                            case "line":
                                {
                                    return Intersect(a1 as Ray, a2 as Line);
                                }
                            case "ray":
                                {
                                    return Intersect(a1 as Ray, a2 as Ray);
                                }
                            case "circle":
                                {
                                    return Intersect(a1 as Ray, a2 as Circle);
                                }
                            case "arc":
                                {
                                    return Intersect(a1 as Ray, a2 as Arc);
                                }

                        }
                        break;
                    }

                case "circle":
                    {
                        switch (a2.Type())
                        {

                            case "point":
                                {
                                    return Intersect(a1 as Circle, a2 as GeoPoint);
                                }
                            case "segment":
                                {
                                    return Intersect(a1 as Circle, a2 as Segment);
                                }
                            case "line":
                                {
                                    return Intersect(a1 as Circle, a2 as Line);
                                }
                            case "ray":
                                {
                                    return Intersect(a1 as Circle, a2 as Ray);
                                }
                            case "circle":
                                {
                                    return Intersect(a1 as Circle, a2 as Circle);
                                }
                            case "arc":
                                {
                                    return Intersect(a1 as Circle, a2 as Arc);
                                }

                        }
                        break;
                    }
                case "arc":
                    {
                        switch (a2.Type())
                        {

                            case "point":
                                {
                                    return Intersect(a1 as Arc, a2 as GeoPoint);
                                }
                            case "segment":
                                {
                                    return Intersect(a1 as Arc, a2 as Segment);
                                }
                            case "line":
                                {
                                    return Intersect(a1 as Arc, a2 as Line);
                                }
                            case "ray":
                                {
                                    return Intersect(a1 as Arc, a2 as Ray);
                                }
                            case "circle":
                                {
                                    return Intersect(a1 as Arc, a2 as Circle);
                                }
                            case "arc":
                                {
                                    return Intersect(a1 as Arc, a2 as Arc);
                                }

                        }
                        break;
                    }
                    
            }
            return null;


        }           

        #region Point Intersects
        public static Double Distance(GeoPoint p1, GeoPoint p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
        public static IGeoEntities Intersect(GeoPoint p1, GeoPoint p2)
        {
            if (p1 == p2) return p1;
            else return null;
        }

        public static IGeoEntities Intersect(GeoPoint p1, Line l1)
        {
            if (l1.Ax * p1.X + l1.By * p1.Y + l1.C == 0) return p1;
            else return null;
        }

        public static IGeoEntities Intersect(GeoPoint p1, Segment s1)
        {
            double SegmentLength = Distance(s1.Starts, s1.End);
            if (s1.Ax * p1.X + s1.By * p1.Y + s1.C == 0)
            {
                if (Distance(s1.Starts, p1) <= SegmentLength && Distance(s1.End, p1) <= SegmentLength)
                    return p1;
                return null;
            }
            return null;
        }

        public static IGeoEntities Intersect(GeoPoint p1, Ray r1)
        {
            double pendiente = r1.Pendiente;
            if (r1.Ax * p1.X + r1.By * p1.Y + r1.C == 0)
            {
                if ((pendiente < 0 && p1.X < r1.StarsFrom.X && p1.Y < r1.StarsFrom.Y) || (pendiente > 0 && p1.X > r1.StarsFrom.X && p1.Y > r1.StarsFrom.Y))
                    return p1;

                else return null;
            }

            return null;
        }

        public static IGeoEntities Intersect(GeoPoint p1, Circle c1)
        {
            if ((c1.Center.X - p1.X) * (c1.Center.X - p1.X) + (c1.Center.Y - p1.Y) * (c1.Center.Y - p1.Y) == c1.Radius.Value * c1.Radius.Value)
                return p1;

            else return null;
        }

        public static IGeoEntities Intersect(GeoPoint p1, Arc ar1)
        {
            if (BelongToArc(ar1, p1)) return new Sequence(new List<IGeoEntities> { p1 });
            return new Sequence();
        }

        #endregion

        #region Line Intersects
        public static IGeoEntities Intersect(Line l1, GeoPoint p1)
        {
            return Intersect(p1, l1);
        }
        public static IGeoEntities Intersect(Line l1, Line l2)
        {
            List<IGeoEntities> result = new List<IGeoEntities>();

            Line first = l1 as Line;
            Line second = l2 as Line;

            if (first.Ax == second.Ax && first.By == second.By && first.C == second.C)
                return new Undefined();
            double x; double y;
            if (first.By != 0 && second.By != 0)
            {
                x = (-second.C / second.By - (-first.C / first.By)) / (-first.Ax / first.By - (-second.Ax / second.By));
                y = (-first.Ax / first.By) * x + (-first.C / first.By);
                result.Add(new GeoPoint(x, y));
                return new Sequence(result);
            }
            if (first.By == 0 && second.By != 0)
            {
                y = (-second.Ax * (-first.C / first.Ax) - second.C) / second.By;
                x = (-second.By - second.C) / second.By;
                result.Add(new GeoPoint(x, y));
                return new Sequence(result);
            }
            y = (-first.Ax * (-second.C / second.Ax) - first.C) / first.By;
            x = (-first.By - first.C) / first.By;
            result.Add(new GeoPoint(x, y));
            return new Sequence(result);


        }
        public static IGeoEntities Intersect(Line figure1, Ray figure2)
        {
            var line = figure1 as Line;
            if (line.p1.X == figure2.StarsFrom.X && line.p1.Y == figure2.StarsFrom.Y && line.p2.X == figure2.GivenPoint.X && line.p1.Y == figure2.GivenPoint.Y)
            {
                return new Undefined();
            }
            else if (line.Ax * figure2.By - figure2.Ax * line.By == 0)
            {
                return new Sequence();
            }

            else if (line.Ax / figure2.Ax == line.By / figure2.By && line.By / figure2.By == line.C / figure2.C)
            {
                return new Sequence();
            }
            double A = line.Ax - figure2.Ax;
            double B = line.By - figure2.By;
            double C = line.C - figure2.C;
            int x = (int)((-line.C + (line.By * C) / B) / (line.Ax - (line.By * A) / B));
            int y = (int)((-C + (-A * x)) / B);
            int dx = (int)(figure2.GivenPoint.X - figure2.StarsFrom.X);
            int dy = (int)(figure2.GivenPoint.Y - figure2.StarsFrom.Y);
            GeoPoint farRay = new GeoPoint(figure2.GivenPoint.X + dx * 100, figure2.GivenPoint.Y + dy * 100);
            double resultRayX = (double)(x - figure2.StarsFrom.X) / (farRay.X - figure2.StarsFrom.X);
            double resultRayY = (double)(y - figure2.StarsFrom.Y) / (farRay.Y - figure2.StarsFrom.Y);
            if ((0 <= resultRayX && resultRayX <= 1) && (0 <= resultRayY && resultRayY <= 1))
            {
                return new Sequence(new List<IGeoEntities> { new GeoPoint(x, y) });
            }
            return new Sequence();
        }
        public static IGeoEntities Intersect(Line figure1, Segment figure2)
        {
            var line = figure1 as Line;
            if (line.p1.X == figure2.Starts.X && line.p1.Y == figure2.Starts.Y && line.p2.X == figure2.End.X && line.p2.Y == figure2.End.Y)
            {
                return new Undefined();
            }
            else if (line.Ax * figure2.By - figure2.Ax * line.By == 0)
            {
                return new Sequence();
            }

            else if (line.Ax / figure2.Ax == line.By / figure2.By && line.By / figure2.By == line.C / figure2.C)
            {
                return new Sequence();
            }
            double A = line.Ax - figure2.Ax;
            double B = line.By - figure2.By;
            double C = line.C - figure2.C;
            int x = (int)((-line.C + (line.By * C) / B) / (line.Ax - (line.By * A) / B));
            int y = (int)((-C + (-A * x)) / B);
            double resultSegmentX = (double)(x - figure2.Starts.X) / (figure2.End.X - figure2.Starts.X);
            double resultSegmentY = (double)(y - figure2.Starts.Y) / (figure2.End.Y - figure2.Starts.Y);
            if ((0 <= resultSegmentX && resultSegmentX <= 1) && (0 <= resultSegmentY && resultSegmentY <= 1))
            {
                return new Sequence(new List<IGeoEntities> { new GeoPoint(x, y) });
            }
            return new Sequence();
        }
        public static IGeoEntities Intersect(Line l1, Circle c1)
        {
            return Intersect(c1, l1);
        }
        public static IGeoEntities Intersect(Line figure1, Arc figure2)
        {
            {
                var line = figure1 as Line;
                var circle = new Circle(figure2.Center, new Measure(figure2.Radius));
                double a = (Math.Pow(line.By, 2) + Math.Pow(line.Ax, 2)) / Math.Pow(line.By, 2);
                double b = (2 * line.Ax * line.C) / Math.Pow(line.By, 2) + circle.D - (circle.E * line.Ax) / line.By;
                double c = Math.Pow(line.C, 2) / Math.Pow(line.By, 2) - circle.E * line.C / line.By + circle.F;
                Discriminante d = new Discriminante(a, b, c);
                var discriminante = d.CalculateDiscriminante();
                if (discriminante == 0)
                {
                    int x;
                    d.CalculateOneValue(out x);
                    int y = (int)((-line.C - line.Ax * x) / line.By);
                    var point = new GeoPoint(x, y);
                    if (BelongToArc(figure2, point))
                        return new Sequence(new List<IGeoEntities> { point });
                }
                else if (discriminante < 0)
                {
                    return new Sequence();
                }

                int x1;
                int x2;
                d.CalculateValues(out x1, out x2);
                int y1 = (int)((-line.C - line.Ax * x1) / line.By);
                int y2 = (int)((-line.C - line.Ax * x2) / line.By);
                var point1 = new GeoPoint(x1, y2);
                var point2 = new GeoPoint(x2, y2);
                if (BelongToArc(figure2, point1))
                {
                    if (BelongToArc(figure2, point2))
                    {
                        return new Sequence(new List<IGeoEntities> { point1, point2 });
                    }
                    return new Sequence(new List<IGeoEntities> { point1 });
                }
                if (BelongToArc(figure2, point2))
                {
                    return new Sequence(new List<IGeoEntities> { point2 });
                }
                return new Sequence();
            }

        }


        #endregion

        #region Ray Intersects
        public static IGeoEntities Intersect(Ray r1, GeoPoint p1)
        {
            return Intersect(p1, r1);
        }

        public static IGeoEntities Intersect(Ray r1, Line l1)
        {
            return Intersect(l1, r1);
        }

        public static IGeoEntities Intersect(Ray figure1, Ray figure2)
        {
            var ray = figure1 as Ray;
            if (ray.StarsFrom.X == ray.StarsFrom.X && ray.StarsFrom.Y == figure2.StarsFrom.Y && ray.StarsFrom.X == figure2.GivenPoint.X && ray.GivenPoint.Y == figure2.GivenPoint.Y)
            {
                return new Undefined();
            }
            else if (ray.Ax * figure2.By - figure2.Ax * ray.By == 0)
            {
                return new Sequence();
            }

            int x = (int)((ray.By * figure2.C - figure2.By * ray.C) / (ray.Ax * figure2.By - figure2.Ax * ray.By));
            int y = (int)((ray.C * figure2.Ax - figure2.C * ray.Ax) / (ray.Ax * figure2.By - figure2.Ax * ray.By));
            int dx = (int)(ray.GivenPoint.X - ray.StarsFrom.X);
            int dy = (int)(ray.GivenPoint.Y - ray.StarsFrom.Y);
            GeoPoint farRay = new GeoPoint(ray.GivenPoint.X + dx * 100, ray.GivenPoint.Y + dy * 100);
            int dxFigure2 = (int)(figure2.GivenPoint.X - figure2.StarsFrom.X);
            int dyFigure2 = (int)(figure2.GivenPoint.Y - figure2.StarsFrom.Y);
            GeoPoint farFigure2 = new GeoPoint(figure2.GivenPoint.X + dxFigure2 * 100, figure2.GivenPoint.Y + dyFigure2 * 100);
            double resultRayX = (double)(x - ray.StarsFrom.X) / (farRay.X - ray.StarsFrom.X);
            double resultRayY = (double)(y - ray.StarsFrom.Y) / (farRay.Y - ray.StarsFrom.Y);
            double resultFigure2X = (double)(x - figure2.StarsFrom.X) / (farFigure2.X - figure2.StarsFrom.X);
            double resultFigure2Y = (double)(y - figure2.StarsFrom.Y) / (farFigure2.Y - figure2.StarsFrom.Y);
            if ((0 <= resultRayX && resultRayX <= 1) && (0 <= resultRayY && resultRayY <= 1))
            {
                if ((0 <= resultFigure2X && resultFigure2X <= 1) && (0 <= resultFigure2Y && resultFigure2Y <= 1))
                {
                    return new Sequence(new List<IGeoEntities> { new GeoPoint(x, y) });
                }
            }

            return new Sequence();
        }

        public static IGeoEntities Intersect(Ray figure1, Segment figure2)
        {
            var ray = figure1 as Ray;
            if ((figure2.Starts.Y == figure2.End.Y && figure2.End.Y == ray.StarsFrom.Y) && (ray.StarsFrom.X < figure2.Starts.X || ray.StarsFrom.X < figure2.End.X))
            {
                return new Undefined();
            }
            if (figure2.Starts.Y == figure2.End.Y || figure2.Ax * ray.By - ray.Ax * figure2.By == 0)
            {
                return new Sequence();
            }

            double A = figure2.Ax - ray.Ax;
            double B = figure2.By - ray.By;
            double C = figure2.C - ray.C;
            int x = (int)((-figure2.C + (figure2.By * C) / B) / (figure2.Ax - (figure2.By * A) / B));
            int y = (int)((-C + (-A * x)) / B);

            double resultSegmentX = (double)(x - figure2.Starts.X) / (figure2.End.X - figure2.Starts.X);
            double resultSegmentY = (double)(y - figure2.Starts.Y) / (figure2.End.Y - figure2.Starts.Y);
            if ((0 <= resultSegmentX && resultSegmentX <= 1) && (0 <= resultSegmentY && resultSegmentY <= 1))
            {
                int dx = (int)(ray.GivenPoint.X - ray.StarsFrom.X);
                int dy = (int)(ray.GivenPoint.Y - ray.StarsFrom.Y);
                GeoPoint farRay = new GeoPoint(ray.GivenPoint.X + dx * 100, ray.GivenPoint.Y + dy * 100);
                double resultRayX = (double)(x - ray.StarsFrom.X) / (farRay.X - ray.StarsFrom.X);
                double resultRayY = (double)(y - ray.StarsFrom.Y) / (farRay.Y - ray.StarsFrom.Y);
                if ((0 <= resultRayX && resultRayX <= 1) && (0 <= resultRayY && resultRayY <= 1))
                {
                    return new Sequence(new List<IGeoEntities> { new GeoPoint(x, y) });
                }

            }
            return new Sequence();
        }

        public static IGeoEntities Intersect(Ray r1, Circle c1)
        {
            return Intersect(c1, r1);
        }

        public static IGeoEntities Intersect(Ray figure1, Arc figure2)
        {
            var ray = figure1 as Ray;
            var circle = new Circle(figure2.Center, new Measure(figure2.Radius));
            double a = (Math.Pow(ray.By, 2) + Math.Pow(ray.Ax, 2)) / Math.Pow(ray.By, 2);
            double b = (2 * ray.Ax * ray.C) / Math.Pow(ray.By, 2) + circle.D - (circle.E * ray.Ax) / ray.By;
            double c = Math.Pow(ray.C, 2) / Math.Pow(ray.By, 2) - circle.E * ray.C / ray.By + circle.F;
            Discriminante d = new Discriminante(a, b, c);
            var discriminante = d.CalculateDiscriminante();
            if (discriminante == 0)
            {
                int x;
                d.CalculateOneValue(out x);
                int y = (int)((-ray.C - ray.Ax * x) / ray.By);
                var point = new GeoPoint(x, y);
                if (BelongToArc(figure2, point))
                    return new Sequence(new List<IGeoEntities> { point });
            }
            else if (discriminante < 0)
            {
                return new Sequence();
            }

            int x1;
            int x2;
            d.CalculateValues(out x1, out x2);
            int y1 = (int)((-ray.C - ray.Ax * x1) / ray.By);
            int y2 = (int)((-ray.C - ray.Ax * x2) / ray.By);
            int dx = (int)(ray.GivenPoint.X - ray.StarsFrom.X);
            int dy = (int)(ray.GivenPoint.Y - ray.StarsFrom.Y);
            GeoPoint farRay = new GeoPoint(ray.GivenPoint.X + dx * 100, ray.GivenPoint.Y + dy * 100);
            double resultRayX1 = (double)(x1 - ray.StarsFrom.X) / (farRay.X - ray.StarsFrom.X);
            double resultRayY1 = (double)(y1 - ray.StarsFrom.Y) / (farRay.Y - ray.StarsFrom.Y);
            double resultRayX2 = (double)(x2 - ray.StarsFrom.X) / (farRay.X - ray.StarsFrom.X);
            double resultRayY2 = (double)(y2 - ray.StarsFrom.Y) / (farRay.Y - ray.StarsFrom.Y);
            var point1 = new GeoPoint(x1, y2);
            var point2 = new GeoPoint(x2, y2);
            if ((0 <= resultRayX1 && resultRayX1 <= 1) && (0 <= resultRayY1 && resultRayY1 <= 1))
            {
                if ((0 <= resultRayX2 && resultRayX2 <= 1) && (0 <= resultRayY2 && resultRayY2 <= 1))
                {
                    if (BelongToArc(figure2, point1))
                    {
                        if (BelongToArc(figure2, point2))
                        {
                            return new Sequence(new List<IGeoEntities> { point1, point2 });
                        }
                        return new Sequence(new List<IGeoEntities> { point1 });
                    }
                    if (BelongToArc(figure2, point2))
                    {
                        return new Sequence(new List<IGeoEntities> { point2 });
                    }
                }
                if (BelongToArc(figure2, point1))
                    return new Sequence(new List<IGeoEntities> { point1 });
            }

            else if ((0 <= resultRayX2 && resultRayX2 <= 1) && (0 <= resultRayY2 && resultRayY2 <= 1))
                if (BelongToArc(figure2, point1))
                {
                    if (BelongToArc(figure2, point2))
                    {
                        return new Sequence(new List<IGeoEntities> { point1, point2 });
                    }
                    return new Sequence(new List<IGeoEntities> { point1 });
                }
            if (BelongToArc(figure2, point2))
            {
                return new Sequence(new List<IGeoEntities> { point2 });
            }

            return new Sequence();
        }
        #endregion

        #region Segment Intersects

        public static IGeoEntities Intersect(Segment s1, GeoPoint p1)
        {
            return Intersect(p1, s1);
        }
        public static IGeoEntities Intersect(Segment s1, Line l1)
        {
            return Intersect(l1, s1);
        }
        public static IGeoEntities Intersect(Segment s1, Ray r1)
        {
            return Intersect(r1, s1);
        }
        public static IGeoEntities Intersect(Segment s1, Circle c1)
        {
            return Intersect(c1, s1);
        }

        public static IGeoEntities Intersect(Segment figure1, Segment figure2)
        {
            var segment = figure1 as Segment;
            if (segment.Starts.X == segment.Starts.X && segment.Starts.Y == figure2.Starts.Y && segment.End.X == figure2.End.X && segment.End.Y == figure2.End.Y)
            {
                return new Undefined();
            }
            else if (segment.Ax * figure2.By - figure2.Ax * segment.By == 0)
            {
                return new Sequence();
            }

            int x = (int)((segment.By * figure2.C - figure2.By * segment.C) / (segment.Ax * figure2.By - figure2.Ax * segment.By));
            int y = (int)((segment.C * figure2.Ax - figure2.C * segment.Ax) / (segment.Ax * figure2.By - figure2.Ax * segment.By));
            double resultSegmentX = (double)(x - segment.Starts.X) / (segment.End.X - segment.Starts.X);
            double resultSegmentY = (double)(y - segment.Starts.Y) / (segment.End.Y - segment.Starts.Y);
            double resultFigure2X = (double)(x - figure2.Starts.X) / (figure2.End.X - figure2.Starts.X);
            double resultFigure2Y = (double)(y - figure2.Starts.Y) / (figure2.End.Y - figure2.Starts.Y);
            if ((0 <= resultSegmentX && resultSegmentX <= 1) && (0 <= resultSegmentY && resultSegmentY <= 1))
            {
                if ((0 <= resultFigure2X && resultFigure2X <= 1) && (0 <= resultFigure2Y && resultFigure2Y <= 1))
                {
                    return new Sequence(new List<IGeoEntities> { new GeoPoint(x, y) });
                }
            }

            return new Sequence();
        }

        public static IGeoEntities Intersect(Segment figure1, Arc figure2)
        {
            var segment = figure1 as Segment;
            var circle = new Circle(figure2.Center, new Measure(figure2.Radius));
            double a = (Math.Pow(segment.By, 2) + Math.Pow(segment.Ax, 2)) / Math.Pow(segment.By, 2);
            double b = (2 * segment.Ax * segment.C) / Math.Pow(segment.By, 2) + circle.D - (circle.E * segment.Ax) / segment.By;
            double c = Math.Pow(segment.C, 2) / Math.Pow(segment.By, 2) - circle.E * segment.C / segment.By + circle.F;
            Discriminante d = new Discriminante(a, b, c);
            var discriminante = d.CalculateDiscriminante();
            if (discriminante == 0)
            {
                int x;
                d.CalculateOneValue(out x);
                int y = (int)((-segment.C - segment.Ax * x) / segment.By);
                var point = new GeoPoint(x, y);
                if (BelongToArc(figure2,point))
                    return new Sequence(new List<IGeoEntities> { point });
            }
            else if (discriminante < 0)
            {
                return new Sequence();
            }

            int x1;
            int x2;
            d.CalculateValues(out x1, out x2);
            int y1 = (int)((-segment.C - segment.Ax * x1) / segment.By);
            int y2 = (int)((-segment.C - segment.Ax * x2) / segment.By);
            double resultSegmentX1 = (double)(x1 - segment.Starts.X) / (segment.End.X - segment.Starts.X);
            double resultSegmentY1 = (double)(y1 - segment.Starts.Y) / (segment.End.Y - segment.Starts.Y);
            double resultSegmentX2 = (double)(x2 - segment.Starts.X) / (segment.End.X - segment.Starts.X);
            double resultSegmentY2 = (double)(y2 - segment.Starts.Y) / (segment.End.Y - segment.Starts.Y);
            var point1 = new GeoPoint(x1, y2);
            var point2 = new GeoPoint(x2, y2);
            if ((0 <= resultSegmentX1 && resultSegmentX1 <= 1) && (0 <= resultSegmentY1 && resultSegmentY1 <= 1))
            {
                if ((0 <= resultSegmentX2 && resultSegmentX2 <= 1) && (0 <= resultSegmentY2 && resultSegmentY2 <= 1))
                {
                    if (BelongToArc(figure2,point1))
                    {
                        if (BelongToArc(figure2,point2))
                        {
                            return new Sequence(new List<IGeoEntities> { point1, point2 });
                        }
                        return new Sequence(new List<IGeoEntities> { point1 });
                    }
                    if (BelongToArc(figure2,point2))
                    {
                        return new Sequence(new List<IGeoEntities> { point2 });
                    }
                }
                if (BelongToArc(figure2,point1))
                    return new Sequence(new List<IGeoEntities> { point1 });
            }
            if ((0 <= resultSegmentX2 && resultSegmentX2 <= 1) && (0 <= resultSegmentY2 && resultSegmentY2 <= 1))
            {
                if (BelongToArc(figure2,point1))
                {
                    if (BelongToArc(figure2,point2))
                    {
                        return new Sequence(new List<IGeoEntities> { point1, point2 });
                    }
                    return new Sequence(new List<IGeoEntities> { point1 });
                }
                if (BelongToArc(figure2,point2))
                {
                    return new Sequence(new List<IGeoEntities> { point2 });
                }
            }
            return new Sequence();
        }

        #endregion

        #region Arc Intersect
        public static IGeoEntities Intersect(Arc ar1, GeoPoint p2)
        {
            return Intersect(p2, ar1);
        }
        public static IGeoEntities Intersect(Arc ar1, Line l1)
        {
            return Intersect(l1, ar1);
        }

        public static IGeoEntities Intersect(Arc ar1, Segment s1)
        {
            return Intersect(s1, ar1);

        }
        public static IGeoEntities Intersect(Arc ar1, Ray r1)
        {
            return Intersect(r1, ar1);
        }
        public static IGeoEntities Intersect(Arc ar1, Circle c1)
        {
            return Intersect(c1, ar1);
        }

        public static IGeoEntities Intersect(Arc figure1, Arc figure2)
        {
            var arc = figure1 as Arc;
            var arc1 = figure2 as Arc;
            var circle = new Circle(arc.Center, new Measure(arc.Radius));
            var circle1 = new Circle(arc1.Center, new Measure(arc1.Radius));

            if (circle.Center.X == circle1.Center.X && circle.Center.Y == circle.Center.Y && circle.Radius.Value == circle1.Radius.Value)
            {
                return new Undefined();
            }

           
            var measure = new Measure(circle.Center, circle1.Center);
            if (measure.Value == circle.Radius.Value + circle1.Radius.Value)
            {
                int x =(int) (circle.Center.X + circle1.Center.X) / 2;
                int y =(int) (circle.Center.Y + circle1.Center.Y) / 2;
                var point = new GeoPoint(x, y);
                if (BelongToArc(arc,point))
                    return new Sequence(new List<IGeoEntities> { point });
            }
            else if (measure.Value < circle.Radius.Value + circle1.Radius.Value)
            {
                double D = circle.D - circle1.D;
                double E = circle.E - circle1.E;
                double F = circle.F - circle1.F;
                double a = (Math.Pow(E, 2) + Math.Pow(D, 2)) / Math.Pow(E, 2);
                double b = circle.D + (2 * F * D) / Math.Pow(E, 2) + (-circle.E * D) / E;
                double c = Math.Pow(F, 2) / Math.Pow(E, 2) + (-circle.E * F) / E + circle.F;
                Discriminante d = new Discriminante(a, b, c);
                var discriminante = d.CalculateDiscriminante();
                if (discriminante < 0)
                {
                    return new Sequence();
                }
                else if (discriminante == 0)
                {
                    int x;
                    d.CalculateOneValue(out x);
                    int y = (int)((-F - D * x) / E);
                    return new Sequence(new List<IGeoEntities> { new GeoPoint(x, y) });
                }

                int x1;
                int x2;
                d.CalculateValues(out x1, out x2);
                int y1 = (int)((-F - D * x1) / E);
                int y2 = (int)((-F - D * x2) / E);
                return new Sequence(new List<IGeoEntities> { new GeoPoint(x1, y1), new GeoPoint(x2, y2) });
            }
            return new Sequence();
        }
        #endregion

        #region Circle Intersects

        public static IGeoEntities Intersect(Circle figure1, GeoPoint figure2)
        {
            return Intersect(figure2, figure1);
        }
        public static IGeoEntities Intersect(Circle figure1, Circle figure2)
        {

            var circle = figure1 as Circle;
            if (circle.Center.X == figure2.Center.X && circle.Center.Y == figure2.Center.Y && circle.Radius.Value == figure2.Radius.Value)
            {
                return new Undefined();
            }


            var measure = new Measure(circle.Center, figure2.Center);
            if (measure.Value == circle.Radius.Value + figure2.Radius.Value)
            {
                int x = (int)(circle.Center.X + figure2.Center.X) / 2;
                int y = (int)(circle.Center.Y + figure2.Center.Y) / 2;
                return new Sequence(new List<IGeoEntities> { new GeoPoint(x, y) });
            }
            else if (measure.Value < circle.Radius.Value + figure2.Radius.Value)
            {
                double D = circle.D - figure2.D;
                double E = circle.E - figure2.E;
                double F = circle.F - figure2.F;
                double a = (Math.Pow(E, 2) + Math.Pow(D, 2)) / Math.Pow(E, 2);
                double b = circle.D + (2 * F * D) / Math.Pow(E, 2) + (-circle.E * D) / E;
                double c = Math.Pow(F, 2) / Math.Pow(E, 2) + (-circle.E * F) / E + circle.F;
                Discriminante d = new Discriminante(a, b, c);
                var discriminante = d.CalculateDiscriminante();
                if (discriminante < 0)
                {
                    return new Sequence();
                }
                else if (discriminante == 0)
                {
                    int x;
                    d.CalculateOneValue(out x);
                    int y = (int)((-F - D * x) / E);
                    return new Sequence(new List<IGeoEntities> { new GeoPoint(x, y) });
                }

                int x1;
                int x2;
                d.CalculateValues(out x1, out x2);
                int y1 = (int)((-F - D * x1) / E);
                int y2 = (int)((-F - D * x2) / E);
                return new Sequence(new List<IGeoEntities> { new GeoPoint(x1, y1), new GeoPoint(x2, y2) });
            }
            return new Sequence();

        }

        public static IGeoEntities Intersect(Circle figure1, Line figure2)
        {
            var circle = figure1 as Circle;
            double a = (Math.Pow(figure2.By, 2) + Math.Pow(figure2.Ax, 2)) / Math.Pow(figure2.By, 2);
            double b = (2 * figure2.Ax * figure2.C) / Math.Pow(figure2.By, 2) + circle.D - (circle.E * figure2.Ax) / figure2.By;
            double c = Math.Pow(figure2.C, 2) / Math.Pow(figure2.By, 2) - circle.E * figure2.C / figure2.By + circle.F;
            Discriminante d = new Discriminante(a, b, c);
            var discriminante = d.CalculateDiscriminante();
            if (discriminante == 0)
            {
                int x;
                d.CalculateOneValue(out x);
                int y = (int)((-figure2.C - figure2.Ax * x) / figure2.By);
                return new Sequence(new List<IGeoEntities> { new GeoPoint(x, y) });
            }
            else if (discriminante < 0)
            {
                return new Sequence();
            }

            int x1;
            int x2;
            d.CalculateValues(out x1, out x2);
            int y1 = (int)((-figure2.C - figure2.Ax * x1) / figure2.By);
            int y2 = (int)((-figure2.C - figure2.Ax * x2) / figure2.By);


            return new Sequence(new List<IGeoEntities> { new GeoPoint(x1, y1), new GeoPoint(x2, y2) });

        }

        public static IGeoEntities Intersect(Circle figure1, Segment figure2)
        {
            var circle = figure1 as Circle;
            double a = (Math.Pow(figure2.By, 2) + Math.Pow(figure2.Ax, 2)) / Math.Pow(figure2.By, 2);
            double b = (2 * figure2.Ax * figure2.C) / Math.Pow(figure2.By, 2) + circle.D - (circle.E * figure2.Ax) / figure2.By;
            double c = Math.Pow(figure2.C, 2) / Math.Pow(figure2.By, 2) - circle.E * figure2.C / figure2.By + circle.F;
            Discriminante d = new Discriminante(a, b, c);
            var discriminante = d.CalculateDiscriminante();
            if (discriminante == 0)
            {
                int x;
                d.CalculateOneValue(out x);
                int y = (int)((-figure2.C - figure2.Ax * x) / figure2.By);
                return new Sequence(new List<IGeoEntities> { new GeoPoint(x, y) });
            }
            else if (discriminante < 0)
            {
                return new Sequence();
            }

            int x1;
            int x2;
            d.CalculateValues(out x1, out x2);
            int y1 = (int)((-figure2.C - figure2.Ax * x1) / figure2.By);
            int y2 = (int)((-figure2.C - figure2.Ax * x2) / figure2.By);
            double resultSegmentX1 = (double)(x1 - figure2.Starts.X) / (figure2.End.X - figure2.Starts.X);
            double resultSegmentY1 = (double)(y1 - figure2.Starts.Y) / (figure2.End.Y - figure2.Starts.Y);
            double resultSegmentX2 = (double)(x2 - figure2.Starts.X) / (figure2.End.X - figure2.Starts.X);
            double resultSegmentY2 = (double)(y2 - figure2.Starts.Y) / (figure2.End.Y - figure2.Starts.Y);
            if ((0 <= resultSegmentX1 && resultSegmentX1 <= 1) && (0 <= resultSegmentY1 && resultSegmentY1 <= 1))
            {
                if ((0 <= resultSegmentX2 && resultSegmentX2 <= 1) && (0 <= resultSegmentY2 && resultSegmentY2 <= 1))
                {
                    return new Sequence(new List<IGeoEntities> { new GeoPoint(x1, y1), new GeoPoint(x2, y2) });
                }
                return new Sequence(new List<IGeoEntities> { new GeoPoint(x1, y1) });
            }
            if ((0 <= resultSegmentX2 && resultSegmentX2 <= 1) && (0 <= resultSegmentY2 && resultSegmentY2 <= 1))
            {
                return new Sequence(new List<IGeoEntities> { new GeoPoint(x2, y2) });
            }
            return new Sequence();
        }

        private static bool IsContained(Ray ray, double x, double y)
        {
            if (ray.StarsFrom.X >= ray.GivenPoint.X)
            {
                if (ray.StarsFrom.Y >= ray.GivenPoint.Y)
                {
                    return (x <= ray.StarsFrom.X && y <= ray.StarsFrom.Y);
                }
                else
                    return (x <= ray.StarsFrom.X && y >= ray.StarsFrom.Y);
            }
            else
            {
                if (ray.StarsFrom.Y >= ray.GivenPoint.Y)
                {
                    return (x >= ray.StarsFrom.X && y <= ray.StarsFrom.Y);
                }
                else
                    return (x >= ray.StarsFrom.X && y >= ray.StarsFrom.Y);
            }
        }
        public static IGeoEntities Intersect(Circle first, Ray second)
        {

            //  private IEnumerable<MainEntity> Intersects(MainEntity first, MainEntity second, out int count)
            {
                List<IGeoEntities> result = new List<IGeoEntities>();
                // count = 0;
                //  if (first is Circle)
                {
                    Circle c1 = first as Circle;
                    Ray r1 = second as Ray;
                    double x = c1.Center.X;
                    double y = c1.Center.Y;
                    double rad = c1.Radius.Value;
                    double aux1 = ((r1.By * r1.By) / (r1.Ax * r1.Ax)) + 1;
                    double aux2 = (((2 * r1.By * r1.C) / (r1.Ax * r1.Ax)) + ((2 * r1.By * x) / r1.Ax) - 2 * y);
                    double aux3 = (((r1.C * r1.C) / (r1.Ax * r1.Ax)) + ((2 * x * r1.C) / r1.Ax) + (x * x) + (y * y) - (rad * rad));

                    double discriminant = (aux2 * aux2) - (4 * (aux1 * aux3));
                    if (discriminant > 0)
                    {
                        double y1 = ((-aux2) - Math.Sqrt(discriminant)) / (2 * aux1);

                        double y2 = ((-aux2) + Math.Sqrt(discriminant)) / (2 * aux1);

                        double x1 = (-r1.By * y1 - r1.C) / r1.Ax;

                        double x2 = (-r1.By * y2 - r1.C) / r1.Ax;
                        if (IsContained(r1, x1, y1))
                        {
                            result.Add(new GeoPoint(x1, y1));

                        }
                        if (IsContained(r1, x2, y2))
                        {
                            result.Add(new GeoPoint(x2, y2));

                        }
                    }
                    if (discriminant == 0)
                    {
                        double y3 = ((-aux2) - Math.Sqrt(discriminant)) / (2 * aux1);
                        double x3 = (-r1.By * y3 - r1.C) / r1.Ax;
                        if (IsContained(r1, x3, y3))
                            result.Add(new GeoPoint(x3, y3));

                    }

                    return new Sequence(result);
                }

            }
        }
        #endregion

        public static bool IsContainedInRay(GeoPoint a, GeoPoint b, GeoPoint c)
        {
            if (a.Y < b.Y && a.X < b.X)
                return c.Y >= a.Y && c.X >= a.X;
            if (a.Y < b.Y && a.X > b.X)
                return c.Y >= a.Y && c.X <= a.X;
            if (a.Y > b.Y && a.X < b.X)
                return c.Y <= a.Y && c.X >= a.X;
            if (a.Y > b.Y && a.X > b.X)
                return c.Y <= a.Y && c.X < a.X;
            return c.X == a.X && c.Y == a.Y;
        }


    }
}
