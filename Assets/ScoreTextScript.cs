using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextScript : MonoBehaviour
{
    public GameObject ScoreText;
    public GameObject GameManager;
    public int Score = 666;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        getScore();
        ScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getScore()
    {
       Score = GameManager.GetComponent<GameManagerScript>().getScore();
    }
}
