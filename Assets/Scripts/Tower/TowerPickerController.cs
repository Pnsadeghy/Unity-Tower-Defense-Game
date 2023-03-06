using System;
using System.Collections;
using System.Collections.Generic;
using Control;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPickerController : MonoBehaviour
{
    public GameObject prefab;
    public LayerMask blockLayer;

    private SpriteRenderer _landChild;
    private SpriteRenderer _rangeChild;
    private Color _landColor;
    private Color _rangeColor;

    private void Start()
    {
        var landChild = transform.GetChild(0);
        var rangeChild = transform.GetChild(1);

        _landChild = landChild.GetComponent<SpriteRenderer>();
        _rangeChild = rangeChild.GetComponent<SpriteRenderer>();

        _landColor = _landChild.color;
        _rangeColor = _rangeChild.color;
    }

    void Update()
    {
        var tower = UIController.Instance.chosenTower;
        if (tower == null || EventSystem.current.IsPointerOverGameObject())
        {
            _landChild.gameObject.SetActive(false);
            _rangeChild.gameObject.SetActive(false);
        }
        else
        {
            _landChild.transform.localScale = new Vector3(tower.land, tower.land, 1);
            _rangeChild.transform.localScale = new Vector3(tower.range, tower.range, 1);
            
            _landChild.gameObject.SetActive(true);
            _rangeChild.gameObject.SetActive(true);
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
            var colliders = Physics2D.OverlapCircleAll(transform.position, tower.land / 2, blockLayer);

            var haveError = !colliders.Length.Equals(0) || !UIController.Instance.CheckCoin(tower.cost);
        
            _landChild.color = !haveError ? _landColor : new Color(1f, 0f, 0f, 0.5f);
            _rangeChild.color = !haveError ? _rangeColor : new Color(1f, 0f, 0f, 0.2f);

            if (Input.GetMouseButtonDown(0))
            {
                if (!haveError)
                {
                    UIController.Instance.DecCoin(tower.cost);
                    Instantiate(tower.gameObject, transform.position, Quaternion.identity, transform.parent);
                }
                UIController.Instance.chosenTower = null;
            }    
        }
    }
}
