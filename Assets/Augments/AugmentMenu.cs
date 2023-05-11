using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AugmentMenu : MonoBehaviour
{
    public Animator anim; 

    public GameObject Menu;

    public bool MenuOpen;

    public Image[] Images;

    public TMP_Text[] Text;

   // public int CurrentSelectedOption;


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
        string str = AugmentManager.AugmentsInInventory[AugmentManager.AugmentsInInventory.Count - 1][0].AugmentName;

        Debug.Log(str);

        Images[0].sprite = AugmentManager.AugmentsInInventory[AugmentManager.AugmentsInInventory.Count - 1][0].Icon;
        Images[1].sprite = AugmentManager.AugmentsInInventory[AugmentManager.AugmentsInInventory.Count - 1][1].Icon;
        Images[2].sprite = AugmentManager.AugmentsInInventory[AugmentManager.AugmentsInInventory.Count - 1][2].Icon;

        Text[0].text = AugmentManager.AugmentsInInventory[AugmentManager.AugmentsInInventory.Count - 1][1].AugmentName;
        Text[1].text = AugmentManager.AugmentsInInventory[AugmentManager.AugmentsInInventory.Count - 1][1].AugmentName;
        Text[2].text = AugmentManager.AugmentsInInventory[AugmentManager.AugmentsInInventory.Count - 1][1].AugmentName;

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
