using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapFollowObject : MonoBehaviour
{
    public GameObject snapZonePrefab;
    public float destroyDelay = 1f;
    public LayerMask layerMask;
    public Transform spawner;

    private GameObject snapZone;
    private bool exists = false;
    private Transform trans;
    private Vector3 rotationVector;
    private float currentHitDistance;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        rotationVector = new Vector3(0, 1, 0);
    }

    public void CreateSnapZone()
    {
        if (!snapZone)
            snapZone = Instantiate(snapZonePrefab, trans.position, trans.rotation);
        else
        {
            snapZone.GetComponentInChildren<MeshRenderer>().enabled = true;
            snapZone.SetActive(true);
        }
        exists = true;
    }

    public void DestroySnapZone()
    {
        if (snapZone)
        {
            snapZone.GetComponentInChildren<MeshRenderer>().enabled = false;
            StartCoroutine(DestroyHelper());
        }
        exists = false;
    }

    private IEnumerator DestroyHelper()
    {
        yield return new WaitForSeconds(destroyDelay);
        if (!exists)
            snapZone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (exists == true && snapZone && snapZone.activeInHierarchy && Vector3.Distance(trans.position, spawner.position) > .1f)
        {
            if (Physics.SphereCast(trans.position, trans.lossyScale.x, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, layerMask) && hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Placeable Ground"))
            {
                currentHitDistance = hitInfo.distance;
                Debug.Log(hitInfo.point);
                snapZone.transform.position = hitInfo.point;//new Vector3(trans.position.x, 0.1f, trans.position.z);
                snapZone.transform.rotation = Quaternion.Euler(0, trans.rotation.eulerAngles.y, 0);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(trans.position, trans.position + Vector3.down * currentHitDistance);
        Gizmos.DrawWireSphere(trans.position + Vector3.down * currentHitDistance, trans.lossyScale.x);
    }
}
