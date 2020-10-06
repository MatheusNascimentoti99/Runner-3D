using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void GamePlay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void About()
    {
        SceneManager.LoadScene("About");
    }

    public void Quit()
    {
        Application.Quit();
        
    }
}
