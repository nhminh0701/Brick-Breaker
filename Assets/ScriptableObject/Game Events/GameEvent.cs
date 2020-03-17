using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners;

    private void OnEnable()
    {
        listeners = new List<GameEventListener>();
    }

    public void RaiseEvent()
    {
        for (int index = 0; index < listeners.Count; index ++)
        {
            listeners[index].OnRaiseEvent();
        }
    }

    public void Register(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnRegister(GameEventListener listner)
    {
        listeners.Remove(listner);
    }
}
