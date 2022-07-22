namespace TheStoryTelling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static Dictionary<string, List<string>> graph;
        private static HashSet<string> passedNodes;
        
        public static void Main(string[] args)
        {
            FillTheGraph();
            
            passedNodes = new HashSet<string>();
            foreach (var parentNode in graph.Keys)
            {
                DFS(parentNode);
            }

            Console.WriteLine(string.Join(" ", passedNodes.Reverse()));
        }

        private static void FillTheGraph()
        {
            graph = new Dictionary<string, List<string>>();
            
            while (true)
            {
                var command = Console.ReadLine();
                if (command == "End")
                {
                    break;
                }

                var tokens = command.Split("->", StringSplitOptions.RemoveEmptyEntries);
                var preStory = tokens[0].Trim();

                if (!graph.ContainsKey(preStory))
                {
                    graph[preStory] = new List<string>();
                }

                if (tokens.Length < 2)
                {
                    continue;
                }

                var stories = tokens[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                graph[preStory] = stories;
            }
        }

        private static void DFS(string node)
        {
            if (passedNodes.Contains(node))
            {
                return;
            }

            foreach (var childNode in graph[node])
            {
                DFS(childNode);
            }

            passedNodes.Add(node);
        }
    }
}
