using System.Net;

public struct Branch
{
    public Int64 LevelIndex;

    public List<char> Calculations = new List<char>();

    public Branch(Int64 levelIndex, List<char> calculations)
    {
        LevelIndex = levelIndex;
        Calculations = calculations;
    }
}

class MainProgram
{
    
    static void Main(string[] args)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        /* the code that you want to measure comes here */

        Dictionary<Int64, List<Int64>> calibrations = new Dictionary<Int64, List<Int64>>();

        StreamReader streamReaderCalibration = new StreamReader("C:\\Users\\Dylan\\Documents\\GitHub\\Advent-of-Code-2024\\Day 7\\PuzzleData.txt");
        string streamReaderCalibrationLine = streamReaderCalibration.ReadLine();
        while (streamReaderCalibrationLine != null)
        {
            string[] calibrationCalculationSplit = streamReaderCalibrationLine.Split(":");
            Int64 calibrationAnswer = Int64.Parse(calibrationCalculationSplit[0]);

            List<Int64> calculationValues = new List<Int64>();
            string[] calculationValuesString = calibrationCalculationSplit[1].Split(" ");
            // Starting at 1 because I am to lazt to format the string and remove that first space
            for (int i = 1; i < calculationValuesString.Length; i++)
            {
                calculationValues.Add(Int64.Parse(calculationValuesString[i]));
            }

            calibrations.Add(calibrationAnswer, calculationValues);

            streamReaderCalibrationLine = streamReaderCalibration.ReadLine();
        }
        streamReaderCalibration.Close();

        // ******************************************************************************************************
        // Its a tree!

        Dictionary<Int64, List<Int64>> correctCalibrations = new Dictionary<Int64, List<Int64>>();
        Dictionary<Int64, List<Int64>> incorrectCalibrations = new Dictionary<Int64, List<Int64>>();
        foreach (KeyValuePair<Int64, List<Int64>> calibration in calibrations)
        {
            if (IsCorrectCalibration_Part1(calibration.Key, calibration.Value))
            {
                correctCalibrations.Add(calibration.Key, calibration.Value);
            }
            else
            {
                incorrectCalibrations.Add(calibration.Key, calibration.Value);
            }
        }

        Int64 correctCalibrationsAdded = 0;
        foreach (var correctCalibration in correctCalibrations)
        {
            correctCalibrationsAdded += correctCalibration.Key;
        }

        Dictionary<Int64, List<Int64>> fixableCalibrations = new Dictionary<Int64, List<Int64>>();
        foreach (KeyValuePair<Int64, List<Int64>> incorrectCalibration in incorrectCalibrations)
        {
            if (IsCorrectCalibration_Part2(incorrectCalibration.Key, incorrectCalibration.Value))
            {
                fixableCalibrations.Add(incorrectCalibration.Key, incorrectCalibration.Value);
            }
        }

        Int64 fixableCalibrationsAdded = 0;
        foreach (var fixableCalibration in fixableCalibrations)
        {
            fixableCalibrationsAdded += fixableCalibration.Key;
        }

        Console.WriteLine("Correct Calibrations Added: " + correctCalibrationsAdded.ToString());
        Console.WriteLine("Incorrect Calibrations: " + incorrectCalibrations.Count.ToString());
        Console.WriteLine("Fixable Calibrations Added: " + fixableCalibrationsAdded.ToString());

        Console.WriteLine("Fixable Calibrations: " + fixableCalibrations.Count.ToString());
        Console.WriteLine("Total (Correct and Fixable): " + (correctCalibrationsAdded + fixableCalibrationsAdded).ToString());

