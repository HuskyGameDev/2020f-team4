using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplayManager : MonoBehaviour
{
    private TextMeshPro mesh; //new text mesh

    void Start()
    {
        mesh = GetComponent<TextMeshPro>();
    }

    //Changes the text displayed in the healthDisplay mesh
    void ChangeDisplay(int health)
    {
        mesh.text = "Health: " + health;
    }
}
