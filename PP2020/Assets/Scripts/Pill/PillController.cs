using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(SpriteRenderer))]
public class PillController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;
    private PostProcessVolume postProcessVolume;

    public float minSideEffectDuration = 10f;
    public float maxSideEffectDuration = 25f;
    private float postProcessingEffectTime;

    //Post processing profiles
    public PostProcessProfile defaultProfile;
    public PostProcessProfile blackAndWhiteProfile;

    PlayerTicController ticController;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("Camera is not defined");
        }
        if (player == null)
        {
            Debug.LogWarning("Player is not defined");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        ticController = player.GetComponent<PlayerTicController>();
        postProcessVolume = mainCamera.GetComponent<PostProcessVolume>();
    }

    public void ConsumePill()
    {
        if (spriteRenderer.enabled)
        {
            spriteRenderer.enabled = false; //Hides the pill to make it look like it was consumed, but it will be destroyed only after the side effects are finished.
            ticController.ResetTicTime();
            SideEffects();
        }
    }

    private void SideEffects()
    {
        int sideEffect = Random.Range(0, 0);
        switch (sideEffect)
        {
            case 0:
                //B&W
                postProcessVolume.profile = blackAndWhiteProfile;
                StartPostProcessTimer();
                break;
            case 1:
                break;
        }
    }

    void Update()
    {
        if (postProcessingEffectTime > 0)
        {
            postProcessingEffectTime -= Time.deltaTime;
        } else
        {
            postProcessVolume.profile = defaultProfile;
        }
    }

    private void StartPostProcessTimer()
    {
        postProcessingEffectTime = Random.Range(minSideEffectDuration, maxSideEffectDuration);
    }
}
