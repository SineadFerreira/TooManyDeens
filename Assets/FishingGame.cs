using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishingGame : MonoBehaviour
{
    [SerializeField] private Inventory  _inventory;
    [SerializeField] private GameObject _fishgame;
    [SerializeField] private GameObject _leftclick;
    [SerializeField] private GameObject _fishbar;
    private Image _frontBar;

    bool canfish = false;
    bool isfishing = false;
    // Start is called before the first frame update
    void Start()
    {
        _leftclick = GameObject.Find("LeftClick");
        _leftclick.SetActive(false);

        _frontBar = GameObject.Find("Bar").GetComponent<Image>();
        _fishbar.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canfish)
        {
            StartCoroutine(FishGame());

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
        _fishbar.SetActive(false);
        isfishing = false; 
    }

    //Need to fix
    private IEnumerator FishGame()
    {
        isfishing = true;
        canfish = false;
        yield return new WaitForSeconds(0.5f);
        _fishbar.SetActive(true);
        _frontBar.rectTransform.sizeDelta = new Vector2(0, 100);
        int x = 0;
       
        while(isfishing)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                x += 10;
                _frontBar.rectTransform.sizeDelta = new Vector2(x, 100);
            }
            if (x >= 100)
            {
                _frontBar.rectTransform.sizeDelta = new Vector2(100, 100);
                _fishbar.SetActive(false);

                _inventory.addfish(1); //will scale on upgrades

                yield return new WaitForSeconds(1f);
                canfish = true;
                isfishing =false; break;

                
            }
        }
   
    }


}
