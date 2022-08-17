using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float horizSpeed = 0.2f;
    public float vertSpeed = 0.2f;
    private Renderer re;
    void Start()
    {
        re = GetComponent<Renderer>();    
    }

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * horizSpeed, Time.time * vertSpeed);
        re.material.mainTextureOffset = offset;
    }
}
