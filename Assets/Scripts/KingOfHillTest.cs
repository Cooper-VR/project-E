using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class KingOfHillTest : MonoBehaviour
{
    public GameObject Trigger;
    public GameObject Hill;
    private Renderer HillColor;
    public GameObject player;
    private float currentTime = 180f;
    public TMP_Text timeText;
    public bool captured= false;
    public float speed = 0.1f;
    public bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        

        if (captured)
        {
            currentTime -= 1 * Time.deltaTime;
            Debug.Log(currentTime);
        }
        else
        {
            currentTime = 180f;
            Debug.Log(currentTime);
        }
    }

    private void OnTriggerEnter(Collider Trigger)
    {
        

        if (Trigger.CompareTag("enemy"))
        {
            HillColor = Hill.GetComponent<Renderer>();
            HillColor.material.color = Color.red;
            captured = true;

        }
        else if (Trigger.CompareTag("Player"))
        {
            HillColor = Hill.GetComponent<Renderer>();
            HillColor.material.color = Color.blue;
            captured = true;

        }



    }

    private void OnTriggerExit(Collider Trigger)
    {
        HillColor = Hill.GetComponent<Renderer>();
        HillColor.material.color = Color.gray;
        captured = false;

    }
    void DisplayTime(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        string timeText = System.TimeSpan.FromSeconds(currentTime).ToString("hh':'mm':'ss");
        


    }

}
