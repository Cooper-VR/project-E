using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine; 

public class MenuCameraPosition : MonoBehaviour
{
    /*
     *********************
     ****DOCUMNETATION****
     *********************
     
     * The Main Purpose of this Script is to register all the cameras and menus. Then switch between each of the menus. 
   
     */


    //Create arrays to store all cameras and menus.
    [SerializeField] GameObject[] Screens = new GameObject[5];
    [SerializeField] CinemachineVirtualCamera[] Cameras = new CinemachineVirtualCamera[5];

    IEnumerator BelndDelay(int index)
    {
        //This Coroutine keeps the new menu from displaying whilst the camera is transitioning.

        yield return new WaitForSeconds(0.75f);
        ChangeUI(index);
    }

    void Start()
    {
        //To start, we want to switch to main camera and display it's menu.

        MainMenuCameraSwitch.SwitchCamera(Cameras[0]);
        ClearUI();
        ChangeUI();
    }

    private void OnEnable()
    {
        //On enable we want to add all of our cameras.
        
        for (int i = 0; i < Cameras.Length; i++)
        {
            MainMenuCameraSwitch.Register(Cameras[i]);
        }
    }

    private void OnDisable()
    {
        //On disable we want to remove all of our cameras.

        for (int i = 0; i < Cameras.Length; i++)
        {
            MainMenuCameraSwitch.Register(Cameras[i]);
        }
    }

    private void ChangeUI(int screen = 0)
    {
        //This function disables all the menus except the menu we want.

        for (int i = 0; i < Screens.Length; i++)
        {
            if (i == screen)
            {
                Screens[i].SetActive(true);
            }
            else
            {
                Screens[i].SetActive(false);
            }
        }
    }

    private void ClearUI()
    {
        //This function deactivates all menus (for transitioning between menus)

        for (int i = 0; i < Screens.Length; i++)
        {
            Screens[i].SetActive(false);
        }
    }

    public void ChangeScreen(GameObject menu)
    {
        //This function will check what menu the player wants and enable that menu.

        //Both arrays have coorsponding indexes to eachother so find the index of the menu.
        int index = Screens.findIndex(menu);
        CinemachineVirtualCamera camera = Cameras[index];
        MainMenuCameraSwitch.SwitchCamera(camera);
        ClearUI();
        StartCoroutine(BelndDelay(index));
    }

    public void StartGame()
    {
        //This function starts the game (crazy).

        SceneManager.LoadScene(1);
    }
}

public static class MainMenuExtentions
{
    /*
    *********************
    ****DOCUMNETATION****
    *********************

    * The Main Purpose of this Script is to build upon prexisting methods to fit my needs.

    */

    public static int findIndex<T>(this T[] array, T item)
    {
        //The purpose of this function is to take any generic data type (T) array and data type item (T) and return the item's index.
        return Array.IndexOf(array, item);
    }
}

