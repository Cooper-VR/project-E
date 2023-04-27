using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameFilter : MonoBehaviour
{
    public Text UsernameField;
    [SerializeField] string Username;

    [SerializeField] string[] BadWords = {"Arse", "Bloody", "Bugger", "Crap", "Damn", "Git", "Goddam", "Minger", "Sod-off", "Arsehole", "Balls", "Bint", "Bitch", "Bollocks", "Bullshit", "Feck", "Munter", "Pissed", "Shit", "Tits", "Bastard", "Bellend", "Clunge", "Cock", "Dick", "Dickhead", "Fanny", "Flaps", "Minge", "Punani", "Pussy", "Snatch", "Twat", "Ass", "Cunt", "Nigga", "Nigger","Fuck", "Shit", "Wanker", "Faggot"};

    /*
    public bool Filter()
    { 
        Username = UsernameField.text;
        if (Username.Length > 10)
        {
            return false; 
        }

        for (int i = 0; i < BadWords.Length; i++)
        {
            if (Username.Contains(BadWords[i]))
            {
                


            }
        }
    }
    */
}
