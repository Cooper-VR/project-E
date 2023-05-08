using System;
using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public class AugmentMenu : MonoBehaviour
{
    public Animator anim; 

    public GameObject Menu;

    public bool MenuOpen;


    private void Start()
    {
        MenuOpen = false;
    }

    IEnumerator DisableDelay()
    { 
        yield return new WaitForSeconds(0.2f);
        MenuOpen = true;

    }

    public void OpenMenu()
    {
        StartCoroutine(DisableDelay());
        anim.SetBool("Menu", true);
    }

    public void CloseMenu()
    {
        MenuOpen = false;
        anim.SetBool("Menu", false);
    }

    private void Update()
    {
        if (MenuOpen)
        {
            if (Input.GetKeyUp(KeyCode.Alpha7))
            {
                Debug.Log("pressed");
                CloseMenu();
            }
        }
    }
}
