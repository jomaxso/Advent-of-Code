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
}