using Godot;
using System;

public partial class ExperienceManager : Node
{
    private float CurrentExperience = 0;
    private GameEvents gameEvents;

    public override void _Ready()
    {
        gameEvents = GetNode<GameEvents>("/root/GameEvents");
        gameEvents.ExperienceVialCollected += OnExperienceVialCollected;
    }

    private void IncrementExperience(float value)
    {
        CurrentExperience += value;
        GD.Print(CurrentExperience);
    }

    private void OnExperienceVialCollected(float value)
    { 
        IncrementExperience(value);
    }
}
