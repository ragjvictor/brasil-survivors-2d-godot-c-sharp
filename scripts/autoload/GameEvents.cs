using Godot;
using System;

[GlobalClass]
public partial class GameEvents : Node
{
    [Signal]
    public delegate void ExperienceVialCollectedEventHandler(float number);

    public void EmitExperienceVialCollected(float value)
    {
        EmitSignal(SignalName.ExperienceVialCollected, value);
    }
}