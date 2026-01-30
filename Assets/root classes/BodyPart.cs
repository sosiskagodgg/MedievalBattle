
using Unity.VisualScripting;
using UnityEditor;
using System;
using UnityEngine;
public class BodyPart 
{
    public readonly string name;
    public readonly BodySection bodySection;
    public readonly float bodyPartSize;//what percentage does the bodyPart occupy of its own BodySection
    public readonly float bloodLossPerSecondOnDestroy;

    float _bodyPartHP = 100f;
    public float BodyPartHP 
    { 
        get => _bodyPartHP;

        set 
        {
            _bodyPartHP = Mathf.Clamp(value,0,100);
            if (_bodyPartHP <= 0) 
            { 
                OnDestroy();
            } 
        } 
    }

    public BodyPart(string name, BodySection bodySection, float bodyPartSize, float bloodLossPerSecond)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        if (bodyPartSize <= 0) throw new ArgumentException("Размер части тела должен быть положительным");
        if (bloodLossPerSecond < 0) throw new ArgumentException("Кровопотеря не может быть отрицательной");

        this.name = name;
        this.bodySection = bodySection;
        this.bodyPartSize = bodyPartSize;
        this.bloodLossPerSecondOnDestroy = bloodLossPerSecond;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage">Урон нанесенный этому органу</param>
    /// <returns>Возвращает bloodLossPerSecond</returns>
    public virtual float TakeDamage(float damage)
    {
        BodyPartHP -= damage;
        float percentageOfMaxBloodLoss = (bloodLossPerSecondOnDestroy / 100) * damage;
        return percentageOfMaxBloodLoss;
    }

    public virtual void OnDestroy()
    {

    }
}
