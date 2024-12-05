using System.Runtime.CompilerServices;

namespace Solution;

public static class LinearSearch
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count(Grid<char> grid, ReadOnlySpan<char> word)
    {
        var count = 0;

        for (var i = 0; i < grid.Source.Length; i++)
        {
            if (grid.Source[i] == word[0])
            {
                count += CheckAllDirections(grid, word, i);
            }
        }

        return count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CheckAllDirections(Grid<char> grid, ReadOnlySpan<char> word, int index)
    {
        Span<int> directions =
        [
            0, 1, // Horizontal right
            1, 0, // Vertical down
            1, 1, // Diagonal down-right
            1, -1, // Diagonal down-left
            0, -1, // Horizontal left
            -1, 0, // Vertical up
            -1, -1, // Diagonal up-left
            -1, 1 // Diagonal up-right
        ];
        
        var count = 0;
        for (var i = 0; i < directions.Length; i += 2)
        {
            if (CheckDirection(grid, word, index, directions[i], directions[i + 1]))
            {
                count++;
            }
        }
        
        return count;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CheckDirection(Grid<char> grid, ReadOnlySpan<char> word, int index, int rowDir, int colDir)
    {
        var wordLength = word.Length;

        var source = grid.Source;
        var sourceLength = grid.Source.Length;
        var width = grid.Width;

        var rowGrowth = width * rowDir;
        var currentColumn = index % width;
        
        for (var i = 0; i < wordLength; i++)
        {
            var columnGrowth = colDir * i;
            var nearbyColumn = currentColumn + columnGrowth;
            
            // Check if the new column is out of bounds
            if(nearbyColumn >= width || nearbyColumn < 0)
            {
                return false;
            }
            
            var newIndex = index + rowGrowth * i + columnGrowth;
            
            // Check if the new index is out of bounds
            if (newIndex < 0 || newIndex >= sourceLength)
            {
                return false;
            }
            
            // Check if the character at the new index is not equal to the character in the word at the current index
            if (source[newIndex] != word[i])
            {
                return false;
            }
        }

        return true;
    }
}