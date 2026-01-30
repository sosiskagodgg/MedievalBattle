using System;
using UnityEngine;

public class BlockComponent
{
    private bool _isUpperBlock;
    public bool IsUpperBlock { get => _isUpperBlock; set { RefreshBlocks(); _isUpperBlock = value; } }


    private bool _isMidleBlock;
    public bool IsMidleBlock { get => _isMidleBlock; set { RefreshBlocks(); IsMidleBlock = value; } }


    private bool _isLowerBlock;
    public bool IsLowerBlock { get => _isLowerBlock; set { RefreshBlocks(); _isLowerBlock = value; } }

    public event Action OnAttackBlock;

    private void RefreshBlocks()
    {
        _isUpperBlock = false;
        _isMidleBlock = false;
        _isLowerBlock = false;
    }

    /// <summary>
    /// Attacked target block
    /// </summary>
    /// <param name="target"></param>
    /// <param name="bodySection"></param>
    /// <param name="damage"></param>
    public void Attack(Entity target, BodySection bodySection, ref float damage)
    {
        if(bodySection == BodySection.UpperBody && IsUpperBlock ||
           bodySection == BodySection.MiddleBody && IsMidleBlock||
           bodySection == BodySection.LowerBody && IsLowerBlock)
        {
            if(target.Combat.Weapon.Hp> damage)
            {
                target.Combat.Weapon.Hp -= damage;
                damage = 0;
            }
            else
            {
                damage -= target.Combat.Weapon.Hp;
                target.Combat.Weapon.Hp = 0;
            }
        }
    }
}
