using Godot;
using System;

// Classe que representa um cartão de upgrade de habilidade
public partial class AbilityUpgradeCard : PanelContainer
{
    // Sinal emitido quando o cartão é selecionado
    [Signal]
    public delegate void SelectedEventHandler(AbilityUpgrade upgrade);

    private Label nameLabel; // Rótulo para o nome do upgrade
    private Label descriptionLabel; // Rótulo para a descrição do upgrade
    private AbilityUpgrade currentUpgrade; // Upgrade atual associado ao cartão

    public override void _Ready()
    {        
        nameLabel = GetNode<Label>("%NameLabel");
        descriptionLabel = GetNode<Label>("%DescriptionLabel");
        this.Connect("gui_input", new Callable(this, nameof(OnGuiEvent))); // Conecta o evento de entrada do GUI
    }

    // Método para definir o upgrade de habilidade no cartão
    public void SetAbilityUpgrade(AbilityUpgrade upgrade)
    {
        currentUpgrade = upgrade; // Define o upgrade atual
        nameLabel.Text = upgrade.Name; // Define o texto do nome do Upgrade
        descriptionLabel.Text = upgrade.Description; // Define o texto da descrição do Upgrade
    }

    // Método chamado quando há um evento de entrada no GUI
    private void OnGuiEvent(InputEvent e)
    {   
        if (e.IsActionPressed("left_click")) // Verifica se o botão esquerdo do mouse foi pressionado
        {
            EmitSignal(SignalName.Selected, currentUpgrade); // Emite sinal de seleção com o upgrade atual
        }
    }
}
