using Godot;
using System;
using System.Runtime;

// Gerenciador de experiência do jogo
public partial class ExperienceManager : Node
{
    // Sinal emitido quando a experiência é atualizada
    [Signal]
    public delegate void ExperienceUpdatedEventHandler(float currentExperience, float targetExperience);

    // Sinal emitido quando o jogador sobe de nível
    [Signal]
    public delegate void LevelUpEventHandler(int newLevel);

    private const float TargetExperienceGrowth = 5; // Crescimento da experiência alvo

    private float currentExperience = 0; // Experiência atual
    private float currentLevel = 1; // Nível atual
    private float targetExperience = 1; // Experiência alvo
 
    public override void _Ready()
    {
        GameEvents gameEvents = GetNode<GameEvents>("/root/GameEvents"); // Obtém o nó de eventos do jogo
        gameEvents.ExperienceVialCollected += OnExperienceVialCollected; // Associa o evento de coleta de frasco de experiência
    }

    public override void _ExitTree()
    {
        // Verifica se o nó GameEvents ainda existe para evitar possíveis erros
        GameEvents gameEvents = GetNodeOrNull<GameEvents>("/root/GameEvents");
        if (gameEvents != null)
        {
            gameEvents.ExperienceVialCollected -= OnExperienceVialCollected;
        }
    }

    // Método para incrementar a experiência
    private void IncrementExperience(float value)
    {
        currentExperience = Mathf.Min(currentExperience + value, targetExperience); // Incrementa a experiência atual
        EmitSignal(SignalName.ExperienceUpdated, currentExperience, targetExperience); // Emite sinal de atualização de experiência
        // Verifica se a experiência atual atingiu a experiência alvo
        if (currentExperience == targetExperience)
        {
            currentLevel += 1; // Aumenta o nível
            targetExperience += TargetExperienceGrowth; // Aumenta a experiência alvo
            currentExperience = 0; // Reseta a experiência atual
            EmitSignal(SignalName.ExperienceUpdated, currentExperience, targetExperience); // Emite sinal de atualização de experiência
            EmitSignal(SignalName.LevelUp, currentLevel); // Emite sinal de subida de nível
        }
    }

    // Método chamado quando um frasco de experiência é coletado
    private void OnExperienceVialCollected(float value)
    { 
        IncrementExperience(value);
    }
}
