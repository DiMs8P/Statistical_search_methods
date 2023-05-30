using MathLibrary.DataTypes;
using Statistical_search_methods.Logging;

namespace Statistical_search_methods.Core.Methods.Implementations;

public class SimpleRandomSearch : ISearchMethod
{
    private readonly SearchMethodParams _methodParams;
    private readonly BaseLogger _logger;
    private readonly double _initialProbability;
    
    public SimpleRandomSearch(SearchMethodParams methodParams, BaseLogger logger, double initialProbability)
    {
        _methodParams = methodParams;
        _logger = logger;
        _initialProbability = initialProbability;
    }

    public override Point FindMinimumPoint(int seed)
    {
        int stepsNum = GetStepsNum(_initialProbability);

        Random random = new Random(seed);
        Point currentPoint = new Point(new double[_methodParams.Eps.Size()]);
        GetNextPoint(currentPoint, random);
        Point nextPoint = new Point(new double[_methodParams.Eps.Size()]);
        for (int i = 1; i < stepsNum; i++)
        {
            //GetNextPoint(nextPoint, random);
            nextPoint[0] = RandomInRange(_methodParams.Area[0].LeftBorder,
                _methodParams.Area[0].RightBorder, random.Next());          
            nextPoint[1] = RandomInRange(_methodParams.Area[1].LeftBorder,
                _methodParams.Area[1].RightBorder, random.Next());
            if (_methodParams.Func(nextPoint) < _methodParams.Func(currentPoint))
            {
                currentPoint = new Point(nextPoint);
            }
        }

        return currentPoint;
    }

    private void GetNextPoint(Point nextPoint, Random random)
    {
        for (int i = 0; i < nextPoint.Size; i++)
        {
            nextPoint[i] = RandomInRange(_methodParams.Area[i].LeftBorder,
                _methodParams.Area[i].RightBorder, random.Next());
        }
    }
    
    private double RandomInRange(double minimum, double maximum, int seed)
    {
        Random random = new Random(seed);
        return random.NextDouble() * (maximum - minimum) + minimum;
    }
    
    private int GetStepsNum(double initialProbability)
    {
        double epsProbability = EpsVolume() / Volume();
        return (int)Math.Ceiling(Math.Log(1 - initialProbability) / Math.Log(1 - epsProbability));
    }

    private double Volume()
    {
        double volume = 1;
        for (int i = 0; i < _methodParams.Area.Size(); i++)
        {
            volume *= _methodParams.Area[i].RightBorder - _methodParams.Area[i].LeftBorder;
        }

        return volume;
    }
    
    private double EpsVolume()
    {
        double volume = 1;
        for (int i = 0; i < _methodParams.Area.Size(); i++)
        {
            volume *= _methodParams.Eps[i];
        }

        return volume;
    }
}