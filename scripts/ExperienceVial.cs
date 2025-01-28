using Godot;
using System;

public partial class ExperienceVial : Node2D
{
    private Area2D area2D;
    private GameEvents gameEvents;

    public override void _Ready()
    {
        area2D = GetNode<Area2D>("Area2D");
        area2D.AreaEntered += OnAreaEntered;
        gameEvents = GetNode<GameEvents>("/root/GameEvents");
    }

    private void OnAreaEntered(Area2D area2D)
    {   
        gameEvents.EmitExperienceVialCollected(1);
        QueueFree();
    }
}
