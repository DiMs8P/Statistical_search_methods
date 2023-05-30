namespace Statistical_search_methods.DataTypes;

public class Eps
{
    private double[] _eps;

    public Eps(params double[] eps)
    {
        _eps = eps;
    }

    public double this[int index]
    {
        get
        {
            if (index >= 0 && index < _eps.Length)
            {
                return _eps[index];
            }

            throw new ArgumentOutOfRangeException("Out of array bounds");
        }
    }

    public int Size() => _eps.Length;
}