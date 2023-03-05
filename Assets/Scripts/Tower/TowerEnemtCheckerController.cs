using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemtCheckerController : MonoBehaviour
{
    private TowerController _parent;

    private void Start()
    {
        _parent = transform.parent.GetComponent<TowerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        _parent.OnEnemyEnter(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        _parent.OnEnemyExit(other);
    }
}
