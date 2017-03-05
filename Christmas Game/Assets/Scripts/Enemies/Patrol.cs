using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    private Rigidbody2D rb2d;

    [Header("Collision")]
    public LayerMask mWhatIsGround;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.left;
	}
	
	// Update is called once per frame
	void Update () {
		
    }
}
