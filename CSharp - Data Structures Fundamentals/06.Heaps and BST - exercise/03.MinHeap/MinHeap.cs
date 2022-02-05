namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public MinHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public T Dequeue()
        {
            var firstElement = this.Peek();
            this.Swap(0, this.Size - 1);
            this.elements.RemoveAt(this.Size - 1);

            this.HeapifyDown();

            return firstElement;
        }

        public void Add(T element)
        {
            this.elements.Add(element);

            this.HeapifyUp();
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this.elements[0];
        }

        private void HeapifyUp()
        {
            var index = this.Size - 1;
            var parentIndex = this.GetParentIndex(index);
            while (index > 0 && this.IsLess(index, parentIndex))
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = this.GetParentIndex(index);
            }
        }

        private void HeapifyDown()
        {
            int index = 0;
            var leftChildIndex = this.GetLeftChildIndex(index);
            while (leftChildIndex < this.Size && this.IsGreater(index, leftChildIndex))
            {
                var toSwapWith = leftChildIndex;
                var rightChildIndex = this.GetRightChildIndex(index);

                if (rightChildIndex < this.Size && this.IsGreater(toSwapWith, rightChildIndex))
                {
                    toSwapWith = rightChildIndex;
                }

                this.Swap(toSwapWith, index);
                index = toSwapWith;
                leftChildIndex = this.GetLeftChildIndex(index);
            }
        }

        private int GetLeftChildIndex(int index)
            => (index * 2) + 1;

        private int GetRightChildIndex(int index)
            => (index * 2) + 2;

        private int GetParentIndex(int index)
            => (index - 1) / 2;

        private void Swap(int index, int parentIndex)
        {
            var temp = this.elements[index];
            this.elements[index] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;
        }

        private bool IsGreater(int index, int parentIndex)
            => (this.elements[index].CompareTo(this.elements[parentIndex])) > 0;

        private bool IsLess(int index, int parentIndex)
            => (this.elements[index].CompareTo(this.elements[parentIndex])) < 0;

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }
        }
    }
}
