using Godot;
using System;
using System.Numerics;

// Classe que controla o comportamento do jogador
public partial class PlayerController : CharacterBody2D
{
    // Velocidade máxima do jogador
    const float MaxSpeed = 125.0f;
    // Suavização da aceleração
    const float AccelarationSmoothing = 25;

    // Referência ao sprite animado do jogador
    private AnimatedSprite2D animationPlayer;

    // Método chamado quando o nó está pronto
    public override void _Ready()
    {
        // Obtém o nó do sprite animado
        animationPlayer = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animationPlayer.Stop(); // Para a animação
        animationPlayer.Play("idle"); // Inicia a animação de "idle"        
    }

    // Método chamado a cada frame de física
    public override void _PhysicsProcess(double delta)
    {
        // Obtém o vetor de movimento
        Godot.Vector2 movementVector = GetMovementVector();
        Godot.Vector2 direction = movementVector.Normalized(); // Normaliza a direção
        Godot.Vector2 targetVelocity = direction * MaxSpeed; // Calcula a velocidade alvo

        // Suaviza a transição da velocidade atual para a velocidade alvo
        Velocity = Velocity.Lerp(targetVelocity, (float)(1 - Math.Exp(-delta * AccelarationSmoothing)));
        
        MoveAndSlide(); // Move o jogador
    }

    // Método que obtém o vetor de movimento baseado na entrada do jogador
    private Godot.Vector2 GetMovementVector()
    {
        // Obtém a entrada do jogador para o movimento
        float movementX = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        float movementY = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        
        // Verifica se há movimento
        if (movementX != 0.0 || movementY != 0.0)
        {
            animationPlayer.Play("run"); // Toca a animação de corrida
            // Verifica a direção do movimento para flipar o sprite
            if (movementX > 0) {
                animationPlayer.FlipH = false; // Direita
            }
            else
            {
                animationPlayer.FlipH = true; // Esquerda
            }            
        }
        else
        {
            animationPlayer.Play("idle"); // Toca a animação de "idle"
        }

        return new Godot.Vector2(movementX, movementY); // Retorna o vetor de movimento
    }
}
