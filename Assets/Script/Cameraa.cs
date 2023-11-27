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
    private void Update()
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
            transform.Translate(direction.x * dragSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x <= 6.22f)
                transform.position = firstPos;
            if (transform.position.x >= 74f)
                transform.position = firstPos + Vector3.right * 67.78f;
            lastPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }
    }
}
