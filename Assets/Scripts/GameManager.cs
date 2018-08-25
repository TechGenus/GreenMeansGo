using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static int currentLevel;
	public static int deathCount = 0;
	public GameObject youWinText;
	public GameObject replayButton;

	private void Update() {
		if (currentLevel >= SceneManager.sceneCount) {
			youWinText.SetActive(true);
			replayButton.SetActive(true);
			Time.timeScale = 0;
		}
	}

	public static void CompleteLevel() {
		if (++currentLevel < SceneManager.sceneCount)
			SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
	}

	public void restartGame() {
        currentLevel = 0;
		SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
		Time.timeScale = 1;
	}
}
