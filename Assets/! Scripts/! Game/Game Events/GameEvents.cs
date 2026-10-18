using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    private static readonly Dictionary<object, Action<object>> eventTable = new();
    private static readonly Dictionary<(object EventType, Action<object> Callback), Action<object>> eventTableOnce = new();


    // Event that Subscribe and can have continuous callbacks when the event is emitted
    public static void On(object gameEventsEnum, Action<object> action)
    {
        if (!eventTable.ContainsKey(gameEventsEnum))
        {
            eventTable[gameEventsEnum] = action;
        }
        else
        {
            eventTable[gameEventsEnum] += action;
        }
    }

    // Event that Subscribe once but then removes itself
    public static void Once(object gameEventsEnum, Action<object> action)
    {
        var key = (gameEventsEnum, action);
        if (eventTableOnce.ContainsKey(key)) return;

        Action<object> wrapper = null;
        wrapper = data =>
        {
            eventTableOnce.Remove(key);
            Off(gameEventsEnum, wrapper);
            action(data);
        };

        eventTableOnce[key] = wrapper;
        On(gameEventsEnum, wrapper);
    }

    public static void Off(object gameEventsEnum, Action<object> action)
    {
        if (eventTable.TryGetValue(gameEventsEnum, out var actions))
        {
            actions -= action;

            if (actions == null)
            {
                eventTable.Remove(gameEventsEnum);
            }
            else
            {
                eventTable[gameEventsEnum] = actions;
            }
        }
    }

    public static void Emit(object gameEventsEnum, params object[] data)
    {
        if (eventTable.TryGetValue(gameEventsEnum, out var actions))
        {
            actions?.Invoke(data);
        }
    }
}