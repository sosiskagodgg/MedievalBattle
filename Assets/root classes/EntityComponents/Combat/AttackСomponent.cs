using UnityEngine;

public class AttackСomponent
{
    /// <summary>
    /// Inflicts damage on a target entity at a specific body section.
    /// </summary>
    public void InflictDamage(Entity damager, Entity target, BodySection bodySection)
    {

        if (damager.Combat.Attack != this) throw new System.Exception("wrong entity damager");

        float damage = damager.Combat.Weapon.Damage;
        //checking the opponent's block
        target.Combat.Block.Attack(target, bodySection, ref damage);
        // Apply damage to target's health component
        target.Health.TakeDamage(damager, target, bodySection, damage);
    }
}
