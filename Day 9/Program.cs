// Puzzle Data Reading
//*****************************************************************************************************************************************************
using System.Linq;

List<string> PuzzleData = new List<string>();

StreamReader streamReaderPuzzleData = new StreamReader("C:\\Users\\Darth Vader\\Documents\\GitHub\\Advent-of-Code-2024\\Day 9\\PuzzleData.txt");
string streamReaderPuzzleDataLine = streamReaderPuzzleData.ReadLine();
while (streamReaderPuzzleDataLine != null)
{
    foreach (char c in streamReaderPuzzleDataLine)
    {
        PuzzleData.Add(c.ToString());
    }

    streamReaderPuzzleDataLine = streamReaderPuzzleData.ReadLine();
}
streamReaderPuzzleData.Close();

Console.WriteLine("Original data:");
OutputData(PuzzleData);
Console.WriteLine("***********************");
Console.WriteLine();
//*****************************************************************************************************************************************************

// Formatting String
//*****************************************************************************************************************************************************
List<String> FileBlockFormatData = new List<String>();
Int32 fileIndex = 0;
bool bCanFormat = true;
String lastValue = "";
for (int i = 0; i < PuzzleData.Count; i++)
{
    Int32 value = Int32.Parse(PuzzleData[i]);
    if (value > 0)
    {
        if (bCanFormat)
        {
            for (int j = 0; j < value; j++)
            {
                FileBlockFormatData.Add(fileIndex.ToString());
                lastValue = fileIndex.ToString();
            }

            fileIndex++;
        }
        else
        {
            for (int j = 0; j < value; j++)
            {
                FileBlockFormatData.Add(".");
            }
        }
    }

    bCanFormat = !bCanFormat;
}

Console.WriteLine("Formated with file indexes:");
OutputData(FileBlockFormatData);
Console.WriteLine("***********************");
Console.WriteLine();
//*****************************************************************************************************************************************************

// Formatting String with Sorting
//*****************************************************************************************************************************************************
Int32 numberOfDots = 0;
foreach (string s in FileBlockFormatData)
{
    if (s == ".")
    {
        numberOfDots++;
    }
}

// Part 1
/*Int32 currentIndex = 0;
while(!CheckDotsAtEnd(FileBlockFormatData, numberOfDots))
{
    if (FileBlockFormatData[currentIndex] == ".")
    {
        Int32 IndexToSwap = 0;
        ValidIndexFromRight(FileBlockFormatData, ref IndexToSwap);

        FileBlockFormatData[currentIndex] = FileBlockFormatData[IndexToSwap];
        FileBlockFormatData[IndexToSwap] = ".";
    }

    currentIndex++;
}*/

Calculation();

Console.WriteLine("Formated with sorting:");
OutputData(FileBlockFormatData);
Console.WriteLine("***********************");
Console.WriteLine();
//*****************************************************************************************************************************************************

// Checksum
//*****************************************************************************************************************************************************
Int64 checkSum = 0;
for (int i = 0; i < FileBlockFormatData.Count; i++)
{
    if (FileBlockFormatData[i] != ".")
    {
        checkSum += Int64.Parse(FileBlockFormatData[i]) * i;
    }

    /*checkSum += Int32.Parse(FileBlockFormatData[i]) * i;*/
}

Console.WriteLine("Checksum: " + checkSum.ToString());

void OutputData(List<String> dataToOutput)
{
    string outputLine = "";
    foreach (string s in dataToOutput)
    {
        outputLine += s;
    }
    Console.WriteLine(outputLine);
}

bool CheckDotsAtEnd(List<String> data, Int32 numberOfDots)
{
    for (int i = data.Count - 1; i > (data.Count - 1) - numberOfDots; i--)
    {
        if (data[i] != ".")
        {
            return false;
        }
    }

    return true;
}

bool ValidIndexFromRight(List<String> data, ref Int32 index)
{
    for (int i = data.Count - 1; i >= 0; i--)
    {
        if (data[i] != ".")
        {
            index = i;
            return true;
        }
    }

    return false;
}

void Calculation()
{
    // Start at the end of the file
    List<string> attemptedFileIndexes = new List<string>();
    for (int i = FileBlockFormatData.Count - 1; i >= 0; i--)
    {
        if (attemptedFileIndexes.Contains(FileBlockFormatData[i]))
        {
            continue;
        }

        bool bMovedSuccessfully = false;
        Int32 sizeOfFileMoved = 0;
        // Count group
        if (FileBlockFormatData[i] != ".")
        {
            attemptedFileIndexes.Add(FileBlockFormatData[i]);

            Int32 fileGroupSize = 0;
            for (int y = i; y >= 0; y--)
            {
                if (FileBlockFormatData[y] == FileBlockFormatData[i])
                {
                    fileGroupSize++;
                }
                else break;
            }

            // Counts how big the dot space is
            // @TODO here is where its gonna break, there are still some dots in the end result
            for (int j = 0; j < i; j++)
            {
                if (FileBlockFormatData[j] == ".")
                {
                    // See how long the dot pair is
                    Int32 sequentialDotsSize = 0;
                    for (int b = j; b < FileBlockFormatData.Count; b++)
                    {
                        if (FileBlockFormatData[b] == ".")
                        {
                            sequentialDotsSize++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    // Slot the fileGroup in
                    if (sequentialDotsSize >= fileGroupSize && j < i)
                    {
                        for (int k = j; k <= j + fileGroupSize - 1; k++)
                        {
                            FileBlockFormatData[k] = FileBlockFormatData[i];
                        }

                        for (int h = j + fileGroupSize; h < FileBlockFormatData.Count; h++)
                        {
                            if (FileBlockFormatData[h] == FileBlockFormatData[i])
                            {
                                FileBlockFormatData[h] = ".";
                            }
                        }

                        bMovedSuccessfully = true;
                        sizeOfFileMoved = fileGroupSize;
                        // OutputData(FileBlockFormatData);

                        break;
                    }
                }
            }

            if (bMovedSuccessfully)
            {
                i -= sizeOfFileMoved - 1;
            }
        }
    }
}
