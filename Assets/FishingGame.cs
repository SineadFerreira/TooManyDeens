using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingGame : MonoBehaviour
{


    [SerializeField] private GameObject _fishgame;
    [SerializeField] private GameObject _leftclick;

    bool canfish = false;
    // Start is called before the first frame update
    void Start()
    {
        _leftclick = GameObject.Find("LeftClick");
        _leftclick.SetActive(false);
        Debug.Log("start");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canfish)
        {
            FishGame();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        _leftclick.SetActive(true);
        canfish = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _leftclick.SetActive(false);
        canfish=false;
    }

 

    void FishGame()
    {
        
    }

}
