using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapFollowObject : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform spawner;
    public bool showShpereCastInGizmo = false;

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
    }

    public void CreateSnapZone()
    {
        snapZone.GetComponentInChildren<MeshRenderer>().enabled = true;
        snapZone.SetActive(true);
        exists = true;
    }

    public void DestroySnapZone()
    {
        if (intersected)
        {
            transform.position = snapZone.transform.GetChild(0).position;
            transform.localRotation = snapZone.transform.rotation;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
        snapZone.GetComponentInChildren<MeshRenderer>().enabled = false;
        snapZone.SetActive(false);
        exists = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (exists)
        {
            if (Physics.SphereCast(trans.position, trans.localScale.x/2, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, layerMask) && hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Placeable Ground"))
            {
                currentHitDistance = hitInfo.distance;
                snapZone.transform.position = hitInfo.point;
                snapZone.GetComponent<Transform>().rotation = Quaternion.Euler(0, trans.rotation.eulerAngles.y, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && snapZone != null)
            if (other.gameObject.GetInstanceID() == snapZone.transform.GetChild(0).gameObject.GetInstanceID())
            {
                intersected = true;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && snapZone != null)
            if (other.gameObject.GetInstanceID() == snapZone.transform.GetChild(0).gameObject.GetInstanceID())
                intersected = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (showShpereCastInGizmo)
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(trans.position, trans.position + Vector3.down * currentHitDistance);
            Gizmos.DrawWireSphere(trans.position + Vector3.down * currentHitDistance, trans.lossyScale.x);
        }
    }
}
