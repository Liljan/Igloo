using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public int playerID;

    public List<GameObject> startingWeapons;
    private List<GameObject> weapons = new List<GameObject>();
    private int activeWeapon = 0;
    public float AIM_THRESHOLD = 0.2f;

    private bool isDpadPressed = false;

    public void Initiate(int playerID)
    {
        this.playerID = playerID;

        for (int i = 0; i < startingWeapons.Count; i++)
        {
            GameObject go = Instantiate(startingWeapons[i], transform.position, transform.rotation, this.transform);
            go.GetComponent<RangedWeapon>().Initiate(playerID);
            weapons.Add(go);
        }
        weapons[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Compensate the input for player turning, i.e. flipping in the x-direction.
        float x = Input.GetAxis("RIGHT_STICK_HORIZONTAL") * transform.parent.localScale.x;
        float y = Input.GetAxis("RIGHT_STICK_VERTICAL") * transform.parent.localScale.x;

        float aimAngle = 0.0f;

        if (Mathf.Abs(x) < AIM_THRESHOLD)
            x = 0.0f;
        if (Mathf.Abs(y) < AIM_THRESHOLD)
            y = 0.0f;

        if (x != 0.0f || y != 0.0f)
        {
            aimAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAngle);
        }

        float dpad = Input.GetAxis("TOGGLE_WEAPON");

        if (dpad != 0.0f)
        {

            if (!isDpadPressed)
            {

                if (dpad == 1.0f)
                {
                    ToggleWeaponForward();
                }
                else if (dpad == -1.0f)
                {
                    ToggleWeaponBackward();
                }

                isDpadPressed = true;
            }
        }
        else
        {
            isDpadPressed = false;
        }
    }

    private void ToggleWeaponForward()
    {

        /**
		 * Toggles weapons forwards
		 */

        weapons[activeWeapon].SetActive(false);

        if (activeWeapon < weapons.Count - 1)
        {
            activeWeapon++;
        }
        else {
            activeWeapon = 0;
        }

        weapons[activeWeapon].SetActive(true);
    }

    private void ToggleWeaponBackward()
    {

        /**
		 * Toggles weapons backwards
		 */

        weapons[activeWeapon].SetActive(false);

        if (activeWeapon > 0)
        {
            activeWeapon--;
        }
        else {
            activeWeapon = weapons.Count - 1;
        }

        weapons[activeWeapon].SetActive(true);
    }

    public void AddWeapon(GameObject newWeapon)
    {
        WeaponID newWeaponID = newWeapon.GetComponent<RangedWeapon>().weaponID;
        RangedWeapon currentWeapon;

        bool found = false;

        for (int i = 0; i < weapons.Count; i++)
        {
            currentWeapon = weapons[i].GetComponent<RangedWeapon>();

            if (currentWeapon.weaponID == newWeaponID)
            {
                currentWeapon.AddAmmo(2 * currentWeapon.clipSize);
                found = true;
            }
        }

        if (!found)
        {
            GameObject go = Instantiate(newWeapon, transform.position, transform.rotation, this.transform);
            go.GetComponent<RangedWeapon>().Initiate(playerID);
            weapons.Add(go);
        }
    }
}
