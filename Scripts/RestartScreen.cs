using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class RestartScreen : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    public Sprite[] tryAgainSprites;
    public Sprite[] leaveSprites;
    public Sprite[] frogSprites;

    public bool loop = true;
	// public bool destroyOnEnd = false;
    [SerializeField] Image tryAgainImage;
    [SerializeField] Image leaveImage;
    [SerializeField] Image frogJumpImage;
    [SerializeField] Image frogLeaveImage;

    bool leaveCount = true;
    bool tryCount = true;
    bool sfxCount = false;
    bool upPressed = true;
    bool spacePressed = false;

	private int tryAgainFrame = 0;
    private int leaveFrame = 0;
    private int frogFrame = 0;

	private int tryAgainIndex = 2;
    private int leaveIndex = 2;
    private int frogJumpIndex = 0;
    private int frogLeaveIndex = 10;


    public Text highScoreScore;
    public Text highScoreText;
    int highScore = 0;
    

    public AudioSource sfxTryAgain;
    public AudioSource sfxLeave;
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
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                sfxArrow.Play();
                upPressed = true;
            
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                sfxArrow.Play();
                upPressed = false;

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

            // reset
            leaveCount = true;
            leaveImage.sprite = leaveSprites [0];
            leaveFrame = 0;
            leaveIndex = 2;


            // smooth transition
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


        } else {
            // reset
            tryCount = true;
            tryAgainImage.sprite = tryAgainSprites [0];
            tryAgainFrame = 0;
            tryAgainIndex = 2;

            if (leaveCount) {
                leaveImage.sprite = leaveSprites [1];
            }

            leaveCount = false;

            leaveFrame ++;

            if (leaveFrame < 70) return;
            leaveImage.sprite = leaveSprites [leaveIndex];
            leaveFrame = 0;
            leaveIndex ++;

            if (leaveIndex >= 3) {
                if (loop) leaveIndex = 1;
            }
        }
    }

    void FrogAnim () {
        if (upPressed && spacePressed) {
            if (!sfxCount) {
                Invoke("PlayTryAgainSFX", .225f);
                sfxCount = true;
            }


            frogLeaveImage.GetComponent<Image>().enabled = false;

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
     else if (!upPressed && spacePressed) {
            if (!sfxCount) {
                Invoke("PlayLeaveSFX", .4f);
                sfxCount = true;
            }

        frogJumpImage.GetComponent<Image>().enabled = false;


        // Instantiate

            if (frogLeaveIndex >= 25) {
                frogFrame ++;

                if (frogFrame < 17) return;
                frogLeaveImage.GetComponent<Image>().enabled = false;
                Invoke("LoadLeaveScene", 2.5f);
            } else {
                leaveImage.sprite = leaveSprites[1];


                frogFrame ++;

                if (frogFrame < 15) return;

                if (frogLeaveIndex == 12 && frogFrame < 100) {
                    return;
                }

                if (frogLeaveIndex == 15 && frogFrame < 50) {
                    return;
                } else {
                    frogLeaveImage.sprite = frogSprites [frogLeaveIndex];
                    frogFrame = 0;
                    frogLeaveIndex ++;
                }
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
    void PlayLeaveSFX() {
        sfxLeave.Play();
    }
}





