﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinMenuHandler : MonoBehaviour
{
    public List<JoystickMenuItem> menuItems;
    public int playerID;

    private int selectedItem = 0;

    private AxisAsButton horizontal;
    private AxisAsButton vertical;

    // Use this for initialization
    public void Awake()
    {
        horizontal = AxisAsButton.CreateAxisAsButton(JoystickControlls.LEFT_HORIZONTAL[playerID]);
        vertical = AxisAsButton.CreateAxisAsButton(JoystickControlls.LEFT_VERTICAL[playerID]);
    }

    public void Start()
    {
        menuItems[selectedItem].SetSelected(true);
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = horizontal.IsDown();

        if (xAxis > 0.0f)
            SwitchRight();
        if (xAxis < 0.0f)
            SwitchLeft();
    }

    private void SwitchRight()
    {
        menuItems[selectedItem].SetSelected(false);
        selectedItem++;
        if (selectedItem == menuItems.Count)
            selectedItem = 0;

        menuItems[selectedItem].SetSelected(true);
    }

    private void SwitchLeft()
    {
        menuItems[selectedItem].SetSelected(false);
        selectedItem--;
        if (selectedItem < 0)
            selectedItem = menuItems.Count - 1;

        menuItems[selectedItem].SetSelected(true);
    }




}
