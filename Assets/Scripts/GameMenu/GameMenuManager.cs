using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    /*
    *********************
    ****DOCUMNETATION****
    *********************

    * The Main Purpose of this Script is to manage if the player is new and manage all functions for navagating the game menu.

    */

    //For the sake of testing the Player is automatically set to "new" status and the username is not saved. Both of these will be accounted for once implemented with the save system. 
    public bool new_player = true;
    //Get the rest of variables nessisary for script.
    public TMP_Text UsernameField;
    public TMP_Text ProfileName;
    public GameObject ErrorText;

    public GameObject[] Menus;

    void Start()
    {
        try
        {
            ProfileName.text = "Profile";
        }
        catch
        {
            ProfileName.text = "Profile";
        }
        if (new_player)
        {
            for (int i = 0; i < Menus.Length; i++)
            {
                Debug.Log(Menus[i].ToString());
                if (Menus[i].ToString() == "NewPlayer" || i == 3 || Menus[i].ToString() == "Background" || i == 0)
                {
                    
                    Menus[i].SetActive(true);
                }
                else
                {
                    Menus[i].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchMenu(string menu)
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            Debug.Log(Menus[i].ToString());
            if (Menus[i].ToString() == "NewPlayer")
            {

                Menus[i].SetActive(true);
            }
            else
            {
                Menus[i].SetActive(false);
            }
        }
    }

    public void OnSubmit()
    {
        if (CheckValidity())
        {
            Debug.Log("Register Acount");
            Menus[3].SetActive(false);
            ProfileName.text = UsernameField.text;
        }
    }
    

    public bool CheckValidity()
    {
        //This function checks the length of the submitted username, if the username is too long retrun false and display error text.

        //Get the username and turn error text off.
        string Username = UsernameField.text;

        //Check username validity return the result. 
        if (Username.Length >= 10 || Username == null || Username.Length == 1)
        {
            ErrorText.SetActive(true);
            return false;
        }
        else
        {
            ErrorText.SetActive(false);
            return true;
        }
    }
}
