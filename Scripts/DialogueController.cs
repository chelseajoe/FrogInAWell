using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public AudioSource sfxArrow;
    public AudioSource sfxMeetFrog;
    public AudioSource sfxDialogueNext;
    public AudioSource sfxError;


    GameObject noClone;
    GameObject yesClone;
    GameObject textboxClone;
    public Text dialogueText;
    public string[] sentences;
    int index = 0;
    float dialogueSpeed = .1f;
    int count = 0;
    public Canvas canvas;
    [SerializeField] CursorMovement cursor;
    [SerializeField] GameObject cursorObject;
    [SerializeField] GameObject yesObject;
    [SerializeField] GameObject noObject;
    [SerializeField] GameObject textBoxObject;
    [SerializeField] GameObject leftCSColliderObject;
    [SerializeField] Animator animator;
    [SerializeField] GameObject noScreenObject;
    Animator yesAnimator;
    Animator noAnimator;
    public bool endCutscene = false;
    bool createBox = false;
    bool turnLeft = false;
    bool sentenceEnd = false;
    public bool yes = false;
    bool spacePressed = false;
    bool upPressed = true;
    bool instantiateOnce = false;
    bool optionStart = false;
    
    void Start()
    {
        canvas.enabled = false;
        sentences[0] = "I guess I've       been here for  quite a while";
        sentences[1] = "I think I'm          meant to stay   here ... ";
        sentences[2] = "Maybe I should stop trying ...";
    }

    void Update()
    {
       if (!cursor.isCutscene) return;

        if (Input.GetKeyDown(KeyCode.Space)) {

            if (!createBox) {
                sfxMeetFrog.Play();
                
                Invoke("TurnLeft", 1.5f);
                Invoke("CreateTextBox", 4f);
                createBox = true;
                return;

            }


            // sad sulk
            if (count == 2) {
                sfxDialogueNext.pitch = .9f;
                sfxDialogueNext.Play();
                animator.SetTrigger("sulk");
                StartCoroutine(SulkScene());
                return;
            }

            // I'm tired
            if (count == 9 && yes) {
                sfxDialogueNext.pitch = .33f;
                sfxDialogueNext.Play();
                animator.SetTrigger("breath");
                NextSentence();
                sentenceEnd = false;
            }

            // wait for text to finish
            if (!sentenceEnd) return;

            // I guess Ive been here for
            if (count == 1) {
                sfxDialogueNext.Play();
                NextSentence();
                sentenceEnd = false;
            }

            // ...
            if (count == 10 && yes){

                NextSentence();
                sentenceEnd = false;
            }

            // Goodnight - last sentence
            if (count == 12 && yes){
                Instantiate(leftCSColliderObject, new Vector3(36.353f, -0.58f, 0), Quaternion.identity);
                Invoke("NextSentence", .5f);
                sentenceEnd = false;
                Invoke("DisableCanvasOnly", 4f);
                DestroyClones();
                Destroy(noScreenObject);
            }

            // last sentence
            if (count == 9 && !yes){
                sfxDialogueNext.pitch = 1f;
                sfxDialogueNext.Play();
                Instantiate(leftCSColliderObject, new Vector3(36.353f, -0.58f, 0), Quaternion.identity);
                Invoke("DisableCanvasOnly", 1f);
                DestroyClones();
            }

            // I think you're right
            if (count == 7 && yes) {
                sfxDialogueNext.pitch = .35f;
                sfxDialogueNext.Play();
                Invoke("NextSentence", 1.5f);
                sentenceEnd = false;
            }

            // Chalk drawing
            if (count == 7 && !yes) {
                animator.SetTrigger("chalk");
                Invoke("NextSentence", 3f);
                // animator.SetTrigger("turnLeft");
                sentenceEnd = false;
            }
            
            if (count == 4 && !optionStart) {
                print ("count IS FOUR");
                StartOption();
                return;
            }

            if (optionStart) {
                count++;
            }

            if (count == 5) {
                spacePressed = true;
                if (upPressed) {
                    sfxDialogueNext.pitch = 1.2f;
                    sfxDialogueNext.Play();
                    yes = false;
                    
                    sentences[3] = "I'll try again     tomorrow then… ";
                    sentences[4] = "I think";
                    sentences[5] = "";
                    sentences[6] = "";
                    sentences[7] = "";

                    // Ill try again tomorrow
                    dialogueText.text = "";
                    Invoke("EnableCanvas", 2.5f);
                    Invoke("NextSentence",3f);
                    sentenceEnd = false;
                    count++;

                } else {
                    sfxDialogueNext.pitch = .4f;
                    sfxDialogueNext.Play();
                    yes = true;
                    sentences[3] = "Yeah… ";
                    sentences[4] = "I think you're     right…";
                    sentences[5] = "I'm tired.";
                    sentences[6] = "…";
                    sentences[7] = "Goodnight";
                    dialogueText.text = "";
                    // Yeah...
                    Invoke("EnableCanvas", 2.5f);
                    Invoke("NextSentence",3f);
                    sentenceEnd = false;
                    count++;
                }
            }
        }
        if (count == 4 && optionStart) {
            if (!instantiateOnce) {
                sfxDialogueNext.pitch = .6f;
                sfxDialogueNext.Play();
                noClone = Instantiate(noObject, new Vector3(38.2f, 1.2f, 0), Quaternion.identity);
                noClone.SetActive(true);
                yesClone = Instantiate(yesObject, new Vector3(38.1f, .27f, 0), Quaternion.identity);
                yesClone.SetActive(true);
                textboxClone = Instantiate(textBoxObject, new Vector3(38.272f, .69f, 0), Quaternion.identity);
                instantiateOnce = true;
                yesAnimator = yesClone.GetComponent<Animator>();
                noAnimator = noClone.GetComponent<Animator>();
                Invoke("DisableCanvas", .5f);
            }
        }
        OptionScene();    
    }

    void NextSentence () 
    {
        if (index <= sentences.Length -1) {
            dialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
    }

    IEnumerator WriteSentence() 
    {
        foreach (char character in sentences[index].ToCharArray()) {
            dialogueText.text += character;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        sentenceEnd = true;
        count++;
        index++;
    }

    

    void CreateTextBox() {
        canvas.enabled = true;
        NextSentence();
        turnLeft = false;
        sentenceEnd = false;
    }
    void TurnLeft() {
        animator.SetTrigger("turnLeft");
    }

    void OptionScene() {
        if (!spacePressed && optionStart) {
        //if press any key jump to gameplay scene
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                sfxArrow.Play();
                upPressed = true;
                yesAnimator.SetBool("selected", false);
                noAnimator.SetBool("notSelected", false);
                
            
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                sfxArrow.Play();
                upPressed = false;
                yesAnimator.SetBool("selected", true);
                noAnimator.SetBool("notSelected", true);
            }
        }
    }
    void DisableCanvas(){
        canvas.enabled = false;
        cursorObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    void EnableCanvas(){
        canvas.enabled = true;
        cursorObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    void DisableCanvasOnly(){
        canvas.enabled = false;
        endCutscene = true;
    }
    void StartOption() {
        optionStart = true;
    }
    IEnumerator SulkScene(){
        Invoke("NextSentence", 1.1f);
        count++;
        sentenceEnd = false;
        yield return new WaitForSecondsRealtime(4);
    }

    IEnumerator EndingScene(){
        Invoke("NextSentence", 1.1f);
        count++;
        sentenceEnd = false;
        yield return new WaitForSecondsRealtime(4);
    }

    void DestroyClones(){
        Destroy(noClone);
        Destroy(yesClone);
        Destroy(textboxClone);
    }
}
