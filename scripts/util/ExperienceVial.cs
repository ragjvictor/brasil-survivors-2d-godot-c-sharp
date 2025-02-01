using Godot;
using System;

// Classe que representa um frasco de experiência
public partial class ExperienceVial : Node2D
{
    private Area2D area2D; // Área de detecção de colisão
    private GameEvents gameEvents; // Gerenciador de eventos do jogo

    public override void _Ready()
    {
        area2D = GetNode<Area2D>("Area2D");
        area2D.AreaEntered += OnAreaEntered; // Conecta o sinal de entrada na área
        gameEvents = GetNode<GameEvents>("/root/GameEvents"); // Obtém o gerenciador de eventos
    }

    private void OnAreaEntered(Area2D area2D)
    {   
        gameEvents.EmitExperienceVialCollected(1); // Emite evento de coleta de frasco
        QueueFree(); // Remove o frasco da cena
    }
}
