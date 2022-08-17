using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class StartScreen : MonoBehaviour {

public AudioClip sfxButton;

private bool oneshotSfx;
public AudioSource audioSource;

[SerializeField] Text startText;
private IEnumerator coroutine;

void Start () {
    audioSource.Play();
    coroutine = StartFlicker();
    StartCoroutine(coroutine);
}
void Update () {

    if(Input.GetKeyDown(KeyCode.Space))
    {
        StopCoroutine(coroutine);
        startText.text = "Start";

        audioSource.Stop();

        if (!oneshotSfx) {
        AudioSource.PlayClipAtPoint(sfxButton,Vector3.zero);
        Invoke("LoadScene",2f);
        oneshotSfx = true;
        }
        

    }
}

void LoadScene() 
{
    SceneManager.LoadScene("MainGame");
}

IEnumerator StartFlicker()
    {
        while(true)
        {
            startText.text = "Start";
            yield return new WaitForSeconds(0.6f);
            startText.text = "";
            yield return new WaitForSeconds(0.6f);
        }
    }

}

