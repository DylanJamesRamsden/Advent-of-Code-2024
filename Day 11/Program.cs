using System.IO;

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

for (int j = 1; j <= 75; j++)
{
    List<Int64> afterBlinkData = new List<Int64>();

    for (Int32 i = 0; i < stones.Count; i++)
    {
        // Rule 1
        if (stones[i] == 0)
        {
            afterBlinkData.Add(1);
        }
        // Rule 2
        else if (stones[i].ToString().Length % 2 == 0)
        {
            string firstString = stones[i].ToString().Substring(0, stones[i].ToString().Length / 2);
            string secondString = stones[i].ToString().Substring(stones[i].ToString().Length / 2, stones[i].ToString().Length / 2);

            firstString = Int64.Parse(firstString).ToString();
            secondString = Int64.Parse(secondString).ToString();

            afterBlinkData.Add(Int64.Parse(firstString));
            afterBlinkData.Add(Int64.Parse(secondString));
        }
        // Rule 3
        else
        {
            afterBlinkData.Add(stones[i] * 2024);
        }
    }

    stones = afterBlinkData;
    Console.WriteLine(afterBlinkData.Count.ToString());
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
