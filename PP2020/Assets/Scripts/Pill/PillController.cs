using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillController : MonoBehaviour
{
    public Camera mainCamera;
    PlayerTicController ticController;
    void Awake()
    {
        ticController = GetComponent<PlayerTicController>();
    }

    public void ConsumePill()
    {
        GetComponent<SpriteRenderer>().enabled = false; //Hides the pill to make it look like it was consumed, but it will be destroyed only after the side effects are finished.
        ticController.ResetTicTime();
        SideEffects();
    }

    private void SideEffects()
    {
        int sideEffect = Random.Range(0, 5);
        switch (sideEffect)
        {
            case 0:
                //B&W
                
                break;
            case 1:
                break;
        }
    }
}
