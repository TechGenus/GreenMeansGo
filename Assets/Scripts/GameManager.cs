using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static int currentLevel;
	public static int deathCount = 0;
	public GameObject youWinText;
	public GameObject replayText;

	private void Update() {
		if (currentLevel >= SceneManager.sceneCount) {
			youWinText.SetActive(true);
            replayText.SetActive(true);
            if (Input.GetKeyDown("joystick button 3")) RestartGame();
            
			
		}
	}

	public static void CompleteLevel() {
		if (++currentLevel < SceneManager.sceneCount)
			SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
	}

	public void RestartGame() {
        currentLevel = 0;
		SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
		Time.timeScale = 1;
	}
}
