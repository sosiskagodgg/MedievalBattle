using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Represents a game entity composed of multiple components.
/// This class coordinates interactions between components.
/// </summary>
public class Entity
{
    public string Name { get; private set; }
    public HealthComponent Health { get; private set; }
    public CombatComponent Combat { get; private set; }
    public BodyComponent Body { get; private set; }
    public MovementComponent Movement { get; private set; }
    public EffectsComponent Effects { get; private set; }

    /// <summary>
    /// Initializes a new entity with specified components.
    /// </summary>
    public Entity(string name, Weapon weapon, List<BodyPart> bodyParts,
                  float bloodVolumeMax, float maxSpeed)
    {
        Name = name;

        // Initialize components
        Health = new HealthComponent(bloodVolumeMax);
        Combat = new CombatComponent(weapon);
        Body = new BodyComponent(bodyParts);
        Movement = new MovementComponent(maxSpeed);
        Effects = new EffectsComponent(this, Combat, Movement);

        // Wire up death event
        Health.OnEntityDeath += HandleEntityDeath;
    }

    public Entity()
    {
        Name = "Test Entity";
        Weapon weapon = new Weapon(20,2,100);
        List<BodyPart> bodyParts = new List<BodyPart>()
        {
            new BodyPart("голова",BodySection.UpperBody,100,5000),
            new BodyPart("торс",BodySection.MiddleBody,100,5000),
            new BodyPart("ноги",BodySection.LowerBody,100,2500),
        };
        float bloodVolumeMax = 5000;
        float maxSpeed = 10;


        // Initialize components
        Health = new HealthComponent(bloodVolumeMax);
        Combat = new CombatComponent(weapon);
        Body = new BodyComponent(bodyParts);
        Movement = new MovementComponent(maxSpeed);
        Effects = new EffectsComponent(this, Combat, Movement);

        // Wire up death event
        Health.OnEntityDeath += HandleEntityDeath;
    }
    protected virtual void HandleEntityDeath(Entity killer)
    {
        // Entity-specific death logic can be added here
        Debug.Log($"{Name} has been killed by {killer?.Name}");
    }

    

    
}



