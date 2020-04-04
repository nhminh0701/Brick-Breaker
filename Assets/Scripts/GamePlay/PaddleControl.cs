using System.Collections;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    [Header("Movement Control")]
    public PaddleMovement paddleMovement;
    public BoolReference controllable;
    public bool canMoveVertical;

    [Header("Ball Control")]
    public Transform ballLockPosition;
    public Ball ball;
    private bool isLockingBall;
    [Tooltip("Attach ball on collide")]
    public bool isStickingMode;

    void Awake()
    {
        paddleMovement.Setup(transform);

        if (ball == null)
            ball = FindObjectOfType<Ball>();
    }

    private void Start()
    {
        LockBall(ball);
    }

    private void LockBall(Ball _ball)
    {
        isLockingBall = true;
        StartCoroutine(LockBallCoroutine(_ball));
    }

    private IEnumerator LockBallCoroutine(Ball _ball)
    {
        _ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        while(isLockingBall)
        {
            _ball.transform.position = ballLockPosition.position;
            if (controllable)
            {
                LaunchBall();
            }
            yield return null;
        }
    }

    private void LaunchBall()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        isLockingBall = false;
        ball.Launch();
    }

    void Update()
    {
        if (controllable.Value)
            paddleMovement.MovePaddle(canMoveVertical);
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        Ball collidedBall = otherCollider.gameObject.GetComponent<Ball>();

        if (collidedBall != null)
        {
            if (isStickingMode)
                LockBall(collidedBall);
        }
    }
}
