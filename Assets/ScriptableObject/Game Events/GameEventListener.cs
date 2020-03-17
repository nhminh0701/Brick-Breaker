using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.Register(this);
    }

    private void OnDisable()
    {
        Event.UnRegister(this);
    }

    public void OnRaiseEvent()
    {
        Response.Invoke();
    }
}
