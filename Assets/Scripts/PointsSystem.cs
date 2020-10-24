using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsSystem : MonoBehaviour
{
    public static PointsSystem pointsSystem;
    private int pontos = 0;
    public GameObject txtPoints;
    public GameObject button;
    public Text txt_points;
    public int life = 5;
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        txtPoints.SetActive(false);
        button.SetActive(false);
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "Upgrade")
        {
            txt_points.text = "Pontos para usar: " + pontos;
            button.SetActive(true);
            txtPoints.SetActive(true);
        } else
        {
            button.SetActive(false);
            txtPoints.SetActive(false);
        }
    }

    private void Awake()
    {
        upToDate();
        if (pointsSystem == null)
        {
            pointsSystem = this;
            DontDestroyOnLoad(pointsSystem);
        }
        else
            Destroy(gameObject);

    }

    public int getPontos()
    {
        if (PlayerPrefs.HasKey("Pontos"))
        {
            return PlayerPrefs.GetInt("Pontos");
        } else
        {
            return 0;
        }
    }

    public void setPontos()
    {
        PlayerPrefs.SetInt("Pontos", pontos);
    }

    public void updatePoints(int value)
    {
        this.pontos =  this.pontos + value;
        setPontos();
    }

    public void upToDate()
    {
        this.pontos = getPontos();
    }

    public void zero()
    {
        PlayerPrefs.SetInt("Pontos", 0);
    }

    public void upLife()
    {
        if (getPontos()>=10) // é pra ser 100
        {
            pontos = pontos - 10;
            setPontos();
            life++;
        }
        txt_points.text = "Pontos para usar: " + pontos;
    }

    public int getLife()
    {
        return life;
    }

    public void isGameOver()
    {
        gameOver.SetActive(true);
    }

    public void isNotGameOver()
    {
        gameOver.SetActive(false);
    }
}
