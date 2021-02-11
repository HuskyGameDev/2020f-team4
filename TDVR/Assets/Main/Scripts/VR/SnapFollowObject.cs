using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapFollowObject : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform spawner;
    public bool showShpereCastInGizmo = false;
    public string allowedLayer = "Placeable Ground";
    public Collider teleportPlayerCollider = null;

    private GameObject snapZone;
    private bool exists = false;
    private bool intersected = false;
    private Transform trans;
    private float currentHitDistance;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        snapZone = transform.parent.GetChild(1).gameObject;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    // CreateSnapZone creates a hovering "holographic" snap zone, called whenever the object is picked up
    public void CreateSnapZone()
    {
        snapZone.GetComponentInChildren<MeshRenderer>().enabled = true;
        snapZone.SetActive(true);
        exists = true;
        //GetComponent<Rigidbody>().isKinematic = false;
        //GetComponent<Rigidbody>().useGravity = true;
        //GetComponent<Rigidbody>().WakeUp();
        GetComponent<Turret>().canShoot = false;
    }

    // DestroySnapZone deactivates the hovering "holographic" snap zone, called whenever the object is let go
    public void DestroySnapZone()
    {
        // snap the object into position if it is in the snapzone
        if (intersected)
        {
            transform.position = snapZone.transform.GetChild(0).position;
            transform.localRotation = snapZone.transform.rotation;
            //GetComponent<Rigidbody>().isKinematic = true;
            //GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Turret>().canShoot = true;

            // select now teleports player to tower
            if (teleportPlayerCollider != null) {
                teleportPlayerCollider.enabled = true;
                GetComponent<XRGrabInteractable>().enabled = false;    
            }
        }
        snapZone.GetComponentInChildren<MeshRenderer>().enabled = false;
        snapZone.SetActive(false);
        exists = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // when object is held, perform spherecast directly down to see if the surface below the object is legal to place the object onto
        if (exists)
        {
            if (Physics.SphereCast(trans.position, trans.localScale.x/2, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, layerMask) && hitInfo.transform.gameObject.layer == LayerMask.NameToLayer(allowedLayer))
            {
                currentHitDistance = hitInfo.distance;
                snapZone.transform.position = hitInfo.point;
                snapZone.GetComponent<Transform>().rotation = Quaternion.Euler(0, trans.rotation.eulerAngles.y, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if it intersects with the snapzone, allow snapping upon drop
        if (other != null && snapZone != null)
            if (other.gameObject.GetInstanceID() == snapZone.transform.GetChild(0).gameObject.GetInstanceID())
            {
                intersected = true;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        // if it stops intersecting with the snapzone, disallow snapping upon drop
        if (other != null && snapZone != null)
            if (other.gameObject.GetInstanceID() == snapZone.transform.GetChild(0).gameObject.GetInstanceID())
                intersected = false;
    }

    // necessary to display wireframe spherecast in editor
    // private void OnDrawGizmosSelected()
    // {
    //     if (showShpereCastInGizmo)
    //     {
    //         Gizmos.color = Color.red;
    //         Debug.DrawLine(trans.position, trans.position + Vector3.down * currentHitDistance);
    //         Gizmos.DrawWireSphere(trans.position + Vector3.down * currentHitDistance, trans.localScale.x);
    //     }
    // }
}
