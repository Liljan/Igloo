using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinMenuHandler : MonoBehaviour
{
    //public JoystickMenuItem[] menuItems;

    public List<JoystickMenuItem> menuItems;
    public int playerID;

    private int selectedItem = 0;

    // Use this for initialization
    public void Awake()
    {

    }

    public void Start()
    {
        menuItems[selectedItem].SetSelected(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SwitchLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            SwitchRight();
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
