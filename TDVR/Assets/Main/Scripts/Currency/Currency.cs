using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Last change MCG Sept 17
//This class is held by the object CurrencyController, Child to CurrencyBox.
public class Currency : MonoBehaviour
{
    public int availableCurrency = 0; //Instance Variable to keep track of current currency

    public GameObject currencyDisplayObject;   

    private float curTime;
    
    void Start()
    {
        curTime = Time.fixedTime;
        if(currencyDisplayObject == null)
        {
            currencyDisplayObject = GameObject.Find("CurrencyDisplay"); //Will find the object in scene named CurrencyDisplay
        }
    }

    //returns value of instance variable availableCurrency
    public int GetCurrency()
    {
        return availableCurrency;
    }

    //Increments currency by one. Calls UpdateCurrency
    public void IncrementCurrency()
    {
        availableCurrency++;
        UpdateCurrency();
    }

    //Decrements currency by one. Calls UpdateCurrency
    public void DecrementCurrency()
    {
        availableCurrency--;
        UpdateCurrency();
    }

    //Decreases currency by parameter int. Calls UpdateCurrency
    public void SpendCurrency(int cost)
    {
        availableCurrency = availableCurrency - cost;
        UpdateCurrency();
    }

    //Increases currency by parameter int. Calls UpdateCurrency
    public void IncreaseCurrency(int gain)
    {
        availableCurrency = availableCurrency + gain;
        UpdateCurrency();
        
    }

    //Increases currency by multiplying by parameter int. Calls UpdateCurrency
    public void MultiplyCurrency(int factor)
    {
        availableCurrency = availableCurrency * factor;
        UpdateCurrency();
    }

    //Decreases currency by dividing by parameter double. Calls UpdateCurrency
    public void DivideCurrency(double factor)
    {
        double dummy = availableCurrency / factor;
        availableCurrency = (int)dummy;     //Watch for trunkation!
        UpdateCurrency();
    }

    //Sends message to CurrencyDisplay object. CurrencyDisplay has a script named CurrencyDisplayManager. CurrencyDisplayManager has a method named ChangeDisplay.
    public void UpdateCurrency()
    {
        currencyDisplayObject.SendMessage("ChangeDisplay", availableCurrency); //Changes text on CurrencyDisplay to match current currency
    }

    // void Update()
    // {
    //     if (Time.fixedTime - curTime >= 1f) {
    //         curTime = Time.fixedTime;
    //         IncreaseCurrency(100);
    //     }
    // }
}
