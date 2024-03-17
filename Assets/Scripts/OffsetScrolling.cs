using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScrolling : MonoBehaviour
{

    public float scrollSpeed;
    public Renderer bgRend;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bgRend.material.mainTextureOffset += new Vector2(0f, scrollSpeed * Time.deltaTime);
    }
}
