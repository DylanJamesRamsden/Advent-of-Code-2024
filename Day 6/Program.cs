// MAP DATA
// **********************************************************************************************************************************
using System.Numerics;

List<List<char>> map = new List<List<char>>();

StreamReader streamReaderMap = new StreamReader("C:\\Users\\Dylan\\Documents\\GitHub\\Advent-of-Code-2024\\Day 6\\PuzzleData.txt");
string streamReaderMapLine = streamReaderMap.ReadLine();
while (streamReaderMapLine != null)
{
    List<char> mapRow = new List<char>();
    foreach (char position in streamReaderMapLine)
    {
        mapRow.Add(position);
    }

    map.Add(mapRow);

    streamReaderMapLine = streamReaderMap.ReadLine();
}
streamReaderMap.Close();

// OutputMap(map);
// **********************************************************************************************************************************

// GUARD STARTING POSITION
// **********************************************************************************************************************************
// Direction:
// Up = 0
// Right = 1
// Down = 2
// Left = 3
Int32 guardDirection = 0;
Vector2 guardPosition = Vector2.Zero;

for (int row = 0; row < map.Count; row++)
{
    for (int colomn = 0; colomn < map[row].Count; colomn++)
    {
        if (map[row][colomn] == '^')
        {
            guardPosition.X = colomn;
            guardPosition.Y = row;

            guardDirection = 0;

            break;
        }
    }
}

Console.WriteLine("Guard position: " + guardPosition.ToString());
// **********************************************************************************************************************************

// GUARD MOVEMENT
// **********************************************************************************************************************************
/*bool bSuccess = false;
Int32 Moves = 0;
Moves = GuardMovement(20000, map, guardPosition, ref bSuccess);
Console.WriteLine("Moves made: " + Moves.ToString());*/

Int32 infiniteLoops = BruteForceInfiniteLoop(map, guardPosition);
Console.WriteLine("Infinite Loops: " + infiniteLoops.ToString());

void OutputMap(List<List<char>> mapData)
{
    for (int row = 0; row < mapData.Count; row++)
    {
        string mapRowString = "";
        for (int colomn = 0; colomn < mapData[row].Count; colomn++)
        {
            mapRowString += mapData[row][colomn];
        }

        Console.WriteLine(mapRowString + " Row: " + row.ToString());
    }
}

