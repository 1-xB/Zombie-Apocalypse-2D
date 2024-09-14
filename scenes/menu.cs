using Godot;
using System;

public partial class menu : CanvasLayer
{

	public void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/main_scene.tscn");
	}

	public void _on_exit_pressed()
	{
		GetTree().Quit();
	}
}
