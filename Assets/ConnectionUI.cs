using System;
using System.Collections;
using System.Collections.Generic;
using Defucilis.TheHandyUnity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConnectionUI : MonoBehaviour
{

    private InputField _connectionKeyInput;
    private Button _connectButton;
    
    private Text _handyStatusText;
    private Text _gamepadText;
    private Text _responseText;

    private Button _continueButton;
    private Slider _testSlider;

    private UiController _controller;
    
    void Awake()
    {
        _controller = FindObjectOfType<UiController>();
        
        _connectionKeyInput = transform.Find("ConnectionKeyInput/InputField").GetComponent<InputField>();
        _connectButton = transform.Find("ConnectButton").GetComponent<Button>();
        _connectButton.onClick.AddListener(() => Connect(_connectionKeyInput.text));
        
        _handyStatusText = transform.Find("HandyStatusText").GetComponent<Text>();
        _gamepadText = transform.Find("GamepadStatusText").GetComponent<Text>();
        _responseText = transform.Find("ResponseText").GetComponent<Text>();
        _continueButton = transform.Find("ContinueButton").GetComponent<Button>();
        _continueButton.onClick.AddListener(() => _controller.SetPanel("ControlUI"));
        
        _testSlider = transform.Find("TestSlider").GetComponent<Slider>();
        _testSlider.onValueChanged.AddListener(value =>  HandyConnection.SetSpeedPercent((int)(value * 100f)));
        
        if (PlayerPrefs.HasKey("ConnectionKey")) {
            _connectionKeyInput.text = PlayerPrefs.GetString("ConnectionKey");
            Connect(_connectionKeyInput.text);
        }
    }

    private void Connect(string connectionKey)
    {
        _connectButton.interactable = false;
        _connectionKeyInput.interactable = false;
        
        HandyConnection.ConnectionKey = connectionKey;
        HandyConnection.GetVersion(data =>
        {
            _handyStatusText.text = "Handy Status: Connected";
            _continueButton.interactable = true;
            _testSlider.interactable = true;
            PlayerPrefs.SetString("ConnectionKey", connectionKey);
                
            _connectButton.interactable = true;
            _connectionKeyInput.interactable = true;
        }, error =>
        {
            _handyStatusText.text = "Handy Status: Failed - " + error;
            _connectButton.interactable = true;
            _connectionKeyInput.interactable = true;
        });
    }

    private void HandleResponse(object sender, JSONNode response)
    {
        _responseText.text = response.ToString(2);
        if (response["success"].IsNull || response["success"] == false) {
            
        } else {
            _handyStatusText.text = "Handy Status: Connected";
            _continueButton.interactable = true;
            _testSlider.interactable = true;
            PlayerPrefs.SetString("ConnectionKey", _connectionKeyInput.text);
        }
        _connectButton.interactable = true;
        _connectionKeyInput.interactable = true;
    }

    private void Update()
    {
        _gamepadText.text = "Gamepad Status: " +
                            (Gamepad.all.Count > 0
                                ? "Connected " + Gamepad.current.shortDisplayName
                                : "Not Connected");
    }
}
