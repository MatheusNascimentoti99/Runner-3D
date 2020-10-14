using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private int score = 0;
    public Text txt_score;
    public static GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        txt_score.text = "Score atual: " + score;
        PointsSystem.pointsSystem.updatePoints(score);
    }
}
