using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    GameObject Player; 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return)) 
        {
            Debug.Log("Move");
            SceneManager.LoadScene(1);
            SceneManager.UnloadScene(2);
        }
    }
}
