using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float timerTick = 0; // so timer ticks as a percentage
    float currentTimerSize;

    public IEnumerator CountDown(float time, Image timerImage)
    {
        timerTick = timerImage.rectTransform.sizeDelta.x / time;
        currentTimerSize = timerImage.rectTransform.sizeDelta.x;
        Vector2 originalSize = timerImage.rectTransform.sizeDelta;
        while (time > 0)
        {
            currentTimerSize -= timerTick;
            yield return new WaitForSecondsRealtime(1);
            timerImage.rectTransform.sizeDelta = new Vector2(currentTimerSize, originalSize.y);
            time--;
        }
        yield return new WaitForSecondsRealtime(1);
        timerImage.rectTransform.sizeDelta = originalSize;
    }
}
