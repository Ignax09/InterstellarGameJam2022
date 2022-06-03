using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool panelActive;
    // Start is called before the first frame update
    void Start()
    {
        panelActive = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeState()
    {
        if (panelActive)
        {
            panelActive = false;
            gameObject.SetActive(false);
        }
        else
        {
            panelActive = true;
            gameObject.SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndGame()
    {
        Debug.Log("game ended");
        Application.Quit();
    }
}
