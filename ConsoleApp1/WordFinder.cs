namespace ConsoleApp1;

public class WordFinder
{
    private IEnumerable<string> _matrix;

    public WordFinder(IEnumerable<string> matrix)
    {
        _matrix = matrix;
    }

    public bool IsValidMatrix()
    {
        var wordLength = _matrix.First().Length;
        var height = _matrix.Count();

        return _matrix.All(x => x.Length <= 64) &&
            _matrix.All(x => x.Length == wordLength) &&
            height <= 64;
    }

    public Dictionary<string, int> Find(IEnumerable<string> wordStream)
    {
        Dictionary<string, int> foundWords = new Dictionary<string, int>();

        IEnumerable<string> verticalMatrix = GetVerticalMatrix();

        foreach (var word in wordStream)
        {
            var horizontalContainer = _matrix.Select(x => (x, x.Split(word).Count() - 1));
            var verticalContainer = (verticalMatrix.Select(x => (x, x.Split(word).Count() - 1)));

            BuildDictionary(ref foundWords, horizontalContainer, word);
            BuildDictionary(ref foundWords, verticalContainer, word);
        }

        return foundWords;
    }

    public void PrintResults(IEnumerable<string> wordStream)
    {
        if (!IsValidMatrix())
        {
            Console.WriteLine("Verify the words' length. " +
                "The length should be the same for each word " +
                "and matrix should not be greater than 64x64");
        }
        else
        {
            var results = Find(wordStream)
                .OrderByDescending(x => x.Value)
                .Where(x => x.Value > 0);

            foreach (var result in results)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("word: " + result.Key + " Repetitions: " + result.Value);
            }
        }
    }

    private void BuildDictionary(ref Dictionary<string, int> foundWords,
        IEnumerable<(string x, int)> contains,
        string word)
    {
        if (contains.Any())
        {
            if (foundWords.ContainsKey(word))
            {
                foundWords[word] += contains.Sum(x => x.Item2);
            }
            else
            {
                foundWords[word] = contains.Sum(x => x.Item2);
            }
        }
    }

    private IEnumerable<string> GetVerticalMatrix()
    {
        var verticalMatrix = new List<string>();

        var wordLenght = _matrix.First().Length;

        for (var i = 0; i < wordLenght; i++)
        {
            var word = new string(_matrix
                .Select(x => x.ToCharArray()[i]).ToArray());

            verticalMatrix.Add(word);
        }

        return verticalMatrix;
    }
}
