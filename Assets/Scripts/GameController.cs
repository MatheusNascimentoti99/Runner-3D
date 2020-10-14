using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private int score = 0;
    private float lengthTrack = 0f;
    private int indexBeach;
    public Text txt_score;
    public Text txt_beach;
    public Text txt_beachNext;
    public static GameController gameController;
    public string[] beaches = new string[] { "Praia de Cabuçu", "Praia de Monte Cristo", "Praia de Saubara", "Praia da Boa Viagem", "Porto da Barra", "Praia de Amaralina", "Praia de Piatã", "Praia do Forte", "Praia Vila de Santo Antônio", "Porto do Sauipe", "Massarandupió Beach", "Praia Barra do Itariri", "Praia do Saco", "Praia de Atalaia" };
    public GameObject bay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Player>().transform.position.z > bay.transform.position.z + 50)
        {
            bay.transform.position = new Vector3(0, -11, (bay.transform.position.z + 500));
        }
    }

    private void FixedUpdate()
    {
        lengthTrack = FindObjectOfType<Player>().transform.position.z;
        indexBeach = (int)lengthTrack / 500;
        txt_beach.text = "Você está na " + beaches[indexBeach];
        txt_beachNext.text = "Faltam " + (int)((500 * (indexBeach+1) - lengthTrack))/5 + "m para a " + beaches[indexBeach + 1];
        
    }
    private void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
        }
        else
            Destroy(gameObject);

    }

    public void IncrementScore()
    {
        score++;
        txt_score.text = "Pontos: " + score;       
    }

    public void DecrementScore()
    {
        score--;
        //txt_score.text = "Pontos: " + score;
    }

    public void sendScore()
    {
        PointsSystem.pointsSystem.updatePoints(score);
    }
}
