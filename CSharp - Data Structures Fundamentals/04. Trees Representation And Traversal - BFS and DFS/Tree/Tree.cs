namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this.children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this.children.Add(child);
            }
        }

        public T Value { get; private set; }

        public Tree<T> Parent { get; private set; }

        public bool IsRootDeleted { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => this.children.AsReadOnly();

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var currentEl = queue.Dequeue();
                result.Add(currentEl.Value);

                foreach (var child in currentEl.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            this.Dfs(this, result);

            return result;

            //with Stack
            //return this.StackDfs();
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var parentSubtree = this.FindBfs(parentKey);
            //var subTree = this.FindDfs(parentKey, this);
            this.CheckEmptyNode(parentSubtree);

            parentSubtree.children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            var currentNode = this.FindBfs(nodeKey);
            this.CheckEmptyNode(currentNode);

            foreach (var child in currentNode.children)
            {
                child.Parent = null;
            }

            currentNode.children.Clear();

            var parentNode = currentNode.Parent;

            if (parentNode is null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                parentNode.children.Remove(currentNode);
                currentNode.Parent = null;
            }

            currentNode.Value = default;
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            var secondNode = this.FindBfs(secondKey);

            this.CheckEmptyNode(firstNode);
            this.CheckEmptyNode(secondNode);

            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;

            if (firstParent is null)
            {
                SwapRoot(secondNode);
                return;
            }

            if (secondParent is null)
            {
                SwapRoot(firstNode);
                return;
            }

            firstNode.Parent = secondParent;
            secondNode.Parent = firstParent;

            var indexOfFirst = firstParent.children.IndexOf(firstNode);
            var indexOfSecond = secondParent.children.IndexOf(secondNode);

            firstParent.children[indexOfFirst] = secondNode;
            secondParent.children[indexOfSecond] = firstNode;
        }

        private void SwapRoot(Tree<T> node)
        {
            this.Value = node.Value;
            this.children.Clear();

            foreach (var child in node.children)
            {
                this.children.Add(child);
            }
        }

        private void CheckEmptyNode(Tree<T> node)
        {
            if (node is null)
            {
                throw new ArgumentNullException("Node is null");
            }
        }

        private Tree<T> FindDfs(T value, Tree<T> subTree)
        {
            if (subTree.Value.Equals(value))
            {
                return subTree;
            }

            foreach (var child in subTree.Children)
            {
                var current = this.FindDfs(value, child);

                if (current != null && current.Value.Equals(value))
                {
                    return current;
                }
            }

            return null;
        }

        private Tree<T> FindBfs(T value)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();

                if (subTree.Value.Equals(value))
                {
                    return subTree;
                }

                foreach (var child in subTree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private void Dfs(Tree<T> subTree, List<T> result)
        {
            foreach (var child in subTree.Children)
            {
                this.Dfs(child, result);
            }

            result.Add(subTree.Value);
        }

        private ICollection<T> StackDfs()
        {
            var result = new Stack<T>();
            var toTraverse = new Stack<Tree<T>>();

            toTraverse.Push(this);
            while (toTraverse.Count > 0)
            {
                var currentTree = toTraverse.Pop();

                foreach (var child in currentTree.Children)
                {
                    toTraverse.Push(child);
                }

                result.Push(currentTree.Value);
            }

            return result.ToList();
        }
    }
}
