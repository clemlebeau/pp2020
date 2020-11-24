using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(SpriteRenderer))]
public class PillController : MonoBehaviour
{
    public string cameraName = "Camera";
    public string playerName = "Player";
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject player;
    private PostProcessVolume postProcessVolume;

    bool pillActive = false;

    public float minSideEffectDuration = 10f;
    public float maxSideEffectDuration = 25f;
    private float sideEffectTime;

    //Post processing profiles
    public PostProcessProfile defaultProfile;
    public PostProcessProfile blackAndWhiteProfile;
    public PostProcessProfile chromaticAberrationProfile;
    public PostProcessProfile negativeProfile;

    PlayerTicController ticController;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        mainCamera = GameObject.Find(cameraName).GetComponent<Camera>();
        player = GameObject.Find(playerName);

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
            //ticController.ResetTicTime();
            SideEffects();
        }
    }

    private void SideEffects()
    {
        ticController.sideEffect = pillActive = true;
        int sideEffectIndex = Random.Range(0, 3 + 1);
        StartSideEffectTimer();
        switch (sideEffectIndex)
        {
            case 0:
                //B&W
                postProcessVolume.profile = blackAndWhiteProfile;
                break;
            case 1:
                postProcessVolume.profile = chromaticAberrationProfile;
                break;
            case 2:
                postProcessVolume.profile = negativeProfile;
                break;
            case 3:
                mainCamera.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 0, 1));
                break;
        }
    }

    void Update()
    {
        if (ticController.sideEffect && pillActive)
        {
            if (sideEffectTime > 0)
            {
                sideEffectTime -= Time.deltaTime;
                ticController.ResetTicTime();
            }
            else
            {
                postProcessVolume.profile = defaultProfile;
                mainCamera.transform.rotation = Quaternion.AngleAxis(0, Vector3.zero);
                ticController.sideEffect = pillActive = false;
                Destroy(gameObject);
            }
        }
    }
    private void StartSideEffectTimer()
    {
        sideEffectTime = Random.Range(minSideEffectDuration, maxSideEffectDuration);
    }
}