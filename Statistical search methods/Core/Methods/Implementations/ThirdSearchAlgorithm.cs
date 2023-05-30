using MathLibrary.DataTypes;
using NDimentionalSearch.Methods;
using Statistical_search_methods.Logging;

namespace Statistical_search_methods.Core.Methods.Implementations;

public class ThirdSearchAlgorithm : ISearchMethod
{
    private readonly SearchMethodParams _methodParams;
    private readonly BaseLogger _logger;
    private readonly double _localMinimumAccuracy;
    private readonly int _attemptAmount;
    private readonly double _initialProbability;

    public ThirdSearchAlgorithm(SearchMethodParams methodParams, BaseLogger logger, double localMinimumAccuracy, int attemptAmount, double initialProbability)
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

        Vector randomDirection = new Vector(new double[_methodParams.Eps.Size()]);
        Point secondPoint = new Point(new double[_methodParams.Eps.Size()]);
        Point minimumFromSecondPoint;
        
        int k = 0;
        for (int i = 0; i < _attemptAmount; i++)
        {
            GetRandomDirection(randomDirection, (seed - i - k));
            randomDirection = randomDirection.Normalize();
            
            int j = 1;
            secondPoint = minimumFromFirstPoint + j * randomDirection;
            while (InBounds(secondPoint))
            {
                if ((_methodParams.Func(secondPoint) <
                     _methodParams.Func(minimumFromFirstPoint + (j - 1) * randomDirection)))
                {
                    minimumFromSecondPoint = localMinimumFinder.Search(secondPoint, _localMinimumAccuracy);
                    
                    if (_methodParams.Func(minimumFromSecondPoint) < _methodParams.Func(minimumFromFirstPoint))
                    {
                        firstPoint = new Point(secondPoint);
                        minimumFromFirstPoint = new Point(minimumFromSecondPoint);
                        i = 0;
                        k += i;
                        break;
                    }
                    else
                    {
                        k += i;
                        break;
                    }
                }
                j++;
                secondPoint = minimumFromFirstPoint + j * randomDirection;
            }
        }
   
        _logger.BufferItem(firstPoint[0].ToString(), 2);
        _logger.BufferItem(firstPoint[1].ToString(), 3);
        _logger.BufferItem(minimumFromFirstPoint[0].ToString(), 4);
        _logger.BufferItem(minimumFromFirstPoint[1].ToString(), 5);
        _logger.BufferItem((-_methodParams.Func(minimumFromFirstPoint)).ToString(), 6);
        return firstPoint;
    }


    private bool InBounds(Point checkPoint)
    {
        for (int i = 0; i < checkPoint.Size; i++)
        {
            if (_methodParams.Area[i].LeftBorder > checkPoint[i] || checkPoint[i] > _methodParams.Area[i].RightBorder)
            {
                return false;
            }
        }

        return true;
    }

    private void GetRandomDirection(Vector randomDirection, int seed)
    {
        for (int i = 0; i < randomDirection.Size; i++)
        {
            randomDirection[i] = RandomInRange(-1, 1, seed *seed* (i + 1));
        }
    }

    private void InitializePoint(Point outputPoint)
    {
        for (int i = 0; i < outputPoint.Size; i++)
        {
            outputPoint[i] = (_methodParams.Area[i].RightBorder + _methodParams.Area[i].LeftBorder) / (double)2;
        }
    }
    
    private double RandomInRange(double minimum, double maximum, int seed)
    {
        Random random = new Random(seed);
        return random.NextDouble() * (maximum - minimum) + minimum;
    }
}