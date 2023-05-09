using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    public Transform head;
    public Vector3 offset;
    void Update()
    {


        transform.position = head.transform.position + offset;
    }
}
