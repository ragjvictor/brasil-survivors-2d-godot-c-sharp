using Godot;
using System;

public partial class AbilityUpgradeCard : PanelContainer
{
    [Signal]
    public delegate void SelectedEventHandler(AbilityUpgrade upgrade);

    private Label nameLabel;
    private Label descriptionLabel;
    private AbilityUpgrade currentUpgrade;

    public override void _Ready()
    {        
        nameLabel = GetNode<Label>("VBoxContainer/NameLabel");
        descriptionLabel = GetNode<Label>("VBoxContainer/DescriptionLabel");
        this.Connect("gui_input", new Callable(this, nameof(OnGuiEvent)));
    }

    public void SetAbilityUpgrade(AbilityUpgrade upgrade)
    {
        currentUpgrade = upgrade;
        nameLabel.Text = upgrade.Name;
        descriptionLabel.Text = upgrade.Description;
    }

    private void OnGuiEvent(InputEvent e)
    {   
        if (e.IsActionPressed("left_click"))
        {
            EmitSignal(SignalName.Selected, currentUpgrade);
        }
    }
}
