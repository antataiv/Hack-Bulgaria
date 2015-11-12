namespace _01_Points
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            //Starting point: (0, 0)
            string startingPointInput = Console.ReadLine().Trim();

            //>>><<<~>>>~^^^
            char[] movingDirections = Console.ReadLine().Trim().ToCharArray();

            int firstBracketIndex = startingPointInput.IndexOf('(');
            int[] coordinates = startingPointInput.Substring(firstBracketIndex + 1, startingPointInput.Length - firstBracketIndex - 2)
                                        .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var currPoint = new Point(coordinates[0], coordinates[1]);

            int wrapSymbolsCount = 0;

            foreach (var direction in movingDirections)
            {
                if (direction == '~')
                {
                    wrapSymbolsCount++;
                }

                if (wrapSymbolsCount % 2 == 0)
                {
                    FollowInitialDirection(currPoint, direction);
                }
                else
                {
                    FollowReversedDirection(currPoint, direction);
                }
            }

            Console.WriteLine(string.Format("({0}, {1})", currPoint.X, currPoint.Y));
        }

        private static void FollowInitialDirection(Point currPoint, char direction)
        {
            switch (direction)
            {
                case '>':
                    currPoint.X++;
                    break;
                case '<':
                    currPoint.X--;
                    break;
                case '^':
                    currPoint.Y--;
                    break;
                case 'v':
                    currPoint.Y++;
                    break;
            }
        }

        private static void FollowReversedDirection(Point currPoint, char direction)
        {
            switch (direction)
            {
                case '>':
                    currPoint.X--;
                    break;
                case '<':
                    currPoint.X++;
                    break;
                case '^':
                    currPoint.Y++;
                    break;
                case 'v':
                    currPoint.Y--;
                    break;
            }
        }
    }
}
