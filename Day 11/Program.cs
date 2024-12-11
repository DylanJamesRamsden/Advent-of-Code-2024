List<Int64> stones = new List<Int64>();

StreamReader streamReaderStoneData = new StreamReader("C:\\Users\\Darth Vader\\Documents\\GitHub\\Advent-of-Code-2024\\Day 11\\PuzzleData.txt");
string streamReaderStoneDataLine = streamReaderStoneData.ReadLine();
while (streamReaderStoneDataLine != null)
{
    string[] splitStones = streamReaderStoneDataLine.Split(" ");
    foreach (string stone in splitStones)
    {
        stones.Add(Int64.Parse(stone));
    }

    streamReaderStoneDataLine = streamReaderStoneData.ReadLine();
}
streamReaderStoneData.Close();

OutputData(stones);

for (int i = 1; i <= 75; i++)
{
    stones = Blink(stones);
    // putData(stones);
}

Console.WriteLine("Stones: " + stones.Count.ToString());

void OutputData(List<Int64> StoneData)
{
    String outputDataString = "";
    foreach (Int64 stone in StoneData)
    {
        outputDataString += stone.ToString() + " ";
    }

    Console.WriteLine(outputDataString);
}

List<Int64> Blink(List<Int64> StoneData)
{
    List<Int64> afterBlinkData = new List<Int64>();

    foreach (Int64 stone in StoneData)
    {
        // Rule 1
        if (stone == 0)
        {
            afterBlinkData.Add(1);
        }
        // Rule 2
        else if (stone.ToString().Length % 2 == 0)
        {
            String string1 = "";
            String string2 = "";
            BrakeString(ref string1, ref string2, stone.ToString());

            afterBlinkData.Add(Int64.Parse(string1));
            afterBlinkData.Add(Int64.Parse(string2));
        }
        // Rule 3
        else
        {
            afterBlinkData.Add(stone * 2024);
        }
    }

    Console.WriteLine(afterBlinkData.Count.ToString());

    return afterBlinkData;
}

void BrakeString(ref String Part1, ref String Part2, string data)
{
    string firstString = data.ToString().Substring(0, data.ToString().Length / 2);
    string secondString = data.ToString().Substring(data.ToString().Length / 2, data.ToString().Length / 2);

    /*FormatString(ref Part1, firstString);
    FormatString(ref Part2, secondString);*/

    Part1 = Int64.Parse(firstString).ToString();
    Part2 = Int64.Parse(secondString).ToString();
}
void FormatString(ref String FormattedString, string data)
{
    string newStringFormatted = "";

    bool bOnlyZeros = true;
    for (int z = 0; z < data.Count(); z++)
    {
        if (data[z] != '0')
        {
            bOnlyZeros = false;
            break;
        }
    }

    if (bOnlyZeros)
    {
        newStringFormatted = data[0].ToString();
    }
    else if (data.Length == 1)
    {
        newStringFormatted = data[0].ToString();

    }
    else
    {
        for (int i = 0; i < data.Count(); i++)
        {
            // First
            if (i == 0)
            {
                if (data[i] != '0') newStringFormatted += data[i].ToString();
            }
            // Rest
            else
            {
                if (data[i] == '0')
                {
                    Int32 consecutiveZeroCounter = 0;
                    for (int j = i + 1; j < data.Count(); j++)
                    {
                        if (data[j] == '0')
                        {
                            consecutiveZeroCounter++;
                        }
                        else break;
                    }

                    newStringFormatted += data[i].ToString();
                    if (consecutiveZeroCounter > 0)
                    {
                        i += consecutiveZeroCounter;
                    }
                }
                else
                {
                    newStringFormatted += data[i].ToString();
                }
            }
        }
    }

    FormattedString = newStringFormatted;
}
