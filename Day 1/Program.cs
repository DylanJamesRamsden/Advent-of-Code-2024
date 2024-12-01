// PART 1
// *********************************************************************************************************************************************************
List<int> puzzleDataA = new List<int>();
List<int> puzzleDataB = new List<int>();

// Reading from the Puzzle Data
StreamReader streamReader = new StreamReader("C:\\Users\\Dylan\\Documents\\GitHub\\Advent-of-Code-2024\\Day 1\\PuzzleData.txt");
string streamReaderLine = streamReader.ReadLine();
while (streamReaderLine != null)
{
    // Console.WriteLine(streamReaderLine);
    string[] streamReaderLineSplit = streamReaderLine.Split(",");

    puzzleDataA.Add(Int32.Parse(streamReaderLineSplit[0]));
    puzzleDataB.Add(Int32.Parse(streamReaderLineSplit[1]));

    streamReaderLine = streamReader.ReadLine();
}
streamReader.Close();

Console.WriteLine("PuzzleDataA length: " + puzzleDataA.Count.ToString());
Console.WriteLine("PuzzleDataB length: " + puzzleDataB.Count.ToString());
Console.WriteLine("**********");

// Bubble sort PuzzleDataA
for (int outer = 0; outer < puzzleDataA.Count; outer++)
{
    for (int inner = 0; inner < puzzleDataA.Count - 1; inner++)
    {
        if (puzzleDataA[inner] > puzzleDataA[inner + 1])
        {
            Int32 placeHolder = puzzleDataA[inner + 1];
            puzzleDataA[inner + 1] = puzzleDataA[inner];
            puzzleDataA[inner] = placeHolder;
        }
    }
}

// Bubble sort PuzzleDataB
for (int outer = 0; outer < puzzleDataB.Count; outer++)
{
    for (int inner = 0; inner < puzzleDataB.Count - 1; inner++)
    {
        if (puzzleDataB[inner] > puzzleDataB[inner + 1])
        {
            Int32 placeHolder = puzzleDataB[inner + 1];
            puzzleDataB[inner + 1] = puzzleDataB[inner];
            puzzleDataB[inner] = placeHolder;
        }
    }
}

// Distance calculation
Int32 distance = 0;
for (int i = 0; i < puzzleDataA.Count; i++)
{
    distance += Math.Abs(puzzleDataA[i] - puzzleDataB[i]);
}

Console.WriteLine("Result: " + distance.ToString());
Console.WriteLine("**********");

// *********************************************************************************************************************************************************

//PART 2
// *********************************************************************************************************************************************************

// Score calculation
Int32 score = 0;
foreach (var indexA in puzzleDataA)
{
    Int32 appearances = 0;
    foreach (var indexB in puzzleDataB)
    {
        if (indexA == indexB)
        {
            appearances++;
        }
    }

    score += indexA * appearances;
}

Console.WriteLine("Score: " + score.ToString());
Console.WriteLine("**********");

Console.ReadLine();
