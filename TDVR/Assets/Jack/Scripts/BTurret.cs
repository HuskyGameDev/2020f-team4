﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTurret : MonoBehaviour
{
    private Transform target;

    [Header("Turret Stats")]
    public float range = 10f;
    public float fireRate = 1f;
    private float cooldown = 0f;

    [Header("SetUp/Testing")]
    public string enemyTag = "Enemy";

    public bool canShoot = false;

    public GameObject BbasicShot;
    public Transform BProjectileSpawn;
    public char TowerType; // specifies the type of tower for sound purposes
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BestTarget", 0f, 0.5f);
    }

    private void BestTarget()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in Enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < minDistance)
            {
                minDistance = enemyDistance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && minDistance <= range)
        {
            target = closestEnemy.transform;
        } 
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        if (cooldown <= 0f && canShoot)
        {
            Shoot();
            cooldown = 1f / fireRate;
        }

        cooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject shooting = (GameObject)Instantiate(BbasicShot, BProjectileSpawn.position, BProjectileSpawn.rotation);
        BasicShot shot = shooting.GetComponent<BasicShot>();

        if (shot != null)
        {
            shot.Attack(target);

            // play correct sound
            switch (TowerType) {
                case 'B':
                    FindObjectOfType<AudioManager>().Play("BasicTowerShoot");
                    break;
                case 'R':
                    FindObjectOfType<AudioManager>().Play("RapidTowerShoot");
                    break;
                case 'S':
                    FindObjectOfType<AudioManager>().Play("SniperTowerShoot");
                    break;
                default:
                    FindObjectOfType<AudioManager>().Play("BasicTowerShoot");
                    break;
            }
            // End of sound switch
        }
    }
}
