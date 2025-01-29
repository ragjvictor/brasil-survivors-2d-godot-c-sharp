using Godot;
using System;
using Godot.Collections;

public partial class UpgradeManager : Node
{
    [Export]
    public Array<AbilityUpgrade> upgradePool = new Array<AbilityUpgrade>();

    [Export]
    public ExperienceManager experienceManager;

    [Export]
    public PackedScene upgradeScreenScene;

    private Dictionary<string, Dictionary<string, Variant>> currentUpgrades = new Dictionary<string, Dictionary<string, Variant>>();

    public override void _Ready()
    {
        experienceManager.LevelUp += OnLevelUp;
    }

    private void OnLevelUp(int currentLevel)
    {
        AbilityUpgrade chosenUpgrade = upgradePool.PickRandom();
        if (chosenUpgrade == null)
        {
            return;
        }

        var upgradeScreenInstance = (UpgradeScreen)upgradeScreenScene.Instantiate();
        AddChild(upgradeScreenInstance);
        upgradeScreenInstance.SetAbilityUpgrade(new Array<AbilityUpgrade> { chosenUpgrade });
    }

    private void ApplyUpgrade(AbilityUpgrade upgrade)
    {
        if (!currentUpgrades.ContainsKey(upgrade.Id))
        {
            currentUpgrades[upgrade.Id] = new Dictionary<string, Variant>
            {
                { "resource", upgrade },
                { "quantity", 1 }
            };
        }
        else
        {
            currentUpgrades[upgrade.Id]["quantity"] = (int)currentUpgrades[upgrade.Id]["quantity"] + 1;
        }
    }
}
