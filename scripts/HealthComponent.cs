using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node
{
    [Signal]
    public delegate void DiedEventHandler();

    [Export]
    private float maxHealth = 10;
    
    private float currentHealth;

    public override void _Ready()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damageAmount)
    {
        currentHealth = Math.Max(currentHealth - damageAmount, 0);
        if (currentHealth == 0)
        {
            EmitSignal(SignalName.Died);
            Owner.CallDeferred("queue_free");
        }
    }
}
