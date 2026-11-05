using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int fish;
    [SerializeField] TextMeshProUGUI fishNum;
    public List<GameObject> spawnedFishStack = new List<GameObject>();
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
        fishNum.text = fish.ToString();

    }
    public void removefish(int removefish) 
    { 
        fish -= removefish;
        fishNum.text = fish.ToString();
    }
    public int getfish()
    {
        return fish;
    }

}
