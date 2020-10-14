using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsSystem : MonoBehaviour
{
    public static PointsSystem pointsSystem;
    private int pontos = 0;
    public GameObject txtPoints;
    public Text txt_points;

    // Start is called before the first frame update
    void Start()
    {
        txtPoints.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "Upgrade")
        {
            txt_points.text = "Pontos para usar: " + pontos;
            txtPoints.SetActive(true);
        } else
        {
            txtPoints.SetActive(false);
        }
    }

    private void Awake()
    {
        if (pointsSystem == null)
        {
            pointsSystem = this;
            DontDestroyOnLoad(pointsSystem);
        }
        else
            Destroy(gameObject);

    }

    public void getPontos()
    {
        if (PlayerPrefs.HasKey("Pontos"))
        {
            pontos = PlayerPrefs.GetInt("Pontos");
        } else
        {
            pontos = 0;
        }
    }

    public void setPontos()
    {
        PlayerPrefs.SetInt("Pontos", pontos);
    }

    public void updatePoints(int value)
    {
        this.pontos = value;
        setPontos();
    }
}
