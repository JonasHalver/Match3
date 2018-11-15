using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetterScript : MonoBehaviour {

    private bool resetting = false;
    public GameObject block;

	void Start () {
		
	}
	
	
	void Update () {
		if (resetting)
        {
            Move();
        }
	}

    public void ResetMatch()
    {
        resetting = true;
    }

    public void Move()
    {
        if (transform.position.y < 5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 10 * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(0f, -3f);
            resetting = false;
            block.GetComponent<BlockScript>().SendMessage("ResetMatch");
        }

    }
}
