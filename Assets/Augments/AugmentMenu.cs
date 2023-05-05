using System;
using UnityEngine;

public class AugmentMenu : MonoBehaviour
{
    public Animation anim; 

    public GameObject Menu;


    private void Start()
    {
        anim = Menu.GetComponent<Animation>();
    }
    public void Awake()
    {
        Menu.SetActive(true);
        anim.Play("OpenMenu");
    }

    public void CloseMenu()
    {
        anim.Play("CloseMenu");
        Menu.SetActive(false);
    }

    private void Update()
    {
        Debug.Log("Working");
        if (Menu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha7))
            { 
                CloseMenu();
            }
        }
    }
}
