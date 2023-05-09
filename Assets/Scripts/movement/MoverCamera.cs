using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition = null;
    [SerializeField] FirstPersonController controller;
    public float offset;

    float difference;

    public Transform head;

    private void Start()
    {
        difference = head.transform.position.y - cameraPosition.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(cameraPosition.position.x, cameraPosition.position.y + difference, cameraPosition.position.z);
    }
}
