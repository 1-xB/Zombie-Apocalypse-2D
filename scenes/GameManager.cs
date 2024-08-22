using Godot;
using System;

public partial class GameManager : Node2D
{
	public bool _canShot = true;

	private Timer _shotTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_shotTimer = new Timer();
		_shotTimer.OneShot = true;
		_shotTimer.WaitTime = 0.3f;
		AddChild(_shotTimer);
		_shotTimer.Connect("timeout", Callable.From(EndsTimer));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("left_click"))
		{
			if (_canShot)
			{
				_canShot = false;
				PackedScene bullet = GD.Load<PackedScene>("res://game_objects/ammo.tscn");
				ammo bullet_instance = bullet.Instantiate<ammo>();
				AddChild(bullet_instance);
				_shotTimer.Start();
				
			}
			

		}
	}

	public void EndsTimer()
	{
		_canShot = true;
	}
}
