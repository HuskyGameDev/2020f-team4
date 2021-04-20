using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnTower : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject spawnedObject;
    public Transform spawnedParent;
    public Currency balance;
    public float cooldownTime = 1;
    public int buyCost = 100;
    
    private Transform trans;
    private float lastPulledTime = 0;
    Renderer[] rs;

    // Start is called before the first frame update
    void Start()
    {
        // if object not already socketed, create object
        trans = GetComponent<Transform>();
        if (!spawnedObject)
        {
            //spawnedObject = Instantiate(objectToSpawn, trans.position, trans.rotation, spawnedParent);
        }
        if (!spawnedParent)
        {
            spawnedParent = trans;
        }
        rs = GetComponentsInChildren<Renderer>();
    }

    void OnTriggerExit(Collider interactable)
    {
        // when a tower is removed, reset spawn timer
        if (interactable.tag == "Tower" && spawnedObject != null)
        {
            spawnedObject = null;
            /*if (Time.fixedTime - lastPulledTime > cooldownTime)
            {
                spawnedObject = Instantiate(objectToSpawn, trans.position, trans.rotation, spawnedParent);
                lastPulledTime = Time.fixedTime;
            }*/
            // testing
            lastPulledTime = Time.fixedTime;
            Debug.Log("Buying Tower");
            balance.SpendCurrency(buyCost);
            foreach(Renderer r in rs)
                r.enabled = true;
        }
    }

    private void Update()
    {
        // if an object is not spawned, check cooldown to see if it can spawn again
        if (!spawnedObject)
        {
            if ((Time.fixedTime - lastPulledTime > cooldownTime) && (balance.availableCurrency >= buyCost))
            {
                spawnedObject = Instantiate(objectToSpawn, trans.position, trans.rotation, spawnedParent);
                lastPulledTime = Time.fixedTime;
                foreach(Renderer r in rs)
                    r.enabled = false;
            } 
        } else if (balance.availableCurrency < buyCost) {
            Destroy(spawnedObject);
            foreach(Renderer r in rs)
                r.enabled = true;
        }
    }
}
