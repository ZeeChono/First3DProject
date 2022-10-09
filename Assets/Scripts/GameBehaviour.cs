using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using UnityEngine.SceneManagement;      // for pausing and restarting the game

public class GameBehaviour : MonoBehaviour, IManager
{
    // backing variables
    private int _itemsCollected = 0;
    private int _playerHP = 10;

    private string _state;
    private Stack<string> LootStack = new Stack<string>();

    // public variables
    [SerializeField] private int MaxItems = 4;
    [SerializeField] private Text HealthText;
    [SerializeField] private Text ItemText;
    [SerializeField] private Text ProgressText;
    [SerializeField] private Button WinButton;
    [SerializeField] private Button LossButton;

    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public void Initialize()
    {
        _state = "Game Manager Initialized";
        Debug.Log(_state);

        LootStack.Push("Sword of Doom");
        LootStack.Push("HP Boost");
        LootStack.Push("Golden Key");
        LootStack.Push("Pair of Winged Boots");
        LootStack.Push("Mytheril Bracer");
        

    }

    void Start()
    {
        ItemText.text = "Items Collected: " + _itemsCollected;   // string concatenation 
        HealthText.text = "Player Health: " + _playerHP;
        Initialize();
    }

    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }

    // gets and sets
    public int Items
    {
        get { return _itemsCollected; }

        set
        {
            _itemsCollected = value;
            ItemText.text = "ItemsCollected: " + _itemsCollected;
            if(_itemsCollected >= MaxItems)
            {
                UpdateScene("You've found all the items!");
                WinButton.gameObject.SetActive(true);       // display the winning message
            }
            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemsCollected) + " more to go!";
            }

            /*Debug.LogFormat("Items: {0}", _itemsCollected);*/    
        }
    }

    public int HP
    {
        get { return _playerHP; }

        set
        {
            _playerHP = value;

            HealthText.text = "Player Health: " + HP;
            Debug.LogFormat("Lives: {0}", _playerHP);

            if (_playerHP <= 0)
            {
                UpdateScene("You want another life with that?");
                LossButton.gameObject.SetActive(true);
            }
            else
            {
                ProgressText.text = "Ouch... that's got hurt.";
            }
        }
    }

    
    public void RestartScene()      // called by the button after the game over
    {
        Utilities.RestartLevel(0);   // restart the game
    }

    public void PringLootReport()
    {
        var currentItem = LootStack.Pop();
        var nextItem = LootStack.Peek();
        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem, nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you!", LootStack.Count);
    }
}
