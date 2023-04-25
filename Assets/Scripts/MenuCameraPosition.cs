using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCameraPosition : MonoBehaviour
{
    public GameObject MainCamera;
    [SerializeField] GameObject MainPos;
    [SerializeField] GameObject MainAng;
    [SerializeField] GameObject PlayPos;
    [SerializeField] GameObject PlayAng;
    [SerializeField] GameObject ConfigPos;
    [SerializeField] GameObject ConfigAng;
    [SerializeField] GameObject GraphPos;
    [SerializeField] GameObject GraphAng;
    [SerializeField] GameObject ControlPos;
    [SerializeField] GameObject ControlAng;


    [SerializeField] GameObject MainScreen;
    [SerializeField] GameObject ConfigScreen;
    [SerializeField] GameObject PlayScreen;
    [SerializeField] GameObject GraphicsScreen;
    [SerializeField] GameObject ControlsScreen;

    [SerializeField] GameObject[] screens = new GameObject[5]; 

    void Start()
    {
        screens[0] = MainScreen;
        screens[1] = ConfigScreen;
        screens[2] = PlayScreen;
        screens[3] = GraphicsScreen;
        screens[4] = ControlsScreen;
        ToMain(0);
    }
    private void ChangeUI(int screen = 0)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (i == screen)
            {
                screens[i].SetActive(true);
                Debug.Log(screens[screen].ToString());
            }
            else
            {
                screens[i].SetActive(false);
            }
        }
    }

    public void ToMain(int index = 0)
    {
        MainCamera.transform.position = MainPos.transform.position;
        MainCamera.transform.LookAt(MainAng.transform.position);
        ChangeUI(index);
    }

    public void ToConfig(int index = 1)
    {
        MainCamera.transform.position = ConfigPos.transform.position;
        MainCamera.transform.LookAt(ConfigAng.transform.position);
        ChangeUI(index);
    }

    public void ToGraphics(int index = 3)
    {
        MainCamera.transform.position = GraphPos.transform.position;
        MainCamera.transform.LookAt(GraphAng.transform.position);
        ChangeUI(index);
    }

    public void ToControls(int index = 4)
    {
        MainCamera.transform.position = ControlPos.transform.position;
        MainCamera.transform.LookAt(ControlAng.transform.position);
        ChangeUI(index);
    }

    public void ToPlay(int index = 2)
    {
        MainCamera.transform.position = PlayPos.transform.position;
        MainCamera.transform.LookAt(PlayAng.transform.position);
        ChangeUI(index);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
