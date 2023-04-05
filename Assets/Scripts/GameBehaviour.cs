using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomExtension;
using System.Linq;

public class GameBehaviour : MonoBehaviour, IManager
{
    public Stack<Loot> LootStack = new Stack<Loot>();

    public Button lossButton;
    public Button winButton;

    private readonly int maxItems = 4;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text itemText;
    [SerializeField] private TMP_Text progressText;

    private string state;
    public string State 
    {
        get { return state; }
        set { state = value; }
    }
    public void Initialize()
    {
        state = "Game Manager initialized...";
        state.FancyDebug();
        Debug.Log(state);

        
        
        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("HP Boost",1));
        LootStack.Push(new Loot("Golden Key",3));
        LootStack.Push(new Loot("Pair of Winged Boots", 4));
        LootStack.Push(new Loot("Mythril Bracer",2));

        FilterLoot();

    }

    public void PrintLootReport()
    {
        var currentItem = LootStack.Pop();
        var nextItem = LootStack.Peek();

        Debug.LogFormat("You got {0}! Youve gota good chance of finding a {1} next", currentItem.name, nextItem.name);

        Debug.LogFormat("There are {0} random loot items waiting for you!", LootStack.Count);
    }

    public void FilterLoot()
    {

        var rareLoot = LootStack.Where(LootPredicate);

        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item: {0}!", item.name);
        }
    }
    public bool LootPredicate(Loot loot)
    {
        return loot.rarity >= 3;
    }

    private void Start()
    {
        itemText.text += itemsCollected;
        healthText.text += playerHP;

        Initialize();

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
                UpdateScene("You've found all the items!");
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

            if(playerHP <= 0)
            {
                UpdateScene("You want another life with that?");
                lossButton.gameObject.SetActive(true);
            }
        }
    }

   
    public void RestartScene()
    {
        Utilities.RestartLevel(0);
            
    }

    public void UpdateScene(string updatedText)
    {
        progressText.text = updatedText;
        Time.timeScale = 0f;
    }

    
}
