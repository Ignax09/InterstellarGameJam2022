using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    public Slider slider;
    public static int totalLength;
    static public int currentLength;
    public float currentLengthFloat;
    public static bool canPlay;
    public static bool timeSkipped;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = totalLength;
        currentLength = 0;
        canPlay = true;
        totalLength = 30;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slider.value = currentLength;
        currentLength = Mathf.RoundToInt(currentLengthFloat);
        if (currentLengthFloat >= totalLength)
        {
            currentLengthFloat = totalLength;
        }
        if (currentLengthFloat <= 0)
        {
            currentLengthFloat = 0;
        }
        if (timeSkipped)
        {
            Invoke("ClearTimeSkip", 0.01f);
        }
        if (canPlay)
        {
            currentLengthFloat += Time.deltaTime;
            
        }
        
    }

    public void ChangeTime(int time)
    {
        currentLengthFloat += time;
        timeSkipped = true;
    }

    public void StopTime()
    {
        canPlay = false;
    }

    public void PlayTime()
    {
        canPlay = true;
    }

    public void ClearTimeSkip()
    {
        timeSkipped = false;
    }
}
