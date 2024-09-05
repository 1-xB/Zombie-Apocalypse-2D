using Godot;
using System;

public partial class GameManager : Node2D
{
	public bool CanShot = true;
	public bool RoundInProgress = true;
	public bool CanSpawn = true;
	public int RoundNumber = 1;
	public int NumberOfKills;
	public int NumberOfZombies;
	public int ZombiesSpawn;

	// Zmienne dotyczące magazynka
	public int MagazineCapacity = 12;
	public int MagazineCurrent = 12;
	public bool IsReloadTimerStart = false;

	// Odniesienia do scen i węzłów
	CharacterBody2D _character;
	[Export] public Area2D[] ZombiesAppearPlaces;

	// Losowość
	private Random _random;

	// Timery
	private Timer _shotTimer;
	public Timer ReloadTimer;
	public Timer ZombieSpawnTimer;

	// UI
	public Label ReloadLabel;
	public Label AmmoLabel;
	public Label RoundLabel;
	
	public override void _Ready()
	{
		// Pobieranie węzłów
		_character = GetTree().Root.GetNode<CharacterBody2D>("Node/CharacterBody2D");
		ReloadLabel = GetTree().Root.GetNode<Label>("Node/CharacterBody2D/ReloadLabel");
		AmmoLabel = GetTree().Root.GetNode<Label>("Node/UI/AmmoLabel");
		RoundLabel = GetTree().Root.GetNode<Label>("Node/UI/RoundLabel");
		

		// Inicjalizacja zmiennych
		_random = new Random();

		// Inicjalizacja i konfiguracja timera do strzelania
		_shotTimer = new Timer();
		_shotTimer.OneShot = true;
		_shotTimer.WaitTime = 0.3f;
		_shotTimer.Autostart = true;
		AddChild(_shotTimer);
		_shotTimer.Connect("timeout", Callable.From(EndsTimer));

		// Pobieranie istniejących timerów z drzewa
		ZombieSpawnTimer = GetNode<Timer>("ZombieSpawnTimer");
		ReloadTimer = GetNode<Timer>("ReloadTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (MagazineCurrent > 0) 
		{
			// Check if the left mouse button is pressed and shoot the bullet if it's allowed'
			if (Input.IsActionPressed("left_click") && CanShot)
			{
				MagazineCurrent--;
				CanShot = false;
				PackedScene bullet = GD.Load<PackedScene>("res://game_objects/ammo.tscn");
				ammo bulletinstance = bullet.Instantiate<ammo>();
				AddChild(bulletinstance);
			
				_shotTimer.Start();
			}

			
		}
		else
		{
			ReloadLabel.Show();
			if (!IsReloadTimerStart)
			{
				ReloadTimer.Start();
				IsReloadTimerStart = true;
			}
			
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
		
		AmmoLabel.Text = $"Ammo: {MagazineCurrent}/{MagazineCapacity}";
		RoundLabel.Text = $"Round: {RoundNumber}";
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

	public void Reload_timeout()
	{
		MagazineCurrent = MagazineCapacity;
		ReloadLabel.Hide();
		ReloadTimer.Stop();
		IsReloadTimerStart = false;
	}
}
