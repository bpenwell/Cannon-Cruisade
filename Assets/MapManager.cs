using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {
	public GameObject Level1Complete;
	public GameObject Level2Complete;
	public GameObject Level3Complete;

    public Button Level1;
	public Image[] Level1Stars;
	//Value => Passes level
	private int m_Level1OneStar;
	//Value => 1 or 2 extra balls or gets bonus
	private int m_Level1TwoStar;
	//Value => 2 extra balls and gets bonus
	private int m_Level1ThreeStar;


    public Button Level2;
    public Image[] lvl2_stars;
	//Value => Passes level
	private int m_Level2OneStar;
	//Value => 1 or 2 extra balls or gets bonus
	private int m_Level2TwoStar;
	//Value => 2 extra balls and gets bonus
	private int m_Level2ThreeStar;

    public Button Level3;
	public Image[] lvl3_stars;
	//Value => Passes level
	private int m_Level3OneStar;
	//Value => 1 or 2 extra balls or gets bonus
	private int m_Level3TwoStar;
	//Value => 2 extra balls and gets bonus
	private int m_Level3ThreeStar;


	private FadeOut m_FadeController;

	// Use this for initialization
	void Start () {
		m_FadeController = GetComponent<FadeOut>();
		int newLevelOutput = PlayerPrefs.GetInt("levelUnlock");
        int B_lvl1 = PlayerPrefs.GetInt("Level1Bonus");
        int B_lvl2 = PlayerPrefs.GetInt("Level2Bonus");
        int B_lvl3 = PlayerPrefs.GetInt("Level3Bonus");
        if (newLevelOutput != PlayerPrefs.GetInt("previousLevel")){
			Debug.Log("--------------");
			Debug.Log("levelUnlock and previousLevel are not the same: lvlUnlock= " + newLevelOutput + " | previousLevel= " + PlayerPrefs.GetInt("previousLevel"));
            Debug.Log("lvl1Bonus: " +B_lvl1);
            Debug.Log("lvl2Bonus: " + B_lvl2);
            Debug.Log("lvl3Bonus: " + B_lvl3);
            Debug.Log("--------------");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("levelUnlock") == 0)
        {

        }
	}
}
