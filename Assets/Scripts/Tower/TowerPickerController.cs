using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPickerController : MonoBehaviour
{
    public GameObject prefab;
    public float land;
    public float range;
    public LayerMask blockLayer;

    private SpriteRenderer _landChild;
    private SpriteRenderer _rangeChild;
    private Color _landColor;
    private Color _rangeColor;

    private void Start()
    {
        var landChild = transform.GetChild(0);
        var rangeChild = transform.GetChild(1);

        landChild.localScale = new Vector3(land, land, 1);
        rangeChild.localScale = new Vector3(range, range, 1);

        _landChild = landChild.GetComponent<SpriteRenderer>();
        _rangeChild = rangeChild.GetComponent<SpriteRenderer>();

        _landColor = _landChild.color;
        _rangeColor = _rangeChild.color;
    }

    void Update()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        var colliders = Physics2D.OverlapCircleAll(transform.position, land / 2, blockLayer);

        var haveError = !colliders.Length.Equals(0) || !UIController.Instance.CheckCoin(7.5f);
        
        _landChild.color = !haveError ? _landColor : new Color(1f, 0f, 0f, 0.5f);
        _rangeChild.color = !haveError ? _rangeColor : new Color(1f, 0f, 0f, 0.2f);

        if (Input.GetMouseButtonDown(0))
        {
            if (!haveError)
            {
                UIController.Instance.DecCoin(7.5f);
                Instantiate(prefab, transform.position, Quaternion.identity, this.transform.parent);
            }
        } 
    }
}
