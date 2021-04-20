using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapFollowObject : MonoBehaviour
{
    public LayerMask layerMask;
    public string allowedLayer = "Placeable Ground";

    private GameObject snapZone;
    private bool exists = false;
    private Transform trans;
    private float currentHitDistance;
    private GameObject placedTower;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        snapZone = trans.parent.GetChild(1).gameObject;
    }

    // CreateSnapZone creates a hovering "holographic" snap zone; called whenever the object is picked up
    public void CreateSnapZone()
    {
        snapZone.GetComponentInChildren<MeshRenderer>().enabled = true;
        snapZone.SetActive(true);
        exists = true;
        GetComponent<BTurret>().canShoot = false;
    }

    // DestroySnapZone deactivates the hovering "holographic" snap zone, called whenever the object is let go
    public void DestroySnapZone()
    {
        // snap the object into position when released and kill momentum
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = snapZone.transform.GetChild(0).position;
        transform.localRotation = snapZone.transform.rotation;
        GetComponent<BTurret>().canShoot = true;
        GetComponent<Rigidbody>().WakeUp();
        // disable the snap zone
        snapZone.GetComponentInChildren<MeshRenderer>().enabled = false;
        snapZone.SetActive(false);
        if (placedTower) {
            placedTower.GetComponent<BTurret>().canShoot = false;
            FindObjectOfType<AudioManager>().Play("TowerUpgrade"); // TOWER UPGRADE SOUND
        } else {
            FindObjectOfType<AudioManager>().Play("BasicTowerPlacement"); // TOWER PLACEMENT SOUND
        }

        exists = false;
        Destroy(GetComponent<XRGrabInteractable>());
    }

    // Update is called once per frame
    void Update()
    {
        // when object is held, perform spherecast directly down to see if the surface below the object is legal to place the object onto
        if (exists)
        {
            if (Physics.SphereCast(trans.position, trans.localScale.x, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, layerMask) && hitInfo.transform.gameObject.layer == LayerMask.NameToLayer(allowedLayer))
            {
                currentHitDistance = hitInfo.distance;
                snapZone.transform.position = hitInfo.point;
                snapZone.GetComponent<Transform>().rotation = Quaternion.Euler(0, trans.rotation.eulerAngles.y, 0);
                if (hitInfo.transform.tag == "Tower")
                    placedTower = hitInfo.transform.gameObject;
                else
                    placedTower = null;
            }
            // Debug.Log(hitInfo.transform.name);
        }
    }

}
