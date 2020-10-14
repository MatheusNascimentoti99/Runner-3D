using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public static void LoadHome()
    {
        SceneManager.LoadScene("Home");
    }

    public static void GamePlay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public static void About()
    {
        SceneManager.LoadScene("About");
    }

    public static void Upgrade()
    {
        SceneManager.LoadScene("Upgrade");
    }

    public static void Quit()
    {
        PointsSystem.pointsSystem.zero();
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
