using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public AudioClip portalSound;
    AudioSource soundSource;

    private void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider2D PlayerCollider = PlayerCharacter.GetComponent<Collider2D>();
        if (other.gameObject.tag== "Player")
        {
            soundSource.PlayOneShot(portalSound);
            Debug.Log("Level Load Trigger"); 
            SceneManager.LoadScene(4); 
        }
    } 

} 