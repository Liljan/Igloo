using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public float timer = 2.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (timer <= 0.0f)
        {
            Destroy(this.gameObject);
        }

        timer -= Time.deltaTime;
    }
}
