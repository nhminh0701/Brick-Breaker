using UnityEngine;

public abstract class PaddleMovement : ScriptableObject
{
    [SerializeField] protected float screenWidth;
    [SerializeField] protected float smoothControl = 0.1f;
    protected Transform transform;
    protected float initialYPos;

    public void Setup(Transform transform)
    {
        this.initialYPos = transform.position.y;
        this.transform = transform;
    }

    public abstract void MovePaddle(bool canMoveVertical);

    protected Vector2 ConvertScreenToWorld(Vector2 positionInScreen)
    {
        return Camera.main.ScreenToWorldPoint(positionInScreen);
    }
}
