using System.Runtime.CompilerServices;

namespace Solution;

public static class XSearch
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count(Grid<char> grid, ReadOnlySpan<char> word)
    {
        if (word.Length % 2 == 0)
        {
            throw new ArgumentException("Word length must be odd", nameof(word));
        }

        return CountOccurrences(grid, word);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CountOccurrences(Grid<char> grid, ReadOnlySpan<char> word)
    {
        var count = 0;
     
        
        for (var i = 0; i < grid.Source.Length; i++)
        {
            if (CheckInsideOut(grid, word, i))
            {
                count++;
            }
        }

        return count;
    }

    // This logic cheks the text in a cross pattern from the middle character of the word
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CheckInsideOut(Grid<char> grid, ReadOnlySpan<char> word, int gridIndex)
    {
        var middleCharacterIndex = word.Length / 2;
        
        if (grid.Source[gridIndex] != word[middleCharacterIndex])
        {
            return false;
        }

        var currentRow = gridIndex / grid.Width;
        var currentColumn = gridIndex % grid.Width;

        var rowCount = grid.Source.Length / grid.Width;
        
        if (currentRow == 0 || currentRow + 1 == rowCount || currentColumn == 0 || currentColumn + 1 == grid.Width)
        {
            return false;
        }
        
        var counter = 1;

        while (middleCharacterIndex - counter >= 0 && middleCharacterIndex + counter < word.Length)
        {
            var beforeTarget = word[middleCharacterIndex - counter];
            var afterTarget = word[middleCharacterIndex + counter];

            var topLeft = grid.Source[gridIndex - grid.Width - counter];
            var bottomRight = grid.Source[gridIndex + grid.Width + counter];

            var topRight = grid.Source[gridIndex - grid.Width + counter];
            var bottomLeft = grid.Source[gridIndex + grid.Width - counter];

            var isLeftToRightDiagonal = (topLeft == beforeTarget && bottomRight == afterTarget) ||
                                        (topLeft == afterTarget && bottomRight == beforeTarget);

            var isRightToLeftDiagonal = (topRight == afterTarget && bottomLeft == beforeTarget) ||
                                        (topRight == beforeTarget && bottomLeft == afterTarget);

            if (isLeftToRightDiagonal && isRightToLeftDiagonal)
            {
                counter++;
                continue;
            }
            
            return false;
        }

        return true;
    }
}