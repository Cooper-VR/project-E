using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfHillTest : MonoBehaviour
{
    public GameObject Trigger;
    public GameObject Hill;
    private Renderer HillColor;
    public GameObject player;
    private float currentTime = 180f; 
    public bool captured = false;
    public bool blueTeam = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        
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
            currentTime -= 1 * Time.deltaTime;
            captured = true;
            blueTeam = true;


        }


    }

    private void OnTriggerExit(Collider Trigger)
    {
        HillColor = Hill.GetComponent<Renderer>();
        HillColor.material.color = Color.gray;
        


    }

}
