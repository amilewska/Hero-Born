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
    public PlayerBehaviour playerBehaviour;
    private int itemsCollected = 0;
    private int playerHP = 1;
    public int items
    {
        get { return itemsCollected; }

        set
        {
            itemsCollected = value;
            itemText.text = "Items Colected: " + items;

            if (itemsCollected >= maxItems)
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

            if (playerHP <= 0)
            {
                UpdateScene("You want another life with that?");
                lossButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        GameObject player = GameObject.Find("Player");

        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        playerBehaviour.playerJump += HandlePlayerJump;
        debug("Jump event subscribed");
    }
    private void OnDisable()
    {
        playerBehaviour.playerJump -= HandlePlayerJump;
        debug("Jump event unsubscribed");
    }
    

    private void HandlePlayerJump()
    {
        debug("Player hsa jumped");
    }

    public delegate void DebugDelegate(string newText);

    public DebugDelegate debug = Prints;

    public static void Prints(string newText)
    {
        Debug.Log(newText);
    }

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
        debug(state);
        LogWithDelegate(debug);

        var itemShop = new Shop<Colectable>();

        itemShop.AddItem(new Potion());
        itemShop.AddItem(new Antidote());

        Debug.Log("Items for sale: " + itemShop.GetStockCount<Potion>());

        //FilterLoot();

    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
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
        var rareLoot = from item in LootStack
                       where item.rarity >= 3
                       orderby item.rarity
                       select item;


        /*var rareLoot = LootStack
            .Where(item => item.rarity >=3)
            .OrderBy(item => item.rarity)
            .Select(item => new
            {
                item.name
            });*/

        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item: {0}!", item.name);
        }
    }
   

    private void Start()
    {
        itemText.text += itemsCollected;
        healthText.text += playerHP;

        Initialize();

    }
    

    

   
    public void RestartScene()
    {
        try
        {
            Utilities.RestartLevel(-1);
        }

        catch (System.ArgumentException exception)
        {
            Utilities.RestartLevel(0);
            debug("reverting to scene 0: " + exception.ToString());
        }
        finally
        {
            debug("Level restart has completed");
        }
            
    }

    public void UpdateScene(string updatedText)
    {
        progressText.text = updatedText;
        Time.timeScale = 0f;
    }

    
}
