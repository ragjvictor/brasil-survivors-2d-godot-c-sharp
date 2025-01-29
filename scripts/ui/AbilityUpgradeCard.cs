using Godot;
using System;

public partial class AbilityUpgradeCard : PanelContainer
{
    private Label nameLabel;
    private Label descriptionLabel;

    public override void _Ready()
    {
        nameLabel = GetNode<Label>("VBoxContainer/NameLabel");
        descriptionLabel = GetNode<Label>("VBoxContainer/DescriptionLabel");
    }

    public void SetAbilityUpgrade(AbilityUpgrade upgrade)
    {
        nameLabel.Text = upgrade.Name;
        descriptionLabel.Text = upgrade.Description;
    }
}
