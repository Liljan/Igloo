using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickMenuList : JoystickMenuItem
{
    public JoinMenuHandler menuHandler;

    private AxisAsButton verticalAxis;
    public Text text;
    public string innerText;

    private int itemIndex;

    public void Awake()
    {
        text.text = innerText;
    }

    public override void SetSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        if (isSelected)
        {
            text.text = "<" + innerText + ">";
        }
        else
        {
            text.text = innerText;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            float yAxis = verticalAxis.IsDown();

            if (yAxis > 0.0f)
                ScrollUp();
            if (yAxis < 0.0f)
                ScrollDown();
        }
    }

    private void ScrollUp()
    {
        itemIndex++;

        if (itemIndex == menuHandler.playerColors.Length)
        {
            itemIndex = 0;
        }

        text.color = menuHandler.playerColors[itemIndex];
    }

    private void ScrollDown()
    {
        itemIndex--;

        if (itemIndex < 0)
        {
            itemIndex = menuHandler.playerColors.Length - 1;
        }

        text.color = menuHandler.playerColors[itemIndex];
    }

    public override void SetID(int playerID)
    {
        this.playerID = playerID;
        Debug.Log(playerID);
        verticalAxis = AxisAsButton.CreateAxisAsButton(JoystickControlls.D_PAD_VERTICAL[playerID]);
        text.color = menuHandler.playerColors[playerID];
        itemIndex = playerID;
    }
}
