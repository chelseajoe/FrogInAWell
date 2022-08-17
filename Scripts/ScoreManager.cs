using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    // public Text highScoreText;
    public int score = 0;
    int highScore = 0;
    int hSAchieved = 0;
    
    
    [SerializeField] PlayerMovement player;

    float lastUpdate;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("highScore", 0);
        PlayerPrefs.GetInt("hSAchieved", 0);

    }

    void Update()
    {
        if(!player.isAlive)
        {
            scoreText.text = score.ToString();
            return;
        }

        if(Time.time - lastUpdate >= .5f){
            score += 1;
            scoreText.text = score.ToString();
            lastUpdate = Time.time;
        }

        if (highScore < score)
        {
            PlayerPrefs.SetInt("hSAchieved", 1);
            PlayerPrefs.SetInt("highScore", score);
        } else {
            PlayerPrefs.SetInt("hSAchieved", 0);
        }
    }
}
