// var watch = System.Diagnostics.Stopwatch.StartNew();
/* the code that you want to measure comes here */

using System.IO.Pipes;
using System.Linq.Expressions;
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

Int32 antinoteLocationsCount = 0;
Int32 pairCounter = 0;
List<Vector2> antinotes = new List<Vector2>();
Dictionary<char, List<Vector2>> antennaLocations = new Dictionary<char, List<Vector2>>();
foreach (char key in symbolLocations.Keys)
{
    // If there isn't a pair, we don't care
    if (symbolLocations[key].Count < 2)
    {
        continue;
    }

    antennaLocations.Add(key, new List<Vector2>());

    // Look at a location
    foreach (Vector2 currentSymbollocation in symbolLocations[key])
    {
        Int32 pairsThatMadeAntinotes = 0;
        // Go through all locations of the same symbol and create antinodes
        foreach (Vector2 otherSymbolLocation in symbolLocations[key])
        {
            bool bPairMadeAntinode = false;
            if (currentSymbollocation != otherSymbolLocation)
            {
                Vector2 offsetVector;
                if (otherSymbolLocation.X < currentSymbollocation.X || otherSymbolLocation.X > currentSymbollocation.X)
                {
                    offsetVector.X = currentSymbollocation.X - otherSymbolLocation.X;
                }
                else
                {
                    offsetVector.X = 0;
                }

                // Y
                if (otherSymbolLocation.Y < currentSymbollocation.Y || otherSymbolLocation.Y > currentSymbollocation.Y)
                {
                    offsetVector.Y = currentSymbollocation.Y - otherSymbolLocation.Y;
                }
                else
                {
                    offsetVector.Y = 0;
                }

                // Console.WriteLine("Offset Vector: " + offsetVector.ToString());

                Int32 pairs = 0;
                bool bOnMap = true;
                // start on the second location of the pair
                Vector2 currentLocation = currentSymbollocation + offsetVector;
                //Console.WriteLine("StartingLocation: " + currentLocation.ToString());
                while (bOnMap)
                {
                    // currentLocation += offsetVector;
                    if (IsLocationOnMap(currentLocation))
                    {
                        pairs++;
                        antinoteLocationsCount++;
                        antinotes.Add(currentLocation);

                        antinoteLocationsCount += IsInLineWithAntenna(currentLocation, key);

                        antennaLocations[key].Add(currentLocation);

                        currentLocation += offsetVector;

                        bPairMadeAntinode = true;
                        //Console.WriteLine("Updated Location: " + currentLocation.ToString());
                    }
                    else
                    {
                        bOnMap = false;
                        //Console.WriteLine("Off map!");
                    }
                }

                pairCounter += pairs;
                //Console.WriteLine();
            }
        }
    }
}

Console.WriteLine("Antinode locations: " + antinoteLocationsCount.ToString());
Console.WriteLine("Pairs: " + pairCounter.ToString());

/*Int32 totalUniqueAntinodeLocations = 0;
foreach(Vector2 key in antinodeLocations.Keys)
{
    if (antinodeLocations[key] == 1)
    {
        totalUniqueAntinodeLocations++;
    }
}

Console.WriteLine("Unique Antinode locations: " + totalUniqueAntinodeLocations.ToString());*/

for (int row = 0; row < rows.Count; row++)
{
    string rowString = "";
    for (int column = 0; column < rows[row].Count; column++)
    {
        if (rows[row][column] == '.')
        {
            bool bFound = false;
            Vector2 locationFound = new Vector2();
            foreach (Vector2 antinote in antinotes)
            {
                if (antinote.Y == row && antinote.X == column)
                {
                    bFound = true;
                    locationFound = antinote;
                    break;
                }
            }

            if (bFound)
            {
                rowString += "#";
            }
            else rowString += ".";
        }
        else
        {
            rowString += rows[row][column];
        }
    }

    Console.WriteLine(rowString);
}

bool IsLocationOnMap(Vector2 Location)
{
    if (Location.Y >= 0 && Location.Y < rows.Count)
    {
        if (Location.X >= 0 && Location.X < rows[0].Count)
        {
            return true;
        }
    }

    return false;
}

Int32 IsInLineWithAntenna(Vector2 location, char symbol)
{
    Int32 sameLineCounter = 0;

    Int32 row = (int)location.Y;
    Int32 column = (int)location.X;

    // Int32 column = (int)location.X;
    for (int i = 0; i < rows[row].Count; i++)
    {
        if (i != column && rows[row][i] == symbol)
        {
            if (!DoesAntennaExistForSymbol(new Vector2(i, row), symbol))
            {
                antennaLocations[symbol].Add(new Vector2(i, row));
                sameLineCounter++;
                Console.WriteLine("Got em!");
            }
        }
    }

    for (int i = 0; i < rows.Count; i++)
    {
        if (i != row && rows[i][column] == symbol)
        {
            if (!DoesAntennaExistForSymbol(new Vector2(column, i), symbol))
            {
                antennaLocations[symbol].Add(new Vector2(column, i));
                sameLineCounter++;
                Console.WriteLine("Got em!");
            }
        }
    }

    return sameLineCounter;
}

bool DoesAntennaExistForSymbol(Vector2 location, char symbol)
{
    Console.WriteLine(location.ToString());
    foreach (var antennaLocation in antennaLocations[symbol])
    {
        if (antennaLocation == location)
        {
            Console.WriteLine("Found duplicate!");
            return true;
        }
    }
    return false;
}

