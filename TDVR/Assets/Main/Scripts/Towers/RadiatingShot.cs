using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiatingShot : BasicShot
{
    private float startTime;
    public float deleteGap = 1f;
    private List<Transform> enemies;
    public GameObject particle;

    public override void Attack (Transform targets)
    {
        enemies = new List<Transform>();
        startTime = Time.time;
    }

    //Damage Enemies when they enter the Collider
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy"){
            enemies.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy"){
            enemies.Remove(other.transform);
        }
    }

    //Timer for when to delete the collider
    void Update()
    {
        if(Time.time - startTime >= deleteGap){
            foreach (Transform enemy in enemies) {
                if (enemy)
                    base.Damage(enemy);
            }
            GameObject p = Instantiate(particle, transform.position, transform.rotation);
            Destroy(p, 5);
            Destroy(gameObject);
        }

    }

}
