using Godot;
using System;
using Godot.Collections;

public partial class UpgradeManager : Node
{
    [Export]
    public Array<AbilityUpgrade> upgradePool = new Array<AbilityUpgrade>();

    [Export]
    public ExperienceManager experienceManager;

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

        if (!currentUpgrades.ContainsKey(chosenUpgrade.Id))
        {
            currentUpgrades[chosenUpgrade.Id] = new Dictionary<string, Variant>
            {
                { "resource", chosenUpgrade },
                { "quantity", 1 }
            };
        }
        else
        {
            currentUpgrades[chosenUpgrade.Id]["quantity"] = (int)currentUpgrades[chosenUpgrade.Id]["quantity"] + 1;
        }
    }
}
