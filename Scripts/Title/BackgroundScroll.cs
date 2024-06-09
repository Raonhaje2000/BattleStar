using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    MeshRenderer backGround;
    Vector2 offset;

    [SerializeField] float scrollSpeed;

    private void Awake()
    {
        backGround = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        offset = Vector2.zero;
        scrollSpeed = 0.1f;
    }

    void Update()
    {
        ScrollBackgroundImage();
    }

    void ScrollBackgroundImage()
    {
        offset.x += scrollSpeed * Time.deltaTime;
        offset.y += scrollSpeed * Time.deltaTime;

        backGround.material.mainTextureOffset = offset;
    }
}
