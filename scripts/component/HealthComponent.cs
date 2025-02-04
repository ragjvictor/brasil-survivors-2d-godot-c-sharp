using Godot;
using System;

// Componente que gerencia a saúde do personagem
[GlobalClass]
public partial class HealthComponent : Node
{
    // Sinal emitido quando a saúde chega a zero
    [Signal]
    public delegate void DiedEventHandler();

    // Sinal emitido quando a saúde muda
    [Signal]
    public delegate void HealthChangedEventHandler();

    // Saúde máxima do personagem
    [Export]
    private float maxHealth = 10;

    // Saúde atual do personagem
    public float currentHealth;

    public override void _Ready()
    {
        // Inicializa a saúde atual com a saúde máxima
        currentHealth = maxHealth;
    }

    // Método para aplicar dano a um personagem
    public void Damage(float damageAmount)
    {
        // Reduz a saúde atual, garantindo que não fique abaixo de zero
        currentHealth = Math.Max(currentHealth - damageAmount, 0);

        EmitSignal(SignalName.HealthChanged);
        // Agenda a chamada do método CheckDeath de forma diferida
        CallDeferred(nameof(CheckDeath));

    }

    // Método para retornar o HP do personagem
    public float GetHelthPercent()
    {
        if (maxHealth <= 0)
        {
            return 0;
        }

        return Math.Min(currentHealth / maxHealth, 1);
    }

    // Método para verificar se um personagem morreu
    public void CheckDeath()
    {
        if (currentHealth == 0)
        {
            // Emite o sinal de morte e remove o personagem
            EmitSignal(SignalName.Died);
            Owner.CallDeferred("queue_free");
        }
    }
}
