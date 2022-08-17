using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sfxButton;
    [SerializeField] CursorMovement cursorMovement;


    private bool oneshotSfx;

    void Start () {
        audioSource.volume = 2f;
        audioSource.time = 1f;
        Invoke("PlayLAudio",.5f);
        }
    void Update () {

        if (audioSource.time > 4f) {
            audioSource.Stop();
        }
            Invoke("LoadScene",5f);
        }

    void LoadScene()
    {
        if (PlayerPrefs.GetInt("goodEnd", 0) == 2) {
            SceneManager.LoadScene("Restart2");
        }
        else {
            SceneManager.LoadScene("Restart");
        }
    }

    void PlayLAudio() {
        audioSource.Play();
    }
}
