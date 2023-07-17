using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public string title;
    public string description;

    // Start is called before the first frame update
    void Start()
    {
        title = "";
        description = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Consumable : MonoBehaviour {

}

public class KeyItem : MonoBehaviour {

}

public class Ingredient : MonoBehaviour {

}
