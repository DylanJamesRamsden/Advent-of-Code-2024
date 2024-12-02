using System.Linq;

List<List<Int32>> PuzzleData = new List<List<Int32>>();

StreamReader streamReader = new StreamReader("C:\\Users\\Dylan\\Documents\\GitHub\\Advent-of-Code-2024\\Day 2\\PuzzleData.txt");
string streamReaderLine = streamReader.ReadLine();
while (streamReaderLine != null)
{
    // Console.WriteLine(streamReaderLine);
    string[] streamReaderLineSplit = streamReaderLine.Split(",");

    List<Int32> newList = new List<Int32>();
    foreach (string value in streamReaderLineSplit)
    {
        newList.Add(Int32.Parse(value));
    }
    PuzzleData.Add(newList);

    streamReaderLine = streamReader.ReadLine();
}
streamReader.Close();

List<List<Int32>> goodApples = new List<List<Int32>>();
List<List<Int32>> badApples = new List<List<Int32>>();
foreach (var report in PuzzleData)
{
    bool validReport = IsReportValid(report);
    if (validReport)
        goodApples.Add(report);
    else badApples.Add(report);
}

Console.WriteLine("Valid reports: " + goodApples.Count.ToString());
Console.WriteLine("Invalid reports: " + badApples.Count.ToString());

Int32 fixableApples = 0;
foreach (var report in badApples)
{
    bool fixable = false;
    for (int i = 0; i <= report.Count - 1; i++)
    {
        string modifiedReportString = "";
        List<Int32> modifiedReport = new List<Int32>();
        for (int copy = 0; copy <= report.Count - 1; copy++)
        {
            if (copy != i)
            {
                modifiedReport.Add(report[copy]);
                modifiedReportString += report[copy].ToString() + ",";
            }
        }

        bool isReportValid = IsReportValid(modifiedReport);
        if (isReportValid == true)
        {
            fixableApples++;
            break;
        }
    }
}

Console.WriteLine("Fixable reports: " + fixableApples.ToString());
Console.WriteLine("*******************************************");
Console.WriteLine("Total valid reports: " + (fixableApples + goodApples.Count).ToString());



bool IsReportValid(List<Int32> reportToCheck)
{
    // Flow
    // 1 = asc
    // 2 = dsc
    Int32 direction = -1;
    for (int i = 0; i < reportToCheck.Count - 1; i++)
    {
        // Duplicate (invalid report)
        if (reportToCheck[i] == reportToCheck[i + 1])
        {
            return false;
        }

        if (i == 0)
        {
            direction = reportToCheck[i] < reportToCheck[i + 1] ? 1 : 2;
        }
        else
        {
            if (direction == 1)
            {
                if (reportToCheck[i] > reportToCheck[i + 1]) { return false; }
            }
            else if (direction == 2)
            {
                if (reportToCheck[i] < reportToCheck[i + 1]) { return false; }
            }
        }
    }

    for (int i = 0; i < reportToCheck.Count - 1; i++)
    {
        Int32 difference = Math.Abs(reportToCheck[i] - reportToCheck[i + 1]);
        if (!(difference >= 1 && difference <= 3))
        {
            return false;
        }
    }


    return true;
}
