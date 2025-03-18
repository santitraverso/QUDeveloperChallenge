using QUDeveloperChallenge;

Console.WriteLine("Enter file path for matrix, each word should be a line");
string matrixPath = Console.ReadLine();

if (!File.Exists(matrixPath))
{
    Console.WriteLine("Could not find the file for the matrix");
    return;
}
string[] matrix = File.ReadAllLines(matrixPath);

Console.WriteLine("Enter file path for word stream, each word should be a line");
string wordStreamPath = Console.ReadLine();

if (!File.Exists(wordStreamPath))
{
    Console.WriteLine("Could not find the file for the word stream");
    return;
}
string[] wordStream = File.ReadAllLines(wordStreamPath);

WordFinder wordFinder = new WordFinder(matrix);
var result = wordFinder.Find(wordStream);

foreach(var word in result)
{
    Console.WriteLine(word);
}
Console.ReadLine();