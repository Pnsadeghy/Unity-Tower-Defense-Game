using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{   
    [SerializeField]
    private List<EnemyWave> waves;

    private int _activeWave;
    private int _activeWaveEnemy;
    private float _waveTimeout;
    private float _enemyTimeout;
    private float _enemyRemain;
    private List<Vector3> PathPoints;

    private void Start()
    {
        _activeWave = -1;
        Debug.Log("Waves: " + waves.Count);
        NextWave();
    }

    private void Update()
    {
        CheckWave();
    }
    
    private void NextWave() {
        int index = _activeWave + 1;
        if (index >= waves.Count) {
            Destroy(this);
            return;
        }
        Debug.Log("Start wave: " + (index + 1));
        _activeWave = index;
        _activeWaveEnemy = -1;
        _enemyRemain = 0;
        _enemyTimeout = 0;
        _waveTimeout = Time.time + waves[_activeWave].timeout;
        _enemyTimeout = 0;
        _enemyRemain = waves[_activeWave].enemies[_activeWaveEnemy].count;

        PathPoints = new List<Vector3>();
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            PathPoints.Add(child.position);
        }
    }

    private void CheckWave() {
        if (_activeWave < 0) return;
        if (!_waveTimeout.Equals(0)) {
            if (_waveTimeout > Time.time) return;
            _waveTimeout = 0;
        }
        CheckEnemy();
    }

    private void CheckEnemy() {
        if (!_enemyTimeout.Equals(0)) {
            if (_enemyTimeout > Time.time) return;
            _enemyTimeout = 0;
        }
        if (_enemyRemain.Equals(0)) {
            _activeWaveEnemy++;
            if (_activeWaveEnemy == waves[_activeWave].enemies.Count) {
                NextWave();
                return;
            }
            _enemyRemain = waves[_activeWave].enemies[_activeWaveEnemy].count;
        }

        Instantiate(
            waves[_activeWave].enemies[_activeWaveEnemy].enemy,
            waves[_activeWave].path.transform.GetChild(0).transform.position,
            Quaternion.identity,
            this.transform);

        _enemyTimeout = waves[_activeWave].enemies[_activeWaveEnemy].timeout + Time.time;
        _enemyRemain--;
    }
}
