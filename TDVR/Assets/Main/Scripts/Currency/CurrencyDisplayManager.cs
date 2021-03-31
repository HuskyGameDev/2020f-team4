using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Last change MCG Sept 17
//This class is held by the CurrencyDisplay, Child of the CurrencyBox
public class CurrencyDisplayManager : MonoBehaviour
{

    private TextMeshPro mesh; //new text mesh

    void Start()
    {
        mesh = GetComponent<TextMeshPro>(); 
    }

    //Changes the text displayed in the CurrencyDisplay mesh
    void ChangeDisplay(int currency)
    {
       mesh.text = "$" + currency.ToString();
    }

}
