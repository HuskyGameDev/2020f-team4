﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public int speed;
    public int health = 5;
    private Currency currency;
    private int moneyGain = 0;

    public void Start() 
    {
        currency = GameObject.FindGameObjectWithTag("Currency").GetComponent<Currency>();
        moneyGain = health;
    }
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("EnemyDeayth_1"); // ENEMY DEATH SOUND
            Die();
        }

        FindObjectOfType<AudioManager>().Play("EnemyGrunt_1"); // ENEMY TAKE DAMAGE SOUND
    }

    void Die()
    {
        WaveSpawner.EnemiesAlive--;
        currency.IncreaseCurrency(moneyGain);
        Destroy(gameObject);
    }
}
