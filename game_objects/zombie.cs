using Godot;
using System;

public partial class zombie : CharacterBody2D
{


	public float speed;
	private CharacterBody2D _character;
	private AnimatedSprite2D _zombieAnimation;
	public float Health = 100;
	public bool canMove = true;
	public GameManager gameManager;
	
	
	public Timer _timer;
	public override void _Ready()
	{

		_timer = GetNode<Timer>("Timer");
		
		_character = GetTree().Root.GetNode<CharacterBody2D>("Node/CharacterBody2D");
		_zombieAnimation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		gameManager = GetTree().Root.GetNode<GameManager>("Node/GameManager");
		
		Random random = new Random();
		speed = random.Next(50, 100 * (1 + (gameManager.GetRound() / 10)));
		
		if (speed > 200)
		{
			speed = 150;
		}
	}

	public override void _PhysicsProcess(double delta)
	{

		if (canMove)
		{
			if (_character == null)
				return;
		
			Vector2 direction = (_character.GlobalPosition - GlobalPosition).Normalized();
			Velocity = direction * speed;
			MoveAndSlide();

		}
		
        


	}

	public void _on_area_2d_body_entered(Node2D body)
	{

		if (body.Name == _character.Name)
		{
			canMove = false;
			_zombieAnimation.Animation = "attack";
		}
		
	}

	public void _on_area_2d_body_exited(Node2D body)
	{
		if (body.Name == _character.Name)
		{
			_timer.Start();
		}
			
	}

	public void DecreaseHealth()
	{
		Health -= 25;
		if (Health <= 0)
        {
            QueueFree();
            gameManager.AddKill();
        }
	}

	public void _on_timer_timeout()
	{
		_zombieAnimation.Animation = "walking";
		canMove = true;
	}
	
}

