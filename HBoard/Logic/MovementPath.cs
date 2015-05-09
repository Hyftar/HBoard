using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;

namespace HBoard.Logic
{
    public class MovementPath : IEnumerable<IVector>
    {
        protected readonly List<IVector> Vectors = new List<IVector>();

        public MovementPath(Point location, params IVector[] vectors)
        {
            this.Location = location;
            this.Vectors.AddRange(vectors);
        }

        public Point Location { get; private set; }

        public IReadOnlyList<IVector> Nodes { get { return this.Vectors.AsReadOnly(); } }

        public void Add(IVector item)
        {
            this.Vectors.Add(item);
        }

        public void AddRange(IEnumerable<IVector> collection)
        {
            this.Vectors.AddRange(collection);
        }

        public void Insert(Int32 index, IVector item)
        {
            this.Vectors.Insert(index, item);
        }

        public void RemoveAt(Int32 index)
        {
            this.Vectors.RemoveAt(index);
        }

        public void Clear()
        {
            this.Vectors.Clear();
        }

        public IEnumerator<IVector> GetEnumerator()
        {
            return this.Vectors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Vectors.GetEnumerator();
        }
    }
}
