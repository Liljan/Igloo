using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour {

	public GameObject WeaponPrefab;
	public float MAX_COOLDOWN_TIME = 3.0f;

	public GameObject icon;
	public GameObject particles;

	private float timer = 0.0f;
	private bool isEnabled = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		if (timer <= 0.0f && !isEnabled) {
			EnablePickup();
		}
	}

	public void OnTriggerEnter2D(Collider2D col)
	{

		if (isEnabled) {
			
			if (col.tag == "Player") {
				WeaponHandler weaponSystem = col.gameObject.GetComponentInChildren<WeaponHandler>();
				weaponSystem.AddWeapon(WeaponPrefab);

				DisablePickup();
			}
		}
	}

	private void EnablePickup(){
		particles.SetActive(true);
		icon.SetActive(true);
		isEnabled = true;
	}

	private void DisablePickup(){
		particles.SetActive(false);
		icon.SetActive(false);

		isEnabled = false;
		timer = MAX_COOLDOWN_TIME;
	}
}
