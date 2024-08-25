using Godot;
using System;

public partial class GameManager : Node2D
{
	public bool _canShot = true;
	private Timer _shotTimer;
	
	
	public bool RoundInProgress = true;
	public int RoundNumber = 1;
	public int NumberOfKills = 0;
	CharacterBody2D _character;


	[Export] public Area2D[] ZombiesAppearPlaces;

	private Random _random;
	
	public override void _Ready()
	{

		_character = GetTree().Root.GetNode<CharacterBody2D>("Node/CharacterBody2D");
		_random = new Random();
		
		
		_shotTimer = new Timer();
		_shotTimer.OneShot = true;
		_shotTimer.WaitTime = 0.3f;
		AddChild(_shotTimer);
		_shotTimer.Connect("timeout", Callable.From(EndsTimer));
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
			int numberOfZombies = 10 * RoundNumber;
			Random random = new Random(); // Move Random outside the loop

			for (int i = 0; i < numberOfZombies; i++) // Start at 0 and loop until i < numberOfZombies
			{
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
			}
			RoundInProgress = false;
		}
	}

	public void EndsTimer()
	{
		_canShot = true;
	}
}
