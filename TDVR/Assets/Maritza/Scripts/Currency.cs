using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public int availableCurrency = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IncrementCurrency()
    {
        availableCurrency++;
    }

    public void SpendCurrency(int cost)
    {
        availableCurrency = availableCurrency - cost;
    }

    public void IncreaseCurrency(int gain)
    {
        availableCurrency = availableCurrency + gain;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
