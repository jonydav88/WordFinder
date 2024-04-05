// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

/******for testing purposes****/
IEnumerable<string> matrix = new List<string>()
{
    "tegtgo",
    "goosgo",
    "teasgo"
};

IEnumerable<string> wordStream = new List<string>()
{
    "go",
    "tea"
};
/****************************/

var wordFinder = new WordFinder(matrix);

wordFinder.PrintResults(wordStream);
