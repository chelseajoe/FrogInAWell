using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class SecondRestartScreen : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    public Sprite[] tryAgainSprites;
    public Sprite[] frogSprites;

    public bool loop = true;
    [SerializeField] Image tryAgainImage;
    [SerializeField] Image frogJumpImage;
    [SerializeField] Image frogLeaveImage;

    bool tryCount = true;
    bool sfxCount = false;
    bool upPressed = true;
    bool spacePressed = false;

	private int tryAgainFrame = 0;
    private int frogFrame = 0;

	private int tryAgainIndex = 2;
    private int frogJumpIndex = 0;


    public Text highScoreScore;
    public Text highScoreText;
    int highScore = 0;
    

    public AudioSource sfxTryAgain;
    public AudioSource sfxArrow;
    public AudioSource sfxHighScore;

    void Start()
    {
        sfxHighScore.volume = .25f;
        Application.targetFrameRate = 60;
        highScore = PlayerPrefs.GetInt("highScore", 0);
        highScoreScore.text = PlayerPrefs.GetInt("highScore", 0).ToString();

        // highscore Text
        if (PlayerPrefs.GetInt("hSAchieved", 0) == 1)
        {
            highScoreText.text = "HIgH SCORE !";
        } else {
            highScoreText.text = "HIgH SCORE";
        }
    }

    void Update () {

        if (!spacePressed) {
        //if press any key jump to gameplay scene
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                sfxArrow.Play();
                upPressed = true;
            
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                sfxArrow.Play();
                upPressed = true;
            }
            OptionAnim();

        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            loop = false;
            spacePressed = true;
        }

        FrogAnim ();

    }
        

    void OptionAnim() {

        if (upPressed) {

            if (tryCount) {
                tryAgainImage.sprite = tryAgainSprites [1];
            }

            tryCount = false;

            // image loop
            tryAgainFrame ++;

            if (tryAgainFrame < 50) return;
            tryAgainImage.sprite = tryAgainSprites [tryAgainIndex];
            tryAgainFrame = 0;
            tryAgainIndex ++;

            if (tryAgainIndex >= 3) {
                if (loop) tryAgainIndex = 1;
            }
        }

    }

    void FrogAnim () {
        if (upPressed && spacePressed) {
            if (!sfxCount) {
                Invoke("PlayTryAgainSFX", .225f);
                sfxCount = true;
            }


            frogLeaveImage.transform.position = new Vector3(74f, 0f, 0f);

            if (frogJumpIndex >= 10) {
                Invoke("LoadGameScene", 2.5f);
            } else {
                tryAgainImage.sprite = tryAgainSprites[1];
                frogFrame ++;

                if (frogFrame < 5) return;

                frogJumpImage.sprite = frogSprites [frogJumpIndex];
                frogFrame = 0;
                frogJumpIndex ++;
            }
    } 
   
}

    void LoadGameScene() 
    {
        SceneManager.LoadScene("MainGame");
    }

    void LoadLeaveScene() 
    {
        SceneManager.LoadScene("BadRestart");
    }

    void PlayTryAgainSFX() {
        sfxTryAgain.Play();
    }
}





