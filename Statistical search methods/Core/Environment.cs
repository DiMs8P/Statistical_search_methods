using MathLibrary.DataTypes;
using Statistical_search_methods.Core.Methods;
using Statistical_search_methods.Logging;
using Statistical_search_methods.Settings;

namespace Statistical_search_methods.Core.Assembly;

public class Environment
{
    private readonly ISearchMethod _method;
    private readonly SearchMethodParams _methodParams;
    private readonly BaseLogger _logger;
    public Environment(ISearchMethod method, SearchMethodParams methodParams, BaseLogger logger)
    {
        _method = method;
        _methodParams = methodParams;
        _logger = logger;
    }

    public Point FindMinimumPoint(int seed)
    {
        return _method.FindMinimumPoint(seed);
    }
}