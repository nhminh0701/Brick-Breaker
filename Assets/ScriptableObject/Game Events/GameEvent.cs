using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners;

    public void RaiseEvent()
    {
        if (listeners == null) return;

        for (int index = 0; index < listeners.Count; index ++)
        {
            listeners[index].OnRaiseEvent();
        }
    }

    public void Register(GameEventListener listener)
    {
        if (listeners == null) listeners = new List<GameEventListener>();

        listeners.Add(listener);
    }

    public void UnRegister(GameEventListener listner)
    {
        listeners.Remove(listner);
    }
}
