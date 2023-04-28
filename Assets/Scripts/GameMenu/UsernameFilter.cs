using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UsernameFilter : MonoBehaviour
{
    /*
     *********************
     ****DOCUMNETATION****
     *********************
     
     * to My knowlege the language filter idea was veto'd by majority rule, But I'm still going to going to add the length filter purely due to formating consistantcy I'm also keeping the legacy code until authorized to remove it. 
     
     
     
     
     * Legacy code:
     
    public Text UsernameField;
    [SerializeField] string Username;
    [SerializeField] Text ErrorText;
    
    [SerializeField] string[] BadWords = {"Arse", "Bloody", "Bugger", "Crap", "Damn", "Git", "Goddam", "Minger", "Sod-off", "Arsehole", "Balls", "Bint", "Bitch", "Bollocks", "Bullshit", "Feck", "Munter", "Pissed", "Shit", "Tits", "Bastard", "Bellend", "Clunge", "Cock", "Dick", "Dickhead", "Fanny", "Flaps", "Minge", "Punani", "Pussy", "Snatch", "Twat", "Ass", "Cunt", "Nigga", "Nigger","Fuck", "Shit", "Wanker", "Faggot"};
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


    public TMP_Text UsernameField;
    public GameObject ErrorText;

    public void Filter()
    {
        //This function checks the length of the submitted username, if the username is too long retrun false and display error text.

        //Get the username and turn error text off.
        string Username = UsernameField.text;

        //Check username length
        bool IsUsernameValid = (Username.Length <= 10) ? true : false;

        //If it isn't valid turn on error message.
        if (!IsUsernameValid)
        {
            ErrorText.SetActive(true);
        }
        else 
        {
            ErrorText.SetActive(false);
        }

        //return the result. 
        //return IsUsernameValid;
        
    }
}
