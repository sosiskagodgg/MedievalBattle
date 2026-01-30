using UnityEngine;
[System.Serializable]
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
    public void InflictDamage(Entity damager, Entity target, BodySection bodySection, float damage)
    {

        if (damager.Combat != this) throw new System.Exception("wrong entity damager");

        // Apply damage to target's health component
        target.Health.TakeDamage(damager,target,bodySection,damage);
    }
}

