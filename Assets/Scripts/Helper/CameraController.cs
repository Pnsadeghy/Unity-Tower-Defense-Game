using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxX;
    public float maxY;
    public float minSize;
    public float maxSize;

    public float zoomSpeed = 1f;
    public float dragSpeed = 2f;

    private float _targetSize;
    private Vector3 _targetPos;

    private void Start()
    {
        _targetSize = Camera.main.orthographicSize;
        _targetPos = transform.position;
    }

    private void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll < 0)
            _targetSize = Mathf.Min(maxSize, _targetSize + 1);
        else if (scroll > 0)
            _targetSize = Mathf.Max(minSize, _targetSize - 1);
        
        var size = Camera.main.orthographicSize;
        if (!size.Equals(_targetSize))
            Camera.main.orthographicSize = Mathf.Lerp(size, _targetSize, zoomSpeed * Time.deltaTime);

        if (Input.GetMouseButton(0) || scroll != 0)
            _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = Vector3.Lerp(transform.position, _targetPos, dragSpeed * Time.deltaTime);
    }
}
