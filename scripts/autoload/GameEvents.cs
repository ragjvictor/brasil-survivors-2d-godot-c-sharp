using Godot;
using Godot.Collections;
using System;

[GlobalClass]
// Classe que gerencia os eventos do jogo
public partial class GameEvents : Node
{
    // Sinal emitido quando um frasco de experiência é coletado
    [Signal]
    public delegate void ExperienceVialCollectedEventHandler(float number);

    // Sinal emitido quando um upgrade de habilidade é adicionado
    [Signal]
    public delegate void AbilityUpgradeAddedEventHandler(
        AbilityUpgrade upgrade,
        Dictionary<string, Dictionary<string, Variant>> currentUpgrades
    );

    // Método para emitir o sinal de coleta de frasco de experiência
    public void EmitExperienceVialCollected(float value)
    {
        EmitSignal(SignalName.ExperienceVialCollected, value); // Emite o sinal com o valor coletado
    }

    // Método para emitir o sinal de adição de upgrade de habilidade
    public void EmitAbilityUpgradeAdded(AbilityUpgrade upgrade, Dictionary<string, Dictionary<string, Variant>> currentUpgrades)
    {
        EmitSignal(SignalName.AbilityUpgradeAdded, upgrade, currentUpgrades); // Emite o sinal com o upgrade e os upgrades atuais
    }
}
