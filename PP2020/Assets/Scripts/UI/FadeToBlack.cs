using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public float lengthInSeconds = 3;
    public GameObject player;

    private AudioSource audioSource;
    private Image image;
    void Awake()
    {
        image = GetComponent<Image>();
        audioSource = player.GetComponent<MusicController>().audioSource;
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            image.color = new Color(0, 0, 0, image.color.a + Time.deltaTime / lengthInSeconds);
            audioSource.volume = Mathf.Max(0, audioSource.volume - Time.deltaTime);
        }

        if(image.color.a >= 1f)
        {
            SceneManager.LoadScene("EndGameScene");
        }
    }
}
