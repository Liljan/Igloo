using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JoystickMenuItem : MonoBehaviour
{
    protected bool isSelected;
    protected int playerID;

    public abstract void SetSelected(bool isSelected);
    public abstract void SetID(int playerID);
}
