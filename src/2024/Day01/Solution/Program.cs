using Solution;

List<int> leftList = [];
List<int> rightList = [];
        
await foreach (var line in File.ReadLinesAsync("input.txt"))
{
    var span = line.AsSpan();
    var spaceIndex = span.IndexOf(' ');
            
    leftList.Add(int.Parse(span[..spaceIndex]));
    rightList.Add(int.Parse(span[(spaceIndex + 1)..]));
}

var totalDistance = leftList.DistanceTo(rightList);

Console.WriteLine($"Total distance: {totalDistance}");