using Godot;
using System;

public partial class GameManager : Node2D
{
	public bool _canShot = true;
	private Timer _shotTimer;
	
	
	public bool RoundInProgress = true;
	public bool CanSpawn = true;
	public int RoundNumber = 100;
	CharacterBody2D _character;


	[Export] public Area2D[] ZombiesAppearPlaces;

	private Random _random;


	public int numberOfKills = 0;
	public int numberOfZombies = 0;
	public int ZombiesSpawn = 0;

	public Timer ZombieSpawnTimer;
	
	public override void _Ready()
	{

		_character = GetTree().Root.GetNode<CharacterBody2D>("Node/CharacterBody2D");
		_random = new Random();
		
		
		_shotTimer = new Timer();
		_shotTimer.OneShot = true;
		_shotTimer.WaitTime = 0.3f;
		_shotTimer.Autostart = true;
		AddChild(_shotTimer);
		_shotTimer.Connect("timeout", Callable.From(EndsTimer));
		ZombieSpawnTimer = GetNode<Timer>("ZombieSpawnTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        
        // Check if the left mouse button is pressed and shoot the bullet if it's allowed'
		if (Input.IsActionPressed("left_click") && _canShot)
		{
			_canShot = false;
			PackedScene bullet = GD.Load<PackedScene>("res://game_objects/ammo.tscn");
			ammo bullet_instance = bullet.Instantiate<ammo>();
			AddChild(bullet_instance);
			_shotTimer.Start();
		}
		
		if (RoundInProgress)
		{
			
			numberOfZombies = 10 * RoundNumber;
			

			if (ZombiesSpawn < numberOfZombies)
			{
				if (CanSpawn)
				{
					ZombieSpawnTimer.Start();
					CanSpawn = false;
				}
				
				
			}
			else if (ZombiesSpawn == numberOfZombies)
			{
				
				RoundInProgress = false;
			}
			
		}
	}

	public void EndsTimer()
	{
		_canShot = true;
	}
	
	public void AddKill()
	{
		numberOfKills++;
		if (numberOfKills >= numberOfZombies)
		{
			RoundNumber++;
			RoundInProgress = true;
			numberOfKills = 0;
			ZombiesSpawn = 0;
		}
		
		GD.Print(numberOfKills + " " + numberOfZombies);
	}

	public int GetRound()
	{
		return RoundNumber;
	}

	public void SpawnZombie()
	{
		

		Random random = new Random();


		
		PackedScene enemy = GD.Load<PackedScene>("res://game_objects/zombie.tscn");
		zombie enemy_instance = enemy.Instantiate<zombie>();
		AddChild(enemy_instance);

		// Spawn the enemy at a random position from ZombiesAppearPlaces
		Vector2 spawnPosition;

		do
		{
			spawnPosition = ZombiesAppearPlaces[random.Next(0, ZombiesAppearPlaces.Length)].GlobalPosition;
		} while (spawnPosition.DistanceTo(_character.GlobalPosition) < 10);

		enemy_instance.GlobalPosition = spawnPosition;
		ZombiesSpawn++;
		CanSpawn = true;



	}
}
