using Godot;
using System;

// Componente que representa uma hitbox com valor de dano
[GlobalClass]
public partial class HitboxComponent : Area2D
{
    // Valor de dano causado pela hitbox
    public float Damage = 0;
}
