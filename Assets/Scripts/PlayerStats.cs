using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    static public PlayerStats stats;

    protected float health;
    public float maxHealth,
        speed;
    public float proyectileDamage,
        proyectileSpeed,
        proyectileSize;

    void Awake()
    {
        if (stats != null)
            Destroy(stats);
        else
            stats = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        health = maxHealth;
        GetComponent<PlayerMovement>().SetSpeed(speed);
        SetBulletStats();
        Physics2D.IgnoreLayerCollision(10, 11, true);
    }

    void Update() { }

    public void Hurt(float dmg)
    {
        health -= dmg;
        Death();
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
        Death();
    }

    public void Death()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

    public void SetSpeed(float spd)
    {
        speed *= spd;
        //if (speed < 1) speed = 1;
        GetComponent<PlayerMovement>()
            .SetSpeed(speed);
    }

    public void SetProyectileDamage(float dmg)
    {
        proyectileDamage *= dmg;
        //if (proyectileDamage < 1) proyectileDamage = 1;
        SetBulletStats();
    }

    public void SetProyectileSpeed(float spd)
    {
        proyectileSpeed *= spd;
        //if (proyectileSpeed < 1) proyectileSpeed = 1;
        SetBulletStats();
    }

    public void SetProyectileSize(float size)
    {
        proyectileSize *= size;
        //if (proyectileSize < 0.05f) proyectileSize = 0.05f;
        SetBulletStats();
    }

    void SetBulletStats()
    {
        GetComponent<SpellScript>().SetStats(proyectileDamage, proyectileSpeed, proyectileSize);
    }
}
