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
    private List<Vector3> _pathPoints;

    private void Start()
    {
        _activeWave = -1;
        NextWave();
    }

    private void Update()
    {
        CheckWave();
    }
    
    private void NextWave() {
        _activeWave++;
        if (_activeWave >= waves.Count) {
            Destroy(this);
            return;
        }
        _activeWaveEnemy = -1;
        _enemyRemain = 0;
        _enemyTimeout = 0;
        _waveTimeout = Time.time + waves[_activeWave].timeout;
        UIController.Instance.SetTimer(waves[_activeWave].timeout);

        _pathPoints = new List<Vector3>();
        foreach (Transform child in waves[_activeWave].path.GetComponentsInChildren<Transform>())
        {
            if (child.Equals(waves[_activeWave].path.transform)) continue;
            _pathPoints.Add(child.position);
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
        if (CheckRemain()) return;

        var enemy = Instantiate(
            waves[_activeWave].enemies[_activeWaveEnemy].enemy,
            waves[_activeWave].path.transform.GetChild(0).transform.position,
            Quaternion.identity,
            this.transform);
        enemy.GetComponentInChildren<EnemyController>().setPoints(_pathPoints);

        _enemyTimeout = waves[_activeWave].enemies[_activeWaveEnemy].timeout + Time.time;
        _enemyRemain--;
        CheckRemain();
    }

    private bool CheckRemain()
    {
        if (_enemyRemain.Equals(0))
        {
            _activeWaveEnemy++;
            if (_activeWaveEnemy == waves[_activeWave].enemies.Count)
            {
                NextWave();
                return true;
            }

            _enemyRemain = waves[_activeWave].enemies[_activeWaveEnemy].count;
        }

        return false;
    }
}
