using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameManager starts and stops the game. It also listens for events to determine the game end state.
/// </summary>
public class GameManager : MonoBehaviour {

    public MainMenu mainMenu;
    public GameObject playerCannon;
    public Castle[] castles;
    public EnemySpawner[] enemySpawners;

    private void Start()
    {
        AddEventListeners();
        mainMenu.ShowStartButton();
    }

    public void StartGame()
    {
        mainMenu.HideAll();

        Invoke("ActivatePlayerCannon", 0.1f);

		foreach (Castle castle in castles)
		{
			castle.Restore();
		}

		foreach (EnemySpawner enemeySpawner in enemySpawners)
		{
			enemeySpawner.StartSpawning();
		}
    }

    public void StopGame()
    {
        DeactivatePlayerCannon();

		foreach (EnemySpawner enemeySpawner in enemySpawners)
		{
			enemeySpawner.StopSpawning();
		}
    }

    private void ActivatePlayerCannon()
    {
        playerCannon.SetActive(true);
    }

    private void DeactivatePlayerCannon()
    {
        playerCannon.SetActive(false);
    }

    private void AddEventListeners()
    {
        RemoveEventListeners();

        foreach(Castle castle in castles)
        {
            castle.OnDeath.AddListener(OnCastleDeath);
        }

		foreach (EnemySpawner enemeySpawner in enemySpawners)
		{
			enemeySpawner.OnDeath.AddListener(OnEnemySpawnerDeath);
		}
    }

    private void RemoveEventListeners()
    {
		foreach (Castle castle in castles)
		{
            castle.OnDeath.RemoveListener(OnCastleDeath);
		}

		foreach (EnemySpawner enemeySpawner in enemySpawners)
		{
			enemeySpawner.OnDeath.RemoveListener(OnEnemySpawnerDeath);
		}
    }

    private void OnCastleDeath()
    {
        bool isAllCastlesDead = true;

		foreach (Castle castle in castles)
		{
            if (castle.isDead == false)
            {
                isAllCastlesDead = false;
                break;
            }
		}

        if (isAllCastlesDead)
        {
            StopGame();
            mainMenu.ShowDefeatRestartButtonButton();
        }
    }

    private void OnEnemySpawnerDeath()
    {
        bool isAllEnemiesDead = true;

        foreach(EnemySpawner enemySpawner in enemySpawners)
        {
            if (enemySpawner.isDead == false)
            {
                isAllEnemiesDead = false;
                break;
            }    
        }

        if (isAllEnemiesDead)
        {
            StopGame();
            mainMenu.ShowVictoryRestartButtonButton();
        }
    }


}
