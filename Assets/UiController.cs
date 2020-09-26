using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{

    private Transform[] _panels;

    void Awake()
    {
        _panels = new Transform[transform.childCount];
        for (var i = 0; i < _panels.Length; i++) _panels[i] = transform.GetChild(i);
        SetPanel("ConnectionUI");
    }
    
    public void SetPanel(string panel)
    {
        var foundPanel = transform.Find(panel);
        if (foundPanel == null) {
            Debug.LogError("Didn't find panel " + panel);
            return;
        }

        foreach (var p in _panels) {
            p.gameObject.SetActive(p == foundPanel);
        }
    }
}