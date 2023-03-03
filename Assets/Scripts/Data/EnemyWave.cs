using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyWave
{
    public float timeout = 10f;

    public Transform path; 

    [SerializeField]
    public List<EnemyWaveItem> enemies;
}