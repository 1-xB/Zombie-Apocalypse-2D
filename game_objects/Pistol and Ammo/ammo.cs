using Godot;
using System;

public partial class ammo : Area2D
{
    [Export]
    public float Speed = 500f; 

    private Vector2 _velocity;
    private CharacterBody2D _characterBody2D;
    public Sprite2D SpawnPos;

    public override void _Ready()
    {
        _characterBody2D = GetTree().Root.GetNode<CharacterBody2D>("Node/CharacterBody2D");
        SpawnPos = GetTree().Root.GetNode<Sprite2D>("Node/CharacterBody2D/Gun");
        
        
        GlobalPosition = SpawnPos.GlobalPosition;
        
        
        // Pobieramy aktualną pozycję myszy w momencie wystrzału
        Vector2 targetPosition = GetGlobalMousePosition();
        LookAt(targetPosition);
        
        // Obliczamy kierunek do celu
        Vector2 direction = (targetPosition - GlobalPosition).Normalized();
        
        // Ustawiamy wektor prędkości
        _velocity = direction * Speed;
    }

    public override void _Process(double delta)
    {
        // Przemieszczamy pocisk według wektora prędkości i czasu
        GlobalPosition += _velocity * (float)delta;
    }

    public void _on_visible_on_screen_enabler_2d_screen_exited()
    {
        QueueFree(); // Usuwamy pocisk, gdy wyjdzie poza ekran
    }

    public void _on_area_2d_body_entered(Node2D body)
    {
        if (body is zombie ZombieBody)
        {
            ZombieBody.DecreaseHealth();
            QueueFree();
        }

    }
}