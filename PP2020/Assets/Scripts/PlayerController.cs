using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical") ;

        controller.Move(new Vector3(xInput, yInput, 0));
    }

}
