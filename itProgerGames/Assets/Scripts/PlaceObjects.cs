using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceObjects : MonoBehaviour
{
    public LayerMask layer;
    public float rotateSpeed = 60f;

    private void Start()
    {
        PositionObject();
    }

    void Update()
    {
        PositionObject();

        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<AutoCarCreate>().enabled = true;
            Destroy(gameObject.GetComponent<PlaceObjects>());

        }
            

        if (Input.GetMouseButton(1))
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        
    }

    private void PositionObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, layer))
            transform.position = hit.point;
    }
}
