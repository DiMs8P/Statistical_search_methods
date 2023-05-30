using MathLibrary.DataTypes;
using Statistical_search_methods.Core.Methods;
using Statistical_search_methods.Core.Methods.Implementations;
using Statistical_search_methods.DataTypes;
using Statistical_search_methods.Logging;
using Statistical_search_methods.Settings;

namespace Statistical_search_methods.Core.Assembly;

public class PrebuildTests
{
    public Environment SimpleRandomSearchTest(Logger logger)
    {
        EnvironmentBuilder builder = new EnvironmentBuilder();

        builder.SetSearchMethod(new SimpleRandomSearch(Config.searchMethodParams, logger, 0.99));

        return builder.Build();
    }
    
    public Environment FirstAlgorithmTest(Logger logger, int M)
    {
        EnvironmentBuilder builder = new EnvironmentBuilder();

        builder.SetSearchMethod(new FirstSearchAlgorithm(Config.searchMethodParams, logger, 1e-7, M));
            
        return builder.Build();
    }
    
    public Environment SecondAlgorithmTest(Logger logger, int M)
    {
        EnvironmentBuilder builder = new EnvironmentBuilder();

        builder.SetSearchMethod(new SecondSearchAlgorithm(Config.searchMethodParams, logger, 1e-7, M, 0.90));

        return builder.Build();
    }
    
    public Environment ThirdAlgorithmTest(Logger logger, int M)
    {
        EnvironmentBuilder builder = new EnvironmentBuilder();

        builder.SetSearchMethod(new ThirdSearchAlgorithm(Config.searchMethodParams, logger, 1e-7, M, 1e-4));

        return builder.Build();
    }
}