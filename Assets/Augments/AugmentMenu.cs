using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class AugmentMenu : MonoBehaviour
{
    static public Animator anim; 

    static public GameObject Menu;

    static public bool MenuOpen;

    static public void OpenMenu()
    {
        anim.SetBool("OpenMenu", true);
    }

    static public void CloseMenu()
    {
        anim.SetBool("OpenMenu", false);
    }

    private void Update()
    {
        if (Menu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha7))
            { 
                CloseMenu();
            }
        }
    }
}
