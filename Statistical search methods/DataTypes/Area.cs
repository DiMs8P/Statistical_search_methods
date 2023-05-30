namespace Statistical_search_methods.DataTypes;

public class Area
{
    private readonly Interval[] _intervals;

    public Area(params Interval[] intervals)
    {
        _intervals = intervals;
    }
    
    public Interval this[int index]
    {
        get
        {
            if (index >= 0 && index < _intervals.Length)
            {
                return _intervals[index];
            }

            throw new ArgumentOutOfRangeException("Out of array bounds");
        }
    }

    public int Size() => _intervals.Length;
}