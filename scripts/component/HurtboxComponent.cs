using Godot;
using System;

// Componente que interage com hitboxes para aplicar dano ao componente de saúde
[GlobalClass]
public partial class HurtboxComponent : Area2D
{
    // Componente de saúde associado
    [Export]
    public HealthComponent healthComponent;

    public override void _Ready()
    {
        // Conecta o sinal de entrada de área ao método OnAreaEntered
        AreaEntered += OnAreaEntered;
    }

    // Método chamado quando uma área entra em contato
    private void OnAreaEntered(Area2D area2D)
    {
        // Verifica se a área é uma HitboxComponent
        if (!(area2D is HitboxComponent))
            return;

        // Verifica se o componente de saúde está definido
        if (healthComponent == null)
            return;

        // Aplica o dano ao componente de saúde
        HitboxComponent hitboxComponent = (HitboxComponent)area2D;
        healthComponent.Damage(hitboxComponent.Damage);
    }
}
