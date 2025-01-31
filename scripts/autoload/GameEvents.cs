using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class GameEvents : Node
{
    [Signal]
    public delegate void ExperienceVialCollectedEventHandler(float number);

    [Signal]
    public delegate void AbilityUpgradeAddedEventHandler(
        AbilityUpgrade upgrade, 
        Dictionary<string, Dictionary<string, Variant>> currentUpgrades
    );

    public void EmitExperienceVialCollected(float value)
    {
        EmitSignal(SignalName.ExperienceVialCollected, value);
    }

    public void EmitAbilityUpgradeAdded(AbilityUpgrade upgrade, Dictionary<string, Dictionary<string, Variant>> currentUpgrades)
    {
        EmitSignal(SignalName.AbilityUpgradeAdded, upgrade, currentUpgrades);
    }
}