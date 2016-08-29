using UnityEngine;
using System.Collections;

public class ControllerState2D
{

    public bool isCollidingLeft { get; set; }
    public bool isCollidingRight { get; set; }
    public bool isCollidingAbove { get; set; }
    public bool isCollidingBelow { get; set; }
    public bool isMovingDownSlope { get; set; }
    public bool isMovingUpSlope { get; set; }
    public bool isGrounded { get { return isCollidingBelow; } }
    public float slopeAngle { get; set; }

    public bool hasCollisions { get { return isCollidingBelow || isCollidingAbove || isCollidingLeft || isCollidingRight; } }

    public void Reset()
    {
        isMovingUpSlope = 
            isMovingDownSlope = 
            isCollidingLeft = 
            isCollidingRight = 
            isCollidingAbove = 
            isCollidingBelow = false;

        slopeAngle = 0;


    }

    public override string ToString()
    {
        return string.Format(
            "(controller: r: {0} l: {1} a: {2} d: {3} downSlope: {4} upSlope: {5} angle: {6})",
            isCollidingRight,
            isCollidingLeft,
            isCollidingAbove,
            isCollidingBelow,
            isMovingDownSlope,
            isMovingUpSlope,
            slopeAngle);
    }

}
