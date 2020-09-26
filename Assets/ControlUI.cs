using System.Collections;
using System.Collections.Generic;
using Defucilis.TheHandyUnity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class ControlUI : MonoBehaviour
{
    private Slider _speedSlider;
    private Slider _lengthSlider;

    private UiController _controller;
    private PlayerInput _playerInput;
    
    void Awake()
    {
        _controller = FindObjectOfType<UiController>();
        
        _speedSlider = transform.Find("Speed/Slider").GetComponent<Slider>();
        _speedSlider.onValueChanged.AddListener(value =>  HandyConnection.SetSpeedPercent((int)(value * 100f)));
        _lengthSlider = transform.Find("StrokeLength/Slider").GetComponent<Slider>();
        _lengthSlider.onValueChanged.AddListener(value =>  HandyConnection.SetStrokePercent((int)(value * 100f)));
        transform.Find("BackButton").GetComponent<Button>().onClick.AddListener(() => _controller.SetPanel("ConnectionUI"));
        transform.Find("ScriptButton").GetComponent<Button>().onClick.AddListener(() => {
            _controller.SetPanel("ScriptUI");
        });

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onActionTriggered += HandleActionTriggered;
    }

    private void HandleActionTriggered(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        switch (ctx.action.name) {
            case "Select":
                _speedSlider.value = 0f;
                break;
            case "A":
                _speedSlider.value = 0.2f;
                break;
            case "B":
                _speedSlider.value = 0.4f;
                break;
            case "X":
                _speedSlider.value = 0.6f;
                break;
            case "Y":
                _speedSlider.value = 0.8f;
                break;
            case "Start":
                _speedSlider.value = 1f;
                break;
            case "Up":
                _lengthSlider.value = Mathf.Clamp01(_lengthSlider.value + 0.1f);
                break;
            case "Down":
                _lengthSlider.value = Mathf.Clamp01(_lengthSlider.value - 0.1f);
                break;
            case "Left":
                _speedSlider.value = _speedSlider.value - 0.05f < 0f
                    ? 0.01f
                    : _speedSlider.value - 0.05f;
                break;
            case "Right":
                _speedSlider.value = _speedSlider.value > 0f && _speedSlider.value < 0.05f
                    ? 0.05f
                    : Mathf.Min(1f, _speedSlider.value + 0.05f);
                break;

        }
    }
}
