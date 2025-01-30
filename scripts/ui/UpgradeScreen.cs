using Godot;
using System;
using Godot.Collections;

public partial class UpgradeScreen : CanvasLayer
{
    [Signal]
    public delegate void UpgradeSelectedEventHandler(AbilityUpgrade upgrade);
    
    [Export]
    public PackedScene upgradeCardScene;

    private HBoxContainer cardContainer;

    public override void _Ready()
    {
        cardContainer = GetNode<HBoxContainer>("MarginContainer/CardContainer");
        GetTree().Paused = true;
    }

    public void SetAbilityUpgrade(Array<AbilityUpgrade> upgrades)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            AbilityUpgradeCard cardInstance = (AbilityUpgradeCard)upgradeCardScene.Instantiate();
            cardContainer.AddChild(cardInstance);
            cardInstance.SetAbilityUpgrade(upgrades[i]);

            cardInstance.Selected += OnUpgradeSelected;
        }
    }

    private void OnUpgradeSelected(AbilityUpgrade upgrade)
    {
        EmitSignal(SignalName.UpgradeSelected, upgrade);
        GetTree().Paused = false;
        QueueFree();
    }
}
