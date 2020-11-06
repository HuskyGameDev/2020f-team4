using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : MonoBehaviour
{
    [Header("Variables (WIP)")]
    private Transform target;
    public float speed = 100f;
    public float damage = 100f;

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
        Destroy(target.gameObject);
    }
}
