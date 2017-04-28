using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisAsButton : MonoBehaviour
{
    private float direction;

    private float POSITIVE = 1F;
    private float NEGATIVE = -1F;
    private float ZERO = 0F;

    // Factory pattern
    private static GameObject obj;
    public static GameObject Obj
    {
        get
        {
            if (obj == null)
            {
                obj = new GameObject();
            }
            return obj;
        }
    }

    public static AxisAsButton CreateAxisAsButton(string axis)
    {
        GameObject go = new GameObject();
        //AxisAsButton thisObj = Obj.AddComponent<AxisAsButton>();
        AxisAsButton thisObj = go.AddComponent<AxisAsButton>();
        thisObj.axis = axis;
        return thisObj;
    }

    private bool isAxisInUse = false;
    private string axis;

    public float IsDown()
    {
        float axisValue = Input.GetAxisRaw(axis);

        if (axisValue > 0.0f)
        {
            if (isAxisInUse == false)
            {
                isAxisInUse = true;
                return POSITIVE;
            }
        }
        if (axisValue < 0.0f)
        {
            if (isAxisInUse == false)
            {
                isAxisInUse = true;
                return NEGATIVE;
            }
        }
        if (axisValue == 0.0f)
        {
            isAxisInUse = false;
            return ZERO;
        }

        return ZERO;
    }
}
