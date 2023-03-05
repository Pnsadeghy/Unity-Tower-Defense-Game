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

    private void Update()
    {
        transform.position += transform.rotation * Vector3.up * speed * Time.deltaTime;
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
}
