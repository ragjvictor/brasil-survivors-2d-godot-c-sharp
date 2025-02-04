using Godot;
using System;
using Godot.Collections;

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
    private Dictionary<string, Dictionary<string, Variant>> currentUpgrades = new Dictionary<string, Dictionary<string, Variant>>();

    private GameEvents gameEvents; // Eventos do jogo

    public override void _Ready()
    {
        gameEvents = GetNode<GameEvents>("/root/GameEvents"); // Inicializa os eventos do jogo
        experienceManager.LevelUp += OnLevelUp; // Associa o evento de subida de nível ao método OnLevelUp
    }

    // Método chamado quando o jogador sobe de nível
    private void OnLevelUp(int currentLevel)
    {
        AbilityUpgrade chosenUpgrade = upgradePool.PickRandom(); // Escolhe um upgrade aleatório dentro do pool de upgrades
        if (chosenUpgrade == null)
        {
            return;
        }

        var upgradeScreenInstance = (UpgradeScreen)upgradeScreenScene.Instantiate(); // Instancia a tela de upgrades
        AddChild(upgradeScreenInstance); // Adiciona a tela de upgrades como filho
        upgradeScreenInstance.SetAbilityUpgrade(new Array<AbilityUpgrade> { chosenUpgrade }); // Define o upgrade escolhido
        upgradeScreenInstance.UpgradeSelected += OnUpgradeSelected; // Associa o evento de seleção de upgrade
    }

    // Método para aplicar um upgrade
    private void ApplyUpgrade(AbilityUpgrade upgrade)
    {
        if (!currentUpgrades.ContainsKey(upgrade.Id))
        {
            currentUpgrades[upgrade.Id] = new Dictionary<string, Variant>
            {
                { "resource", upgrade }, // Armazena o recurso do upgrade
                { "quantity", 1 } // Inicializa a quantidade
            };
        }
        else
        {
            currentUpgrades[upgrade.Id]["quantity"] = (int)currentUpgrades[upgrade.Id]["quantity"] + 1; // Incrementa a quantidade
        }

        gameEvents.EmitAbilityUpgradeAdded(upgrade, currentUpgrades); // Emite evento de upgrade adicionado
    }

    // Método chamado quando um upgrade é selecionado
    private void OnUpgradeSelected(AbilityUpgrade upgrade)
    {
        ApplyUpgrade(upgrade);
    }
}
