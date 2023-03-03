using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyWave
{
    public float timeout = 10f;

    [SerializeField]
    private List<EnemyWaveItem> enemies;
}