using UnityEngine;

public class VarialbleReference<T>: ScriptableObject
{
    //public T Value { get; protected set; }
    public T Value;

    public void SetValue(T value)
    {
        this.Value = value;
    }
}
