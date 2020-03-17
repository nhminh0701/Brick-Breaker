﻿using UnityEngine;

[CreateAssetMenu(fileName = "DefaultPaddleMovement", menuName = "Paddle Movement/Default")]
public class DefaultPaddleMovement : PaddleMovement
{
    public override void MovePaddle(bool canMoveVertical)
    {
        if (!canMoveVertical) LerpYPosBack(transform);

        if (!Input.GetMouseButton(0)) return;

        MoveHorizontal(transform);

        if (canMoveVertical)
            MoveVertical(transform);
    }

    private void MoveVertical(Transform transform)
    {
        Vector2 touchPositionInWorld = ConvertScreenToWorld(Input.mousePosition);
        float targetYPos = touchPositionInWorld.y;

        float targetYPosThisFrame = Mathf.Lerp(transform.position.y, targetYPos, smoothControl);

        transform.position = new Vector2(transform.position.x, targetYPosThisFrame);
    }

    private void MoveHorizontal(Transform transform)
    {
        Vector2 touchPositionInWorld = ConvertScreenToWorld(Input.mousePosition);
        float targetXPos = Mathf.Clamp(touchPositionInWorld.x, -screenWidth, screenWidth);

        float targetXPosThisFrame = Mathf.Lerp(transform.position.x, targetXPos, smoothControl);

        transform.position = new Vector2(targetXPosThisFrame, transform.position.y);
    }

    private void LerpYPosBack(Transform transform)
    {
        float targetYPosThisFrame = Mathf.SmoothStep(transform.position.y, initialYPos, smoothControl);

        transform.position = new Vector2(transform.position.x, targetYPosThisFrame);
    }
}