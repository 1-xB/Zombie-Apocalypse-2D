using Godot;
using System;

public partial class UpgradeMenu : CanvasLayer
{
	[Export] public GameManager GameManagerScript;
	[Export] public main_character MainCharacterScript;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_bulletUpgrade_pressed()
	{
		GameManagerScript.IncreaseMagazineCapacity();
		GetTree().Paused = false;
		Visible = false;
	}

	private void _on_movementUpgrade_pressed()
	{
		MainCharacterScript.IncreaseMovementSpeed();
		GetTree().Paused = false;
		Visible = false;
	}
	
	private void _on_reloadspeedUpgrade_pressed()
    {
	    GameManagerScript.DecreaseReloadTime();
	    GetTree().Paused = false;
	    Visible = false;
    }

	private void _on_shootingUpgrade_pressed()
	{
		GameManagerScript.DecreaseShootingCooldown();
		GetTree().Paused = false;
		Visible = false;
	}
}
