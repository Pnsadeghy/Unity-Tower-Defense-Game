using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    public float health;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = health;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            _currentHealth -= col.GetComponent<EnemyController>().damage;
            if (_currentHealth < 0)
                Destroy(this.gameObject);
        }
    }
}
