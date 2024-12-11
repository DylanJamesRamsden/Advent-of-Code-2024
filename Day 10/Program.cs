using System.Data.Common;
using System.Numerics;

List<List<Int32>> map = new List<List<Int32>>();

// EXTRACTING THE MAP
// ***********************************************************************************************************************************************
StreamReader streamReaderPuzzleData = new StreamReader("C:\\Users\\Dylan\\Documents\\GitHub\\Advent-of-Code-2024\\Day 10\\PuzzleData.txt");
string streamReaderPuzzleDataLine = streamReaderPuzzleData.ReadLine();
while (streamReaderPuzzleDataLine != null)
{
    List<Int32> mapLine = new List<Int32>();
    foreach (char c in streamReaderPuzzleDataLine)
    {
        mapLine.Add(Int32.Parse(c.ToString()));
    }
    map.Add(mapLine);

    streamReaderPuzzleDataLine = streamReaderPuzzleData.ReadLine();
}
streamReaderPuzzleData.Close();

OutputMap();
// ***********************************************************************************************************************************************

// FINDING ALL PATHS
// ***********************************************************************************************************************************************
List<Vector2> pathStartingLocations = new List<Vector2>();
for (int row = 0; row < map.Count(); row++)
{
    for (int column = 0; column < map[row].Count(); column++)
    {
        if (map[row][column] == 0)
        {
            pathStartingLocations.Add(new Vector2(column, row));
        }
    }
}

Console.WriteLine("Starting end locations: " + pathStartingLocations.Count());

Int32 totalScore = 0;
foreach (Vector2 sartingLocation in pathStartingLocations)
{
    Int32 scoreToAdd = FollowPath(0, sartingLocation);
    Console.WriteLine(scoreToAdd.ToString());
    totalScore += scoreToAdd;
}

Console.WriteLine("Score: " + totalScore.ToString());
// ***********************************************************************************************************************************************

void OutputMap()
{
    foreach (List<Int32> row in map)
    {
        String mapRow = "";
        foreach (Int32 column in row)
        {
            mapRow += column.ToString();
        }
        Console.WriteLine(mapRow);
    }
}

Int32 CheckAllPaths(Vector2 startingLocation)
{
    return 0;
}

Int32 FollowPath(Int32 height, Vector2 location)
{
    if (height == 9)
    {
        return 1;
    }

    Int32 score = 0;
    Vector2 cachedLocation = location;
    Int32 cachedHeight = height;

    if (IsLeftValid(cachedHeight, cachedLocation))
    {
        cachedLocation.X -= 1;
        cachedHeight += 1;

        /*if (cachedHeight == 9)
        {
            return 1;
        }*/

        score += FollowPath(cachedHeight, cachedLocation);
    }

    if (IsRightValid(cachedHeight, cachedLocation))
    {
        cachedLocation.X += 1;
        cachedHeight += 1;

        /*if (cachedHeight == 9)
        {
            return 1;
        }*/

        score += FollowPath(cachedHeight, cachedLocation);
    }

    if (IsUpValid(cachedHeight, cachedLocation))
    {
        cachedLocation.Y -= 1;
        cachedHeight += 1;

        /*if (cachedHeight == 9)
        {
            return 1;
        }*/

        score += FollowPath(cachedHeight, cachedLocation);
    }

    if (IsDownValid(cachedHeight, cachedLocation))
    {
        cachedLocation.Y += 1;
        cachedHeight += 1;

        /*if (cachedHeight == 9)
        {
            return 1;
        }*/

        score += FollowPath(cachedHeight, cachedLocation);
    }

    return score; 
}

bool IsLeftValid(Int32 height, Vector2 location)
{
    if (location.X > 0)
    {
        if (map[(Int32)location.Y][(Int32)location.X - 1] == height + 1)
        {
            return true;
        }
    }

    return false;
}

bool IsRightValid(Int32 height, Vector2 location)
{
    if (location.X < map[0].Count() - 1)
    {
        if (map[(Int32)location.Y][(Int32)location.X + 1] == height + 1)
        {
            return true;
        }
    }

    return false;
}

bool IsUpValid(Int32 height, Vector2 location)
{
    if (location.Y > 0)
    {
        if (map[(Int32)location.Y - 1][(Int32)location.X] == height + 1)
        {
            return true;
        }
    }

    return false;
}

bool IsDownValid(Int32 height, Vector2 location)
{
    if (location.Y < map.Count - 1)
    {
        if (map[(Int32)location.Y + 1][(Int32)location.X] == height + 1)
        {
            return true;
        }
    }

    return false;
}
