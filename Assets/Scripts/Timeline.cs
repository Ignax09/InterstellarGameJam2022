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
    public static float currentLengthFloat;
    public static bool canPlay;
    public static bool timeSkipped;
    public static GameObject[] gos;
    // Start is called before the first frame update
    void Start()
    {
        
        currentLength = 0;
        currentLengthFloat = 0;
        slider.minValue = 0;
        canPlay = true;
        totalLength = 30;
        slider.maxValue = totalLength;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slider.value = Mathf.RoundToInt(currentLengthFloat);
        currentLength = Mathf.RoundToInt(currentLengthFloat);
        if (currentLengthFloat >= totalLength)
        {
            currentLengthFloat = totalLength;
        }
        if (currentLengthFloat <= 0)
        {
            currentLengthFloat = 0;
        }
        if (canPlay)
        {
            currentLengthFloat += Time.deltaTime;
            
        }
        
    }

    public void ChangeTime(int time)
    {
        currentLengthFloat += time;
        MovePlatforms();
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

    private void LateUpdate()
    {
        timeSkipped = false;
    }

    void MovePlatforms()
    {
        gos = GameObject.FindGameObjectsWithTag("MovingPlatform");

        for (var i = 0; i < gos.Length; i++)
        {
            gos[i].SendMessage("MoveToPoint");
        }
    }
}
