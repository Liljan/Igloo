using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour {

	public GameObject WeaponPrefab;
	public float MAX_COOLDOWN_TIME = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") {
			Weapons weaponSystem = col.gameObject.GetComponentInChildren<Weapons>();
			weaponSystem.AddWeapon(WeaponPrefab);
		}
	}
}
