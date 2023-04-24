using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraPosition : MonoBehaviour
{
    public GameObject MainPos;
    public GameObject MainAng;
    public GameObject PlayPos;
    public GameObject PlayAng;
    public GameObject ConfigPos;
    public GameObject ConfigAng;
    public GameObject GraphPos;
    public GameObject GraphAng;
    public GameObject ControlPos;
    public GameObject ControlAng;


    public GameObject MainScreen;
    public GameObject ConfigScreen;
    public GameObject PlayScreen;
    //public GameObject GraphicsScreen;
    //public GameObject ControlsScreen;

    void Start()
    {
        this.transform.position = MainPos.transform.position;
        this.transform.LookAt(MainAng.transform.position);
        ConfigScreen.SetActive(false);
        PlayScreen.SetActive(false);
        MainScreen.SetActive(true);

    }

    public void ToMain()
    {
        this.transform.position = MainPos.transform.position;
        this.transform.LookAt(MainAng.transform.position);
        ConfigScreen.SetActive(false);
        PlayScreen.SetActive(false);
        MainScreen.SetActive(true);
    }

    public void ToConfig()
    {
        this.transform.position = ConfigPos.transform.position;
        this.transform.LookAt(ConfigAng.transform.position);
        ConfigScreen.SetActive(true);
        PlayScreen.SetActive(false);
        MainScreen.SetActive(false);
    }

    public void ToGraphics()
    {
        this.transform.position = GraphPos.transform.position;
        this.transform.LookAt(GraphAng.transform.position);
    }

    public void ToControls()
    {
        this.transform.position = ControlPos.transform.position;
        this.transform.LookAt(ControlAng.transform.position);
    }

    public void ToPlay()
    {
        this.transform.Translate(MainPos.transform.position);
        this.transform.LookAt(MainAng.transform.position);
    }

    public void ToCampaign()
    {
        this.transform.Translate(MainPos.transform.position);
        this.transform.LookAt(MainAng.transform.position);
    }
}
