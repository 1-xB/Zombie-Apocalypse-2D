using Godot;
using System;

public partial class ammo : Area2D
{
    [Export]
    public float Speed = 500f; 

    private Vector2 _velocity; 

    public override void _Ready()
    {
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
}