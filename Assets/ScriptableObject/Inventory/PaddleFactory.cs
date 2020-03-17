using UnityEngine;

[CreateAssetMenu(fileName = "DefaultFactory", menuName = "Factory/Paddle")]
public class PaddleFactory : GamePlayObjFactory
{
    [SerializeField] private PaddleMovement paddleMovement;

    public override GameObject CreateObject(Transform transform)
    {
        GameObject newPaddle = base.CreateObject(transform);
        newPaddle.GetComponent<PaddleControl>().paddleMovement = paddleMovement;

        return newPaddle;
    }

    public override GameObject CreateObject(Vector3 position)
    {
        GameObject newPaddle = base.CreateObject(position);
        newPaddle.GetComponent<PaddleControl>().paddleMovement = paddleMovement;

        return newPaddle;
    }
}
