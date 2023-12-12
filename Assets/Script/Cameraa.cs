using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraa : MonoBehaviour
{
    public float dragSpeed = 3000f;
    public bool isDrag = false;
    public Vector3 lastPos;
    public Vector3 firstPos;


    private void Awake()
    {
        firstPos = transform.position;
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrag = true;
            lastPos = Input.mousePosition;
        }
        if (isDrag && Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastPos;
            Vector3 direction = Camera.main.ScreenToViewportPoint(delta);           
            transform.Translate(direction.x * dragSpeed * Time.deltaTime, 0, direction.y * dragSpeed * Time.deltaTime);
            if (transform.position.x <= 6.22f)
                transform.position = new Vector3(6.22f,transform.position.y,transform.position.z);
            if (transform.position.x >= 74f)
                transform.position = new Vector3(74f, transform.position.y, transform.position.z);
            if (transform.position.z >= -9f)
                transform.position = new Vector3(transform.position.x, transform.position.y, -9f);
            if (transform.position.z <= -16f)
                transform.position = new Vector3(transform.position.x, transform.position.y, -16f);
            if (transform.position.y != 10.42f)
                transform.position = new Vector3(transform.position.x, 10.42f, transform.position.z);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }
    }
}
