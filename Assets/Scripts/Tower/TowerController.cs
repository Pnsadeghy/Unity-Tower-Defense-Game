using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject bullet;
    public float timeout;
    public float cost;
    public float land;
    public float range;
    
    private List<GameObject> _enemies;
    private Transform _weapon;
    private GameObject _target;
    private Quaternion _lookAt;
    private float _lastShot;

    private void Start()
    {
        _enemies = new List<GameObject>();
        _target = null;
        _weapon = transform.Find("Weapon");
    }

    private void Update() {
        if (_target != null) {
            _lookAt = ValueHelper.LookAt(_weapon.position, _target.transform.position);
            _weapon.rotation = Quaternion.Slerp(_weapon.rotation, _lookAt, 0.1f);

            if (_lastShot + timeout < Time.time)
            {
                _lastShot = Time.time;
                var _bullet = Instantiate(bullet, transform.position, _lookAt);
                _bullet.GetComponent<BulletController>().SetTarget(_target.transform);
            }
        }
    }

    public void OnEnemyEnter(Collider2D other)
    {
        _enemies.Add(other.gameObject);
        if (_target == null)
            _target = other.gameObject;
    }

    public void OnEnemyExit(Collider2D other)
    {
        _enemies.Remove(other.gameObject);
        if (_target.Equals(other.gameObject)) {
            _target = GetNearEnemy();
        }
    }

    private GameObject GetNearEnemy() {
        if (_enemies.Count.Equals(0)) return null;
        GameObject target = null;
        var minDistance = Mathf.Infinity;
        foreach(var enemy in _enemies) {
            var distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < minDistance) {
                target = enemy;
                minDistance = distanceToEnemy;
            }
        }
        return target;
    }
}
