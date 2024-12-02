using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

List<List<Int32>> PuzzleData = new List<List<Int32>>();

StreamReader streamReader = new StreamReader("C:\\Users\\Darth Vader\\Documents\\GitHub\\Advent-of-Code-2024\\Day 2\\PuzzleData.txt");
string streamReaderLine = streamReader.ReadLine();
while (streamReaderLine != null)
{
    // Console.WriteLine(streamReaderLine);
    string[] streamReaderLineSplit = streamReaderLine.Split(" ");

    List<Int32> newList = new List<Int32>();
    foreach (string value in streamReaderLineSplit)
    {
        newList.Add(Int32.Parse(value));
    }
    PuzzleData.Add(newList);

    streamReaderLine = streamReader.ReadLine();
}
streamReader.Close();

foreach (var report in PuzzleData)
{
    string reportString = "";
    foreach (var reportItem in report)
    {
        reportString += reportItem.ToString() + ", ";
    }

    Console.WriteLine(reportString);
}

Console.WriteLine("Reports: " + PuzzleData.Count.ToString());

// FLOW ***************************************************************************************************
// 0 = Increasing
// 1 = Decreasing
List<Int32> reportFlowData = new List<Int32>();
foreach (var report in PuzzleData)
{
    // @TODO Could have a problem here if we 2 values are the same and next to eachother
    Int32 currentReportFlow = report[0] < report[1] ? 0 : 1;
    reportFlowData.Add(currentReportFlow);
}

foreach (Int32 flow in reportFlowData)
{
    Console.WriteLine(flow.ToString());
}

List<List<Int32>> correctFlowPuzzleData = new List<List<Int32>>();
List<List<Int32>> badApples = new List<List<Int32>>();
for (int i = 0; i < PuzzleData.Count; i++)
{
    Int32 flow = reportFlowData[i];
    bool validFlow = true;
    for (int j = 0; j < PuzzleData[i].Count - 1; j++)
    {
        if (flow == 0)
        {
            if (PuzzleData[i][j] > PuzzleData[i][j + 1])
            {
                validFlow = false;
                break;
            }
        }
        else
        {
            if (PuzzleData[i][j] < PuzzleData[i][j + 1])
            {
                validFlow = false;
                break;
            }
        }
    }

    if (validFlow)
    {
        correctFlowPuzzleData.Add(PuzzleData[i]);
    }
}

foreach (var report in correctFlowPuzzleData)
{
    string reportString = "";
    foreach (var reportItem in report)
    {
        reportString += reportItem.ToString() + ", ";
    }

    Console.WriteLine(reportString);
}
// FLOW ***************************************************************************************************

//DIFFERENCE ***************************************************************************************************
List<List<Int32>> correctDifferencePuzzleData = new List<List<Int32>>();
foreach (var report in correctFlowPuzzleData)
{
    bool differenceValid = true;
    for (int i = 0; i < report.Count - 1; i++)
    {
        Int32 difference = Math.Abs(report[i] - report[i + 1]);
        if (!(difference >= 1 && difference <= 3))
        {
            differenceValid = false;
            break;
        }
    }

    if (differenceValid)
    {
        correctDifferencePuzzleData.Add(report);
    }
    else
    {
        badApples.Add(report);
    }
}

Console.WriteLine("Valid Differences:");
foreach (var report in correctDifferencePuzzleData)
{
    string reportString = "";
    foreach (var reportItem in report)
    {
        reportString += reportItem.ToString() + ", ";
    }

    Console.WriteLine(reportString);
}

Console.WriteLine("Valid reports: " + correctDifferencePuzzleData.Count.ToString());
//DIFFERENCE ***************************************************************************************************

List<List<Int32>> goodApples = new List<List<Int32>>();
for (int i = badApples.Count - 1; i >= 0; i--)
{
    if (BruteForce(badApples[i]) == 1)
    {
        goodApples.Add(badApples[i]);
    }
}

Console.WriteLine("Single Differences:");
foreach (var report in goodApples)
{
    string reportString = "";
    foreach (var reportItem in report)
    {
        reportString += reportItem.ToString() + ", ";
    }

    Console.WriteLine(reportString);
}

Console.WriteLine("Good Apples: " + badApples.Count.ToString());

Int32 BruteForce(List<Int32> ListToBruteForce)
{
    Int32 correctionsNeeded = 0;
    for (int i = 0; i < ListToBruteForce.Count; i++)
    {
        List<Int32> modifiedList = ListToBruteForce;
        modifiedList.RemoveAt(i);

        bool Fixed = true;

        //Check Duplicates
        for (int j = 0; j < modifiedList.Count - 1; j++)
        {
            if (modifiedList[j] == modifiedList[j + 1])
            {
                Fixed = false;
            }
        }

        // Check calculations
        for (int j = 0; j < modifiedList.Count - 1; j++)
        {
            Int32 difference = Math.Abs(modifiedList[j] - modifiedList[j + 1]);
            if (!(difference >= 1 && difference <= 3))
            {
                Fixed = false;
            }
        }

        if (Fixed == true)
        {
            correctionsNeeded = 1;
            break;
        }
    }

    return correctionsNeeded;
}
