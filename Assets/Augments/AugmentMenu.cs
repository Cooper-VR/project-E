using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class AugmentMenu : MonoBehaviour
{
    public Animator anim; 

    public GameObject Menu;

    public bool MenuOpen;

    public GameObject Slot1;

    public GameObject Slot2;

    public GameObject Slot3;

    public int CurrentSelectedOption;


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
        Image image1 = Slot1.GetComponentInChildren<Image>();
        Text text1 = Slot1.GetComponentInChildren<Text>();
        //image1.sprite = AugmentManager.Augments[0][0].Icon;
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
