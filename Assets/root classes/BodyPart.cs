
using Unity.VisualScripting;
using UnityEditor;
using System;
using UnityEngine;
public class BodyPart 
{
    public readonly string name;
    public readonly BodySection bodySection;
    public readonly float bodyPartSize;//сколько процентов занимает орган от своего BodySection
    public readonly float bloodLossPerSecond;

    public float bodyPartHP = 100f;
    public BodyPart(string name, BodySection bodySection, float bodyPartSize,float bloodLossPerSecond)
    {
        this.name = name;
        this.bodySection = bodySection;
        this.bodyPartSize = bodyPartSize;
        this.bloodLossPerSecond = bloodLossPerSecond;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage">Урон нанесенный этому органу</param>
    /// <returns>Возвращает bloodLossPerSecond</returns>
    public float TakeDamage(float damage)
    {
        bodyPartHP -= damage;
        float percentageOfMaxBloodLoss = (bloodLossPerSecond / 100) * damage;
        return percentageOfMaxBloodLoss;
    }
}
