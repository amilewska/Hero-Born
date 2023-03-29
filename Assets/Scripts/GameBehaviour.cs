using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    


    private int itemsCollected = 0;
    private int playerHP = 10;
    public int items 
    { 
        get { return itemsCollected; } 

        set 
        { 
            itemsCollected = value;
            Debug.LogFormat("Items: {0}", itemsCollected);
        }
    
    }

    public int HP
    {
        get { return playerHP; }

        set 
        { 
            playerHP = value;
            Debug.LogFormat("Lives: {0}", playerHP);
        }
    }

  
}
