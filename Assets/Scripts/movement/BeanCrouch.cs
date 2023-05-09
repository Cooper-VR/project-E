

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeanCrouch : MonoBehaviour
{
    [SerializeField] FirstPersonController firstPersonController;

    Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }
    private void Update()
    {
        if (firstPersonController.isCrouching)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y/2, originalScale.z);
        }
        else 
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y / 2, originalScale.z);
        }
    }   
    
}


