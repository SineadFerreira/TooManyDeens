using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Degutter : MonoBehaviour, Iinteract
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Timer _timer;
    Image timerImage;
    float secs = 1; // will change length
    int loadStrength = 1;
    float countdownlength = 10;

    bool isProcessing = false;
    int degutterStorage = 0; //how many fish are in the degutter
    public void interact()
    {
        if(_inventory.getfish() >0)
        {
            StartCoroutine(Unload());
        }
    }

    void Start()
    {
        timerImage = transform.Find("DegutterCanvas").Find("Timer").GetComponent<Image>();
    }

    void Update()
    {
        if(degutterStorage > 0 &&isProcessing == false )
        {
            isProcessing = true;
            Debug.Log("degutter inv is " + degutterStorage);
            StartCoroutine(processFish());
           
        }
    }

    private IEnumerator processFish()
    {
        while(degutterStorage > 0)
        {
            yield return StartCoroutine(_timer.CountDown(countdownlength, timerImage));
            degutterStorage--;
        }
        isProcessing = false;
    }
    private IEnumerator Unload()
    {
        while (Input.GetMouseButton(0))
        {
            if(_inventory.getfish() >= loadStrength)
            {
                _inventory.removefish(loadStrength); //REMOVE X FISH
                degutterStorage += loadStrength; //add to degutter
                yield return new WaitForSeconds(secs);

            }
            else if(_inventory.getfish() > 0)
            {
                _inventory.removefish(_inventory.getfish()); //REMOVE ALL FISH
                degutterStorage += _inventory.getfish(); //add to degutter
                yield return new WaitForSeconds(secs);
            }

        }
    }
}
