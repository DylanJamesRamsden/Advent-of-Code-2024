
List<List<char>> PuzzleData = new List<List<char>>();

StreamReader streamReader = new StreamReader("C:\\Users\\Darth Vader\\Documents\\GitHub\\Advent-of-Code-2024\\Day 4\\PuzzleData.txt");
string streamReaderLine = streamReader.ReadLine();
while (streamReaderLine != null)
{
    List<char> charList = new List<char>();
    foreach (char c in streamReaderLine)
    {
        charList.Add(c);
    }

    PuzzleData.Add(charList);

    streamReaderLine = streamReader.ReadLine();
}
streamReader.Close();

Console.WriteLine("Lines: " + PuzzleData.Count.ToString());
Console.WriteLine("Characters Per Line: " + PuzzleData[0].Count.ToString());

Int32 combinationCounter = 0;
for (int row = 0; row < PuzzleData.Count; row++)
{
    for (int coloumn = 0; coloumn < PuzzleData[row].Count; coloumn++)
    {
        combinationCounter += GetNumberOfXmas(row, coloumn);
    }
}

Console.WriteLine("XMAS Counter: " + combinationCounter.ToString());

Int32 xmasCounter = 0;
for (int row = 1; row < PuzzleData.Count - 1; row++)
{
    for (int coloumn = 1; coloumn < PuzzleData[row].Count - 1; coloumn++)
    {
       if (PuzzleData[row][coloumn] == 'A')
       {
            if (IsXmas(row, coloumn))
            {
                xmasCounter += 1;
            }
       }
    }
}

Console.WriteLine("X-MAS Counter: " + xmasCounter.ToString());

Int32 GetNumberOfXmas(Int32 row, Int32 coloumn)
{
    List<string> combinations = new List<string>();
    combinations.Add(GetRightCombination(row, coloumn));
    combinations.Add(GetLeftCombination(row, coloumn));
    combinations.Add(GetDownCombination(row, coloumn));
    combinations.Add(GetUpCombination(row, coloumn));
    combinations.Add(GetDiagonalTopLeftCombination(row, coloumn));
    combinations.Add(GetDiagonalTopRightCombination(row, coloumn));
    combinations.Add(GetDiagonalBottomLeftCombination(row, coloumn));
    combinations.Add(GetDiagonalBottomRightCombination(row, coloumn));

    Int32 xmasCounter = 0;
    foreach (string combination in combinations)
    {
        if (combination == "XMAS")
        {
            xmasCounter++;
        }
    }

    return xmasCounter;
}

string GetRightCombination(Int32 row, Int32 coloumn)
{
    string newCombination = "";

    bool bIsValidIndex = true;
    Int32 charCounter = 0;
    Int32 cachedRow = row;
    Int32 cachedColoumn = coloumn;
    while (bIsValidIndex && charCounter < 4)
    {
        if (IsValidIndex(cachedRow, cachedColoumn))
        {
            newCombination += PuzzleData[cachedRow][cachedColoumn];
            cachedColoumn++;

            charCounter++;
        }
        else bIsValidIndex = false;
    }

    // Console.WriteLine(newCombination);

    return newCombination;
}

string GetLeftCombination(Int32 row, Int32 coloumn)
{
    string newCombination = "";

    bool bIsValidIndex = true;
    Int32 charCounter = 0;
    Int32 cachedRow = row;
    Int32 cachedColoumn = coloumn;
    while (bIsValidIndex && charCounter < 4)
    {
        if (IsValidIndex(cachedRow, cachedColoumn))
        {
            newCombination += PuzzleData[cachedRow][cachedColoumn];
            cachedColoumn--;

            charCounter++;
        }
        else bIsValidIndex = false;
    }

    // Console.WriteLine(newCombination);

    return newCombination;
}

string GetDownCombination(Int32 row, Int32 coloumn)
{
    string newCombination = "";

    bool bIsValidIndex = true;
    Int32 charCounter = 0;
    Int32 cachedRow = row;
    Int32 cachedColoumn = coloumn;
    while (bIsValidIndex && charCounter < 4)
    {
        if (IsValidIndex(cachedRow, cachedColoumn))
        {
            newCombination += PuzzleData[cachedRow][cachedColoumn];
            cachedRow--;

            charCounter++;
        }
        else bIsValidIndex = false;
    }

    // Console.WriteLine(newCombination);

    return newCombination;
}

