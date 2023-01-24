using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // All private variables
    private PlayerInput playerInput;

    private InputAction navAction;
    private InputAction clickAction;
    private InputAction backAction;
    private InputAction scrollAction;

    private InputActionMap currentActionMap;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        currentActionMap = playerInput.currentActionMap;

        navAction = playerInput.actions["Navigate"];
        clickAction = playerInput.actions["Click"];
        backAction = playerInput.actions["Back"];
        scrollAction = playerInput.actions["Scroll"];

        navAction.performed += OnMovement;
        clickAction.performed += OnClick;
    }

    private void OnEnable() {
        navAction.Enable();
        clickAction.Enable();

        currentActionMap.Enable();
    }

    private void OnDisable() {
        navAction.Disable();
        clickAction.Disable();

        currentActionMap.Disable();
    }

    private void Update() {

    }

    private void OnMovement(InputAction.CallbackContext value) {
        Debug.LogWarning("MOvin NOW " + value);
    }

    private void OnClick(InputAction.CallbackContext value) {
        Debug.LogWarning("Clickin NOW " + value);
    }

    private void OnControlSchemeChange(InputAction.CallbackContext value) {
        if (currentActionMap != playerInput.currentActionMap) {
            currentActionMap = playerInput.currentActionMap;
        }
    }
}
