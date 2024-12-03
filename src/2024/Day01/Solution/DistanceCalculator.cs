namespace Solution;

public static class DistanceCalculator
{
    public static int DistanceTo(this List<int> leftList, List<int> rightList)
    {
        leftList.Sort();
        rightList.Sort();

        return leftList.Zip(rightList)
            .Sum(x => Math.Abs(x.First - x.Second));
    }
    
    public static int GetSimilarityScore(this List<int> leftList, List<int> rightList)
    {
        var repetitionCount = rightList
            .CountBy(x => x)
            .ToDictionary();
        
        return leftList.Aggregate(0, (acc , current) =>
            acc + repetitionCount.TryGetValue(current, out var count) switch
            {
                true => current * count,
                _ => 0
            });
    }
}