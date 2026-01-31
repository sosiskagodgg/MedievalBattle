using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
[System.Serializable]
/// <summary>
/// Manages health, blood volume, and death state of an entity.
/// </summary>
public class HealthComponent
{
    public event Action<Entity> OnEntityDeath;
    public event Action OnEntityBloodLoss; 

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

    
    private float _bloodLossPerSecond;
    public float BloodLossPerSecond { get=> _bloodLossPerSecond; private set=> _bloodLossPerSecond = value; }
    public bool isBloodLoss { get;private set;  }

   private Entity _lastDamager;


    public HealthComponent(float bloodVolumeMax)
    {
        _bloodVolumeMax = bloodVolumeMax;
        BloodVolume = bloodVolumeMax;
    }

    /// <summary>
    /// Applies damage to the entity and tracks the damager.
    /// </summary>
    public void TakeDamage(Entity damager,Entity target, BodySection bodySection, float damage)
    {
        _lastDamager = damager;

        if (target.Health != this) throw new Exception("wrong entity target!!");

        // Get random body part from target's body component
        List<BodyPart> targetBodyParts = target.Body.BodyParts.Where(b=>b.bodySection == bodySection).ToList();
        BodyPart targetBodyPart = RandomEventGeneration.GetRandomBodyPart(targetBodyParts);

        // Calculate blood loss from the damaged body part
        float bloodLoss = targetBodyPart.TakeDamage(damage);
        AddBloodLoss(bloodLoss);
        Debug.Log($"{target.Name} took {damage} damage from {damager?.Name} to {targetBodyPart} (BV: {BloodVolume}/{_bloodVolumeMax})\nAdd bloodLoss: {bloodLoss},total bloodLoss: {target.Health.BloodLossPerSecond}");

        OnEntityBloodLoss?.Invoke();
    }

    /// <summary>
    /// Adds blood loss per second to the entity.
    /// </summary>
    public void AddBloodLoss(float bloodLoss)
    {
        BloodLossPerSecond += bloodLoss;
    }

    public IEnumerator BloodLoseCoroutine(Action<string> callback)
    {

        while (BloodLossPerSecond > 0 && BloodVolume > 0)
        {
            isBloodLoss = true;
            BloodVolume -= BloodLossPerSecond;
            if(BloodVolume!=0) callback?.Invoke($"{BloodVolume} / {BloodLossPerSecond}");
            yield return new WaitForSeconds(1f);
        }
        isBloodLoss = false;
    }
}