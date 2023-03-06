using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float power;
    public float range;
    
    private bool _isHit = false;
    private Transform _target;

    private void Update()
    {
        if (_target)
        {
            var lookAt = ValueHelper.LookAt(transform.position, _target.position);
            transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, 0.1f);
        }
        else
        {
            transform.position += transform.rotation * Vector3.up * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_isHit) return;
        if (col.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }
        if (!col.CompareTag("Enemy")) return;
        _isHit = true;
        var enemies = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var enemy in enemies)
            if (enemy.CompareTag("Enemy"))
                enemy.GetComponent<EnemyController>().Hit(power);
        Destroy(gameObject);
    }
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
