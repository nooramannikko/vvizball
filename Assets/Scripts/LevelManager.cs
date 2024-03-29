﻿// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private Vector3 playerSpawnPoint;

	public Powerup[] powerups;
	public MovingPlatforms[] platforms;
	public CheckPoint[] checkpoints;
	//public MoveEnemy[] enemies;
	public BasicEnemy[] enemyObjects;
	public AdvancedEnemy[] advEnemyObjects;
	public LaserWeapon[] lasers;
	public ZigzagWeapon[] zigzags;
	public int next;

	private bool gravity;

	public PlayerController player;
	public Transform mapStart;

	//Sound effects
	enum Sounds {
		FLOORHIT1 = 0,
		FLOORHIT2 = 1,
		FLOORHIT3 = 2,
		COIN = 3,
		DAMAGE = 4,
		LASER = 5,
		POWERUP = 6,
		CHECKPOINT = 7,
		MOUSEOVER = 8
	}

	void Start () 
	{
		playerSpawnPoint = mapStart.position;
		next = 0;
		gravity = true;
	}

	void FixedUpdate()
	{
		if (player.gravity != gravity)
		{
			GravityChange();
			gravity = (!gravity);
		}
	}
		
	// Checkpoints

	public void NewCheckpoint(Vector3 checkPoint)
	{
		if (playerSpawnPoint != checkPoint)
		{
			SoundManager.instance.PlaySingle((int)Sounds.CHECKPOINT);
			playerSpawnPoint = checkPoint;
		}
	}

	// Only called when player dies
	public Vector3 CurrentCheckpoint()
	{
		ResetPowerups();
		ResetEnemies();
		ResetWeapons ();
		return playerSpawnPoint;
	}

	// Changes players spawnpoint to next checkpoint
	public Vector3 NextCheckpoint()
	{
		for (var i = 0; i < checkpoints.Length; ++i)
		{
			if(checkpoints[i].Position() == playerSpawnPoint)
			{
				next = i + 1;
			}
		}

		if (next == checkpoints.Length)
		{
			next = 0;
			playerSpawnPoint = mapStart.position;
			ResetPowerups ();
			ResetEnemies ();
			ResetWeapons();
		}
		else
		{
			playerSpawnPoint = checkpoints[next].Position();
			ResetPowerups();
			ResetEnemies();
			ResetWeapons();
		}
		
		return playerSpawnPoint;
	}

	// Powerups
	
	void ResetPowerups()
	{
		for (var i = 0; i < powerups.Length; ++i)
		{
			powerups[i].Reset();		
		}
	}

	// Platforms

	public void GravityChange()
	{
		for (var i = 0; i < platforms.Length; ++i)
		{
			platforms[i].GravityChange();
		}
	}

	// Enemies
	void ResetEnemies()
	{
		/*for (var i = 0; i < enemies.Length; ++i) 
		{
			enemies[i].Reset();
		}*/
		for (var j = 0; j < enemyObjects.Length; ++j) 
		{
			enemyObjects[j].Reset();
		}
		for (var j = 0; j < advEnemyObjects.Length; ++j) 
		{
			advEnemyObjects[j].Reset();
		}
	}

	// Weapons
	void ResetWeapons()
	{
		for (var i = 0; i < lasers.Length; ++i) {
			lasers [i].Reset ();
		}
		for (var j = 0; j < zigzags.Length; j++) {
			zigzags [j].Reset ();
		}
	}

}
