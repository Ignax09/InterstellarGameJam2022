using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineMovement : MonoBehaviour
{
    public Vector3[] placeInTime;
    public Vector3 boxExtents;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = placeInTime[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Timeline.canPlay)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

    void MoveToPoint()
    {
        transform.position = placeInTime[Timeline.currentLength];
    }
}
