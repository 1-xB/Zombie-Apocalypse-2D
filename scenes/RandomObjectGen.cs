using Godot;
using System;

public partial class RandomObjectGen : Node2D
{
	[Export ]public PackedScene Heart;
	[Export ]public PackedScene Plus;

	public Timer HeartTimer;
	public Timer PlusTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HeartTimer = GetNode<Timer>("HeartTimer");
		PlusTimer = GetNode<Timer>("PlusTimer");
		HeartTimer.Start();
		PlusTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_heart_timer_timeout()
	{
		SpawnObject(Heart);
		HeartTimer.Start();
	}
	public void _on_plus_timer_timeout()
    {
	    SpawnObject(Plus);
	    PlusTimer.Start();
    }

	private void SpawnObject(PackedScene area)
	{
		Area2D area2D = (Area2D)area.Instantiate();
		AddChild(area2D);
	}
}
