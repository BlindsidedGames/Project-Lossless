using System.Collections.Generic;
using UnityEngine;
using static Oracle;


public class WeightedDrops : MonoBehaviour
{
    [SerializeField] public List<WeightedSpawnSO> WeightsSO = new();
    [SerializeField] public float[] weights;

    private void Awake()
    {
        weights = new float[WeightsSO.Count];
    }

    private void Start()
    {
        CalculateSpawnWeights();
    }

    private void CalculateSpawnWeights()
    {
        float TotalWeight = 0;

        for (var i = 0; i < WeightsSO.Count; i++)
        {
            weights[i] = WeightsSO[i].GetWeight();
            TotalWeight += weights[i];
        }

        for (var i = 0; i < weights.Length; i++) weights[i] = weights[i] / TotalWeight;
    }
}