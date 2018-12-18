using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUpandDown : MonoBehaviour {

    private float up_YOffset = .3f;
    private float original_Y;

    private bool movingUp = true;
    private bool turnOff = false;
    private Coroutine process;

	// Use this for initialization
	void Start () {
        original_Y = transform.position.y;

    }

    private void FixedUpdate()
    {
        process = StartCoroutine(MoveUpandDown());
        if (turnOff)
        {
            transform.gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }

    private IEnumerator MoveUpandDown()
    {
        if(movingUp)
        {
            float newYvalue = transform.position.y + .01f;
            transform.position = new Vector3(transform.position.x, newYvalue, transform.position.z);
            if(transform.position.y > (up_YOffset + original_Y))
            {
                movingUp = false;
            }
            yield return new WaitForSeconds(.1f); 
        }
        else
        {
            float newYvalue = transform.position.y - .01f;
            transform.position = new Vector3(transform.position.x, newYvalue, transform.position.z);
            if (transform.position.y < (original_Y - up_YOffset))
            {
                movingUp = true;
            }
            yield return new WaitForSeconds(.1f);
        }


    }

    public void turnOnBool()
    {
        Debug.Log("turn off object call");
        turnOff = true;
    }

}
