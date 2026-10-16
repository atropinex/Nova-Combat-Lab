using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    private static Dictionary<GameEventsEnum, Action> eventTable = new Dictionary<GameEventsEnum, Action>();
    private static Dictionary<(GameEventsEnum, Action), Action> eventTableOnce = new Dictionary<(GameEventsEnum, Action), Action>();

    // Event that Subscribe and can have continuous callbacks when the event is emitted
    public static void On(GameEventsEnum gameEventsEnum, Action action)
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
    public static void Once(GameEventsEnum gameEventsEnum, Action action)
    {
        var key = (gameEventsEnum, action);
        if (eventTableOnce.ContainsKey(key)) return;

        Action wrapper = null;
        wrapper = () =>
        {
            eventTableOnce.Remove(key);
            Off(gameEventsEnum, wrapper);
            action();
        };
        
        eventTableOnce[key] = wrapper;
        On(gameEventsEnum, wrapper);
    }

    public static void Off(GameEventsEnum gameEventsEnum, Action action)
    {
        if (eventTable.ContainsKey(gameEventsEnum))
        {
            eventTable[gameEventsEnum] -= action;
            if (eventTable[gameEventsEnum] == null)
            {
                eventTable.Remove(gameEventsEnum);
            }
        }
    }

    public static void Emit(GameEventsEnum gameEventsEnum)
    {
        if (eventTable.ContainsKey(gameEventsEnum))
        {
            eventTable[gameEventsEnum]?.Invoke();
        }
    }
}