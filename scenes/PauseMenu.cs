using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{

	public bool Pause = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("esc"))
		{
			Pause = !Pause;
			GetTree().Paused = Pause;
			Visible = Pause;
		}
	}

	public void _on_button_pressed()
	{
		Pause = !Pause;
		GetTree().Paused = Pause;
		Visible = Pause;
	}

	public void _on_button_2_pressed()
	{
		Pause = !Pause;
		GetTree().Paused = Pause;
		GetTree().ChangeSceneToFile("res://scenes/start_menu.tscn");
	}	
}
