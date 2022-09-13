using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public GameObject enemy;

    public GameObject healthBar;
    public Slider healthBarSlider;
    private Animator animator;
    private bool dead;
    protected float health;
    public float maxHealth;
    public float damage;
    public float speed;
    private GameObject me;

    void Start()
    {
        me = gameObject;
        dead = false;
        health = maxHealth;
        animator = GetComponent<Animator>();
        if (GetComponent<EnemyShoot>())
        {
            GetComponent<EnemyShoot>().SetDamage(damage);
            GetComponent<EnemyShoot>().SetSpeed(speed);
        }
    }

    void Update()
    {
        if (dead && animator.GetBool("death") == false)
        {
            Destroy(gameObject);
        }
    }

    public void Hurt(float dmg)
    {
        animator.SetBool("Damage", true);
        healthBar.SetActive(true);
        health -= dmg;
        Invoke("Damage", 0.25f);
        SliderPercentage();
    }

    public void Damage()
    {
        animator.SetBool("Damage", false);
        if (health <= 0)
            Death();
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
        SliderPercentage();
    }

    void Death()
    {
        animator.SetBool("death", true);
        dead = true;
    }

    private void SliderPercentage()
    {
        healthBarSlider.value = (health / maxHealth);
    }
}
