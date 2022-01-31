namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public Stack()
        {
            this._top = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._top;

            while (current != null)
            {
                if (current.Element.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Peek()
        {
            this.ValidateIfEmpty();

            return this._top.Element;
        }

        public T Pop()
        {
            this.ValidateIfEmpty();

            var topItem = this._top.Element;
            var nextItem = this._top.Next;
            this._top.Next = null;

            this._top = nextItem;
            this.Count--;

            return topItem;
        }

        public void Push(T item)
        {
            var node = new Node<T>(item)
            {
                Next = this._top
            };
            this._top = node;

            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._top;

            while (true)
            {
                if (current == null)
                {
                    break;
                }

                yield return current.Element;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => this.GetEnumerator();

        private void ValidateIfEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
        }
    }
}