using Godot;
using System;
using System.Collections.Generic;

// Classe que controla as habilidades do cartão de trabalho
public partial class WorkCardAbilityController : Node
{
    [Export]
    private PackedScene workCardAbility; // Cena do cartão de trabalho

    private Timer timer; // Timer para controlar o tempo entre habilidades
    private Double baseWaitTime; // Tempo base de espera entre habilidades
    private float damage = 5; // Dano causado pelas habilidades
    const int MaxRange = 150; // Distância máxima para atacar inimigos

    private GameEvents gameEvents; // Referência aos eventos do jogo

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout; // Conecta ao sinal de timeout

        baseWaitTime = timer.WaitTime; // Armazena o tempo base de espera

        gameEvents = GetNode<GameEvents>("/root/GameEvents"); // Obtém o nó de eventos do jogo
        gameEvents.AbilityUpgradeAdded += OnAbilityUpgradeAdded; // Conecta ao sinal de atualização de habilidade
    }

    public override void _ExitTree()
    {
        // Verifica se o nó GameEvents ainda existe para evitar possíveis erros
        GameEvents gameEvents = GetNodeOrNull<GameEvents>("/root/GameEvents");
        if (gameEvents != null)
        {
            gameEvents.AbilityUpgradeAdded -= OnAbilityUpgradeAdded;
        }
    }

    // Método chamado quando o timer atinge o tempo limite
    private void OnTimerTimeout()
    {
        Node2D player = (Node2D)GetTree().GetFirstNodeInGroup("player"); // Obtém o jogador
        if (player == null)
        {
            return;
        }

        var enemyNodes = GetTree().GetNodesInGroup("enemy"); // Obtém todos os inimigos
        List<Node2D> enemies = new List<Node2D>(); // Lista para armazenar inimigos dentro do alcance

        // Filtra os inimigos baseado na distância entre o jogador
        foreach (Node enemy in enemyNodes)
        {
            if (((Node2D)enemy).GlobalPosition.DistanceSquaredTo(player.GlobalPosition) < Math.Pow(MaxRange, 2))
            {
                enemies.Add((Node2D)enemy); // Adiciona inimigo à lista se estiver dentro do alcance
            }
        }

        if (enemies.Count == 0)
        {
            return; // Retorna se não houver inimigos dentro do alcance
        }

        // Classifica qual inimigo será atacado baseado na distância mais próxima
        enemies.Sort((a, b) =>
        {
            float aDistance = a.GlobalPosition.DistanceSquaredTo(player.GlobalPosition);
            float bDistance = b.GlobalPosition.DistanceSquaredTo(player.GlobalPosition);
            return aDistance.CompareTo(bDistance);
        });

        WorkCardAbility workCardInstance = (WorkCardAbility)workCardAbility.Instantiate(); // Instancia a habilidade

        Node2D foregroundLayer = (Node2D)GetTree().GetFirstNodeInGroup("foreground_layer"); // Obtém a camada de primeiro plano
        foregroundLayer.AddChild(workCardInstance); // Adiciona a habilidade à camada
        workCardInstance.hitboxComponent.Damage = damage; // Define o dano da habilidade

        workCardInstance.GlobalPosition = enemies[0].GlobalPosition; // Define a posição da habilidade
        workCardInstance.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0, Math.Tau)) * 4; // Adiciona um pequeno deslocamento aleatório

        Vector2 enemyDirection = enemies[0].GlobalPosition - workCardInstance.GlobalPosition; // Calcula a direção do inimigo
        workCardInstance.Rotation = enemyDirection.Angle(); // Define a rotação da habilidade
    }

    // Método chamado quando uma atualização de habilidade é adicionada
    private void OnAbilityUpgradeAdded(
        AbilityUpgrade upgrade,
        Godot.Collections.Dictionary<string, Godot.Collections.Dictionary<string, Variant>> currentUpgrades)
    {
        if (upgrade.Id != "work_card_rate")
            return; // Retorna se a atualização não for do tipo "work_card_rate"

        // Cada "work_card_rate" aumenta a redução em 10%
        float percentReduction = (float)currentUpgrades["work_card_rate"]["quantity"] * 0.1f;

        // Limita a redução para não exceder 99%
        if (percentReduction >= 1f)
            percentReduction = 0.99f;

        // Calcula o novo tempo e garante que nunca seja menor que 0.1
        float newWaitTime = (float)baseWaitTime * (1 - percentReduction);
        if (newWaitTime < 0.1f)
            newWaitTime = 0.1f;

        timer.WaitTime = newWaitTime; // Atualiza o tempo de espera do timer
        timer.Start(); // Reinicia o timer
    }
}
