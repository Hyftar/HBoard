using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;

namespace HBoard.Logic
{
    public class MovementPath : IEnumerable<IVector>
    {
        protected readonly List<IVector> vectors = new List<IVector>();

        public MovementPath(Point location, params IVector[] vectors)
        {
            this.Location = location;
            this.vectors.AddRange(vectors);
        }

        public Point Location { get; private set; }

        public IReadOnlyList<IVector> Nodes { get { return this.vectors.AsReadOnly(); } }

        public void Add(IVector item)
        {
            this.vectors.Add(item);
        }

        public void AddRange(IEnumerable<IVector> collection)
        {
            this.vectors.AddRange(collection);
        }

        public void Insert(Int32 index, IVector item)
        {
            this.vectors.Insert(index, item);
        }

        public void RemoveAt(Int32 index)
        {
            this.vectors.RemoveAt(index);
        }

        public void Clear()
        {
            this.vectors.Clear();
        }

        public IEnumerator<IVector> GetEnumerator()
        {
            return this.vectors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.vectors.GetEnumerator();
        }
    }
}
