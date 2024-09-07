using Godot;
using System;

public partial class Heart : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public main_character Character;
	public override void _Ready()
	{
		Character = GetTree().Root.GetNode<main_character>("Node/CharacterBody2D");
		
		Random rand = new Random();
		Vector2 pos = new Vector2(rand.Next(10, 2200), rand.Next(10, 1100));
		Position = pos;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_body_entered(Node2D body)
	{
		if (body.Name == "CharacterBody2D")
		{
			Character.Heal();
			QueueFree();
		}
		
	}
}
