using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : MonoBehaviour
{
    [Header("Variables (WIP)")]
    private Transform target;
    public float speed = 100f;
    public int damage = 1;

    public void Attack (Transform targets)
    {
        target = targets;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float traveled = speed * Time.deltaTime;

        if(direction.magnitude <= traveled)
        {
            Hit();
            return;
        }

        transform.Translate(direction.normalized * traveled, Space.World);

    }

    void Hit()
    {
        Destroy(gameObject);
        Damage(target);
    }

    void Damage(Transform enemy)
    {
        EnemyStat e = enemy.GetComponent<EnemyStat>();

        if ( e!= null)
        {
            e.TakeDamage(damage);
        }
      
    }
}
