using Godot;
using System;
using Godot.Collections;

// Classe que gerencia a tela de upgrades
public partial class UpgradeScreen : CanvasLayer
{
    // Sinal emitido quando um upgrade é selecionado
    [Signal]
    public delegate void UpgradeSelectedEventHandler(AbilityUpgrade upgrade);

    [Export]
    public PackedScene upgradeCardScene; // Cena do cartão de upgrade

    private HBoxContainer cardContainer; // Contêiner para os cartões de upgrade

    public override void _Ready()
    {
        cardContainer = GetNode<HBoxContainer>("%CardContainer"); // Obtém o contêiner de cartões
        GetTree().Paused = true; // Pausa a árvore de nós
    }

    // Método para definir os upgrades de habilidade
    public void SetAbilityUpgrade(Array<AbilityUpgrade> upgrades)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            AbilityUpgradeCard cardInstance = (AbilityUpgradeCard)upgradeCardScene.Instantiate(); // Instancia o cartão de upgrade
            cardContainer.AddChild(cardInstance); // Adiciona o cartão ao contêiner
            cardInstance.SetAbilityUpgrade(upgrades[i]); // Define o upgrade no cartão

            cardInstance.Selected += OnUpgradeSelected; // Associa o evento de seleção ao método OnUpgradeSelected
        }
    }

    // Método chamado quando um upgrade é selecionado
    private void OnUpgradeSelected(AbilityUpgrade upgrade)
    {
        EmitSignal(SignalName.UpgradeSelected, upgrade); // Emite sinal de upgrade selecionado
        GetTree().Paused = false; // Despausa a árvore de nós
        QueueFree(); // Remove a tela de upgrades
    }
}
