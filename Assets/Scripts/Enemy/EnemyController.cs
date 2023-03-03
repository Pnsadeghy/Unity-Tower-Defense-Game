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

    public void setPoints(List<Vector3> points) {
        this._points = points;
    }

    private void Update()
    {
        if (_currentPoint.Equals(-1) || transform.position.Equals(_points[_currentPoint])) {
            _currentPoint++;
            if (_currentPoint >= _points.Count) {
                Destroy(gameObject);
                return;
            }

            Vector3 direction = _points[_currentPoint] - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;

        }
        transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint], speed * Time.deltaTime);
    }
}
