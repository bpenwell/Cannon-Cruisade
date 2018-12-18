using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyDetection : MonoBehaviour {

    private Coroutine TextCoroutine;

    private bool hasHitGround = false;
    private bool Flying = true;
    private float x_multiplier;
    private float y_multiplier;

    private void Start()
    {
        //pointsText.SetInvisible();
    }

    void Update(){
        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
        if(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude <= 1 && hasHitGround){
            Invoke("killObject", 1.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground"){
            hasHitGround = true;
        }
        if(collision.gameObject.tag != "shot"){
            Flying = false;
        }
        if(collision.gameObject.tag == "points"){
            string scoreText = GameObject.FindWithTag("cannon").GetComponent<ShootCannon>().scoreCount.text;
            int value = int.Parse(scoreText);
            value += 5000;


            collision.gameObject.GetComponentInChildren<FadeOut>().TurnOnThenFade("+5000");
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<Animator>().enabled = false;

            GameObject.FindWithTag("cannon").GetComponent<ShootCannon>().scoreCount.text = value.ToString();
            GameObject.FindWithTag("cannon").GetComponent<ShootCannon>().targetObjectsRemaining--;
        }
        if (collision.gameObject.tag == "extraPoints")
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "BonusLevel") {
                GameObject.FindWithTag("cannon").GetComponent<ShootCannon>().targetObjectsRemaining--;
            }
            string scoreText = GameObject.FindWithTag("cannon").GetComponent<ShootCannon>().scoreCount.text;
            GameObject.FindWithTag("cannon").GetComponent<ShootCannon>().SetBonus(true);
            int value = int.Parse(scoreText);
            int temp = Random.Range(10000, 20000);
            value += temp;

            collision.gameObject.GetComponentInChildren<FadeOut>().TurnOnThenFade("RANDOM BONUS: +" + temp);
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            GameObject.FindWithTag("cannon").GetComponent<ShootCannon>().scoreCount.text = value.ToString();
        }
        if (collision.gameObject.tag == "ballKiller"){
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "speedUp")
        {
            //Debug.Log("Pushing up");
            float angle = collision.gameObject.transform.localRotation.eulerAngles.z;
            Debug.Log("push angle " + angle);
            x_multiplier = Mathf.Cos(angle);
            y_multiplier = Mathf.Sin(angle);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(x_multiplier*100, y_multiplier*100, 0));
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "speedUp")
        {
            float angle = collision.gameObject.transform.localRotation.eulerAngles.z;
            //Debug.Log("Pushing up");
            Debug.Log("push angle " + angle);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(x_multiplier * 100, y_multiplier * 100, 0));
        }
    }

    public bool isFlying()
    {
        return Flying;
    }

    private void killObject(){
        if(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude == 0)
        {
            Destroy(this.gameObject);
        }
    } 

    public bool isGrounded()
    {
        return hasHitGround;
    }
  
}
