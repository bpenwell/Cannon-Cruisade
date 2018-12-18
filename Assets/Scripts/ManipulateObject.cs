using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulateObject : MonoBehaviour {

	public float speed;

	private Color tempColor;
	private SpriteRenderer m_Sprite;
	private bool isFading;
	private Vector3 maxScale;
	private Vector3 minScale;
	private float range;
	private float X_temp;
	private float Y_temp;
	private float start_XYscale;
	// Update is called once per frame
	void Start () {
		if(tag == "Map"){
			m_Sprite = GetComponent<SpriteRenderer>();
			tempColor = m_Sprite.color;
		}
		isFading = true;
		maxScale = new Vector3(1.2f, 1.2f, 1f);
		minScale = new Vector3(0.8f, 0.8f, 1f);
		range = maxScale.x - minScale.x;
		start_XYscale = transform.localScale.x;
	}

	void Update(){
		if(gameObject.tag != "Map"){
			X_temp = (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f * range + minScale.x;
			Y_temp = (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f * range + minScale.y;
			transform.localScale = new Vector3(X_temp,Y_temp,1f);
		}		
		//Code for map alpha flucuation 
		if(gameObject.tag == "Map"){

			X_temp = (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f * .1f + start_XYscale;
			Y_temp = (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f * .1f + start_XYscale;
			transform.localScale = new Vector3(X_temp,Y_temp,1f);

			//Should pulse alpha
			if(isFading){
				tempColor.a = Mathf.Lerp(1,0,Time.deltaTime);
				//Debug.Log("isFading "+tempColor.a);
				m_Sprite.color = tempColor;
				if(tempColor.a < .1){
					isFading = false;
				}
			}
			else{
				tempColor.a = Mathf.Lerp(0,1,Time.deltaTime);
				//Debug.Log("NOT isFading "+tempColor.a);
				m_Sprite.color = tempColor;
				if(tempColor.a > .9){
					isFading = true;
				}
			}
		}
	}
}
