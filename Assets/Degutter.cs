using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Degutter : MonoBehaviour, Iinteract
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Timer _timer;
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private GameObject _spawnPoint;

    Image timerImage;
    float secs = 1; // will change length
    int loadStrength = 1;
    float countdownlength = 2;
    float loadSpeed = 0.2f;

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
        if(degutterStorage > 0 &&isProcessing == false && proccessedFish <10)
        {
            isProcessing = true;
            StartCoroutine(processFish());
           
        }
    }
    int proccessedFish = 0;
    private IEnumerator processFish()
    {
        while(degutterStorage > 0)
        {
            if(proccessedFish <10)
            {
                yield return StartCoroutine(_timer.CountDown(countdownlength, timerImage));
                degutterStorage--;
                proccessedFish++;

                Vector3 spawnPosition = new Vector3(0, 0, 0);
                if (proccessedFish <= 5)
                {
                    spawnPosition = _spawnPoint.transform.position + new Vector3(proccessedFish, 0, 0);
                }
                else if (proccessedFish > 5)
                {
                    spawnPosition = _spawnPoint.transform.position + new Vector3((proccessedFish-5), 0, -1);
                }

                Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                break;
            }
        }
        isProcessing = false;
    }
    private IEnumerator Unload()
    {
        while (Input.GetMouseButton(0))
        {
            if(_inventory.getfish() >= loadStrength)
            {
                for(int x = 0; x < loadStrength; x++)
                {
                    GameObject topFish = _inventory.spawnedFishStack[_inventory.spawnedFishStack.Count - 1];
                    _inventory.spawnedFishStack.RemoveAt(_inventory.spawnedFishStack.Count - 1);

                    StartCoroutine(AnimateFishToDegutter(topFish, (topFish.transform.position), (transform.position), loadSpeed));

                }         

                _inventory.removefish(loadStrength); //REMOVE X FISH
                degutterStorage += loadStrength; //add to degutter
                yield return new WaitForSeconds(secs);

            }
            else if(_inventory.getfish() > 0)
            {
               int y =  _inventory.getfish();
                for (int x = 0; x < y; x++)
                {
                    GameObject topFish = _inventory.spawnedFishStack[_inventory.spawnedFishStack.Count - 1];
                    _inventory.spawnedFishStack.RemoveAt(_inventory.spawnedFishStack.Count - 1);

                    StartCoroutine(AnimateFishToDegutter(topFish, (topFish.transform.position), (transform.position), loadSpeed));

                }
                _inventory.removefish(y); //REMOVE ALL FISH
                degutterStorage += y; //add to degutter
                yield return new WaitForSeconds(secs);
            }

        }
        yield return null;
    }

    private IEnumerator AnimateFishToDegutter(GameObject fish, Vector3 startPos, Vector3 targetPos, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Increment time based on how much time has passed
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / duration;

            // smooth
            fish.transform.position = Vector3.Lerp(startPos, targetPos, percentage);
            yield return null;
        }
        fish.transform.position = targetPos;
        yield return new WaitForSeconds(0.1f);
        Destroy(fish);
    }
}
