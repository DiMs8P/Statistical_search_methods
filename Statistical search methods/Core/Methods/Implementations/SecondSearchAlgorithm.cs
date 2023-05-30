using MathLibrary.DataTypes;
using NDimentionalSearch.Methods;
using Statistical_search_methods.Logging;

namespace Statistical_search_methods.Core.Methods.Implementations;

public class SecondSearchAlgorithm : ISearchMethod
{
    private readonly SearchMethodParams _methodParams;
    private readonly BaseLogger _logger;
    private readonly double _localMinimumAccuracy;
    private readonly int _attemptAmount;
    private readonly double _initialProbability;

    public SecondSearchAlgorithm(SearchMethodParams methodParams, BaseLogger logger, double localMinimumAccuracy, int attemptAmount, double initialProbability)
    {
        _methodParams = methodParams;
        _logger = logger;
        _localMinimumAccuracy = localMinimumAccuracy;
        _attemptAmount = attemptAmount;
        _initialProbability = initialProbability;
    }
    public override Point FindMinimumPoint(int seed)
    {
        Bfgs localMinimumFinder = new Bfgs(_methodParams.Func);
        ISearchMethod nonDetermicSearchMethod = new SimpleRandomSearch(_methodParams, _logger, _initialProbability);

        Point firstPoint = new Point(new double[_methodParams.Eps.Size()]);
        InitializePoint(firstPoint);
        
        Point minimumFromFirstPoint = localMinimumFinder.Search(firstPoint, _localMinimumAccuracy);

        Point minimumPointFromNonDetermicSearch;
        
        int k = 0;
        for (int i = 0; i < _attemptAmount; i++)
        {
            minimumPointFromNonDetermicSearch = nonDetermicSearchMethod.FindMinimumPoint((seed - i - k));
            
            if (_methodParams.Func(minimumPointFromNonDetermicSearch) < _methodParams.Func(minimumFromFirstPoint) 
                && !IsNearlyEqual(minimumPointFromNonDetermicSearch, minimumFromFirstPoint))
            {
                firstPoint = new Point(minimumPointFromNonDetermicSearch);
                minimumFromFirstPoint = localMinimumFinder.Search(firstPoint, _localMinimumAccuracy);
                i = 0;
                k += i;
            }
        }
   
        // _logger.BufferItem(firstPoint[0].ToString(), 2);
        // _logger.BufferItem(firstPoint[1].ToString(), 3);
        // _logger.BufferItem(minimumFromFirstPoint[0].ToString(), 4);
        // _logger.BufferItem(minimumFromFirstPoint[1].ToString(), 5);
        // _logger.BufferItem((-_methodParams.Func(firstPoint)).ToString(), 6);
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

    private void InitializePoint(Point outputPoint)
    {
        for (int i = 0; i < outputPoint.Size; i++)
        {
            outputPoint[i] = (_methodParams.Area[i].RightBorder + _methodParams.Area[i].LeftBorder) / (double)2;
        }
    }

    private void GetNextPoint(Point nextPoint, int seed)
    {
        for (int i = 0; i < nextPoint.Size; i++)
        {
            nextPoint[i] = RandomInRange(_methodParams.Area[i].LeftBorder,
                _methodParams.Area[i].RightBorder, seed);
        }
    }
    
    private double RandomInRange(double minimum, double maximum, int seed)
    {
        Random random = new Random(seed);
        return random.NextDouble() * (maximum - minimum) + minimum;
    }
}