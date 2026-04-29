using System;
using System.Collections.Generic;

public class EventDispatcher
{
    private static EventDispatcher _instance;

    private readonly Dictionary<string, Action> _events = new Dictionary<string, Action>();

    private EventDispatcher() { }

    public static EventDispatcher GetInstance()
    {
        if (_instance == null)
        {
            _instance = new EventDispatcher();
        }
        return _instance;
    }

    public void Subscribe(string eventType, Action handler)
    {
        if (!_events.ContainsKey(eventType))
        {
            _events[eventType] = null;
        }
        _events[eventType] += handler;
    }

    public void Unsubscribe(string eventType, Action handler)
    {
        if (_events.ContainsKey(eventType))
        {
            _events[eventType] -= handler;
        }
    }

    public void Dispatch(string eventType)
    {
        if (_events.ContainsKey(eventType) && _events[eventType] != null)
        {
            _events[eventType].Invoke();
        }
    }
}
