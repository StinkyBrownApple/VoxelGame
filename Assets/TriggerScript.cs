using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<PlaceCube>().PlayerCollision(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<PlaceCube>().PlayerCollision(false);
        }
    }
}
