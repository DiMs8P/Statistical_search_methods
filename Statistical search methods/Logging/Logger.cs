namespace Statistical_search_methods.Logging;

public class Logger : BaseLogger
{
    public Logger(Action<String> logAction) : base(logAction)
    {
    }
    protected override bool ShouldLog()
    {
        return true;
    }
}