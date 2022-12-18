using System;
using System.IO;
using System.Linq;

public interface ITestAppConfig
{
    int ColumnNumber
    {
        get;
    }
    char Delimiter
    {
        get;
    }
}

public class TestAppConfig : ITestAppConfig
{
    public int ColumnNumber
    {
        get;
    }
    public char Delimiter
    {
        get;
    }

    private static TestAppConfig instance = new TestAppConfig(1, '|');

    private TestAppConfig(int ColumnNumber, char Delimiter)
    {
        this.ColumnNumber = ColumnNumber;
        this.Delimiter = Delimiter;
    }

    public static TestAppConfig Instance
    {
        get
        {
            return instance;
        }
    }
}

class Program
{

    public string sortingwitherror(string line, string delimiter, int ColumnNumber) {

        var array_words = line.Split(delimiter);

        string returnvalue;
        returnvalue = array_words.Length ==1 ?  throw new InvalidOperationException() : array_words[ColumnNumber-1];
        return returnvalue;
    }

    static void Main(string[] args)
    {

        String path = args[0];
        Program pr = new Program(); 
        TestAppConfig myTestAppConfig = TestAppConfig.Instance;

        try
        {
            string[] lines = File.ReadAllLines(path);

            if (lines.Length >= 2)
            {

                string delimiter = myTestAppConfig.Delimiter.ToString();
                int ColumnNumber = myTestAppConfig.ColumnNumber;

                var res = from line in lines
                          let x = pr.sortingwitherror(line, delimiter, ColumnNumber)
                          orderby x
                          ascending
                          select line;

                try
                {
                    foreach (var line in res)
                    {
                        Console.WriteLine(line.Replace(myTestAppConfig.Delimiter, ' '));
                    }
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Delimiter not found in one of the lines");
                }
            }
            else
            {

                Console.WriteLine("The number of rows are not sufficent to do sorting");
            }
        }
        catch (FileNotFoundException)
        {

            Console.WriteLine("Specified file does not exist");
        }
    }
}
