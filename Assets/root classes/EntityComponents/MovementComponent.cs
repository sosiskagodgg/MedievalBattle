using UnityEngine;

/// <summary>
/// Manages movement speed and speed modifiers.
/// </summary>
public class MovementComponent
{
    public readonly float MaxSpeed;
    private float _currentSpeed;

    public float speedCoefficient = 1f;
    public float CurrentSpeed
    {
        get => _currentSpeed * speedCoefficient;
        set => _currentSpeed = Mathf.Clamp(value, 0, MaxSpeed);
    }

    public MovementComponent(float maxSpeed)
    {
        MaxSpeed = maxSpeed;
        CurrentSpeed = maxSpeed;
    }
}
