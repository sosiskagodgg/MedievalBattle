using UnityEngine;              
using System.Collections.Generic; 
using System.Linq;
public class RandomEventGeneration
{
    public static T GetRandomObject<T>(Dictionary<T, float> itemsWithWeights)
    {
        float totalWeight = itemsWithWeights.Values.Sum();
        float randomValue = Random.Range(0f, totalWeight);

        float currentWeight = 0f;
        foreach (var item in itemsWithWeights)
        {
            currentWeight += item.Value;
            if (randomValue <= currentWeight)
                return item.Key;
        }

        // Ќа вс€кий случай возвращаем последний
        return itemsWithWeights.Last().Key;
    }
    public static BodyPart GetRandomBodyPart(List<BodyPart> bodyParts)
    {
        Dictionary<BodyPart, float> items = new();
        foreach (var part in bodyParts) { items.Add(part, part.bodyPartSize); }
        return GetRandomObject(items);
    }
}
