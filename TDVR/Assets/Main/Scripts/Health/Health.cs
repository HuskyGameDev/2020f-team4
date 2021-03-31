using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public GameObject healthDisplayObject;

    public int maxHealth;   //make sure this number in the unity editor matches the text mesh of the child display manager
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void DecrementHealth()
    {
        if (!(currentHealth <= 0))
        {
            currentHealth--;
            ChangeDisplay();
        }
    }

    public void GainHealth(int amount)
    {
        int temp = currentHealth + amount;
        if(temp > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = temp;
        }
        ChangeDisplay();
    }

    private void ChangeDisplay()
    {
        healthDisplayObject.SendMessage("ChangeDisplay", currentHealth);
    }



}
