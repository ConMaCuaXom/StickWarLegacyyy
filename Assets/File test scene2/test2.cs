using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class test2 : MonoBehaviour
{
    public float angularSpeed = 20f;
    public Transform target;
    private void Update()
    {
        Quaternion quaternionTarget = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, quaternionTarget, angularSpeed * Time.deltaTime);
    }
}
