using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine; 

public class MenuCameraPosition : MonoBehaviour
{
    public GameObject MainCamera;

    [SerializeField] GameObject MainScreen;
    [SerializeField] GameObject ConfigScreen;
    [SerializeField] GameObject PlayScreen;
    [SerializeField] GameObject GraphicsScreen;
    [SerializeField] GameObject ControlsScreen;

    [SerializeField] CinemachineVirtualCamera Main;
    [SerializeField] CinemachineVirtualCamera Config;
    [SerializeField] CinemachineVirtualCamera Play;
    [SerializeField] CinemachineVirtualCamera Graphics;
    [SerializeField] CinemachineVirtualCamera Controls;

    [SerializeField] GameObject[] screens = new GameObject[5];


    IEnumerator BelndDelay(int index)
    {
        yield return new WaitForSeconds(0.75f);
        ChangeUI(index);
    }

    void Start()
    {
        screens[0] = MainScreen;
        screens[1] = ConfigScreen;
        screens[2] = PlayScreen;
        screens[3] = GraphicsScreen;
        screens[4] = ControlsScreen;
        MainMenuCameraSwitch.SwitchCamera(Main);
        ClearUI();
        ChangeUI();
    }

    private void OnEnable()
    {
        MainMenuCameraSwitch.Register(Main);
        MainMenuCameraSwitch.Register(Config);
        MainMenuCameraSwitch.Register(Play);
        MainMenuCameraSwitch.Register(Graphics);
        MainMenuCameraSwitch.Register(Controls);
    }

    private void OnDisable()
    {
        MainMenuCameraSwitch.Unregister(Main);
        MainMenuCameraSwitch.Unregister(Config);
        MainMenuCameraSwitch.Unregister(Play);
        MainMenuCameraSwitch.Unregister(Graphics);
        MainMenuCameraSwitch.Unregister(Controls);
    }

    private void ChangeUI(int screen = 0)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (i == screen)
            {
                screens[i].SetActive(true);
            }
            else
            {
                screens[i].SetActive(false);
            }
        }
    }

    private void ClearUI()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(false);
        }
    }

    public void ToMain(int index = 0)
    {
        MainMenuCameraSwitch.SwitchCamera(Main);
        ClearUI();
        StartCoroutine(BelndDelay(index));
    }

    public void ToConfig(int index = 1)
    {
        MainMenuCameraSwitch.SwitchCamera(Config);
        ClearUI();
        StartCoroutine(BelndDelay(index));
    }

    public void ToGraphics(int index = 3)
    {

        MainMenuCameraSwitch.SwitchCamera(Graphics);
        ClearUI();
        StartCoroutine(BelndDelay(index));
    }

    public void ToControls(int index = 4)
    {
        MainMenuCameraSwitch.SwitchCamera(Controls);
        ClearUI();
        StartCoroutine(BelndDelay(index));
    }

    public void ToPlay(int index = 2)
    {
        MainMenuCameraSwitch.SwitchCamera(Play);
        ClearUI();
        StartCoroutine(BelndDelay(index));
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
