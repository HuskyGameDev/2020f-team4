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
