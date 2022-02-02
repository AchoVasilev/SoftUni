namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children
            => this.children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            var result = new StringBuilder();
            var depth = 0;

            this.OrderDfs(depth, result, this);

            return result.ToString().Trim();
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            bool predicate(Tree<T> node) => this.IsLeaf(node);

            var nodes = this.TreeOrderBfs(predicate);

            var maxDepth = 0;
            Tree<T> deepestNode = null;

            foreach (var node in nodes)
            {
                var currentDepth = this.GetLeafDepthToRoot(node);
                if (currentDepth > maxDepth)
                {
                    maxDepth = currentDepth;
                    deepestNode = node;
                }
            }

            return deepestNode;
        }

        public List<T> GetLeafKeys()
        {
            bool predicate(Tree<T> node) => this.IsLeaf(node);

            return this.OrderBfs(predicate);
        }

        public List<T> GetMiddleKeys()
        {
            bool predicate(Tree<T> node) => this.IsMiddle(node);

            return this.OrderBfs(predicate);
        }

        public List<T> GetLongestPath()
        {
            var deepestNode = this.GetDeepestLeftomostNode();
            var resultedPath = new Stack<T>();
            var currentNode = deepestNode;

            while (currentNode != null)
            {
                resultedPath.Push(currentNode.Key);
                currentNode = currentNode.Parent;
            }

            return resultedPath.ToList();
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<T>>();

            var currentPath = new List<T>();
            currentPath.Add(this.Key);

            var currentSum = Convert.ToInt32(this.Key);

            this.DfsGetPathsWithSum(this, result, currentPath, ref currentSum, sum);

            return result;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            bool predicate(Tree<T> node, int predicateSum) 
                => this.HasGivenSum(node, predicateSum);

            return this.OrderBfsNodes(predicate, sum);
        }

        private List<Tree<T>> OrderBfsNodes(Func<Tree<T>, int, bool> predicate, int sum)
        {
            var result = new List<Tree<T>>();
            var nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);
            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();

                if (predicate.Invoke(currentNode, sum))
                {
                    result.Add(currentNode);
                }

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        private bool HasGivenSum(Tree<T> currentNode, int sum)
        {
            int actualSum = this.GetSubtreeDfsSum(currentNode);

            return actualSum == sum;
        }

        private int GetSubtreeDfsSum(Tree<T> node)
        {
            var currentSum = Convert.ToInt32(node.Key);
            var childrenSum = 0;

            foreach (var child in node.Children)
            {
                childrenSum += this.GetSubtreeDfsSum(child);
            }

            return currentSum + childrenSum;
        }

        private void DfsGetPathsWithSum(Tree<T> tree, List<List<T>> result, List<T> currentPath, ref int currentSum, int sum)
        {
            foreach (var child in tree.Children)
            {
                currentPath.Add(child.Key);
                currentSum += Convert.ToInt32(child.Key);

                this.DfsGetPathsWithSum(child, result, currentPath, ref currentSum, sum);
            }

            if (currentSum == sum)
            {
                result.Add(new List<T>(currentPath));
            }

            currentSum -= Convert.ToInt32(tree.Key);
            currentPath.RemoveAt(currentPath.Count - 1);
        }

        private void OrderDfs(int depth, StringBuilder result, Tree<T> tree)
        {
            result.AppendLine(new string(' ', depth) + tree.Key);

            foreach (var child in tree.Children)
            {
                this.OrderDfs(depth + 2, result, child);
            }
        }

        private bool IsLeaf(Tree<T> node)
            => node.Children.Count == 0;

        private bool IsRoot(Tree<T> node)
            => node.Parent is null;

        private bool IsMiddle(Tree<T> node)
            => this.IsLeaf(node) == false && this.IsRoot(node) == false;

        private List<T> OrderBfs(Func<Tree<T>, bool> predicate)
        {
            var result = new List<T>();
            var nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);
            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();
                if (predicate.Invoke(currentNode))
                {
                    result.Add(currentNode.Key);
                    continue;
                }

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        private List<Tree<T>> TreeOrderBfs(Func<Tree<T>, bool> predicate)
        {
            var result = new List<Tree<T>>();
            var nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);
            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();

                if (predicate.Invoke(currentNode))
                {
                    result.Add(currentNode);
                }

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        private int GetLeafDepthToRoot(Tree<T> node)
        {
            var steps = 0;
            var currentNode = node;

            while (currentNode.Parent != null)
            {
                steps++;
                currentNode = currentNode.Parent;
            }

            return steps;
        }
    }
}
