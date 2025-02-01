using Godot;
using System;

// Classe que controla o comportamento do jogador
public partial class PlayerController : CharacterBody2D
{
    const float MaxSpeed = 125.0f;  // Velocidade máxima do jogador
    const float AccelarationSmoothing = 25; // Suavização da aceleração

    private int numberCollidingBodies = 0;  // variável do numero de corpos em colisão
    private AnimatedSprite2D animationPlayer;   // Referência ao sprite animado do jogado
    private Area2D collisionArea2D; // Referência ao area de recebimento de danos do Player
    private HealthComponent healthComponent;    // Referência ao componente de vida (HP)
    private Timer damageIntervalTimer;  // Referência ao timer do intervalo de recebimento de dano do Player
    private ProgressBar healthBar;  // Referência a barra de HP do personagem

    public override void _Ready()
    {
        // Obtém o nó do sprite animado
        animationPlayer = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animationPlayer.Stop(); // Para a animação
        animationPlayer.Play("idle"); // Inicia a animação de "idle" 

        healthBar = GetNode<ProgressBar>("HealthBar");
        healthComponent = GetNode<HealthComponent>("HealthComponent");
        collisionArea2D = GetNode<Area2D>("CollisionArea2D");
        damageIntervalTimer = GetNode<Timer>("DamageIntervalTimer");

        collisionArea2D.BodyEntered += OnBodyEntered; // Conecta o evento de entrada de colisão com o método OnBodyEntered
        collisionArea2D.BodyExited += OnBodyExited; // Conecta o evento de saída de colisão com o método OnBodyExited         
        damageIntervalTimer.Timeout += OnDamageIntervalTimerTimeout; // Conecta o evento do fim do Timer ao método OnDamageIntervalTimerTimeout
        healthComponent.HealthChanged += OnHealthChanged; // Conecta o evento de Mudança de HP com o método OnHealthChanged

        UpdateHealthDisplay(); // Chama o método no inicia para iniciar com a vida padrão
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 movementVector = GetMovementVector();
        Vector2 direction = movementVector.Normalized(); // Normaliza a direção
        Vector2 targetVelocity = direction * MaxSpeed; // Calcula a velocidade alvo

        // Suaviza a transição da velocidade atual para a velocidade alvo
        Velocity = Velocity.Lerp(targetVelocity, (float)(1 - Math.Exp(-delta * AccelarationSmoothing)));
        
        MoveAndSlide(); // Move o jogador
    }

    // Método que obtém o vetor de movimento baseado na entrada do jogador
    private Vector2 GetMovementVector()
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

        return new Vector2(movementX, movementY); // Retorna o vetor de movimento
    }

    // Método de checagem e aplicação do Dano que o Player recebeu
    private void CheckDealDamage()
    {
        if (numberCollidingBodies == 0 || !damageIntervalTimer.IsStopped())
        {
            return;
        }
       
        healthComponent.Damage(1);
        damageIntervalTimer.Start();
    }

    // Método que atualiza a barra da vida
    private void UpdateHealthDisplay()
    {
        healthBar.Value = healthComponent.GetHelthPercent();
    }

    // Método chamado quando um corpo entra na área de colisão
    private void OnBodyEntered(Node2D otherBody)
    {
        numberCollidingBodies += 1;
        CheckDealDamage();
    }

    // Método chamado quando um corpo sai da área de colisão
    private void OnBodyExited(Node2D otherBody)
    {
        numberCollidingBodies -= 1;
    }

    // Método chamado quando Timer de intervalo de dano termina
    private void OnDamageIntervalTimerTimeout()
    {
        CheckDealDamage();
    }

    // Método chamado quando o HP altera
    private void OnHealthChanged()
    {
        UpdateHealthDisplay();
    }
}
