using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 5f;
    public float health = 5f;
    public float damage = 5f;
    public float coin;
    public Transform enemyHealth;

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
        var pos = transform.parent.position;
        if (_currentPoint.Equals(-1) || pos.Equals(_points[_currentPoint])) {
            _currentPoint++;
            if (_currentPoint >= _points.Count) {
                Destroy(gameObject);
                return;
            }

            _lookAt = ValueHelper.LookAt(pos, _points[_currentPoint]);
            
        }
        transform.parent.position = Vector3.MoveTowards(pos, _points[_currentPoint], speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookAt, 0.1f);
    }

    public void Hit(float damage)
    {
        _currentHealth -= damage;
        enemyHealth.gameObject.SetActive(true);
        var scale = enemyHealth.localScale;
        scale.x = _currentHealth / health;
        enemyHealth.localScale = scale;
        if (_currentHealth <= 0)
        {
            UIController.Instance.AddCoin(this.coin);
            Destroy(transform.parent.gameObject);
        }
    }
}
