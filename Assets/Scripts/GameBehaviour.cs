using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    public Button winButton;

    private int maxItems = 1;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text itemText;
    [SerializeField] private TMP_Text progressText;

    private void Start()
    {
        itemText.text += itemsCollected;
        healthText.text += playerHP;
    }


    private int itemsCollected = 0;
    private int playerHP = 10;
    public int items 
    { 
        get { return itemsCollected; } 

        set 
        { 
            itemsCollected = value;
            itemText.text = "Items Colected: " + items;

            if(itemsCollected >= maxItems)
            {
                progressText.text = "You've found all the items!";
                winButton.gameObject.SetActive(true);
            }
            else
            {
                progressText.text = "Item found, only " + (maxItems - itemsCollected) + " more!";
            }
        }
    
    }

    public int HP
    {
        get { return playerHP; }

        set 
        { 
            playerHP = value;
            healthText.text = "Player health: " + HP;
            Debug.LogFormat("Lives: {0}", playerHP);
        }
    }

  
}
