using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveShowing : MonoBehaviour
{
    public KeyCode menuKey;
    public GameObject[] UIObjects;

    public bool showing = false;
    private void Update()
    {
        if (Input.GetKeyDown(menuKey) && showing == false)
        {
            showing = true;
        } 
        else if (Input.GetKeyDown(menuKey) && showing == true) 
        {
            showing = false;
        }

        if (showing == true)
        {
            Cursor.lockState = CursorLockMode.None;
            for (int i = 0; i < UIObjects.Length; i++)
            {
                UIObjects[i].SetActive(true);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            for (int i = 0; i < UIObjects.Length; i++)
            {
                UIObjects[i].SetActive(false);
            }
        }
    }
}
