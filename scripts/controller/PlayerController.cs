using Godot;
using System;

// Classe que controla o comportamento do jogador
public partial class PlayerController : CharacterBody2D
{
    // Velocidade máxima do jogador
    const float MaxSpeed = 125.0f;
    // Suavização da aceleração
    const float AccelarationSmoothing = 25;

    // variável do numero de corpos em colisão
    private int numberCollidingBodies = 0;

    // Referência ao sprite animado do jogador
    private AnimatedSprite2D animationPlayer;

    // Referência ao area de recebimento de danos do Player
    private Area2D collisionArea2D;

    // Referência ao componente de vida (HP)
    private HealthComponent healthComponent;
    // Referência ao timer do intervalo de recebimento de dano do Player
    private Timer damageIntervalTimer;

    // Método chamado quando o nó está pronto
    public override void _Ready()
    {
        // Obtém o nó do sprite animado
        animationPlayer = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animationPlayer.Stop(); // Para a animação
        animationPlayer.Play("idle"); // Inicia a animação de "idle" 

        // Obtém o nó do componente de vida do Player
        healthComponent = GetNode<HealthComponent>("HealthComponent");
        // Obtém o nó do Timer que representa o intervalo de tempo entre os danos recebidos
        damageIntervalTimer = GetNode<Timer>("DamageIntervalTimer");
        // Conecta o evento do fim do Timer ao método OnDamageIntervalTimerTimeout
        damageIntervalTimer.Timeout += OnDamageIntervalTimerTimeout;

        // Obtém o nó da area de colisão 2D
        collisionArea2D = GetNode<Area2D>("CollisionArea2D");
        // Conecta o evento de entrada de colisão com o método OnBodyEntered
        collisionArea2D.BodyEntered += OnBodyEntered;
        // Conecta o evento de saída de colisão com o método OnBodyExited
        collisionArea2D.BodyExited += OnBodyExited;
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

    // Método de checagem e aplicação do Dano que o Player recebeu
    private void CheckDealDamage()
    {
        if (numberCollidingBodies == 0 || !damageIntervalTimer.IsStopped())
        {
            return;
        }
       
        healthComponent.Damage(1);
        damageIntervalTimer.Start();
        GD.Print(healthComponent.currentHealth);

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
}
