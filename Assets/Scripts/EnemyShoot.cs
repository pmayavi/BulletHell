using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    protected GameObject playerObj = null;
    protected Vector2 direction;
    public float damage;
    protected float speed;
    public float minCooldown;
    public float maxCooldown;
    public GameObject proyectile;
    public float force;
    public int bullets;
    public float bulletSize;
    private bool canAttack;
    private bool beaming;
    private bool offset;
    public float beamSpeed;

    public virtual void Start()
    {
        canAttack = true;
        offset = true;
        if (playerObj == null)
            playerObj = GameObject.FindGameObjectWithTag("Player");
        Direction();
    }

    public virtual void Update()
    {
        if (canAttack)
        {
            canAttack = false;
            var choices = new[] { "ChargeCircle", "ChargeBeam", "ChargeCircle", "ChargeBeam" };
            Invoke(choices[Random.Range(0, 4)], 0.1f);
        }
    }

    public virtual void Direction()
    {
        if (playerObj)
        {
            Vector2 playerPos = playerObj.transform.position;
            Vector2 myPos = transform.position;
            direction = (playerPos - myPos).normalized;
        }
        //else direction = Vector2.zero;
    }

    void Stand()
    {
        Invoke("ResumeAttack", Random.Range(minCooldown, maxCooldown));
    }

    void ResumeAttack()
    {
        canAttack = true;
    }

    void ChargeCircle()
    {
        for (float c = 0; c < 4f; c += 0.3f)
        {
            Invoke("Circle", c);
        }
        Invoke("ResumeAttack", 4f);
    }

    void Circle()
    {
        float angle = 180;
        if (offset)
            offset = false;
        else
        {
            offset = true;
            angle -= (360 / (2 * bullets));
        }

        for (int i = 0; i < bullets; i++)
        {
            GameObject bullet = Instantiate(proyectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity =
                new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))
                * force;
            bullet.GetComponent<ProyectileScript>().dmg = damage;
            bullet.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);
            angle -= 360 / bullets;
        }
    }

    void ChargeBeam()
    {
        for (float c = 0; c < 3f; c += beamSpeed)
        {
            Invoke("Beam", c);
        }
        Invoke("ResumeAttack", 3f);
    }

    void Beam()
    {
        Direction();
        GameObject bullet = Instantiate(proyectile, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * force;
        bullet.GetComponent<ProyectileScript>().dmg = damage;
        bullet.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public void SetSpeed(float spd)
    {
        speed = spd;
    }
}
