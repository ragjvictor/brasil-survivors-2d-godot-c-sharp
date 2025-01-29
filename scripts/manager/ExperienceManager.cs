using Godot;
using System;
using System.Runtime;

public partial class ExperienceManager : Node
{
    [Signal]
    public delegate void ExperienceUpdatedEventHandler(float currentExperience, float targetExperience);

    [Signal]
    public delegate void LevelUpEventHandler(int newLevel);

    private const float TargetExperienceGrowth = 5;

    private float currentExperience = 0;
    private float currentLevel = 1;
    private float targetExperience = 5;
    private GameEvents gameEvents;

    public override void _Ready()
    {
        gameEvents = GetNode<GameEvents>("/root/GameEvents");
        gameEvents.ExperienceVialCollected += OnExperienceVialCollected;
    }

    private void IncrementExperience(float value)
    {
        currentExperience = Mathf.Min(currentExperience + value, targetExperience);
        EmitSignal(SignalName.ExperienceUpdated, currentExperience, targetExperience);
        if (currentExperience == targetExperience)
        {
            currentLevel += 1;
            targetExperience += TargetExperienceGrowth;
            currentExperience = 0;
            EmitSignal(SignalName.ExperienceUpdated, currentExperience, targetExperience);
            EmitSignal(SignalName.LevelUp, currentLevel);
        }

    }

    private void OnExperienceVialCollected(float value)
    { 
        IncrementExperience(value);
    }
}
