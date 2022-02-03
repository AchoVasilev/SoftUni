namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public PriorityQueue()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public T Dequeue()
        {
            this.EnsureNotEmpty();
            var element = this.elements[0];

            this.Swap(0, this.Size - 1);
            this.elements.RemoveAt(this.Size - 1);
            this.HeapifyDown(0);

            return element;
        }

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

        private void HeapifyDown(int index)
        {
            var leftChildIndex = this.GetLeftChildIndex(index);
            while (this.IsValidIndex(leftChildIndex) && this.IsGreater(index, leftChildIndex) == false)
            {
                var toSwapWith = leftChildIndex;
                var rightChildIndex = this.GetRightChildIndex(index);

                if (this.IsValidIndex(rightChildIndex) && this.IsGreater(rightChildIndex, toSwapWith))
                {
                    toSwapWith = rightChildIndex;
                }

                this.Swap(toSwapWith, index);
                index = toSwapWith;
                leftChildIndex = this.GetLeftChildIndex(index);
            }
        }

        private bool IsValidIndex(int index)
            => index >= 0 && index < this.Size;

        private int GetLeftChildIndex(int index)
            => (2 * index) + 1;

        private int GetRightChildIndex(int index)
            => (2 * index) + 2;

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Empty queue");
            }
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = this.GetParentIndex(index);
            while (index > 0 && this.IsGreater(index, parentIndex))
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = this.GetParentIndex(index);
            }
        }

        private void Swap(int index, int parentIndex)
        {
            var temp = this.elements[index];
            this.elements[index] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;
        }

        private bool IsGreater(int index, int parentIndex)
            => (this.elements[index].CompareTo(this.elements[parentIndex])) > 0;

        private int GetParentIndex(int index)
            => (index - 1) / 2;
    }
}
