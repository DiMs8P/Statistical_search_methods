using Statistical_search_methods.Core.Methods;
using Statistical_search_methods.Logging;

namespace Statistical_search_methods.Core.Assembly;

public enum SearchMethods
{
    SimpleRandomSearch,
}
public class EnvironmentBuilder
{
    private ISearchMethod _method = new ISearchMethod();
    private SearchMethodParams _methodParams = new SearchMethodParams();
    private BaseLogger _logger = new BaseLogger(Console.WriteLine);
    
    public EnvironmentBuilder SetSearchMethod(ISearchMethod method)
    {
        _method = method;
        return this;
    }
    
    public Environment Build()
    {
        return new Environment(_method, _methodParams, _logger);
    }
}