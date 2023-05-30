using MathLibrary.DataTypes;

namespace Statistical_search_methods.Core.Methods;

public class ISearchMethod
{
    public virtual Point FindMinimumPoint(int seed)
    {
        return new Point();
    }
}