using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace DataStructures
{
    public struct Point
    {
        public int x, y;


        public Point(float x, float y):this((int)x, (int)y)
        {
           
        }
        public Point(Vector2 v) : this(v.x, v.y)
        {

        }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /*
       public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point operator +(Point a, Vector2 b)
        {
            return new Point(a.x + (int)b.x, a.y + (int)b.y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);
        }

        public static Point operator -(Point a, Vector2 b)
        {
            return new Point(a.x - (int)b.x, a.y - (int)b.y);
        }

        public static Point operator *(Point a, int b)
        {
            return new Point(a.x * b, a.y * b);
        }
        */
        public override bool Equals(object obj)
        {
            Point p;

            if (!(obj is DBNull))
            {
                p =(Point)obj;
                return this.x.Equals(p.x) && this.y.Equals(p.y);
            }
            return false;
        }
        public override string ToString()
        {
            return x+" " +y;
        }


        public static bool operator !=(Point n1, Point n2)
        {
            return !n1.Equals(n2);
        }

        public static bool operator ==(Point n1, Point n2)
        {
            return n1.Equals(n2);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + x.GetHashCode();
            hash = (hash * 7) + x.GetHashCode();
            return hash;
        }
    }

    class Pair<T>
    {
        public T First { get; private set; }
        public T Second { get; private set; }

        public Pair(T first, T second)
        {
            First = first;
            Second = second;
        }

        public override int GetHashCode()
        {
            return First.GetHashCode() ^ Second.GetHashCode();
        }

        public override bool Equals(object other)
        {
            Pair<T> pair = other as Pair<T>;
            if (pair == null)
            {
                return false;
            }
            return (this.First.Equals(pair.First) && this.Second.Equals(pair.Second));
        }
    }

    class PairComparer<T> : IComparer<Pair<T>> where T : IComparable
    {
        public int Compare(Pair<T> x, Pair<T> y)
        {
            if (x.First.CompareTo(y.First) < 0)
            {
                return -1;
            }
            else if (x.First.CompareTo(y.First) > 0)
            {
                return 1;
            }
            else
            {
                return x.Second.CompareTo(y.Second);
            }
        }
    }

    class PriorityQueue<T>
    {
        SortedList<Pair<int>, T> _list;
        int count;

        public PriorityQueue()
        {
            _list = new SortedList<Pair<int>, T>(new PairComparer<int>());
        }

        public void Enqueue(T item, int priority)
        {
            _list.Add(new Pair<int>(priority, count), item);
            count++;
        }

        public T Dequeue()
        {
            T item = _list[_list.Keys[0]];
            _list.RemoveAt(0);
            return item;
        }
        public int Count()
        {
            return _list.Count;
        }
    }

    public class BiDictionary<T1, T2>
    {

        private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();
        private int _position;
        public BiDictionary()
        {
            this.GetForward = _forward;
            this.GetReverse = _reverse;
        }

        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

        public void Set(T1 t1, T2 t2)
        {
            SetForward(t1, t2);
            SetReverse(t2, t1);
        }
        public void SetForward(T1 t1, T2 t2)
        {
            _forward[t1] = t2;
        }
        public void SetReverse(T2 t2, T1 t1)
        {
            _reverse[t2]=t1;
        }
        public void Remove(T1 t1, T2 t2)
        {
            RemoveForward(t1);
            RemoveReverse(t2);
        }
        public void RemoveForward(T1 t1)
        {
            _forward.Remove(t1);
        }
        public void RemoveReverse(T2 t2)
        {
            _reverse.Remove(t2);
        }
        public Dictionary<T1, T2> GetForward { get; private set; }
        public Dictionary<T2, T1> GetReverse { get; private set; }
    }

}
