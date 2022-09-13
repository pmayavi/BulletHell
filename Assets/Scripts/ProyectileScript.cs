using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileScript : MonoBehaviour
{
    public float dmg;
    public string self;
    private Rigidbody2D rb;
    private bool rotate;
    private bool n180;
    private float angle;
    private Vector2 vel;
    private float maxAngle;

    void Start()
    {
        n180 = false;
        angle = 0;
        rb = GetComponent<Rigidbody2D>();
        vel = rb.velocity;
    }

    void Update()
    {
        if (rotate)
        {
            rotate = false;
            Invoke("rotateStyle", 0.01f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != self)
        {
            //if (collision.GetComponent<EnemyStats>() != null)
            //{
            //    collision.GetComponent<EnemyStats>().Hurt(dmg);
            //}
            if (collision.GetComponent<PlayerStats>() != null)
            {
                collision.GetComponent<PlayerStats>().Hurt(dmg);
            }
            Destroy(gameObject);
        }
    }

    public void rotateStyle()
    {
        transform.Rotate(0, 0, 1);
        rb.velocity = Quaternion.Euler(0, 0, angle) * vel;

        if (n180)
            angle += 1;
        else
            angle -= 1;

        if (angle >= maxAngle)
            n180 = false;
        else if (angle <= 0)
            n180 = true;
        //aqui se podria hacer una matemaica rara,
        //que le sume al angulo cada vez menos para que cuando
        //este lejos gire menos y se mantega la proporcion de giro
        rotate = true;
    }

    public void StartRotate(float ma)
    {
        maxAngle = ma;
        rotate = true;
    }
}
