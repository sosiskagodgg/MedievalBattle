using UnityEngine;

/// <summary>
/// Handles combat-related functionality including weapons and damage dealing.
/// </summary>
public class CombatComponent
{
    public Weapon Weapon { get; private set; }

    public CombatComponent(Weapon weapon)
    {
        Weapon = weapon;
    }

    /// <summary>
    /// Inflicts damage on a target entity at a specific body section.
    /// </summary>
    public void InflictDamage(Entity attacker, Entity target, BodySection bodySection, float baseDamage)
    {
        // Calculate final damage with weapon coefficients
        float finalDamage = baseDamage * Weapon.Damage * Weapon.weaponDamageCoefficient;

        // Get random body part from target's body component
        BodyPart targetBodyPart = target.Body.GetRandomBodyPart(bodySection);

        // Calculate blood loss from the damaged body part
        float bloodLoss = targetBodyPart.TakeDamage(finalDamage);

        // Apply damage to target's health component
        target.Health.TakeDamage(finalDamage, attacker, bloodLoss);

        Debug.Log($"{attacker.Name} inflicted {finalDamage} damage to {target.Name}'s {targetBodyPart.name}");
    }
}

