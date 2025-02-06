using Godot;
using System;

// Classe que controla a habilidade de machado
public partial class AxeAbilityController : Node
{
    [Export]
    public PackedScene axeAbilityScene; // Cena da habilidade de machado

    private float damage = 10; // Dano causado pela habilidade

    private Timer timer; // Temporizador para controlar a habilidade

    public override void _Ready()
    {
        // Inicializa o temporizador e conecta o sinal de timeout
        timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout;
    }

    private void OnTimerTimeout()
    {
        // Obtém o jogador e a camada de primeiro plano
        Node2D player = (Node2D)GetTree().GetFirstNodeInGroup("player");
        if (player == null)
        {
            return;
        }

        Node2D foreground = (Node2D)GetTree().GetFirstNodeInGroup("foreground_layer");
        if (foreground == null)
        {
            return;
        }

        // Instancia a habilidade de machado e define sua posição
        AxeAbility axeInstance = (AxeAbility)axeAbilityScene.Instantiate();
        foreground.AddChild(axeInstance);
        axeInstance.GlobalPosition = player.GlobalPosition;
        axeInstance.hitboxComponent.Damage = damage; // Define o dano da hitbox
    }
}
