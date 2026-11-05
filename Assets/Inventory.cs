using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int fish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addfish(int addfish)
    {
        fish += addfish;
        Debug.Log("Added " + addfish + " fish");
    }
    public void removefish(int removefish) 
    { 
        fish -= removefish;
        Debug.Log("Removed " + removefish + " fish");
    }
    public int getfish()
    {
        return fish;
    }

}
