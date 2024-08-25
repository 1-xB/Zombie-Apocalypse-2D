using Godot;
using System;

public partial class zombie : CharacterBody2D
{


	public float speed;
	private CharacterBody2D _character;
	public float Health = 100;
	public override void _Ready()
	{
		_character = GetTree().Root.GetNode<CharacterBody2D>("Node/CharacterBody2D");
		Random random = new Random();
		speed = random.Next(50, 100);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_character == null)
			return;
		
		Vector2 direction = (_character.GlobalPosition - GlobalPosition).Normalized();
		Velocity = direction * speed;
        MoveAndSlide();

        


	}

	public void _on_area_2d_body_entered(Node2D body)
	{
		if (body.Name == "CharacterBody2D")
		{
			Vector2 direction = (_character.GlobalPosition - GlobalPosition).Normalized();
			Velocity = direction * speed;
			MoveAndSlide();
		}
		
	}

	public void DecreaseHealth()
	{
		Health -= 25;
		if (Health <= 0)
        {
            QueueFree();
        }
	}
	
}

