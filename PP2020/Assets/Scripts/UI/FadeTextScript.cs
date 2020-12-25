using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeTextScript : MonoBehaviour
{
    bool canSpace = true;
    bool fadeInDone = false;

    void Update()
    {
        if (canSpace && fadeInDone && Input.GetKey(KeyCode.Space))
        {
            StopCoroutine("FadeTextToFullAlpha");
            StartCoroutine(FadeTextToZeroAlpha(3f, GetComponent<Text>()));
        }

        if (!canSpace && GetComponent<Text>().color.a <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void Awake()
    {
        GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, 0);
    }

    void Start()
    {
        StartCoroutine(FadeTextToFullAlpha(4f, GetComponent<Text>()));
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / t));
            yield return null;
        }
        fadeInDone = true;
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text text)
    {
        canSpace = false;

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
