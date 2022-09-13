using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private SpriteRenderer sprite;
    private float time;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Physics();
    }

    private void OnInput()
    {
        direction = Vector2.zero;
        int j = 0;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            j++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (j == 0)
            {
                direction += Vector2.left;
                j++;
            }
            else
            {
                j++;
                direction *= (1 - 1 / j);
                direction += (Vector2.left / j);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (j == 0)
            {
                direction += Vector2.down;
                j++;
            }
            else
            {
                j++;
                direction *= (1 - 1 / j);
                direction += (Vector2.down / j);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (j == 0)
            {
                direction += Vector2.right;
            }
            else
            {
                j++;
                direction *= (1 - 1 / j);
                direction += (Vector2.right / j);
            }
        }
    }

    private void Move()
    {
        float delta = Time.deltaTime;
        time -= delta;
        transform.Translate(direction * speed * delta);
    }

    void Physics()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hor, ver, 0.0f) * speed;
        GetComponent<Rigidbody2D>().MovePosition(transform.position + movement);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
