using Godot;
using System;

public partial class zombie : CharacterBody2D
{


	public float Speed;
	private CharacterBody2D _character;
	private AnimatedSprite2D _zombieAnimation;
	public float Health = 100;
	public bool CanMove = true;
	public GameManager MaingameManager;
	public main_character CharacterScript;
	
	public bool CanBite = true;
	
	public Timer ShotTimer;
	public Timer BiteTimer;
	public Timer FallTimer;
	public override void _Ready()
	{

		ShotTimer = GetNode<Timer>("Timer");
		BiteTimer = GetNode<Timer>("BiteTimer");
		
		_character = GetTree().Root.GetNode<CharacterBody2D>("Node/CharacterBody2D");
		CharacterScript = _character.GetNode<main_character>(".");
		_zombieAnimation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		MaingameManager = GetTree().Root.GetNode<GameManager>("Node/GameManager");
		
		Random random = new Random();
		Speed = random.Next(50, 100 * (1 + (MaingameManager.GetRound() / 10)));
		
		if (Speed > 200)
		{
			Speed = 150;
		}
	}

	public override void _PhysicsProcess(double delta)
	{

		if (CanMove)
		{
			if (_character == null)
				return;
		
			Vector2 direction = (_character.GlobalPosition - GlobalPosition).Normalized();
			Velocity = direction * Speed;
			if (Velocity.X > 0)
			{
				_zombieAnimation.FlipH = false;
			}
			else if (Velocity.X < 0)
			{
				_zombieAnimation.FlipH = true;
			}
			MoveAndSlide();

		}
		
        


	}

	public void _on_area_2d_body_entered(Node2D body)
	{

		if (body.Name == _character.Name)
		{
			CanBite = true;
			CanMove = false;
			_zombieAnimation.Animation = "attack";
			CharacterScript.TakeDamage(10);
			BiteTimer.Start();
			
			
		}
		
	}

	public void _on_area_2d_body_exited(Node2D body)
	{
		if (body.Name == _character.Name)
		{
			BiteTimer.Stop();
			CanBite = false;
			ShotTimer.Start();
		}
			
	}

	public void DecreaseHealth()
	{
		Health -= 25;
		if (Health <= 0)
		{
            _zombieAnimation.Play("death");
            QueueFree();
            MaingameManager.AddKill();
            
        }
	}

	public void _on_timer_timeout()
	{
		_zombieAnimation.Animation = "walking";
		CanMove = true;
	}

	public void BiteTimer_timeout()
	{
		if (CanBite)
		{
			CharacterScript.TakeDamage(10);
			BiteTimer.Start();
		}
	}


}

