using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Alert;
    bool AlertAugment;

    void Start()
    {
       AlertAugment = false;
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
            if (Alert.activeSelf && !Menu.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    Menu.SetActive(true);
                }
            }
        }
    }
    
}
