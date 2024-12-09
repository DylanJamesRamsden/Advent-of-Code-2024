// var watch = System.Diagnostics.Stopwatch.StartNew();
/* the code that you want to measure comes here */

using System.IO.Pipes;
using System.Net;
using System.Numerics;

List<List<char>> rows = new List<List<char>>();

StreamReader streamReaderRows = new StreamReader("C:\\Users\\Dylan\\Documents\\GitHub\\Advent-of-Code-2024\\Day 8\\PuzzleData.txt");
string streamReaderRowLine = streamReaderRows.ReadLine();
while (streamReaderRowLine != null)
{
    List<char> row = new List<char>();
    foreach (char c in streamReaderRowLine)
    {
        row.Add(c);
    }
    rows.Add(row);

    streamReaderRowLine = streamReaderRows.ReadLine();
}
streamReaderRows.Close();

Dictionary<char, List<Vector2>> symbolLocations = new Dictionary<char, List<Vector2>>();
for (int rowsIndex = 0; rowsIndex < rows.Count; rowsIndex++)
{
    for (int coloumnIndex = 0; coloumnIndex < rows[rowsIndex].Count; coloumnIndex++)
    {
        if (rows[rowsIndex][coloumnIndex] != '.')
        {
            if (symbolLocations.ContainsKey(rows[rowsIndex][coloumnIndex]))
            {
                symbolLocations[rows[rowsIndex][coloumnIndex]].Add(new Vector2(coloumnIndex, rowsIndex));
            }
            else
            {
                List<Vector2> locationsList = new List<Vector2>();
                locationsList.Add(new Vector2(coloumnIndex, rowsIndex));
                symbolLocations.Add(rows[rowsIndex][coloumnIndex], locationsList);
            }
        }
    }
}

Console.WriteLine("Symbols: ");
foreach (char key in symbolLocations.Keys)
{
    Console.WriteLine(key.ToString() + ": " + symbolLocations[key].Count.ToString());
}
Console.WriteLine("---------------------------------------");

Dictionary<Vector2, Int32> antinodeLocations = new Dictionary<Vector2, Int32>();
foreach (char key in symbolLocations.Keys)
{
    // If there isn't a pair, we don't care
    if (symbolLocations[key].Count < 2)
    {
        continue;
    }

    // Look at a location
    foreach (Vector2 currentSymbollocation in symbolLocations[key])
    {
        // Go through all locations of the same symbol and create antinodes
        foreach (Vector2 otherSymbolLocation in symbolLocations[key])
        {
            if (currentSymbollocation != otherSymbolLocation)
            {
                Vector2 newLocation = new Vector2();

                //X
                if (otherSymbolLocation.X < currentSymbollocation.X || otherSymbolLocation.X > currentSymbollocation.X)
                {
                    newLocation.X = currentSymbollocation.X + (currentSymbollocation.X - otherSymbolLocation.X);
                }
                else
                {
                    newLocation.X = currentSymbollocation.X;
                }

                // Y
                if (otherSymbolLocation.Y < currentSymbollocation.Y || otherSymbolLocation.Y > currentSymbollocation.Y)
                {
                    newLocation.Y = currentSymbollocation.Y + (currentSymbollocation.Y - otherSymbolLocation.Y);
                }
                else
                {
                    newLocation.Y = currentSymbollocation.Y;
                }

                // Is location on map
                if (newLocation.Y >= 0 && newLocation.Y < rows.Count)
                {
                    if (newLocation.X >= 0 && newLocation.X < rows[0].Count)
                    {

                        if (antinodeLocations.ContainsKey(newLocation))
                        {
                            antinodeLocations[newLocation] = antinodeLocations[newLocation] + 1;
                        }
                        else
                        {
                            antinodeLocations.Add(newLocation, 1);
                        }
                    }
                }
            }
        }
    }
}

Console.WriteLine("Antinode locations: " + antinodeLocations.Count.ToString());

Int32 totalUniqueAntinodeLocations = 0;
foreach(Vector2 key in antinodeLocations.Keys)
{
    if (antinodeLocations[key] == 1)
    {
        totalUniqueAntinodeLocations++;
    }
}

Console.WriteLine("Unique Antinode locations: " + totalUniqueAntinodeLocations.ToString());

