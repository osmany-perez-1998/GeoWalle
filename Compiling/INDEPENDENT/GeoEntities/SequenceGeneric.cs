using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Sequence : IGeoEntities, IPaintable
    {
        public string InsideType { get; private set; }
        public bool EmptySequence { get; private set; }
        public bool FiniteSequence { get; private set; }
        public bool NumberInfiniteSequence { get; private set; }
        public bool NumberIntervalSequence { get; private set; }
        public bool GeoentityInfiniteSequence { get; private set; }

        public IEnumerable<IGeoEntities> InfiniteStorage { get; private set; }
        public List<IGeoEntities> FiniteStorage { get; private set; }
        public int InitialFiniteStorageCount { get; private set; }

        public Sequence()
        {
            InsideType = "empty";
            EmptySequence = true;
        }
        public Sequence(List<IGeoEntities> sequence)
        {
            if (sequence.Count == 0)
            {
                InsideType = "empty";
                EmptySequence = true;
            }
            else
            {
                InsideType = sequence[0].Type();
                FiniteSequence = true;
                FiniteStorage = sequence;
                InitialFiniteStorageCount = sequence.Count();
            }

        }
        public Sequence(Number a)
        {
            InsideType = "infiniteNumbers";
            NumberInfiniteSequence = true;
            InfiniteStorage = new InfiniteNumberSequence(a);
        }

        public Sequence(IGeoEntities a)
        {
            InsideType = a.Type();
            GeoentityInfiniteSequence = true;

            if (a.Type() == "circle")
                InfiniteStorage = new InfiniteCircleSequence(new GeoPoint());
            if (a.Type() == "point")
                InfiniteStorage = new InfinitePointSequence(new GeoPoint());
            if (a.Type() == "segment")
                InfiniteStorage = new InfiniteSegmentSequence(new GeoPoint());
            if (a.Type() == "ray")
                InfiniteStorage = new InfiniteRaySequence(new GeoPoint());
            if (a.Type() == "line")
                InfiniteStorage = new InfiniteLineSequence(new GeoPoint());
            if (a.Type() == "arc")
                InfiniteStorage = new InfiniteArcSequence(new GeoPoint());

        }
        public Sequence(Number a, Number b)
        {
            InsideType = "finiteNumbers";
            NumberIntervalSequence = true;
            List<IGeoEntities> bla = new List<IGeoEntities>();
            for (int i = (int)a.Value; i <= (int)b.Value; i++)
            {
                bla.Add(new Number(i));
            }
            FiniteStorage = bla;
            InitialFiniteStorageCount = bla.Count();
        }


        public string Type()
        {
            return "sequence";
        }

        public struct InfiniteNumberSequence : IEnumerable<Number>
        {
            public int Current { get; private set; }
            public int Min { get; private set; }
            public InfiniteNumberSequence(Number a)
            {
                Current = (int)a.Value;
                Min = Current;
            }
            public IEnumerator<Number> GetEnumerator()
            {
                while (true)
                {
                    yield return new Number(Current);
                    Current++;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            public void Reset()
            {
                Current = Min;
            }
        }

        //public struct IntervalNumberSequence : IEnumerable<Number>
        //{
        //    public int Current { get; private set; }
        //    public int Min { get; private set; }
        //    public int Max { get; private set; }
        //    public IntervalNumberSequence(Number min, Number max)
        //    {
        //        Current =(int) min.Value;
        //        Min = Current;
        //        Max = (int)max.Value;

        //    }
        //    public IEnumerator<Number> GetEnumerator()
        //    {
        //        while (Current <= Max) yield return new Number(Current);
        //        Current++;
        //    }

        //    IEnumerator IEnumerable.GetEnumerator()
        //    {
        //        return this.GetEnumerator();
        //    }
        //}//NO LA NECESITO

        public struct InfiniteCircleSequence : IEnumerable<IGeoEntities>
        {
            public InfiniteCircleSequence(GeoPoint a)
            {

            }
            public IEnumerator<IGeoEntities> GetEnumerator()
            {
                while (true)
                {
                    yield return new Circle(new GeoPoint(), new Measure());
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        public struct InfiniteSegmentSequence : IEnumerable<IGeoEntities>
        {
            public InfiniteSegmentSequence(GeoPoint a)
            {

            }
            public IEnumerator<IGeoEntities> GetEnumerator()
            {
                while (true)
                {
                    yield return new Segment(new GeoPoint(), new GeoPoint());
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        public struct InfinitePointSequence : IEnumerable<IGeoEntities>
        {
            public InfinitePointSequence(GeoPoint a)
            {

            }
            public IEnumerator<IGeoEntities> GetEnumerator()
            {
                while (true)
                {
                    yield return new GeoPoint();
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        public struct InfiniteArcSequence : IEnumerable<IGeoEntities>
        {
            public InfiniteArcSequence(GeoPoint a)
            {

            }
            public IEnumerator<IGeoEntities> GetEnumerator()
            {
                while (true)
                {
                    yield return new Arc(new GeoPoint(), new GeoPoint(), new GeoPoint(), new Measure());
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
        public struct InfiniteLineSequence : IEnumerable<IGeoEntities>
        {
            public InfiniteLineSequence(GeoPoint a)
            {

            }
            public IEnumerator<IGeoEntities> GetEnumerator()
            {
                while (true)
                {
                    yield return new Line(new GeoPoint(), new GeoPoint());
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
        public struct InfiniteRaySequence : IEnumerable<IGeoEntities>
        {
            public InfiniteRaySequence(GeoPoint a)
            {

            }
            public IEnumerator<IGeoEntities> GetEnumerator()
            {
                while (true)
                {
                    yield return new Ray(new GeoPoint(), new GeoPoint());
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        //public struct 

    }
}
