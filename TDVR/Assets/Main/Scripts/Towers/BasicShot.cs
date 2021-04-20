using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : MonoBehaviour
{
    [Header("Variables (WIP)")]
    private Transform target;
    public float speed = 100f;
    public int damage = 1;
    Vector3 direction;
    float traveled;

    public virtual void Attack (Transform targets)
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

        direction = target.position - transform.position;
        traveled = speed * Time.deltaTime;

        transform.Translate(direction.normalized * traveled, Space.World);

    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            Hit();
        } else {
            Destroy(gameObject);
        }
    }

    void Hit()
    {
        Damage(target);
        Destroy(gameObject);
    }

    protected void Damage(Transform enemy)
    {
        EnemyStat e = enemy.GetComponent<EnemyStat>();

        if ( e!= null)
        {
            e.TakeDamage(damage);
        }
      
    }
}
