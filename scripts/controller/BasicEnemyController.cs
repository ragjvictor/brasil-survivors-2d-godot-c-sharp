using Godot;
using System;

// Classe que controla o comportamento do inimigo básico
public partial class BasicEnemyController : CharacterBody2D
{
    // Velocidade máxima do inimigo
    const float MaxSpeed = 50.0f;

    private Area2D area2D; // Área de detecção do inimigo
    private AnimatedSprite2D animationEnemy; // Sprite animado do inimigo
    private HealthComponent healthComponent; // Componente de saúde do inimigo

    // Método chamado quando o nó está pronto
    public override void _Ready()
    {
        // Obtém o componente de saúde
        healthComponent = GetNode<HealthComponent>("HealthComponent");

        // Obtém o sprite animado e inicia a animação
        animationEnemy = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animationEnemy.Stop();
        animationEnemy.Play("run");
    }

    // Método chamado a cada frame de física
    public override void _PhysicsProcess(double delta)
    {
        // Obtém a direção para o jogador
        Vector2 direction = GetDirectionToPlayer();
        Velocity = direction * MaxSpeed; // Define a velocidade do inimigo

        // Verifica a direção e ajusta a animação
        if (direction.X > 0)
        {
            animationEnemy.FlipH = false; // Inimigo se movendo para a direita
        }
        else
        {
            animationEnemy.FlipH = true; // Inimigo se movendo para a esquerda
        }

        MoveAndSlide(); // Move o inimigo
    }

    // Método para obter a direção em relação ao jogador
    private Vector2 GetDirectionToPlayer()
    {
        Node2D player = (Node2D)GetTree().GetFirstNodeInGroup("player");

        if (player != null)
        {
            // Retorna a direção normalizada para o jogador
            return (player.GlobalPosition - GlobalPosition).Normalized();
        }

        return Vector2.Zero; // Retorna vetor nulo se o jogador não for encontrado
    }
}
