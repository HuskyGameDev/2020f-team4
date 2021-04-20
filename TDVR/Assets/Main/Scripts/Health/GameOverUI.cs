using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{

    private GameObject GO;
    private Canvas menu;

    // Start is called before the first frame update
    void Start()
    {
        GO = GameObject.Find("Game Over");
        menu = GameObject.Find("Game Over Menu").GetComponent<Canvas>();
        menu.enabled = false;
        GO.SetActive(false);
    }

    void Update()
    {
    }

    public void makeVisible()   //Makes the GameOver UI box visible and game over button in front of the player after a gameover.
    {
        GO.SetActive(true); //game over text
        menu.enabled = true; //make quit button visible
    }

}
