using Godot;
using System;

// Componente que gerencia a saúde do personagem
[GlobalClass]
public partial class HealthComponent : Node
{
    // Sinal emitido quando a saúde chega a zero
    [Signal]
    public delegate void DiedEventHandler();

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

    // Método para aplicar dano ao personagem
    public void Damage(float damageAmount)
    {
        // Reduz a saúde atual, garantindo que não fique abaixo de zero
        currentHealth = Math.Max(currentHealth - damageAmount, 0);
        if (currentHealth == 0)
        {
            // Emite o sinal de morte e remove o personagem
            EmitSignal(SignalName.Died);
            Owner.CallDeferred("queue_free");
        }
    }
}
