using UnityEngine;
[System.Serializable]
/// <summary>
/// Handles combat-related functionality including weapons and damage dealing.
/// </summary>
public class CombatComponent
{
    public Weapon Weapon { get; private set; }
    public Attack—omponent Attack { get; private set; }
    public BlockComponent Block { get; private set;  }
    public CombatComponent(Weapon weapon)
    {
        Weapon = weapon;
        Attack = new();
        Block = new();
    }

}

