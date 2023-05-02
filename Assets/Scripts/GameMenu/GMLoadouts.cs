using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMLoadouts : MonoBehaviour
{

    public Image Slot1;
    public Image Slot2;
    public Image Slot3;

    public Sprite[] Augments;

    void Start()
    {
        Slot1.sprite = Augments[0];
        Debug.Log("dfhsjkfhks");
    }


}
