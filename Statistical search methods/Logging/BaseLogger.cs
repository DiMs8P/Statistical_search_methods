namespace Statistical_search_methods.Logging;

public class BaseLogger
{
    private Dictionary<int, List<String>> _bufferedLogs;
    protected Action<String> _logAction;
    public BaseLogger(Action<String> logAction)
    {
        _logAction = logAction;
        _bufferedLogs = new Dictionary<int, List<String>>();
    }
    
    public virtual void Log(String logItem)
    {
        if (ShouldLog())
        {
            _logAction(logItem);
        }
    }
    
    public virtual void BufferItem(String logItem, int priority) 
    {
        if (ShouldLog())
        {
            if (!_bufferedLogs.ContainsKey(priority))
            {
                _bufferedLogs.Add(priority, new List<string>());
            }
            
            _bufferedLogs[priority].Add(logItem);
        }
    }

    public virtual void LogBuffer()
    {
        if (ShouldLog())
        {
            foreach (var bufferedLog in _bufferedLogs)
            {
                foreach (var logString in bufferedLog.Value)
                {
                    _logAction(logString);
                }
            }
            
            _bufferedLogs.Clear();
        }
    }
    
    protected virtual bool ShouldLog()
    {
        return false;
    }
}