        watch.Stop();
        var elapsedMs = watch.Elapsed;
        Console.WriteLine("Execution time: " + elapsedMs.ToString());
    }

    static bool IsCorrectCalibration_Part1(Int64 Key, List<Int64> calibrationData)
    {
        // just adding index 0
        List<Branch> branches = new List<Branch>();
        branches.Add(new Branch(0, new List<char>()));
        bool bAtBottom = false;
        Int64 LevelIndex = 0;
        while (!bAtBottom)
        {
            LevelIndex++;

            List<Branch> previousLevelBranches = new List<Branch>();
            foreach (Branch branch in branches)
            {
                if (branch.LevelIndex == LevelIndex - 1)
                {
                    previousLevelBranches.Add(branch);
                }
            }

            Int64 branchesAdded = 0;
            foreach (Branch branch in previousLevelBranches)
            {
                for (Int64 i = 0; i < 2; i++)
                {
                    // Copy list
                    List<char> newCalculation = new List<char>();
                    foreach (char c in branch.Calculations)
                    {
                        newCalculation.Add(c);
                    }

                    // +
                    if (i == 0)
                    {
                        newCalculation.Add('+');
                        branches.Add(new Branch(LevelIndex, newCalculation));

                        branchesAdded++;
                    }
                    // -
                    else if (i == 1)
                    {
                        newCalculation.Add('*');
                        branches.Add(new Branch(LevelIndex, newCalculation));

                        branchesAdded++;
                    }
                }
            }

            if (LevelIndex == calibrationData.Count - 1)
            {
                bAtBottom = true;
            }

            // Console.WriteLine("Branches added: " + branchesAdded.ToString());
        }

        // Console.WriteLine();

        List<Branch> answerBranches = new List<Branch>();
        foreach (Branch branch in branches)
        {
            if (branch.LevelIndex == LevelIndex)
            {
                answerBranches.Add(branch);
            }
        }

        bool bCorrectAnswerFound = false;
        List<Int64> Answers = new List<Int64>();
        foreach (Branch answerBranch in answerBranches)
        {
            Int64 Answer = calibrationData[0];
            string calculationString = Answer.ToString() + " ";
            for (int i = 1; i < calibrationData.Count; i++)
            {
                if (answerBranch.Calculations[i - 1] == '+')
                {
                    Answer = Answer + calibrationData[i];
                    calculationString += answerBranch.Calculations[i - 1] + " " + calibrationData[i].ToString() + " ";
                }
                else if (answerBranch.Calculations[i - 1] == '*')
                {
                    Answer = Answer * calibrationData[i];
                    calculationString += answerBranch.Calculations[i - 1] + " " + calibrationData[i].ToString() + " ";
                }
            }

            /*Console.WriteLine(calculationString);
            Console.WriteLine("Answer: " + Answer);*/
            if (Answer == Key)
            {
                bCorrectAnswerFound = true;
                break;
            }
        }

        return bCorrectAnswerFound;
    }

    static bool IsCorrectCalibration_Part2(Int64 Key, List<Int64> calibrationData)
    {
        // Console.WriteLine("Got em!");

        // just adding index 0
        List<Branch> branches = new List<Branch>();
        branches.Add(new Branch(0, new List<char>()));
        bool bAtBottom = false;
        Int64 LevelIndex = 0;
        while (!bAtBottom)
        {
            LevelIndex++;

            List<Branch> previousLevelBranches = new List<Branch>();
            foreach (Branch branch in branches)
            {
                if (branch.LevelIndex == LevelIndex - 1)
                {
                    previousLevelBranches.Add(branch);
                }
            }

            foreach (Branch branch in previousLevelBranches)
            {
                for (Int64 i = 0; i < 3; i++)
                {
                    // Copy list
                    List<char> newCalculation = new List<char>();
                    foreach (char c in branch.Calculations)
                    {
                        newCalculation.Add(c);
                    }

                    // +
                    if (i == 0)
                    {
                        newCalculation.Add('+');
                        branches.Add(new Branch(LevelIndex, newCalculation));
                    }
                    // -
                    else if (i == 1)
                    {
                        newCalculation.Add('*');
                        branches.Add(new Branch(LevelIndex, newCalculation));
                    }
                    else if (i == 2)
                    {
                        newCalculation.Add('|');
                        branches.Add(new Branch(LevelIndex, newCalculation));
                    }
                }
            }

            if (LevelIndex == calibrationData.Count - 1)
            {
                bAtBottom = true;
            }
        }

        // Console.WriteLine("Branches----: " + branches.Count.ToString());

        // Console.WriteLine();

        List<Branch> answerBranches = new List<Branch>();
        foreach (Branch branch in branches)
        {
            if (branch.LevelIndex == LevelIndex)
            {
                answerBranches.Add(branch);
            }
        }

        // Console.WriteLine(answerBranches.Count.ToString());

        bool bCorrectAnswerFound = false;
        List<Int64> Answers = new List<Int64>();
        foreach (Branch answerBranch in answerBranches)
        {
            Int64 Answer = calibrationData[0];
            string calculationString = Answer.ToString() + " ";
            for (int i = 1; i < calibrationData.Count; i++)
            {
                if (answerBranch.Calculations[i - 1] == '+')
                {
                    Answer = Answer + calibrationData[i];
                    //calculationString += answerBranch.Calculations[i - 1] + " " + calibrationData[i].ToString() + " ";
                }
                else if (answerBranch.Calculations[i - 1] == '*')
                {
                    Answer = Answer * calibrationData[i];
                    //calculationString += answerBranch.Calculations[i - 1] + " " + calibrationData[i].ToString() + " ";
                }
                else if (answerBranch.Calculations[i - 1] == '|')
                {
                    Answer = Int64.Parse(Answer.ToString() + calibrationData[i].ToString());
                    //calculationString += "(->)" + Answer.ToString();
                }
            }

           /* Console.WriteLine(calculationString);
            Console.WriteLine("Answer: " + Answer);*/
            if (Answer == Key)
            {
                bCorrectAnswerFound = true;
                break;
            }
        }

        return bCorrectAnswerFound;
    }
}


