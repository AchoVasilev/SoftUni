namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public SinglyLinkedList()
        {
            this._head = null;
            this.Count = 0;
        }

        public SinglyLinkedList(Node<T> head)
        {
            this._head = head;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item, this._head);

            this._head = newNode;
            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node<T>(item);
            if (this._head is null)
            {
                this._head = newNode;
            }
            else
            {
                var current = this._head;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.EnsureNotEmpty();

            return this._head.Element;
        }

        public T GetLast()
        {
            this.EnsureNotEmpty();

            var current = this._head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            return current.Element;
        }

        public T RemoveFirst()
        {
            EnsureNotEmpty();
            var elementToRemove = this._head;

            var nextElement = this._head.Next;
            this._head.Next = null;

            this._head = nextElement;
            this.Count--;

            return elementToRemove.Element;
        }

        public T RemoveLast()
        {
            EnsureNotEmpty();

            if (this.Count == 1)
            {
                var element = this._head;
                this._head = null;

                this.Count--;
                return element.Element;
            }

            var current = this._head;
            while (current.Next.Next != null)
            {
                current = current.Next;
            }

            var elementToRemove = current.Next.Element;
            current.Next = null;
            this.Count--;

            return elementToRemove;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;
            while (current != null)
            {
                yield return current.Element;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
        }
    }
}