using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public int damage;
    public int playerID;

    public void Initiate(int ID)
    {
        playerID = ID;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.TakeDamage(damage, playerID);
        }

        HitPlayerEffect();

    }
    public abstract void HitPlayerEffect();
}
