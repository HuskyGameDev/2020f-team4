using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiatingShot : BasicShot
{
    private float startTime;
    public float deleteGap = 1f;

    public override void Attack (Transform targets)
    {
        startTime = Time.time;
    }

    //Damage Enemies when they enter the Collider
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy"){
            Debug.Log("Hitting Enemy with Collider");
            base.Damage(other.transform);
        }

    }

    //Timer for when to delete the collider
    void Update()
    {
        if(Time.time - startTime >= deleteGap){
            Destroy(gameObject);
        }

    }

}
