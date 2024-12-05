using System.Runtime.CompilerServices;

namespace Solution;

public readonly ref struct Grid<T>(ReadOnlySpan<T> source, int width)
{
    public ReadOnlySpan<T> Source { get; } = source;
    public int Width { get; } = width;
}

public static class Grid
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Grid<T> Create<T>(ReadOnlySpan<T> source, int width) => 
        new(source, width);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Grid<T> AsGrid<T>(this Span<T> source, int width) => new(source, width);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Grid<T> AsGrid<T>(this ReadOnlySpan<T> source, int width) => new(source, width);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyGridTo(this string[] source, Span<char> destination, int rowLength) =>
        source.AsSpan().CopyGridTo(destination, rowLength);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyGridTo(this Span<string> source, Span<char> destination, int rowLength) =>
        ((ReadOnlySpan<string>)source).CopyGridTo(destination, rowLength);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyGridTo(this ReadOnlySpan<string> source, Span<char> destination, int rowLength)
    {
        var columnLength = source[0].Length;

        for (var r = 0; r < rowLength; r++)
        {
            var line = source[r].AsSpan();

            if (line.Length != columnLength)
            {
                throw new ArgumentException("All rows must have the same length", nameof(source));
            }

            var offset = r * columnLength;
            line.CopyTo(destination.Slice(offset, columnLength));
        }
    }
}