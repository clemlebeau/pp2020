using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    public float fadeLength = 3f;
    public float stayFullAlpha = 5f;
    [NonSerialized]
    public bool done = false;
    private Text text;
    private bool fadedIn = false;
    private bool waited = false;

    void Awake()
    {
        text = GetComponent<Text>();
        text.color = ChangeAlpha(text.color, 0);
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            if (!fadedIn)
            {
                text.color = ChangeAlpha(text.color, text.color.a + Time.deltaTime / fadeLength);
                fadedIn = text.color.a >= 1;
            } else if(!waited)
            {
                stayFullAlpha -= Time.deltaTime;
                waited = stayFullAlpha <= 0;
            } else
            {
                text.color = ChangeAlpha(text.color, text.color.a - Time.deltaTime / fadeLength);
                if (text.color.a <= 0)
                {
                    done = true;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    Color ChangeAlpha(Color color, float newAlpha)
    {
        return new Color(color.r, color.g, color.b, newAlpha);
    }
}
