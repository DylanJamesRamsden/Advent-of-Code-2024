// RULES
// ***********************************************************************************************************************************
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

List<List<Int32>> puzzleRules = new List<List<Int32>>();

StreamReader streamReaderPuzzleRules = new StreamReader("C:\\Users\\Dylan\\Documents\\GitHub\\Advent-of-Code-2024\\Day 5\\PuzzleRules.txt");
string streamReaderPuzzleRuleLine = streamReaderPuzzleRules.ReadLine();
while (streamReaderPuzzleRuleLine != null)
{
    List<Int32> splitRules = new List<Int32>();
    string[] splitRulesString = streamReaderPuzzleRuleLine.Split("|");
    splitRules.Add(Int32.Parse(splitRulesString[0]));
    splitRules.Add(Int32.Parse(splitRulesString[1]));

    puzzleRules.Add(splitRules);

    streamReaderPuzzleRuleLine = streamReaderPuzzleRules.ReadLine();
}
streamReaderPuzzleRules.Close();

/*foreach (List<Int32> puzzleRule in puzzleRules)
{
    string ruleString = "";
    foreach (Int32 rule in puzzleRule)
    {
        ruleString += rule.ToString() + ",";
    }

    Console.WriteLine(ruleString);
}*/

Console.WriteLine("Rules loaded: " + puzzleRules.Count.ToString());
// ***********************************************************************************************************************************

// PUZZLE DATA
// ***********************************************************************************************************************************
List<List<Int32>> puzzleData = new List<List<Int32>>();

StreamReader streamReaderPuzzleData = new StreamReader("C:\\Users\\Dylan\\Documents\\GitHub\\Advent-of-Code-2024\\Day 5\\PuzzleData.txt");
string streamReaderPuzzleDataLine = streamReaderPuzzleData.ReadLine();
while (streamReaderPuzzleDataLine != null)
{
    List<Int32> splitData = new List<Int32>();
    string[] splitDataString = streamReaderPuzzleDataLine.Split(",");
    foreach (var data in splitDataString)
    {
        splitData.Add(Int32.Parse(data));
    }

    puzzleData.Add(splitData);

    streamReaderPuzzleDataLine = streamReaderPuzzleData.ReadLine();
}
streamReaderPuzzleData.Close();

/*foreach (List<Int32> data in puzzleData)
{
    string dataString = "";
    foreach (Int32 value in data)
    {
        dataString += value.ToString() + ",";
    }

    Console.WriteLine(dataString);
}*/

Console.WriteLine("Data loaded: " + puzzleData.Count.ToString());
// ***********************************************************************************************************************************

// CORRECT UPDATES
// ***********************************************************************************************************************************
List<List<Int32>> correctUpdates = new List<List<Int32>>();
List<List<Int32>> incorrectUpdates= new List<List<Int32>>();
foreach (List<Int32> data in puzzleData)
{
    if (IsCorrectUpdate(puzzleRules, data))
    {
        correctUpdates.Add(data);
    }
    else
    {
        incorrectUpdates.Add(data);
    }
}

Console.WriteLine("Correct Updates: " + correctUpdates.Count.ToString());
// ***********************************************************************************************************************************

// ADDITIONS
// ***********************************************************************************************************************************
Int32 middlePageNumberAddition = 0;
foreach (List<Int32> data in correctUpdates)
{
    middlePageNumberAddition += data[(data.Count - 1) / 2];
}

Console.WriteLine("Additions: " + middlePageNumberAddition.ToString());
// ***********************************************************************************************************************************

// FIXING INCORRECT UPDATES
// ***********************************************************************************************************************************
List<List<Int32>> fixedUpdate = new List<List<int>>();
fixedUpdate = UpdateIncorrectFixes(incorrectUpdates);

Console.WriteLine("Fixed number:" + fixedUpdate.Count.ToString());

Int32 fixedMiddlePageNumberAddition = 0;
foreach (List<Int32> data in fixedUpdate)
{
    double index = data.Count / 2;
    fixedMiddlePageNumberAddition += data[Int32.Parse(Math.Floor(index).ToString())];
}

Console.WriteLine("Fixed Additions: " + fixedMiddlePageNumberAddition.ToString());

