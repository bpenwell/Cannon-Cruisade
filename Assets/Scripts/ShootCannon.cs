using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShootCannon : MonoBehaviour {
    [Header("Input")]
	public GameObject[] list_Shots;
    public GameObject[] visual_ShotsRemaining;
    public GameObject visual_ShotAim;
    public GameObject visual_ShotAimDim;

    public GameObject m_Sign;
    //public GameObject dotPrefab;
    public float dotFreq = 500f;
    public int shotsAllowed = 7;
    public int targetObjectsRemaining;

    [Header("UI")]
    public Text shotPower;
    public Text scoreCount;
    public Text shotsLeft;
    public GameObject levelWin;
    public GameObject levelLoss;
    public GameObject b_Retry;
    public GameObject b_Menu;
    public GameObject b_NextLevel;
    public GameObject b_Skip;
    public GameObject scalingText;
    public GameObject levelSprite;
    public GameObject playingUI;
    public GameObject pausedUI;
    public GameObject outOfAmmo;
    public GameObject scoreBoard;
    public GameObject scoreText_obj;
    public GameObject shotsLeftText_obj;
    public GameObject shotsNumText_obj;
    public GameObject totalText_obj;

    [Header("SceneManager")]
    public sceneManager m_SceneManager;

    private bool firstShot = false;
    private Rigidbody2D m_Shot;
	private float angleOffset = 25f;
    private float forceVar;
    private bool isLevelDone = false;
    private bool gotBonus = false;

    private List<GameObject> dotList = new List<GameObject>();
    // Use this for initialization
    void Start () {
        forceVar = 1200f;
        visual_ShotAim.transform.localScale = new Vector3(visual_ShotAim.transform.localScale.x, ((forceVar / 12) / 30), visual_ShotAim.transform.localScale.z);

        shotPower.text = Math.Truncate(forceVar / 12).ToString();
        scoreCount.text = "0";
        shotsLeft.text = shotsAllowed.ToString();
        levelLoss.SetActive(false);
        levelWin.SetActive(false);
        b_Retry.SetActive(false);
        b_Menu.SetActive(false);
        b_NextLevel.SetActive(false);
        b_Skip.SetActive(false);
        outOfAmmo.SetActive(false);
        scoreBoard.SetActive(false);

        for (int i=0; i<shotsAllowed-1; i++){
            if(i<shotsAllowed && shotsAllowed <= 6){
                visual_ShotsRemaining[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        //when playing
        if(playingUI.activeInHierarchy == true)
        {
            if (firstShot && scalingText.activeInHierarchy == true)
            {
                scalingText.SetActive(false);
                levelSprite.SetActive(false);

            }
            //If no targets left (aka win) 
            if (targetObjectsRemaining == 0)
            {
                levelWin.SetActive(true);
                isLevelDone = true;
                b_NextLevel.SetActive(true);
                m_SceneManager.LevelCompleted();

                //Open scoreboard
                OpenScoreboard();
            }
            if (targetObjectsRemaining != 0 && isLevelDone)
            {
                levelLoss.SetActive(true);
                b_Retry.SetActive(true);
                b_Menu.SetActive(true);
            }
            //adjust catapult angle/size
            if (Input.GetMouseButton(0) && !isLevelDone)
            {
                //----Begin finding distance----
                Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - angleOffset);

                float dist = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
                Vector3 v3Pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
                float distanceBetween = Vector3.Distance(v3Pos, transform.position);
                //----Done finding distance----

                //ignore hardcoded force values
                forceVar = 240 * distanceBetween;
                if (forceVar > 1200)
                {
                    forceVar = 1200;
                }
                //Update UI
                shotPower.text = Math.Truncate(forceVar / 12).ToString();
                //Update catapult aim sprite's scale and position to re-center it
                //Expected: sprite should look like it's "length" is changing while retaining aspect
                visual_ShotAim.transform.localScale = new Vector3(visual_ShotAim.transform.localScale.x, ((forceVar / 12) / 28), visual_ShotAim.transform.localScale.z);
            }

            //Shot being selected
            int returnIndex = 0;

            //If flying
            if (m_Shot != null)
            {
                //Generate dots
                for (float i = 0; i < 5f; i += Time.deltaTime)
                {
                    if (i % dotFreq == 0)
                    {
                        GameObject temp = Instantiate(m_Shot.transform.GetChild(0).gameObject, m_Shot.transform.GetChild(0).gameObject.transform);
                        temp.transform.parent = null;
                        dotList.Add(temp);
                    }
                }
            }



            //Shoot check
            if (Input.GetKeyDown(KeyCode.Space) && !isLevelDone)
            {
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name != "BonusLevel")
                {
                    m_Sign.GetComponent<moveUpandDown>().turnOnBool();
                }
                string holdShots = shotsLeft.text;
                int value = int.Parse(holdShots);
                //Shoot
                if (value > 0)
                {
                    firstShot = true;
                    GameObject newShot = Instantiate(list_Shots[returnIndex], list_Shots[returnIndex].transform);
                    newShot.SetActive(true);
                    newShot.transform.parent = null;
                    newShot.transform.position = list_Shots[returnIndex].transform.position;
                    newShot.transform.localScale = new Vector3(3.3f, 3.3f, 1);
                    float catapultAngle = transform.rotation.eulerAngles.z;
                    m_Shot = newShot.GetComponent<Rigidbody2D>();
                    m_Shot.gravityScale = 1;

                    float varianceVal = UnityEngine.Random.Range(-2.5f, 2.5f);

                    Shoot(forceVar, angleOffset + catapultAngle + varianceVal);
                    value--;
                    shotsLeft.text = value.ToString();
                    //Destroy all dots
                    DestroyDots();
                    if (value != 0 && value <= 6)
                    {
                        visual_ShotsRemaining[value - 1].SetActive(false);
                    }
                }

                if (value == 0)
                {
                    outOfAmmo.SetActive(true);
                    b_Skip.SetActive(true);
                    visual_ShotsRemaining[0].SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                playingUI.SetActive(false);
                pausedUI.SetActive(true);
            }
        }
        //when paused
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                playingUI.SetActive(true);
                pausedUI.SetActive(false);
            }
        }
    }

    public void SetBonus(bool input)
    {
        gotBonus = input;
    }

    public void GotBonus()
    {
        int temp1 = PlayerPrefs.GetInt("Level1Bonus");
        int temp2 = PlayerPrefs.GetInt("Level2Bonus");
        int temp3 = PlayerPrefs.GetInt("Level3Bonus");
        
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("Starting GotBonus.." + gotBonus + " " + scene.name + " " + temp1 + " " + temp2 + " " + temp3);
        if (gotBonus && scene.name == "Level1" && PlayerPrefs.GetInt("Level1Bonus") == 0)
        {
            PlayerPrefs.SetInt("Level1Bonus", 1);
            Debug.Log("GOT LVL1 BONUS");
        }
        if (gotBonus && scene.name == "Level2" && PlayerPrefs.GetInt("Level2Bonus") == 0)
        {
            PlayerPrefs.SetInt("Level2Bonus", 1);
            Debug.Log("GOT LVL2 BONUS");
        }
        if (gotBonus && scene.name == "Level3" && PlayerPrefs.GetInt("Level3Bonus") == 0)
        {
            PlayerPrefs.SetInt("Level3Bonus", 1);
            Debug.Log("GOT LVL3 BONUS");
        }
    }

    public void SkipPress(){
        isLevelDone = true;

    }

    public void ResumeGame()
    {
        playingUI.SetActive(true);
        pausedUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

	private void Shoot(float force, float angle){
        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        m_Shot.AddForce(dir * force);
    }

	private int findShot(string input){
		for(int i=0; i<list_Shots.Length; i++){
			if(list_Shots[i].name == input){
				return i;
			}
		}
		Debug.Log("Error: input name missing.");
		return -1;
	}

    private void DestroyDots(){
            for(int i=0; i< dotList.Count;i++){
                Destroy(dotList[i]);
            }
            dotList.Clear();
    }

    private void OpenScoreboard()
    {
        string holdShots = shotsLeft.text;
        string score = scoreCount.text;
        scoreBoard.SetActive(true);
        int value = int.Parse(holdShots);
        int scoreValue = int.Parse(score);
        shotsNumText_obj.GetComponent<Text>().text = shotsLeft.text;
        shotsLeftText_obj.GetComponent<Text>().text = (value * 10000).ToString();
        scoreText_obj.GetComponent<Text>().text = scoreCount.text;
        totalText_obj.GetComponent<Text>().text = ((value * 10000) + scoreValue).ToString();
    }
}
