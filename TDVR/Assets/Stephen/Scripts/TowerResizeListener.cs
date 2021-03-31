using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TowerResizeListener : MonoBehaviour
{
    public Transform spawnPosition = null;
    private GameObject rig = null;
    void Awake() 
    {
        // Debug.Log("Running Listener Script");
        rig = GameObject.FindWithTag("Player");
        Debug.Log("Object: " + rig.name);
        if (rig != null && spawnPosition != null) {
            Debug.Log("Setting Listener");
            GetComponent<XRSimpleInteractable>().onSelectEnter.AddListener(delegate{rig.GetComponent<ResizePlayer>().Minimize(spawnPosition);});
        }
    }

    public void MovePlayerToTower()
    {
        Debug.Log("Moving Player to Tower");
        //rig.GetComponent<ResizePlayer>().Minimize(spawnPosition);
    }
}