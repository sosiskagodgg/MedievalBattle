using UnityEngine;

public class DamageMono : MonoBehaviour
{
    [SerializeField] EntityMono Entity;
    
    public void Damage(float damage)
    {
        Entity.entity.Health.TakeDamage(Entity.entity, Entity.entity, BodySection.LowerBody, damage);
    }
}
