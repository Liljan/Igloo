using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class JoystickMenuButton : JoystickMenuItem
{
    public Text text;
    public string innerText;

    public void Awake()
    {
        text.text = innerText;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void SetSelected(bool isSelected)
    {
        if (isSelected)
        {
            text.text = "<" + innerText + ">";
        }
        else
        {
            text.text = innerText;
        }
    }

    public void Activate()
    {

    }

    public override void SetID(int playerID)
    {
        this.playerID = playerID;
    }
}
