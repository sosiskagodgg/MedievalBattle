using System.Collections.Generic;

/// <summary>
/// Manages all active effects on an entity.
/// </summary>
public class EffectsComponent
{
    private List<EntityEffect> _activeEffects = new List<EntityEffect>();
    private Entity _owner;

    // Store original stat values
    private float _originalWeaponDamageCoefficient;
    private float _originalWeaponDurationCoefficient;
    private float _originalMovementSpeedCoefficient;

    public EffectsComponent(Entity owner, CombatComponent combat, MovementComponent movement)
    {
        _owner = owner;

        // Save original values
        _originalWeaponDamageCoefficient = combat.Weapon.weaponDamageCoefficient;
        _originalWeaponDurationCoefficient = combat.Weapon.weaponDurationCoefficient;
        _originalMovementSpeedCoefficient = movement.speedCoefficient;
    }

    /// <summary>
    /// Applies an effect to the entity.
    /// </summary>
    public void ApplyEffect(EntityEffect effect)
    {
        if (!_activeEffects.Contains(effect))
        {
            _activeEffects.Add(effect);
            RecalculateAllStats();
        }
    }

    /// <summary>
    /// Removes an effect from the entity.
    /// </summary>
    public void RemoveEffect(EntityEffect effect)
    {
        if (_activeEffects.Remove(effect))
        {
            RecalculateAllStats();
        }
    }

    /// <summary>
    /// Checks if the entity has a specific effect.
    /// </summary>
    public bool HasEffect(EntityEffect effect) => _activeEffects.Contains(effect);

    /// <summary>
    /// Recalculates all stats based on active effects.
    /// </summary>
    private void RecalculateAllStats()
    {
        // Get entity components
        var combat = _owner.Combat;
        var movement = _owner.Movement;

        // Reset to original values
        combat.Weapon.weaponDamageCoefficient = _originalWeaponDamageCoefficient;
        combat.Weapon.weaponDurationCoefficient = _originalWeaponDurationCoefficient;
        movement.speedCoefficient = _originalMovementSpeedCoefficient;

        // Apply all active effects
        foreach (var effect in _activeEffects)
        {
            effect.ApplyEffectToStats(
                ref combat.Weapon.weaponDamageCoefficient,
                ref combat.Weapon.weaponDurationCoefficient,
                ref movement.speedCoefficient
            );
        }
    }

    /// <summary>
    /// Gets all active effects.
    /// </summary>
    public IReadOnlyList<EntityEffect> GetActiveEffects() => _activeEffects.AsReadOnly();
}





public class EntityEffect
{
    public float WeaponDamageMultiplier { get; private set; } = 1f;
    public float WeaponDurationMultiplier { get; private set; } = 1f;
    public float EntitySpeedMultiplier { get; private set; } = 1f;

    public EntityEffect(float damageMultiplier = 1f, float durationMultiplier = 1f, float speedMultiplier = 1f)
    {
        WeaponDamageMultiplier = damageMultiplier;
        WeaponDurationMultiplier = durationMultiplier;
        EntitySpeedMultiplier = speedMultiplier;
    }

    public void ApplyEffectToStats(ref float damage, ref float duration, ref float speed)
    {
        damage *= WeaponDamageMultiplier;
        duration *= WeaponDurationMultiplier;
        speed *= EntitySpeedMultiplier;
    }
}