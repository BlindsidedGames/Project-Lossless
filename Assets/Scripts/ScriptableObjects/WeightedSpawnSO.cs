using UnityEngine;

[CreateAssetMenu(fileName = "WeightedSpawnConfig", menuName = "SO/weight")]
public class WeightedSpawnSO : ScriptableObject
{
    public int obj;

    [Range(0, 1)] public float MinWeight;

    [Range(0, 1)] public float MaxWeight;

    public float GetWeight()
    {
        return Random.Range(MinWeight, MaxWeight);
    }
}