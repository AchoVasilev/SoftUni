namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;

            if (this.RightChild != null)
            {
                this.RightChild.Parent = this;
            }

            if (this.LeftChild != null)
            {
                this.LeftChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            var firstList = new List<BinaryTree<T>>();
            var secondList = new List<BinaryTree<T>>();

            this.FindNodeDfs(this, first, firstList);
            this.FindNodeDfs(this, second, secondList);

            var firstNode = firstList[0];
            var secondNode = firstList[0];

            var parentToLookFor = firstNode.Parent.Value;
            while (parentToLookFor.Equals(firstNode.Value) == false || parentToLookFor.Equals(secondNode.Value) == false)
            {
                if (parentToLookFor.Equals(firstNode.Value) == false)
                {
                    firstNode = firstNode.Parent;
                }

                if (parentToLookFor.Equals(secondNode.Value) == false)
                {
                    secondNode = secondNode.Parent;
                }
            }

            return firstNode.Value;
        }

        private void FindNodeDfs(BinaryTree<T> current, T toLookUp, List<BinaryTree<T>> list)
        {
            if (current is null)
            {
                return;
            }

            if (current.Value.Equals(toLookUp))
            {
                list.Add(current);
                return;
            }

            this.FindNodeDfs(current.LeftChild, toLookUp, list);
            this.FindNodeDfs(current.RightChild, toLookUp, list);
        }
    }
}
