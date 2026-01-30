using System;

using UnityEngine;

/// <summary>
/// Manages health, blood volume, and death state of an entity.
/// </summary>
public class HealthComponent
{
    public event Action<Entity> OnEntityDeath;

    private float _bloodVolume;
    private readonly float _bloodVolumeMax;

    public float BloodVolume
    {
        get => _bloodVolume;
        private set
        {
            _bloodVolume = Mathf.Clamp(value, 0, _bloodVolumeMax);
            if (_bloodVolume <= 0)
            {
                OnEntityDeath?.Invoke(_lastDamager);
            }
        }
    }

    public float BloodLossPerSecond { get; private set; }
    private Entity _lastDamager;

    public HealthComponent(float bloodVolumeMax)
    {
        _bloodVolumeMax = bloodVolumeMax;
        BloodVolume = bloodVolumeMax;
    }

    /// <summary>
    /// Applies damage to the entity and tracks the damager.
    /// </summary>
    public void TakeDamage(float damage, Entity damager, float additionalBloodLoss)
    {
        _lastDamager = damager;
        BloodVolume -= damage;
        BloodLossPerSecond += additionalBloodLoss;
    }

    /// <summary>
    /// Adds blood loss per second to the entity.
    /// </summary>
    public void AddBloodLoss(float bloodLoss)
    {
        BloodLossPerSecond += bloodLoss;
    }
}