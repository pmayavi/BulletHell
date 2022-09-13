using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMother : MonoBehaviour
{
    public int bullets;
    public GameObject proyectile;
    public float damage;
    public float force;
    public float bulletSize;
    public float maxAngle;
    private Rigidbody2D rb;
    private float rotation;
    public float cooldown;

    void Start()
    {
        rotation = 0;
        rb = GetComponent<Rigidbody2D>();
        Invoke("Circle", cooldown);
    }

    void Update() { }

    void Circle()
    {
        rotation += 3.6f;
        if (rotation == bullets * 3.6f)
            rotation = 0;
        float angle = 180 - rotation;
        for (int i = 0; i < bullets; i++)
        {
            GameObject bullet = Instantiate(proyectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity =
                new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))
                * force;
            bullet.GetComponent<ProyectileScript>().dmg = damage;
            bullet.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);
            bullet.GetComponent<ProyectileScript>().StartRotate(maxAngle);
            angle -= 360 / bullets;
        }
        Invoke("Circle", cooldown);
    }
}
