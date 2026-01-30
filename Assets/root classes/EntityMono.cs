using System.Collections;
using UnityEngine;
using UnityEditor;
/// <summary>
/// MonoBehaviour class for work with coroutines.
/// </summary>
public class EntityMono : MonoBehaviour
{
    [SerializeField] public Entity entity;





    private void OnEnable()
    {
        entity ??= new Entity();
        entity.Health.OnEntityBloodLoss += BloodLoseStart;

    }
    private void OnDisable()
    {
        entity.Health.OnEntityBloodLoss -= BloodLoseStart;
    }

    private void BloodLoseStart()
    {
        if (!entity.Health.isBloodLoss)
        {
            StartCoroutine(entity.Health.BloodLoseCoroutine(Debug));
        } 
        void Debug(string callback)
        {
            UnityEngine.Debug.Log($"{entity.Name} истекает кровью! {callback}");
        }
    }
}
