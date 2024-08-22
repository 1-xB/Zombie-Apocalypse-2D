using Godot;
using System;

public partial class Luger : Sprite2D
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 targetPosition = GetGlobalMousePosition();
		LookAt(targetPosition);
	}
}
