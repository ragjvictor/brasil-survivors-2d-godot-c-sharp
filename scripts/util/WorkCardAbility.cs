using Godot;
using System;

// Classe que representa uma habilidade de cartão de trabalho
[GlobalClass]
public partial class WorkCardAbility : Node2D
{
    public HitboxComponent hitboxComponent; // Componente de hitbox associado

    public override void _Ready()
    {
        hitboxComponent = GetNode<HitboxComponent>("HitboxComponent"); // Obtém o componente de hitbox
    }
}
