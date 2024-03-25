using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    private Vector3 firstPos;
    private Vector3 firstRotation;
    private bool isDrag = false;

    private void Update()
    {
        Rotation();
        
    }

    private void Rotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = new Vector3(Input.mousePosition.y,Input.mousePosition.x,Input.mousePosition.z);
            firstRotation = transform.eulerAngles;
            isDrag = true;
        }
        if (isDrag && Input.GetMouseButton(0))
        {
            Vector3 posNow = new Vector3(Input.mousePosition.y, Input.mousePosition.x, Input.mousePosition.z);
            Vector3 direction = new Vector3(posNow.x - firstPos.x,firstPos.y - posNow.y,firstPos.z - posNow.z);
            if (Input.GetKey(KeyCode.K))
            {
                direction = Vector3.zero;
            }
            //transform.eulerAngles = firstRotation + direction;
            transform.RotateAround(transform.position, Vector3.right, direction.x * Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.up, direction.y * Time.deltaTime);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }

    }
}
