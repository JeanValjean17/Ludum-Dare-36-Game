using UnityEngine;
using System.Collections;

[System.Serializable]
public class ControllerParameters2D
{
    public enum JumpBehavior
    {
        CanJumpGround,
        CanJumpAnywhere,
        CantJump
    }

    public Vector2 MaxVelocity = new Vector2(float.MaxValue, float.MaxValue);

    [Range(0, 90)]
    public float slopeLimit = 30;

    public float gravity = -24f;

    public JumpBehavior JumpRestrictions;

    public float jumpFrequency = .05f;

    public float jumpMagnitude = 12f;
}
