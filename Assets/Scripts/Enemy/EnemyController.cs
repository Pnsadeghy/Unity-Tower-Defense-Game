using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 5f;
    public float health = 5f;
    public float damage = 5f;

    private int _currentPoint = 0;
    private List<Vector3> _points;
    private Quaternion _lookAt;
    private float _currentHealth;

    public void setPoints(List<Vector3> points) {
        this._points = points;
    }

    private void Start()
    {
        _currentHealth = health;
    }

    private void Update()
    {
        if (_currentPoint.Equals(-1) || transform.position.Equals(_points[_currentPoint])) {
            _currentPoint++;
            if (_currentPoint >= _points.Count) {
                Destroy(gameObject);
                return;
            }

            _lookAt = ValueHelper.LookAt(transform.position, _points[_currentPoint]);
        }
        transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint], speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookAt, 0.1f);
    }

    public void Hit(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Destroy(gameObject);
    }
}
