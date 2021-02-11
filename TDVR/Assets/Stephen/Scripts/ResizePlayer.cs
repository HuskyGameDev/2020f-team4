using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePlayer : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform = null;
    [SerializeField]
    private float scale = 1f;
    private Transform homePosition;
    private Transform originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = homePosition = playerTransform;
    }

    public void Minimize(Transform newPosition) {
        Debug.Log("Minimize");
        originalPosition = playerTransform;
        playerTransform.SetPositionAndRotation(new Vector3(0f,0f,0f), playerTransform.rotation);
        playerTransform.localScale.Set(playerTransform.localScale.x * scale, playerTransform.localScale.y * scale, playerTransform.localScale.z * scale);
        playerTransform.SetPositionAndRotation(newPosition.position, newPosition.rotation);
    }

    public void Maximize() {
        Debug.Log("Maximize");
        playerTransform.SetPositionAndRotation(new Vector3(0f,0f,0f), playerTransform.rotation);
        playerTransform.localScale.Set(originalPosition.localScale.x, originalPosition.localScale.y, originalPosition.localScale.z);
        playerTransform.SetPositionAndRotation(originalPosition.position, originalPosition.rotation);
    }

    public void Home() {
        Debug.Log("Home");
        playerTransform.SetPositionAndRotation(new Vector3(0f,0f,0f), playerTransform.rotation);
        playerTransform.localScale.Set(homePosition.localScale.x, homePosition.localScale.y, homePosition.localScale.z);
        playerTransform.SetPositionAndRotation(homePosition.position, homePosition.rotation);
    }
}
