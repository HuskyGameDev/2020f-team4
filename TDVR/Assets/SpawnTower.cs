using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnTower : XRSocketInteractor
{
    public GameObject objectToSpawn;
    public GameObject spawnedObject;
    public Transform spawnedParent;
    public float cooldownTime = 1;

    private XRSocketInteractor socket;
    private Transform trans;
    private float lastPulledTime = 0;

    // Start is called before the first frame update
    new void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
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

    new void OnTriggerExit(Collider interactable)
    {

        base.OnTriggerExit(interactable);
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
