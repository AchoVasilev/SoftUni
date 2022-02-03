namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);
            this.HeapifyUp(this.Size - 1);
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this.elements[0];
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = this.GetParentIndex(index);
            while (index > 0 && IsGreater(index, parentIndex))
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = this.GetParentIndex(index);
            }
        }

        private void Swap(int childIndex, int parentIndex)
        {
            var temp = this.elements[childIndex];
            this.elements[childIndex] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;
        }

        private int GetParentIndex(int index)
            => (index - 1) / 2;

        private bool IsGreater(int childIndex, int parentIndex) 
            => (this.elements[childIndex].CompareTo(this.elements[parentIndex])) > 0;

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Empty heap");
            }
        }
    }
}
