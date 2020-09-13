using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapFollowObject : MonoBehaviour
{
    public GameObject snapZonePrefab;
    public float destroyDelay = 1f;

    private GameObject snapZone;
    private bool exists = false;
    private Transform trans;
    private Vector3 rotationVector;

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
        if (exists == true && snapZone && snapZone.activeInHierarchy)
        {
            snapZone.transform.position = new Vector3(trans.position.x, 0.1f, trans.position.z);
            snapZone.transform.rotation = Quaternion.Euler(0,trans.rotation.eulerAngles.y,0);
        }
    }
}
