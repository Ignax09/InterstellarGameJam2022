using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{
    public GameObject mainCamera;
    public int NextLevelID;
    public Vector2 playerDestination;
    public Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            NextLevel(NextLevelID);
        }
    }

    void NextLevel(int LevelId)
    {
        Timeline.currentLengthFloat = 0;
        mainCamera.transform.position = new Vector3(28 * LevelId, 0.5f, -10);
        playerPosition.position = playerDestination;
    }
}
