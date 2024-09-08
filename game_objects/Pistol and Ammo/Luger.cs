using Godot;
using System;

public partial class Luger : Sprite2D
{

	private Sprite2D _bulletSpawn;


	public override void _Ready()
	{
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		Vector2 targetPosition = GetGlobalMousePosition();

		float mouseX = targetPosition.X;
		float spriteX = GlobalPosition.X;
		LookAt(targetPosition);

		if (mouseX < spriteX )
		{
			FlipV = true;
		}

		else
		{
			FlipV = false;
		}

	}
}
