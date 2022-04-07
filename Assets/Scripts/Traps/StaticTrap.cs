using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTrap : MonoBehaviour
{
    public GameObject StartingPos;
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject PlayerCharachter = GameObject.FindGameObjectWithTag("Player");
        Collider2D PlayerCollider = PlayerCharachter.GetComponent<Collider2D>();

        if (other == PlayerCollider)
        {
            PlayerCharachter.transform.position = StartingPos.transform.position;
        }
    }
}
