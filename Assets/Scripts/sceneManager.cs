using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {

	void Start(){
		Debug.Log("At the start of the scene, levelUnlock = " + PlayerPrefs.GetInt("levelUnlock"));
	}

	public void LoadScene(string input){
		SceneManager.LoadScene(input);
	}

	public void ReloadScene(){
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	public void LevelCompleted(){
		Scene scene = SceneManager.GetActiveScene();
		if(scene.name == "Level1"){
			//Level 1 complete
			PlayerPrefs.SetInt("levelUnlock", 2);
		}
		else if(scene.name == "Level2"){
			//Level 2 complete
			PlayerPrefs.SetInt("levelUnlock", 3);
		}
		else if(scene.name == "Level3"){
			//Level 3 complete
			PlayerPrefs.SetInt("levelUnlock", 4);
		}
	}

    public void restartGame()
    {
        PlayerPrefs.SetInt("levelUnlock", 1);
        PlayerPrefs.SetInt("Level1Bonus", 0);
        PlayerPrefs.SetInt("Level2Bonus", 0);
        PlayerPrefs.SetInt("Level3Bonus", 0);
    }
}
