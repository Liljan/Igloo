using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconHover : MonoBehaviour {

	public float amplitude = 1.0f;
	public float period = 1.0f;

	private float y;
	private float frequency;

	// Use this for initialization
	void Start () {
		y = transform.position.y;
		frequency = 1.0f / period;
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.y = amplitude * Mathf.Sin(2.0f * Mathf.PI * frequency * Time.time) + y;
		transform.position = pos;
	}
}
