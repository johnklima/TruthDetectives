using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SearchText : GUIState
{

    public string text ="First Search";
    private InputField input;
    private void Start()
    {
        input = GetComponent<InputField>();
        input.text = text;
    }
    private void Update()
    {
        text = input.text;
        base.Update();
    }

}
