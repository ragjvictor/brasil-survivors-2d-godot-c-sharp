using Godot;
using System;
using System.Numerics;

public partial class PlayerController : CharacterBody2D
{
    const float MaxSpeed = 125.0f;
    const float AccelarationSmoothing = 25;

    private AnimatedSprite2D animationPlayer;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animationPlayer.Stop();
        animationPlayer.Play("idle");        
    }

    public override void _PhysicsProcess(double delta)
    {
        Godot.Vector2 movementVector = GetMovementVector();
        Godot.Vector2 direction = movementVector.Normalized();
        Godot.Vector2 targetVelocity = direction * MaxSpeed;

        Velocity = Velocity.Lerp(targetVelocity, (float)(1 - Math.Exp(-delta * AccelarationSmoothing)));
        
        MoveAndSlide();
    }

    private Godot.Vector2 GetMovementVector()
    {
        float movementX = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        float movementY = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        
        if (movementX != 0.0 || movementY != 0.0)
        {
            animationPlayer.Play("run");
            if (movementX > 0) {
                animationPlayer.FlipH = false;
            }
            else
            {
                animationPlayer.FlipH = true;
            }            
        }
        else
        {
            animationPlayer.Play("idle");
        }

        return new Godot.Vector2(movementX, movementY);
    }
}
