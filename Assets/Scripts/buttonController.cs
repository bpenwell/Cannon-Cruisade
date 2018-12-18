using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonController : Selectable {
	public GameObject levelPulse_obj;
	public GameObject m_manager;

	BaseEventData m_BaseEvent;
	private int levelComplete;
    private int B_lvl1;
    private int B_lvl2;
    private int B_lvl3;
    // Use this for initialization
    void Start () {
        levelComplete = PlayerPrefs.GetInt("levelUnlock");
        B_lvl1 = PlayerPrefs.GetInt("Level1Bonus");
        B_lvl2 = PlayerPrefs.GetInt("Level2Bonus");
        B_lvl3 = PlayerPrefs.GetInt("Level3Bonus");
        Debug.Log("Reporting: " + levelComplete);
        if(levelComplete == 0)
        {
            interactable = false;
        }
		if(levelComplete == 1){
            if(tag == "lvl1")
            {
                interactable = true;
            }
            if(tag != "lvl1"){
                interactable = false;
            }
			if(tag != "lvl1"){
				//levelPulse_obj.SetActive(false);
            }
		}
		if(levelComplete == 2){
			if(tag == "lvl2" || tag == "lvl1")
            {
				interactable = true;
			}
            if (tag != "lvl1" && tag != "lvl2")
            {
                interactable = false;
            }
            if (tag != "lvl1" || tag != "lvl2"){
				//levelPulse_obj.SetActive(false);
			}
		}
		if(levelComplete == 3){
            if (tag == "lvl3" || tag == "lvl2" || tag == "lvl1")
            {
                interactable = true;
            }
            if (tag != "lvl3" && tag != "lvl2" && tag != "lvl1")
            {
                interactable = false;
            }
            if (tag != "lvl1" || tag != "lvl2" || tag != "lvl3")
            {
                //levelPulse_obj.SetActive(false);
            }
		}
        if (levelComplete == 4)
        {
            if (tag == "lvl3" || tag == "lvl2" || tag == "lvl1")
            {
                interactable = true;
            }
            if (tag == "Bonus" && (B_lvl1 == 1) && (B_lvl2 == 1) && (B_lvl3 == 1))
            {
                interactable = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {    
		if(IsHighlighted(m_BaseEvent) == true && interactable){
			levelPulse_obj.SetActive(true);
		}
		else{
			if(levelPulse_obj){
				levelPulse_obj.SetActive(false);
			}
		}

		if(IsPressed() && interactable){
			if(tag == "lvl1"){
				m_manager.GetComponent<sceneManager>().LoadScene("Level1");
			}
			if(tag == "lvl2"){
				m_manager.GetComponent<sceneManager>().LoadScene("Level2");
			}
			if(tag == "lvl3"){
				m_manager.GetComponent<sceneManager>().LoadScene("Level3");
			}
            if (tag == "Bonus")
            {
                m_manager.GetComponent<sceneManager>().LoadScene("BonusLevel");
            }
        }
	}
}
