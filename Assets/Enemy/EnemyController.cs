using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public string direction = "left"; // 방향
    public float range = 0.0f; //범위
    Vector3 startLocation;
    // Start is called before the first frame update
    void Start()
    {
        if (direction == "right")
        {
            transform.localScale = new Vector3(-1f, 0f);
        }
        startLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (range > 0.0f)
        {
            if (transform.position.x < startLocation.x - (range / 2))
            {
                direction = "right";
                transform.localScale = new Vector2(-1f, 1f);
            }
            if (transform.position.x > startLocation.x + (range / 2))
            {
                direction = "left";
                transform.localScale = new Vector2(1f, 1f);
            }
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (direction == "right")
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (direction == "right")
        {
            direction = "left";
            transform.localScale = new Vector2(1f, 1f);
        }
        else
        {
            direction = "right";
            transform.localScale = new Vector2(-1f, 1f);
        }
    }
}