string GetUpCombination(Int32 row, Int32 coloumn)
{
    string newCombination = "";

    bool bIsValidIndex = true;
    Int32 charCounter = 0;
    Int32 cachedRow = row;
    Int32 cachedColoumn = coloumn;
    while (bIsValidIndex && charCounter < 4)
    {
        if (IsValidIndex(cachedRow, cachedColoumn))
        {
            newCombination += PuzzleData[cachedRow][cachedColoumn];
            cachedRow++;

            charCounter++;
        }
        else bIsValidIndex = false;
    }

    // Console.WriteLine(newCombination);

    return newCombination;
}

string GetDiagonalTopLeftCombination(Int32 row, Int32 coloumn)
{
    string newCombination = "";

    bool bIsValidIndex = true;
    Int32 charCounter = 0;
    Int32 cachedRow = row;
    Int32 cachedColoumn = coloumn;
    while (bIsValidIndex && charCounter < 4)
    {
        if (IsValidIndex(cachedRow, cachedColoumn))
        {
            newCombination += PuzzleData[cachedRow][cachedColoumn];
            cachedColoumn--;
            cachedRow--;

            charCounter++;
        }
        else bIsValidIndex = false;
    }

    // Console.WriteLine(newCombination);

    return newCombination;
}

string GetDiagonalTopRightCombination(Int32 row, Int32 coloumn)
{
    string newCombination = "";

    bool bIsValidIndex = true;
    Int32 charCounter = 0;
    Int32 cachedRow = row;
    Int32 cachedColoumn = coloumn;
    while (bIsValidIndex && charCounter < 4)
    {
        if (IsValidIndex(cachedRow, cachedColoumn))
        {
            newCombination += PuzzleData[cachedRow][cachedColoumn];
            cachedColoumn++;
            cachedRow--;

            charCounter++;
        }
        else bIsValidIndex = false;
    }

    // Console.WriteLine(newCombination);

    return newCombination;
}

string GetDiagonalBottomLeftCombination(Int32 row, Int32 coloumn)
{
    string newCombination = "";

    bool bIsValidIndex = true;
    Int32 charCounter = 0;
    Int32 cachedRow = row;
    Int32 cachedColoumn = coloumn;
    while (bIsValidIndex && charCounter < 4)
    {
        if (IsValidIndex(cachedRow, cachedColoumn))
        {
            newCombination += PuzzleData[cachedRow][cachedColoumn];
            cachedColoumn--;
            cachedRow++;

            charCounter++;
        }
        else bIsValidIndex = false;
    }

    //Console.WriteLine(newCombination);

    return newCombination;
}

string GetDiagonalBottomRightCombination(Int32 row, Int32 coloumn)
{
    string newCombination = "";

    bool bIsValidIndex = true;
    Int32 charCounter = 0;
    Int32 cachedRow = row;
    Int32 cachedColoumn = coloumn;
    while (bIsValidIndex && charCounter < 4)
    {
        if (IsValidIndex(cachedRow, cachedColoumn))
        {
            newCombination += PuzzleData[cachedRow][cachedColoumn];
            cachedColoumn++;
            cachedRow++;

            charCounter++;
        }
        else bIsValidIndex = false;
    }

    // Console.WriteLine(newCombination);

    return newCombination;
}

bool IsValidIndex(Int32 row, Int32 coloumn)
{
    if (row >= 0 && row < PuzzleData.Count)
    {
        if (coloumn >= 0 && coloumn < PuzzleData[row].Count)
        {
            return true;
        }
    }

    return false;
}

// Part 2
bool IsXmas(Int32 row, Int32 coloumn)
{
    string outputString;

    outputString = PuzzleData[row - 1][coloumn - 1].ToString() + PuzzleData[row][coloumn].ToString() + PuzzleData[row + 1][coloumn + 1].ToString();
    if (outputString != "MAS" && outputString != "SAM")
    {
        return false;
    }

    outputString = PuzzleData[row + 1][coloumn - 1].ToString() + PuzzleData[row][coloumn].ToString() + PuzzleData[row - 1][coloumn + 1].ToString();
    if (outputString != "MAS" && outputString != "SAM")
    {
        return false;
    }

    return true;
}