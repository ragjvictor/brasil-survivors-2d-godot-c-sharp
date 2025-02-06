using Godot;
using System;

// Classe que representa a habilidade do machado
[GlobalClass]
public partial class AxeAbility : Node2D
{
    const float MaxRadius = 100; // Raio máximo da habilidade

    public HitboxComponent hitboxComponent; // Componente de hitbox associado à habilidade

    private Vector2 baseRotation = Vector2.Right; // Rotação base da habilidade

    public override void _Ready()
    {
        // Inicializa a rotação base aleatoriamente
        baseRotation = Vector2.Right.Rotated((float)GD.RandRange(0, Math.Tau));

        // Obtém o componente de hitbox
        hitboxComponent = GetNode<HitboxComponent>("HitboxComponent");

        // Cria um tween para animar a habilidade
        Tween tween = CreateTween();
        tween.TweenMethod(new Callable(this, nameof(SetupTweenMethod)), 0.0, 2.0, 3);
        tween.TweenCallback(Callable.From(QueueFree)); // Elimina o machado da cena no final da rotação
    }

    private void SetupTweenMethod(float rotations)
    {
        // Configura a posição da habilidade com base na rotação
        float percent = rotations / 2;
        float currentRadius = percent * MaxRadius;
        Vector2 currentDirection = baseRotation.Rotated((float)(rotations * Math.Tau));

        Vector2 rootPosition = Vector2.Zero;
        Node2D player = (Node2D)GetTree().GetFirstNodeInGroup("player");
        if (player == null)
        {
            return;
        }

        // Define a posição global da habilidade
        GlobalPosition = player.GlobalPosition + (currentDirection * currentRadius);
    }
}
