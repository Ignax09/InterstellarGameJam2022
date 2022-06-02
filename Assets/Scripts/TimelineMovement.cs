using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineMovement : MonoBehaviour
{
    public Vector3[] placeInTime;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = placeInTime[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Timeline.timeSkipped)
        {
            transform.position = placeInTime[Timeline.currentLength];

        }
        else if (Timeline.canPlay)
        {
            if (Timeline.currentLength < Timeline.totalLength && Timeline.currentLength > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, placeInTime[Timeline.currentLength], 1 * Time.deltaTime);
            }
            if (Timeline.currentLength > Timeline.totalLength -1)
            {
                transform.position = placeInTime[Timeline.totalLength -1];
            }
            if (Timeline.currentLength < 0)
            {
                transform.position = placeInTime[0];
            }
        }
    }
}
