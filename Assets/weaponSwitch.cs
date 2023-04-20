using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitch : MonoBehaviour
{
    public GameObject ak47;
    public GameObject miniGun;

    public GameObject[] loadout = new GameObject[2];
    private int currentIndex = 0;

    private void Start()
    {
        WeaponSpawning(loadout[currentIndex % 2]);
    }
    private void Update()
    {
        if (Input.mouseScrollDelta.y == 1)
        {
            Debug.Log(currentIndex);
            loadout[currentIndex % 2].SetActive(false);
            currentIndex++;
            WeaponSpawning(loadout[currentIndex % 2]);

        }
    }

    void WeaponSpawning(GameObject weapon)
    {
        GameObject gun = GameObject.Instantiate(weapon, transform.position, transform.rotation);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.SetParent(transform, false);
    }
}
