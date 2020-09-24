using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnTower : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject spawnedObject;
    public Transform spawnedParent;
    public float cooldownTime = 1;
    
    private Transform trans;
    private float lastPulledTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        // if object not already socketed, create object
        trans = GetComponent<Transform>();
        if (!spawnedObject)
        {
            spawnedObject = Instantiate(objectToSpawn, trans.position, trans.rotation, spawnedParent);
        }
        if (!spawnedParent)
        {
            spawnedParent = trans;
        }
    }

    void OnTriggerExit(Collider interactable)
    {
        // when a tower is removed, check if it can spawn another
        if (interactable.tag == "Tower")
        {
            spawnedObject = null;
            if (Time.fixedTime - lastPulledTime > cooldownTime)
            {
                spawnedObject = Instantiate(objectToSpawn, trans.position, trans.rotation, spawnedParent);
                lastPulledTime = Time.fixedTime;
            }
        }
    }

    private void Update()
    {
        // if an object is not spawned, check cooldown to see if it can spawn again
        if (!spawnedObject)
        {
            if (Time.fixedTime - lastPulledTime > cooldownTime)
            {
                spawnedObject = Instantiate(objectToSpawn, trans.position, trans.rotation, spawnedParent);
                lastPulledTime = Time.fixedTime;
            }
        }
    }
}
