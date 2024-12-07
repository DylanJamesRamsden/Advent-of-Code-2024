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
        Console.WriteLine("Got em!");
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
        foreach (KeyValuePair<Int64, List<Int64>> calibration in calibrations)
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
                foreach(Branch branch in branches)
                {
                    if (branch.LevelIndex == LevelIndex - 1)
                    {
                        previousLevelBranches.Add(branch);
                    }
                }

                Int64 branchesAdded = 0;
                foreach(Branch branch in previousLevelBranches)
                {
                    for (Int64 i = 0; i < 2; i++)
                    {
                        // +
                        if (i == 0)
                        {
                            // Copy list
                            List<char> newCalculation = new List<char>();
                            foreach (char c in branch.Calculations)
                            {
                                newCalculation.Add(c);
                            }

                            newCalculation.Add('+');
                            branches.Add(new Branch(LevelIndex, newCalculation));

                            branchesAdded++;
                        }
                        // -
                        else if (i == 1)
                        {
                            List<char> newCalculation = new List<char>();
                            foreach (char c in branch.Calculations)
                            {
                                newCalculation.Add(c);
                            }

                            newCalculation.Add('*');
                            branches.Add(new Branch(LevelIndex, newCalculation));

                            branchesAdded++;
                        }                       
                    }
                }

                if (LevelIndex == calibration.Value.Count - 1)
                {
                    bAtBottom = true;
                }

                Console.WriteLine("Branches added: " + branchesAdded.ToString());
            }

            Console.WriteLine();

            List<Branch> answerBranches = new List<Branch>();
            foreach (Branch branch in branches)
            {
                if (branch.LevelIndex == LevelIndex)
                {
                    answerBranches.Add(branch);
                }
            }

            List<Int64> Answers = new List<Int64>();
            foreach (Branch answerBranch in answerBranches)
            {
                Int64 Answer = calibration.Value[0];
                string calculationString = Answer.ToString() + " ";
                for (int i = 1; i < calibration.Value.Count; i++)
                {
                    if (answerBranch.Calculations[i - 1] == '+')
                    {
                        Answer = Answer + calibration.Value[i];
                        calculationString += answerBranch.Calculations[i - 1] + " " + calibration.Value[i].ToString() + " ";
                    }
                    else if (answerBranch.Calculations[i - 1] == '*')
                    {
                        Answer = Answer * calibration.Value[i];
                        calculationString += answerBranch.Calculations[i - 1] + " " + calibration.Value[i].ToString() + " ";
                    }
                }

                Console.WriteLine(calculationString);
                Console.WriteLine("Answer: " + Answer);
                if (Answer == calibration.Key)
                {
                    correctCalibrations.Add(calibration.Key, calibration.Value);
                    break;
                }
            }
        }

        Int64 correctCalibrationsAdded = 0;
        foreach (var correctCalibration in correctCalibrations)
        {
            correctCalibrationsAdded += correctCalibration.Key;
        }

        Console.WriteLine("Correct Calibrations Added: " + correctCalibrationsAdded.ToString());
    }
}


