using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 0.01f;
    private float direction = -1;
    public GameObject boundsLeft;
    public GameObject boundsRight;
    public GameObject StartingPos;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2( direction * 1, 0) * Time.deltaTime * moveSpeed);
        if (gameObject.transform.position.x <= boundsLeft.transform.position.x)
        {
            direction = direction * -1;
            sprite.flipX = true;
        }

        else if (gameObject.transform.position.x >= boundsRight.transform.position.x)
        {
            direction = direction * -1;
            sprite.flipX = false;
        }
    }

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
