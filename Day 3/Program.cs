string puzzleDataString = "";
StreamReader streamReader = new StreamReader("C:\\Users\\Darth Vader\\Documents\\GitHub\\Advent-of-Code-2024\\Day 3\\PuzzleData.txt");
string streamReaderLine = streamReader.ReadLine();
while (streamReaderLine != null)
{
    // Console.WriteLine(streamReaderLine);
    // string[] streamReaderLineSplit = streamReaderLine.Split(",");

    puzzleDataString += streamReaderLine;
    streamReaderLine = streamReader.ReadLine();
}
streamReader.Close();

List<string> multiplications = SanitizeString(puzzleDataString);

Int32 total = 0;
foreach (string multiplication in multiplications)
{
    List<Int32> extractedNumbers = GetNumbers(multiplication);

    if (extractedNumbers.Count == 2)
    {
        total += extractedNumbers[0] * extractedNumbers[1];
    }

    Console.WriteLine(multiplication);
}

Console.WriteLine("Total: " + total.ToString());

List<string> SanitizeString(string stringToSanitize)
{
    List<string> multiplications = new List<string>();
    bool bCanMultiply = true;
    for (int i = 0; i < stringToSanitize.Length; i++)
    {
        if (IsDont(i, stringToSanitize))
        {
            bCanMultiply = false;
            i += 6;
        }
        else if (IsDo(i, stringToSanitize))
        {
            bCanMultiply = true;
            i += 3;
        }
        else if (IsValidMultiplicationSubstring(i, stringToSanitize) && bCanMultiply == true)
        {
            string multiplication = "";
            Int32 cachedIndex = 0;
            for (int j = i; j < stringToSanitize.Length; j++)
            {
                multiplication += stringToSanitize[j];
                cachedIndex = i;

                if (stringToSanitize[j] == ')' || IsValidMultiplicationSubstring(j + 1, stringToSanitize))
                {
                    break;
                }
            }

            i = cachedIndex;

            if (AsciiCheck(multiplication))
            {
                multiplications.Add(multiplication);
            }
        }
    }

    return multiplications;
}

bool IsValidMultiplicationSubstring(Int32 i, string stringToValidate)
{
    if (stringToValidate.Length - 1 < i + 4)
        return false;

    string cut = stringToValidate.Substring(i, 4);
    return cut == "mul(";
}

bool AsciiCheck(string stringToValidate)
{
    string cut = stringToValidate.Substring(3);
    foreach (char character in cut)
    {
        if (!((int)character == 40 || (int)character == 41 || (int)character == 44 || ((int)character >= 48 && (int)character <= 57)))
        {
            return false;
        }
    }

    return true;
}

List<Int32> GetNumbers(string stringToValidate)
{
    List<Int32> numbers = new List<Int32>();
    string cut = stringToValidate.Substring(3);

    cut = cut.Remove(0, 1);
    cut = cut.Remove(cut.Length - 1, 1);
    string[] stringToValidateSplit = cut.Split(",");

    foreach (string value in stringToValidateSplit)
    {
        numbers.Add(Int32.Parse(value));
    }

    return numbers;
}

bool IsDo(Int32 i, string stringToValidate)
{
    if (stringToValidate.Length - 1 > i + 4)
    {
        string cut = stringToValidate.Substring(i, 4);
        return cut == "do()";
    }
    return false;
}

bool IsDont(Int32 i, string stringToValidate)
{
    if (stringToValidate.Length - 1 > i + 7)
    {
        string cut = stringToValidate.Substring(i, 7);
        return cut == "don't()";
    }
    return false;
}


