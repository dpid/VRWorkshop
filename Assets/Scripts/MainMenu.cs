using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple menu system that provides helper methods for showing and hiding menu elements.
/// </summary>
public class MainMenu : MonoBehaviour {

    public GameObject startButton;
    public GameObject victoryRestartButton;
    public GameObject defeatRestartButton;

    public void ShowStartButton()
    {
        HideAll();
		startButton.SetActive(true);
    }

	public void ShowVictoryRestartButtonButton()
	{
        HideAll();
		victoryRestartButton.SetActive(true);
	}

    public void ShowDefeatRestartButtonButton()
	{
        HideAll();
		defeatRestartButton.SetActive(true);
	}

	public void HideAll()
	{
		startButton.SetActive(false);
		victoryRestartButton.SetActive(false);
		defeatRestartButton.SetActive(false);
	}
}