Int32 GuardMovement(Int32 MaxAttempts, List<List<char>> mapData, Vector2 guardStartingPostion, ref bool bSuccess)
{
    List<List<char>> mapDataCopy = new List<List<char>>();
    for (int mapDataRow = 0; mapDataRow < mapData.Count; mapDataRow++)
    {
        List<char> rowCopy = new List<char>();
        for (int mapDataColomn = 0; mapDataColomn < mapData[mapDataRow].Count; mapDataColomn++)
        {
            rowCopy.Add(mapData[mapDataRow][mapDataColomn]);
        }
        mapDataCopy.Add(rowCopy);
    }

    Int32 numberOfLoops = 0;
    Int32 NumberOfMoves = 0;
    Int32 guardCurrentDirection = 0;
    Vector2 guardCurrentPosition = guardStartingPostion;
    while(IsGuardOnMap(mapDataCopy, guardCurrentPosition) && numberOfLoops <= MaxAttempts)
    {
        //Console.WriteLine(guardDirection.ToString());
        switch (guardCurrentDirection)
        {           
            // UP
            case 0:
                if (guardCurrentPosition.Y - 1 >= 0)
                {
                    if (mapDataCopy[(int)guardCurrentPosition.Y - 1][(int)guardCurrentPosition.X] == '#')
                    {
                        guardCurrentDirection = 1;
                    }
                    else
                    {
                        mapDataCopy[(int)guardCurrentPosition.Y][(int)guardCurrentPosition.X] = 'X';
                        guardCurrentPosition.Y = guardCurrentPosition.Y - 1;

                        if (mapDataCopy[(int)guardCurrentPosition.Y][(int)guardCurrentPosition.X] != 'X')
                        {
                            NumberOfMoves++;
                        }
                    }
                }
                else
                {
                    guardCurrentPosition.Y = guardCurrentPosition.Y - 1;
                    NumberOfMoves++;
                }
                break;
                // Right
            case 1:
                if (guardCurrentPosition.X + 1 < mapDataCopy[0].Count)
                {
                    if (mapDataCopy[(int)guardCurrentPosition.Y][(int)guardCurrentPosition.X + 1] == '#')
                    {
                        guardCurrentDirection = 2;
                    }
                    else
                    {
                        mapDataCopy[(int)guardCurrentPosition.Y][(int)guardCurrentPosition.X] = 'X';
                        guardCurrentPosition.X = guardCurrentPosition.X + 1;

                        if (mapDataCopy[(int)guardCurrentPosition.Y][(int)guardCurrentPosition.X] != 'X')
                        {
                            NumberOfMoves++;
                        }
                    }
                }
                else
                {
                    guardCurrentPosition.X = guardCurrentPosition.X + 1;
                    NumberOfMoves++;
                }
                break;
                //Down
            case 2:
                if (guardCurrentPosition.Y + 1 < mapDataCopy.Count)
                {
                    if (mapDataCopy[(int)guardCurrentPosition.Y + 1][(int)guardCurrentPosition.X] == '#')
                    {
                        guardCurrentDirection = 3;
                    }
                    else
                    {
                        mapDataCopy[(int)guardCurrentPosition.Y][(int)guardCurrentPosition.X] = 'X';
                        guardCurrentPosition.Y = guardCurrentPosition.Y + 1;

                        if (mapDataCopy[(int)guardCurrentPosition.Y][(int)guardCurrentPosition.X] != 'X')
                        {
                            NumberOfMoves++;
                        }
                    }
                }
                else
                {
                    guardCurrentPosition.Y = guardCurrentPosition.Y + 1;
                    NumberOfMoves++;
                }
                break;
                // Left
            case 3:
                // Next move is still on map
                if (guardCurrentPosition.X - 1 >= 0)
                {
                    if (mapDataCopy[(int)guardCurrentPosition.Y][(int)guardCurrentPosition.X - 1] == '#')
                    {
                        guardCurrentDirection = 0;
                    }
                    else
                    {
                        mapDataCopy[(int)guardCurrentPosition.Y][(int)guardCurrentPosition.X] = 'X';
                        guardCurrentPosition.X = guardCurrentPosition.X - 1;

                        if (mapDataCopy[(int)guardCurrentPosition.Y][(int)guardCurrentPosition.X] != 'X')
                        {
                            NumberOfMoves++;
                        }
                    }
                    break;
                }
                else
                {
                    guardCurrentPosition.X = guardCurrentPosition.X - 1;
                    NumberOfMoves++;
                }
                break;
            default:
                break;
        }

        numberOfLoops++;
    }

    bSuccess = numberOfLoops > MaxAttempts ? false : true;

    // Console.WriteLine(NumberOfMoves.ToString());

    return NumberOfMoves++;
}

bool IsGuardOnMap(List<List<char>> mapData, Vector2 guardCurrentPostion)
{
    if (guardCurrentPostion.X >= 0 && guardCurrentPostion.X < mapData[0].Count &&
        guardCurrentPostion.Y >= 0 && guardCurrentPostion.Y < mapData.Count)
    {
        return true;
    }
    else return false;
}

Int32 BruteForceInfiniteLoop(List<List<char>> mapData, Vector2 guardStartingPosition)
{
    Int32 infiniteLoops = 0;
    for (int row = 0; row < mapData.Count; row++)
    {
        for (int colomn = 0; colomn < mapData[row].Count; colomn++)
        {
            List<List<char>> mapDataCopy = new List<List<char>>();
            for (int mapDataRow = 0; mapDataRow < mapData.Count; mapDataRow++)
            {
                List<char> rowCopy = new List<char>();
                for (int mapDataColomn = 0; mapDataColomn < mapData[mapDataRow].Count; mapDataColomn++)
                {
                    rowCopy.Add(mapData[mapDataRow][mapDataColomn]);
                }
                mapDataCopy.Add(rowCopy);
            }

            if (mapDataCopy[row][colomn] == '.')
            {
                mapDataCopy[row][colomn] = '#';
                bool bSuccess = false;
                GuardMovement(6750, mapDataCopy, guardStartingPosition, ref bSuccess);
                if (!bSuccess)
                {
                    infiniteLoops++;
                }
            }
        }
    }

    return infiniteLoops;
}

