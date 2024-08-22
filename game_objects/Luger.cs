using Godot;
using System;

public partial class Luger : Sprite2D
{

	private Sprite2D _sprite;
	private AnimatedSprite2D _character;


	public override void _Ready()
	{
		_character = GetTree().Root.GetNode<AnimatedSprite2D>("Node/CharacterBody2D/AnimatedSprite2D");
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		Vector2 targetPosition = GetGlobalMousePosition();
		LookAt(targetPosition);
		
		_character = GetTree().Root.GetNode<AnimatedSprite2D>("Node/CharacterBody2D/AnimatedSprite2D");
		if (Rotation > -Mathf.Pi / 2 & Rotation < Mathf.Pi / 2)
		{
			FlipV = false; 
			targetPosition = GetGlobalMousePosition();
			LookAt(targetPosition);

		}
		else
		{
			
			FlipV = true;
			targetPosition = GetGlobalMousePosition();
			LookAt(targetPosition);
		}

		if (_character.Animation != "backward")
		{
			ZIndex = 3;
		}
		else
		{
			ZIndex = -1;
		}
	}
}