List<List<Int32>> UpdateIncorrectFixes(List<List<Int32>> fixedUpdate)
{
    Console.WriteLine("______________________________________");
    bool FixesPending = false;

    for (int i = 0; i < incorrectUpdates.Count; i++)
    {
        if (!IsCorrectUpdate(puzzleRules, incorrectUpdates[i]))
        {
            List<Int32> attemptedFix = AttemptToFix(puzzleRules, incorrectUpdates[i]);
            if (IsCorrectUpdate(puzzleRules, attemptedFix))
            {
                //Console.WriteLine("Found correct!");
                incorrectUpdates[i] = attemptedFix;
            }
            else
            {
                //Console.WriteLine("Found incorrect!");
                incorrectUpdates[i] = attemptedFix;
                FixesPending = true;
            }

            string updateString = "";
            foreach (Int32 value in attemptedFix)
            {
                updateString += value.ToString() + ",";
            }
            Console.WriteLine(updateString + "=========" + i.ToString());

            /*string updateString = "";
            foreach (Int32 value in attemptedFix)
            {
                updateString += value.ToString() + ",";
            }
            Console.WriteLine(updateString);*/
        }
    }

    if (FixesPending)
    {
       fixedUpdate = UpdateIncorrectFixes(incorrectUpdates);
    }

    return fixedUpdate;
}

bool IsCorrectUpdate(List<List<Int32>> Rules, List<Int32> dataToCheck)
{
    foreach (var rule in Rules)
    {
        if (!CheckRule(rule, dataToCheck))
            return false;
    }

    return true;
}

bool CheckRule(List<Int32> Rule, List<Int32> dataToCheck)
{
    // Rule doesn't apply to current data
    if (!(dataToCheck.Contains(Rule[0])) || !(dataToCheck.Contains(Rule[1])))
        return true;

    Int32 rule1Index = -1;
    Int32 rule2Index = -1;
    for (int i = 0; i < dataToCheck.Count; i++)
    {
        if (dataToCheck[i] == Rule[0] && rule1Index == -1)
        {
            rule1Index = i;
        }
        else if (dataToCheck[i] == Rule[1] && rule2Index == -1)
        {
            rule2Index = i;
        }
    }

    if (rule1Index < rule2Index)
        return true;
    else
        return false;
}

List<Int32> AttemptToFix(List<List<Int32>> Rules, List<Int32> dataToCheck)
{
    List<Int32> attemptedFix = dataToCheck;

    for (int i = 0; i < dataToCheck.Count; i++)
    {
        attemptedFix = AttemptRuleFixInteger(Rules, dataToCheck, dataToCheck[i]);
    }

    return attemptedFix;
}

List<Int32> AttemptRuleFix(List<Int32> Rule, List<Int32> dataToCheck)
{
    // Rule doesn't apply to current data
    if (!(dataToCheck.Contains(Rule[0])) || !(dataToCheck.Contains(Rule[1])))
        return dataToCheck;

    Int32 rule1Index = -1;
    Int32 rule2Index = -1;
    for (int i = 0; i < dataToCheck.Count; i++)
    {
        if (dataToCheck[i] == Rule[0] && rule1Index == -1)
        {
            rule1Index = i;
        }
        else if (dataToCheck[i] == Rule[1] && rule2Index == -1)
        {
            rule2Index = i;
        }
    }

    if (rule1Index > rule2Index)
    {
        string beforeFixString = "";
        foreach (var dataValue in dataToCheck)
        {
            beforeFixString += dataValue.ToString() + ", ";
        }
        Console.WriteLine("Before fix: " + beforeFixString);

        Int32 rule1Value = dataToCheck[rule1Index];
        Int32 rule2Value = dataToCheck[rule2Index];

        dataToCheck[rule1Index] = rule2Value;
        dataToCheck[rule2Index] = rule1Value;

        string afterFixString = "";
        foreach (var dataValue in dataToCheck)
        {
            afterFixString += dataValue.ToString() + ", ";
        }
        Console.WriteLine("After fix: " + afterFixString);
    }

    return dataToCheck;
}

List<Int32> AttemptRuleFixInteger(List<List<Int32>> Rules, List<Int32> dataToCheck, Int32 IntegerToFix)
{
    foreach (var rule in Rules)
    {
        // 3, 4, 5, 9
        // 4
        if (rule[0] == IntegerToFix || rule[1] == IntegerToFix)
        {
            if (!(dataToCheck.Contains(rule[0])) || !(dataToCheck.Contains(rule[1])))
                continue;

            Int32 rule1Index = -1;
            Int32 rule2Index = -1;
            for (int i = 0; i < dataToCheck.Count; i++)
            {
                if (dataToCheck[i] == rule[0] && rule1Index == -1)
                {
                    rule1Index = i;
                }
                else if (dataToCheck[i] == rule[1] && rule2Index == -1)
                {
                    rule2Index = i;
                }
            }

            if (rule1Index > rule2Index)
            {
                Int32 rule1Value = dataToCheck[rule1Index];
                Int32 rule2Value = dataToCheck[rule2Index];

                dataToCheck[rule1Index] = rule2Value;
                dataToCheck[rule2Index] = rule1Value;
            }
        }
    }

    return dataToCheck;
}