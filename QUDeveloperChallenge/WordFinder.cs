namespace QUDeveloperChallenge
{
    public class WordFinder
    {
        public char[][] Matrix { get; private set; }
        private int Rows;
        private int Columns;
        private bool validMatrix = true;

        public WordFinder(IEnumerable<string> matrix)
        {
            //I turn the string enumerable into a matrix by turning each string into an array of chars, and then building an array of those arrays.
            //The first array represents my rows, the second represents my columns.
            if (matrix.Count() > 0)
            {
                Matrix = matrix.Select(row => row.ToCharArray()).ToArray();
                Rows = Matrix.Length;
                //Make sure I have any rows.
                if (Rows > 0)
                    Columns = Matrix[0].Length;
                //If my rows and columns are not equal my matrix is invalid
                if(Rows != Columns)
                {
                    Console.WriteLine("The provided matrix is invalid");
                    validMatrix = false;
                }
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            if (validMatrix)
            {
                //Using a HashSet makes sure that words are not repeated
                var words = new HashSet<string>(wordstream);
                //I save each word and the number of times it appears in a dictionary for easier access.
                var wordsCount = new Dictionary<string, int>();

                foreach (var word in words)
                {
                    //I count how many times the word comes up in the matrix
                    int count = CountWordInMatrix(word);
                    //I add the word to my dictionary with the amount of times it came up in the matrix
                    wordsCount.Add(word, count);
                }

                //If no words are found in the matrix I return an empty array
                if (wordsCount.Values.All(count => count == 0))
                {
                    return [];
                }

                //I order by value (the number of times each word appears), then take the top 10, then return the words.
                return wordsCount.OrderByDescending(count => count.Value)
                                .Take(10)
                                .Select(word => word.Key);
            }
            else
            {
                Console.WriteLine("Can't operate, invalid matrix");
                return [];
            }
        }

        private int CountWordInMatrix(string word)
        {
            int count = 0;

            //First I go through each row, transforming the array into a string and then looking for the word in that string
            for (int row = 0; row < Rows; row++)
            {
                string rowWord = new string(Matrix[row]);
                count += CountWordInString(rowWord, word);
            }

            //I then go through the columns
            for (int column = 0; column < Columns; column++)
            {
                //I create an array of chars the same size as the rows I have to save my words.
                char[] columnArray = new char[Rows];
                //For each row, I get the letter corresponding to (row, column), then save it to my column array
                for (int row = 0; row < Rows; row++)
                    columnArray[row] = Matrix[row][column];

                //I turn my column array into a string
                string colWord = new string(columnArray);
                count += CountWordInString(colWord, word);
            }

            return count;
        }

        private int CountWordInString(string stringToCheck, string word)
        {
            int count = 0;
            //Get the index of the word
            int index = stringToCheck.IndexOf(word);

            //If I found the word I look for it in the next part of the string
            while (index != -1)
            {
                count++;
                index = stringToCheck.IndexOf(word, index + 1);
            }

            return count;
        }
    }
}
