using System.Collections;
using System.Collections.Generic;
using Defucilis.TheHandyUnity;
using UnityEngine;
using UnityEngine.UI;

public class ScriptUI : MonoBehaviour
{
    
    private UiController _controller;

    private Button _sendButton;
    private Button _playButton;
    private Button _pauseButton;

    void Awake()
    {
        _controller = FindObjectOfType<UiController>();
        
        transform.Find("BackButton").GetComponent<Button>().onClick.AddListener(() => _controller.SetPanel("ConnectionUI"));
        transform.Find("ControlButton").GetComponent<Button>().onClick.AddListener(() => {
            _controller.SetPanel("ControlUI");
        });
        
        

        _sendButton = transform.Find("SendButton").GetComponent<Button>();
        _playButton = transform.Find("PlayButton").GetComponent<Button>();
        _pauseButton = transform.Find("PauseButton").GetComponent<Button>();
        
        _sendButton.onClick.AddListener(() =>
        {
            var data = new List<Vector2Int>();
            for (var i = 0; i < 100; i++) {
                data.Add(new Vector2Int(i * 50, Random.Range(0, 100)));
            }
            //_connection.GetCsvUrl(data.ToArray(), output =>
            //{
                //Debug.Log("CSV Output: " + output);
                //if (!string.IsNullOrEmpty(output)) {
                //    _playButton.interactable = true;
                //}
            //});
        });
        
        _playButton.onClick.AddListener(async () =>
        {
            _playButton.interactable = false;
            //await _connection.SyncPlay();
            _pauseButton.interactable = true;
        });
        _pauseButton.onClick.AddListener(async () =>
        {
            _pauseButton.interactable = false;
            //await _connection.SyncPause();
            _playButton.interactable = true;
        });
        
        
        transform.Find("From/Slider").GetComponent<Slider>().onValueChanged.AddListener(newValue =>
        {
            
        });
        transform.Find("To/Slider").GetComponent<Slider>().onValueChanged.AddListener(newValue =>
        {
            
        });
        
    }

    void Update()
    {
        
    }
} 