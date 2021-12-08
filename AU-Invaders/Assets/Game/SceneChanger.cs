using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public void LoadGame()
	{
		SceneManager.LoadScene(1);//skifter scene til game
	}
	public void Exit()
	{
		Application.Quit();
	}
}