using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinScreenManager : MonoBehaviour {
    public ColorSlider colorSlider;

    public Color[] playerColors;

    void Awake()
    {
        colorSlider.Initiate(playerColors);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
