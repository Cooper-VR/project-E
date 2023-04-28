using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class MainMenuCameraSwitch
{
    /*
    *********************
    ****DOCUMNETATION****
    *********************

    * The Main Purpose of this Script is to register and unregister cameras. This script also activates and deactivates each camera when needed.

    */

    //Initalize the camera list and set the current active camera to null.
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera ActiveCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        //This function checks the current active camerea is acitve, retrun active. This is to create a failsafe to stop other cameras from activating. 

        return camera == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        //This function sets sets the camera in it's parameter and gives it the highest priority.

        //Make camera we want the active priority.
        camera.Priority = 10;
        ActiveCamera = camera;

        //checks the camera list and deactivates all other cameras except the specified one. 
        foreach (CinemachineVirtualCamera c in cameras)
        {
            if (c != camera && c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }

    public static void Register(CinemachineVirtualCamera camera)
    {
        //This function adds a specified camera to the camera list. 

        cameras.Add(camera);
    }

    public static void Unregister(CinemachineVirtualCamera camera)
    {
        //This function removes a specified camera to the camera list. 

        cameras.Remove(camera);
    }
}
