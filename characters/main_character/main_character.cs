using Godot;
using System;

public partial class main_character : CharacterBody2D
{                                                                                    
    public float Speed = 200.0f; // Maksymalna prędkość postaci
    public float Acceleration = 1000.0f; // Przyspieszenie postaci
    public float Friction = 1000.0f; // Tarcie postaci

    private Vector2 _velocity = Vector2.Zero; // Wektor prędkości postaci
    private AnimatedSprite2D sprite2D;

    public override void _Ready()
    {
        sprite2D = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D"); 
        PackedScene Luger = GD.Load<PackedScene>("res://game_objects/Luger.tscn");
        Luger Luger_instance = Luger.Instantiate<Luger>();
        AddChild(Luger_instance);
    }

    public override void _Process(double delta)
    {
        Vector2 inputDirection = Vector2.Zero;
        
        // Określanie kierunku ruchu
        if (Input.IsActionPressed("right"))
        {
            inputDirection.X += 1; // Ruch w prawo
        }
        if (Input.IsActionPressed("left"))
        {
            inputDirection.X -= 1; // Ruch w lewo
        }
        if (Input.IsActionPressed("down"))
        {
            inputDirection.Y += 1; // Ruch w dół
        }
        if (Input.IsActionPressed("up"))
        {
            inputDirection.Y -= 1; // Ruch w górę
        }

        if (Input.IsActionPressed("left_click"))
        {
            PackedScene bullet = GD.Load<PackedScene>("res://game_objects/ammo.tscn");
            ammo bullet_instance = bullet.Instantiate<ammo>();
            AddChild(bullet_instance);

        }

        // Normalizujemy kierunek ruchu
        if (inputDirection.Length() > 0)
        {
            inputDirection = inputDirection.Normalized();
        }

        // Ustawienie animacji
        if (inputDirection != Vector2.Zero)
        {
            // Ruch w prawo i w lewo
            if (inputDirection.X != 0)
            {
                sprite2D.Animation = "run";
                sprite2D.FlipH = inputDirection.X < 0; // Ustawia kierunek w zależności od kierunku X
            }
            // Ruch w górę i w dół
            else if (inputDirection.Y != 0)
            {
                if (inputDirection.Y > 0)
                {
                    sprite2D.Animation = "forward";
                }
                else
                {
                    sprite2D.Animation = "backward";
                }
                sprite2D.FlipH = false; // Ustawienie na podstawie kierunku Y
            }
        }
        
        else
        {
            sprite2D.Animation = "idle";
        }

        // Przyspieszanie
        Vector2 targetVelocity = inputDirection * Speed;
        _velocity = _velocity.MoveToward(targetVelocity, Acceleration * (float)delta);
        if (inputDirection == Vector2.Zero)
        {
            _velocity = _velocity.MoveToward(Vector2.Zero, Friction * (float)delta);
        }

        // Przesuwamy postać
        Velocity = _velocity;
        MoveAndSlide();
    }
}
