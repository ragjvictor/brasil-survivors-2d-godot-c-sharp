using Godot;
using System;
using Godot.Collections;
using System.Linq;

// Gerenciador de upgrades do jogo
public partial class UpgradeManager : Node
{
    [Export]
    public Array<AbilityUpgrade> upgradePool = new Array<AbilityUpgrade>(); // Pool de upgrades disponíveis

    [Export]
    public ExperienceManager experienceManager; // Gerenciador de experiência associado

    [Export]
    public PackedScene upgradeScreenScene; // Cena da tela de upgrades

    // Dicionário de upgrades atuais
    private readonly Dictionary<string, Dictionary<string, Variant>> currentUpgrades = new Dictionary<string, Dictionary<string, Variant>>();

    private GameEvents gameEvents; // Eventos do jogo

    public override void _Ready()
    {   
        gameEvents = GetNode<GameEvents>("/root/GameEvents"); // Inicializa os eventos do jogo
        experienceManager.LevelUp += OnLevelUp; // Associa o evento de subida de nível ao método OnLevelUp
    }

    // Método para aplicar um upgrade
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

        gameEvents.EmitAbilityUpgradeAdded(upgrade, currentUpgrades);
    }

    private Array<AbilityUpgrade> PickUpgrades()
    {
        Array<AbilityUpgrade> chosenUpgrades = new Array<AbilityUpgrade>();
        Array<AbilityUpgrade> filteredUpgrades = upgradePool.Duplicate();
        if (filteredUpgrades == null)
        {
            filteredUpgrades = new Array<AbilityUpgrade>();
        }

        foreach (int i in Enumerable.Range(0, 2))
        {
            if (filteredUpgrades.Count == 0)
            {
                break;
            }

            // Escolhe um upgrade aleatório
            AbilityUpgrade chosenUpgrade = filteredUpgrades.PickRandom();
            chosenUpgrades.Add(chosenUpgrade);

            // Filtra os upgrades removendo aqueles com o mesmo id do upgrade escolhido
            Array<AbilityUpgrade> newFilteredUpgrades = new Array<AbilityUpgrade>();
            foreach (AbilityUpgrade upgrade in filteredUpgrades)
            {
                if (!upgrade.Id.Equals(chosenUpgrade.Id))
                {
                    newFilteredUpgrades.Add(upgrade);
                }
            }
            filteredUpgrades = newFilteredUpgrades;
        }

        return chosenUpgrades;
    }

    // Método chamado quando um upgrade é selecionado
    private void OnUpgradeSelected(AbilityUpgrade upgrade)
    {
        ApplyUpgrade(upgrade);
    }

    // Método chamado quando o jogador sobe de nível
    private void OnLevelUp(int currentLevel)
    {
        var upgradeScreenInstance = (UpgradeScreen)upgradeScreenScene.Instantiate(); // Instancia a tela de upgrades
        AddChild(upgradeScreenInstance); // Adiciona a tela de upgrades como filho
        Array<AbilityUpgrade> chosenUpgrades = PickUpgrades();
        upgradeScreenInstance.SetAbilityUpgrade(chosenUpgrades); // Define o upgrade escolhido
        upgradeScreenInstance.UpgradeSelected += OnUpgradeSelected; // Associa o evento de seleção de upgrade
    }
}

