using UnityEngine;

[CreateAssetMenu(fileName = "IntReference", menuName = "References/Int")]
public class IntReference : VarialbleReference<int>
{
    public void ApplyChange(int value)
    {
        this.Value += value;
    }
}
