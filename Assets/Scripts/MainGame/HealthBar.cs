using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image bar;
    int num;

    [SerializeField] playerStats Stats;

    private void Start()
    {
        slider.maxValue = 150;
    }

    public void SetHealth(int health)
    { 
        slider.value = health;
        if (health > 100)
        {
            bar.color = new Color32(0, 101, 0, 255);
        }
        else if (health > 50)
        {
            bar.color = new Color32(171, 167, 23, 255);
        }
        else
        {
            bar.color = new Color32(101, 0, 0, 255);
        }
    }

    private void Update()
    {
        Debug.Log(Stats.health);
        SetHealth(Stats.health);
    }
}
