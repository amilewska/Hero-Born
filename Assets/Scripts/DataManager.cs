using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour, IManager      
{
    private string state;
    public string State 
    {
        get { return state; }
        set { state = value; }
    }
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        state = "Data Manager initialzied";
        Debug.Log(state);
    }

    
}
