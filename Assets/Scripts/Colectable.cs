using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectable : MonoBehaviour
{
    public string name;
}

public class Potion : Colectable
{
    public Potion()
    {
        this.name = "Potion";
    }
}

public class Antidote : Colectable
{
    public Antidote()
    {
        this.name = "Antidote";
    }
}
