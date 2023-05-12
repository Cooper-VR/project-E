using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameObject Alert;
    bool AlertAugment;
    bool cooldown;
    public AugmentMenu AugmentMenu;
    [SerializeField] private Item[] AllAugments;

    void Start()
    {
        AugmentManager.AllAugments = AllAugments;
        AlertAugment = false;
        cooldown = false;
        AugmentManager.StartCount();
        Debug.Log(AugmentManager.AugmentsInReserve);
    }

    void Update()
    {
        if (AugmentManager.AugmentsInReserve >= 1)
        {
            if (AlertAugment == false)
            {
                Alert.SetActive(true);
                AlertAugment = true;
            }
            if (AlertAugment && AugmentManager.AugmentsInReserve < 1)
            {
                AlertAugment = false;
            }
            if (Alert.activeSelf && !AugmentMenu.MenuOpen && !cooldown)
            {
                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    AugmentMenu.OpenMenu();
                }
            }
        }
    }
    
}
