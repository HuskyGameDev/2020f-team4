using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTurret : MonoBehaviour
{
    private Transform target;

    [Header("Turret Stats")]
    public float range = 10f;
    public float fireRate = 2f;
    private float cooldown = 0f;

    [Header("SetUp/Testing")]
    public string enemyTag = "Enemy";

    public bool canShoot = false;

    public GameObject RbasicShot;
    public Transform RProjectileSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BestTarget", 0f, 0.5f);
    }

    private void BestTarget()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Debug.Log(Enemies.Length);
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
        Debug.Log(closestEnemy.name);
        Debug.Log(minDistance);
        Debug.Log(range);
        if (closestEnemy != null && minDistance <= range)
        {
            target = closestEnemy.transform;
            Debug.Log("EA");
        }
        else
        {
            Debug.Log("NULL");
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("update");
        if (target == null)
        {
            return;
        }
        if (cooldown <= 0f && canShoot)
        {
            Debug.Log("pre shot");
            Shoot();
            cooldown = 1f / fireRate;
        }

        cooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        Debug.Log("Try");
        GameObject shooting = (GameObject)Instantiate(RbasicShot, RProjectileSpawn.position, RProjectileSpawn.rotation);
        BasicShot shot = shooting.GetComponent<BasicShot>();

        if (shot != null)
        {
            Debug.Log("shoot");
            shot.Attack(target);
        }
    }
}
