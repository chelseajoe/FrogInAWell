using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour {

    public PlayerMovement script;
	public Sprite[] sprites;
	public int spritePerFrame = 8;
	public bool loop = true;
	private int index = 0;
	private Image image;
	private int frame = 0;


	void Awake () {
		image = GetComponent<Image> ();
		        // Application.targetFrameRate = 120;

	}

	void Update () {
		if (!loop && index == 2) return;
		frame ++;
		if (frame < spritePerFrame) return;
		image.sprite = sprites [index];
		frame = 0;
		index ++;
		if (index >= 2) {
			if (loop) index = 0;
		}
       
        if (!script.isAlive)
        {
            image.sprite = sprites[3]; 
            return;
        }

        if (script.isJumping)
        {
            image.sprite = sprites[2]; 
        }


	}
}