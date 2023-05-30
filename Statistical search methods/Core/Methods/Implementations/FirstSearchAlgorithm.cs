using MathLibrary.DataTypes;
using NDimentionalSearch.Methods;
using Statistical_search_methods.Logging;

namespace Statistical_search_methods.Core.Methods.Implementations;

public class FirstSearchAlgorithm : ISearchMethod
{
    private readonly SearchMethodParams _methodParams;
    private readonly BaseLogger _logger;
    private readonly double _localMinimumAccuracy;
    private readonly int _attemptAmount;

    public FirstSearchAlgorithm(SearchMethodParams methodParams, BaseLogger logger, double localMinimumAccuracy, int attemptAmount)
    {
        _methodParams = methodParams;
        _logger = logger;
        _localMinimumAccuracy = localMinimumAccuracy;
        _attemptAmount = attemptAmount;
    }
    public override Point FindMinimumPoint(int seed)
    {
        Bfgs localMinimumFinder = new Bfgs(_methodParams.Func);

        Point firstPoint = new Point(new double[_methodParams.Eps.Size()]);
        GetNextPoint(firstPoint, seed);
        
        Point minimumFromFirstPoint;
        minimumFromFirstPoint = localMinimumFinder.Search(firstPoint, _localMinimumAccuracy);
        
        Point secondPoint = new Point(new double[_methodParams.Eps.Size()]);
        Point minimumFromSecondPoint;

        int k = 0;
        for (int i = 1; i <= _attemptAmount; i++)
        {
            GetNextPoint(secondPoint, (seed - i - k));
            minimumFromSecondPoint = localMinimumFinder.Search(secondPoint, _localMinimumAccuracy);

            if (_methodParams.Func(minimumFromSecondPoint) < _methodParams.Func(minimumFromFirstPoint) 
                && !IsNearlyEqual(minimumFromSecondPoint, minimumFromFirstPoint))
            {
                firstPoint = new Point(secondPoint);
                minimumFromFirstPoint = new Point(minimumFromSecondPoint);
                k += i;
                i = 1;
            }
        }

        // _logger.BufferItem(firstPoint[0].ToString(), 2);
        // _logger.BufferItem(firstPoint[1].ToString(), 3);
        // _logger.BufferItem(minimumFromFirstPoint[0].ToString(), 4);
        // _logger.BufferItem(minimumFromFirstPoint[1].ToString(), 5);
        // _logger.BufferItem((-_methodParams.Func(minimumFromFirstPoint)).ToString(), 6);
        return firstPoint;
    }

    private bool IsNearlyEqual(Point minimumFromSecondPoint, Point minimumFromFirstPoint)
    {
        for (int i = 0; i < minimumFromSecondPoint.Size; i++)
        {
            if ((Math.Abs(minimumFromSecondPoint[i] - minimumFromFirstPoint[i]) > 1e-2))
            {
                return false;
            }
        }

        return true;
    }

    private void GetNextPoint(Point nextPoint, int seed)
    {
        for (int i = 0; i < nextPoint.Size; i++)
        {
            nextPoint[i] = RandomInRange(_methodParams.Area[i].LeftBorder,
                _methodParams.Area[i].RightBorder, seed * (i + 1));
        }
    }
    
    private double RandomInRange(double minimum, double maximum, int seed)
    {
        Random random = new Random(seed);
        return random.NextDouble() * (maximum - minimum) + minimum;
    }

}