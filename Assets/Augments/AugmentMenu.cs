using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEngine.Analytics;

public class AugmentMenu : MonoBehaviour
{
    public Animator anim; 

    public GameObject Menu;

    public bool MenuOpen;

    public GameObject selector;

    public Image[] Images;

    public TMP_Text[] Text;

    enum Options 
    {
        one, 
        two,
        three
    }

    Options SelectedOption = Options.one;

    // public int CurrentSelectedOption;


    private void Start()
    {
        MenuOpen = false;
        selector.transform.position = Images[0].transform.position;
    }

    IEnumerator DisableDelay()
    { 
        yield return new WaitForSeconds(0.2f);
        MenuOpen = true;
    }

    public void OpenMenu()
    {
        SelectedOption = Options.one;

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
            if (Input.GetMouseButtonDown(0))
            {
                AugmentManager.Augments.Add(AugmentManager.AugmentsInInventory[AugmentManager.AugmentsInInventory.Count - 1][(int)SelectedOption]);
                AugmentManager.AugmentsInInventory.Remove(AugmentManager.AugmentsInInventory[AugmentManager.AugmentsInInventory.Count - 1]);
                Debug.Log("Clicked");
                Debug.Log(AugmentManager.Augments[0]);
                CloseMenu();

            }
            if (Input.mouseScrollDelta.y > 0)
            {
                Debug.Log(Input.mouseScrollDelta.y);
                switch (SelectedOption)
                {
                    case Options.one: SelectedOption = Options.two; selector.transform.position = Images[1].transform.position; break;
                    case Options.two: SelectedOption = Options.three; selector.transform.position = Images[2].transform.position; break;
                    case Options.three: SelectedOption = Options.one; selector.transform.position = Images[0].transform.position; break;
                    default: SelectedOption = Options.one; break;
                }
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                Debug.Log(Input.mouseScrollDelta.y);
                switch (SelectedOption)
                {
                    case Options.one: SelectedOption = Options.three; selector.transform.position = Images[2].transform.position; break;
                    case Options.two: SelectedOption = Options.one; selector.transform.position = Images[0].transform.position; break;
                    case Options.three: SelectedOption = Options.two; selector.transform.position = Images[1].transform.position; break;
                    default: SelectedOption = Options.one; break;
                }
            }
        }
    }
}
