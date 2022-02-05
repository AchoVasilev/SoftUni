namespace _04.CookiesProblem
{
    using Wintellect.PowerCollections;

    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var bag = new OrderedBag<int>();

            foreach (var cookie in cookies)
            {
                bag.Add(cookie);
            }

            var currentMinSweetness = bag.GetFirst();
            var steps = 0;

            while (currentMinSweetness < k && bag.Count > 1)
            {
                var leastSweetCookie = bag.RemoveFirst();
                var secondLeastSweetCookie = bag.RemoveFirst();

                var combined = leastSweetCookie + (2 * secondLeastSweetCookie);
                bag.Add(combined);

                currentMinSweetness = bag.GetFirst();
                steps++;
            }

            return currentMinSweetness < k ? -1 : steps;
        }
    }
}
