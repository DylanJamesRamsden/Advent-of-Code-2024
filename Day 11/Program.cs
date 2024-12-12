using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

public struct Branch
{
    public Int64 LevelIndex;

    public List<char> Stones = new List<char>();

    public Branch(Int64 levelIndex, List<char> stones)
    {
        LevelIndex = levelIndex;
        Stones = stones;
    }
}

class MainProgram
{
    static void Main(string[] args)
    {
        Dictionary<Int64, List<List<Int64>>> stoneDictionary = new Dictionary<Int64, List<List<Int64>>>();
        /*List<Thread> runningThreads = new List<Thread>();
        List<List<Int64>> newStoneDataList = new List<List<Int64>>();*/
        /*List<Int64> stones = new List<Int64>();*/

       /* List<List<Int64>> stoneTree = new List<List<Int64>>();*/

        StreamReader streamReaderStoneData = new StreamReader("C:\\Users\\Darth Vader\\Documents\\GitHub\\Advent-of-Code-2024\\Day 11\\PuzzleData.txt");
        string streamReaderStoneDataLine = streamReaderStoneData.ReadLine();
        while (streamReaderStoneDataLine != null)
        {
            string[] splitStones = streamReaderStoneDataLine.Split(" ");
            List<Int64> startingStoneData = new List<Int64>();
            foreach (string stone in splitStones)
            {
                startingStoneData.Add(Int64.Parse(stone));
            }

            List<List<Int64>> newStoneLevel = new List<List<Int64>>();
            newStoneLevel.Add(startingStoneData);
            stoneDictionary.Add(0, newStoneLevel);

            streamReaderStoneDataLine = streamReaderStoneData.ReadLine();
        }
        streamReaderStoneData.Close();

        OutputAllData(stoneDictionary);

        for (int j = 1; j <= 75; j++)
        {
            if (j >= 2)
            {
                stoneDictionary.Remove(j - 2);
            }
            Console.WriteLine("[LEVEL] " + j.ToString());
            // Level is j - 1
            // List<Int64> afterBlinkData = new List<Int64>();
            Int64 level = j;
            List<List<Int64>> newStoneList = new List<List<Int64>>();
            stoneDictionary.Add(level, newStoneList);
            foreach (List<Int64> previousStones in stoneDictionary[level - 1])
            {
                // Console.WriteLine("[SIZE] " + previousStones.Count.ToString());

                // @TODO if greater than 500 million, we need to split the array
                if (previousStones.Count > 25000 && previousStones.Count % 2 == 0)
                {
                    // Thread thread1 = new Thread(new ParameterizedThreadStart(Blink))
                    // Console.WriteLine("Split!");
                    List<Int64> newstoneList1 = new List<Int64>();
                    List<Int64> newstoneList2 = new List<Int64>();
                    for (int copyIndex = 0; copyIndex < previousStones.Count; copyIndex++)
                    {
                        if (copyIndex < previousStones.Count / 2)
                        {
                            newstoneList1.Add(previousStones[copyIndex]);
                        }
                        else newstoneList2.Add(previousStones[copyIndex]);
                    }

                    /*Console.WriteLine("[SPLIT SIZE 1] " + newstoneList1.Count.ToString());
                    Console.WriteLine("[SPLIT SIZE 2] " + newstoneList2.Count.ToString());*/
                    // Handle the split
                    List<Int64> stonesAfterBlink1 = Blink(newstoneList1);
                    // Console.WriteLine(stonesAfterBlink1.Count.ToString());
                    stoneDictionary[level].Add(stonesAfterBlink1);

                    // Handle the split
                    List<Int64> stonesAfterBlink2 = Blink(newstoneList2);
                    // Console.WriteLine(stonesAfterBlink2.Count.ToString());
                    stoneDictionary[level].Add(stonesAfterBlink2);
                }
                else
                {
                    // Handle the split
                    List<Int64> stonesAfterBlink = Blink(previousStones);
                    //Console.WriteLine(stonesAfterBlink.Count.ToString());
                    stoneDictionary[level].Add(stonesAfterBlink);
                }
            }
        }

        Int64 totalRocks = 0;
        foreach (List<Int64> stones in stoneDictionary[25])
        {
            totalRocks += stones.Count;
        }

        //OutputAllData(stoneDictionary);
        Console.WriteLine("Total Rocks: " + totalRocks.ToString());
    }

    static List<Int64> Blink(List<Int64> previousStoneData)
    {
        List<Int64> afterBlinkStoneData = new List<Int64>();
        for (Int32 i = 0; i < previousStoneData.Count; i++)
        {
            // Rule 1
            if (previousStoneData[i] == 0)
            {
                afterBlinkStoneData.Add(1);
            }
            // Rule 2
            else if (previousStoneData[i].ToString().Length % 2 == 0)
            {
                string firstString = previousStoneData[i].ToString().Substring(0, previousStoneData[i].ToString().Length / 2);
                string secondString = previousStoneData[i].ToString().Substring(previousStoneData[i].ToString().Length / 2, previousStoneData[i].ToString().Length / 2);

                firstString = Int64.Parse(firstString).ToString();
                secondString = Int64.Parse(secondString).ToString();

                afterBlinkStoneData.Add(Int64.Parse(firstString));
                afterBlinkStoneData.Add(Int64.Parse(secondString));
            }
            // Rule 3
            else
            {
                afterBlinkStoneData.Add(previousStoneData[i] * 2024);
            }
        }

        return afterBlinkStoneData;
    }

    static void OutputAllData(Dictionary<Int64, List<List<Int64>>> data)
    {
        // Level
        foreach (Int64 i in data.Keys)
        {
            // List of stones in Level
            Console.WriteLine("-----------Level " + i.ToString() + "-----------");
            foreach(List<Int64> stoneData in data[i])
            {
                string stoneDataString = "Child: ";
                foreach (Int64 stone in stoneData)
                {
                    stoneDataString += "[" + stone.ToString() + "]";
                }
                Console.WriteLine(stoneDataString);
            }
        }
    }
}