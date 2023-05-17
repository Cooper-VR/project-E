using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject sword;
    public GameObject swordCollider;
    public GameObject bulletDeflector;
    public float attackSpeed = 1f;
    public int damage = 10;
    private bool isAttacking = false;
    private bool isBlocking = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking && !isBlocking)
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttacking && !isBlocking)
        {
            StartCoroutine(Block());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        swordCollider.SetActive(true);

        // Store the initial x rotation value
        float initialXRotation = sword.transform.localRotation.eulerAngles.x;

        // Rotate the sword from right to left and left to right
        for (float t = 0; t < 1; t += Time.deltaTime * attackSpeed)
        {
            sword.transform.localRotation = Quaternion.Euler(initialXRotation, Mathf.Lerp(45, -45, t), 0);
            yield return null;
        }

        for (float t = 0; t < 1; t += Time.deltaTime * attackSpeed)
        {
            sword.transform.localRotation = Quaternion.Euler(initialXRotation, Mathf.Lerp(-45, 45, t), 0);
            yield return null;
        }

        swordCollider.SetActive(false);
        isAttacking = false;
    }

    IEnumerator Block()
    {
        isBlocking = true;
        bulletDeflector.SetActive(true);

        // Position the sword in front of the player
        sword.transform.localPosition = new Vector3(0, 0, 1);

        // Wait for the block duration
        yield return new WaitForSeconds(1f);

        bulletDeflector.SetActive(false);
        sword.transform.localPosition = Vector3.zero;
        isBlocking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.CompareTag("enemy"))
        {
            // Deal damage to the enemy
            other.GetComponent<enemyController>().health -= damage;
        }
    }
}


