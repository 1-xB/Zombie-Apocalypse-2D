using Godot;
using System;

public partial class GameManager : Node2D
{
	public bool CanShot = true;
	private Timer _shotTimer;
	
	
	public bool RoundInProgress = true;
	public bool CanSpawn = true;
	public int RoundNumber = 1;
	CharacterBody2D _character;


	[Export] public Area2D[] ZombiesAppearPlaces;

	private Random _random;


	public int NumberOfKills;
	public int NumberOfZombies;
	public int ZombiesSpawn;

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
		if (Input.IsActionPressed("left_click") && CanShot)
		{
			CanShot = false;
			PackedScene bullet = GD.Load<PackedScene>("res://game_objects/ammo.tscn");
			ammo bulletinstance = bullet.Instantiate<ammo>();
			AddChild(bulletinstance);
			_shotTimer.Start();
		}
		
		if (RoundInProgress)
		{
			
			NumberOfZombies = 10 * RoundNumber;
			

			if (ZombiesSpawn < NumberOfZombies)
			{
				if (CanSpawn)
				{
					ZombieSpawnTimer.Start();
					CanSpawn = false;
				}
				
				
			}
			else if (ZombiesSpawn == NumberOfZombies)
			{
				
				RoundInProgress = false;
			}
			
		}
	}

	public void EndsTimer()
	{
		CanShot = true;
	}
	
	public void AddKill()
	{
		NumberOfKills++;
		if (NumberOfKills >= NumberOfZombies)
		{
			RoundNumber++;
			RoundInProgress = true;
			NumberOfKills = 0;
			ZombiesSpawn = 0;
		}
		
		GD.Print(NumberOfKills + " " + NumberOfZombies);
	}

	public int GetRound()
	{
		return RoundNumber;
	}

	public void SpawnZombie()
	{
		


		
		PackedScene enemy = GD.Load<PackedScene>("res://game_objects/zombie.tscn");
		zombie enemyinstance = enemy.Instantiate<zombie>();
		AddChild(enemyinstance);

		// Spawn the enemy at a random position from ZombiesAppearPlaces
		Vector2 spawnPosition;

		do
		{
			spawnPosition = ZombiesAppearPlaces[_random.Next(0, ZombiesAppearPlaces.Length)].GlobalPosition;
		} while (spawnPosition.DistanceTo(_character.GlobalPosition) < 10);

		enemyinstance.GlobalPosition = spawnPosition;
		ZombiesSpawn++;
		CanSpawn = true;



	}
}
