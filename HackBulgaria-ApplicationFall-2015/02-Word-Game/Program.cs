namespace _02_Word_Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static int occurrences = 0;

        static void Main()
        {
            //ivan
            //evnh
            //inav
            //mvvn
            //qrit
            List<char[]> matrix = new List<char[]>();

            string currLineInput = Console.ReadLine().Trim();

            while (currLineInput != String.Empty)
            {
                matrix.Add(currLineInput.ToCharArray());

                currLineInput = Console.ReadLine().Trim();
            }

            string searchWord = "ivan";

            for (int row = 0; row < matrix.Count; row++)
            {
                for (int col = 0; col < matrix[row].Count(); col++)
                {
                    for (int direction = 0; direction < 8; direction++)
                    {
                        SearchForWord(matrix, row, col, matrix.Count, matrix[row].Count(), "", direction, searchWord);
                    }
                }
            }

            Console.WriteLine(occurrences);
        }

        private static void SearchForWord(List<char[]> matrix, int row, int col, int height, int width, string sequence, int direction, string searchWord)
        {
            if (row < 0 || row >= height || col < 0 || col >= width)
            {
                return;
            }

            char currentLetter = matrix[row][col];
            string wordToCheck = sequence + currentLetter;

            if (wordToCheck.Equals(searchWord))
            {
                occurrences++;
            }

            switch (direction)
            {
                case 0:
                    //go left
                    SearchForWord(matrix, row, col - 1, matrix.Count, matrix[row].Count(), wordToCheck, direction, searchWord);
                    break;
                case 1:
                    //go right
                    SearchForWord(matrix, row, col + 1, matrix.Count, matrix[row].Count(), wordToCheck, direction, searchWord);
                    break;
                case 2:
                    //go up
                    SearchForWord(matrix, row - 1, col, matrix.Count, matrix[row].Count(), wordToCheck, direction, searchWord);
                    break;
                case 3:
                    //go down
                    SearchForWord(matrix, row + 1, col, matrix.Count, matrix[row].Count(), wordToCheck, direction, searchWord);
                    break;
                case 4:
                    //go left-up diagonal
                    SearchForWord(matrix, row - 1, col - 1, matrix.Count, matrix[row].Count(), wordToCheck, direction, searchWord);
                    break;
                case 5:
                    //go right-up diagonal
                    SearchForWord(matrix, row - 1, col + 1, matrix.Count, matrix[row].Count(), wordToCheck, direction, searchWord);
                    break;
                case 6:
                    //go left-down diagonal
                    SearchForWord(matrix, row + 1, col - 1, matrix.Count, matrix[row].Count(), wordToCheck, direction, searchWord);
                    break;
                case 7:
                    //go right-down diagonal
                    SearchForWord(matrix, row + 1, col + 1, matrix.Count, matrix[row].Count(), wordToCheck, direction, searchWord);
                    break;
            }
        }
    }
}
