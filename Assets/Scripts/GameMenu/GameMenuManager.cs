using System.Collections;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;

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
    public static string usernameText;
    public GameObject ErrorText;
    public Image ProfileBackground;
    public Image ProfileAvatar;
    public Image ProfileBackground2;
    public Image ProfileAvatar2;

    public GameObject[] Menus;
    [SerializeField] Sprite[] Avatars;

    int index = 0;

    void Start()
    {        
        playerData data = saveSystem.loadPlayer();

        if (data.account != "")
        { 
            new_player = false;
        }

        //When the scene starts we want to see if the player is new, if so iniate newplayer portocol if else load their save data
        ProfileAvatar.sprite = Avatars[index];
        try
        {
            ProfileName.text = data.account;
        }
        catch
        {
            ProfileName.text = "Profile";
        }
        if (new_player)
        {
            SwitchMenu(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProfileBackground2.color = ProfileBackground.color;
        ProfileAvatar2.sprite = ProfileAvatar.sprite;
    }

    private void SwitchMenu(int menu)
    {
        //This function switches the menu panels
        for (int i = 1; i < Menus.Length; i++)
        {
            if (i == menu)
            {
                Menus[i].SetActive(true);
                Debug.Log(Menus[i]);
                Debug.Log(i == menu);
                break;
            }
            else 
            { 
                Menus[i].SetActive(false);
            }
        }
    }

    public void OnSubmit()
    {
        //this function runs on submit to check if the user created a valid name and how to procceed. 

        if (CheckValidity())
        {
            Debug.Log("Register Acount");
            Menus[3].SetActive(false);
            ProfileName.text = UsernameField.text;
            usernameText = ProfileName.text;
            SwitchMenu(1);
        }
    }
    

    public bool CheckValidity()
    {
        //This function checks the length of the submitted username, if the username is too long retrun false and display error text.

        //Get the username and turn error text off.
        string Username = UsernameField.text;

        //Check username validity return the result. 
        if (Username.Length >= 15 || Username == null || Username.Length == 1)
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

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ChangeBGColor()
    {
        Color32 Color = UnityEngine.Random.ColorHSV();
        ProfileBackground.color = Color;
    }

    public void ChangeAvatar()
    {
        index += 1;
        if (index > Avatars.Length)
        {
            index = 0;
        }
        ProfileAvatar.sprite = Avatars[index];
    }
}
