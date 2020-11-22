using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MusicNoteController : MonoBehaviour
{
    public float noteSpeed = .5f;
    public float noteLifeTime = 3f;

    private float alphaStep;

    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //float hue = //Random.Range(0, 1);
        //Color color = Color.HSVToRGB(hue, 1, 1);
        spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
    }

    void Start()
    {
        alphaStep = 1 / noteLifeTime;
    }

    void Update()
    {
        Color noteColor = spriteRenderer.color;
        noteColor.a -= alphaStep * Time.deltaTime;
        spriteRenderer.color = noteColor;

        if (noteColor.a <= 0)
        {
            Destroy(gameObject);
        }

        transform.position += new Vector3(0, noteSpeed * Time.deltaTime, 0);
    }
}
