using Godot;
using System;
using System.Collections.Generic;

public partial class WorkCardAbilityController : Node
{
    [Export]
    private PackedScene workCardAbility;

    private Timer timer;

    private Double baseWaitTime;

    private float damage = 5;

    const int MaxRange = 150;

    private GameEvents gameEvents;

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout; // Conecta ao sinal de timeout em C#

        baseWaitTime = timer.WaitTime;

        gameEvents = GetNode<GameEvents>("/root/GameEvents");
        gameEvents.AbilityUpgradeAdded += OnAbilityUpgradeAdded;
    }

    private void OnTimerTimeout()
    {
        Node2D player = (Node2D)GetTree().GetFirstNodeInGroup("player");
        if (player == null)
        {
            return;
        }

        var enemyNodes = GetTree().GetNodesInGroup("enemy");
        List<Node2D> enemies = new List<Node2D>();

        // Filtra os inimigos baseado na distancia entre o player
        foreach (Node enemy in enemyNodes)
        {
            if (((Node2D)enemy).GlobalPosition.DistanceSquaredTo(player.GlobalPosition) < Math.Pow(MaxRange, 2))
            {
                enemies.Add((Node2D)enemy);
            }
        }

        if (enemies.Count == 0)
        {
            return;
        }

        // Classifiqua qual inimigo será atacado baseado na distancia mais proxima.
        enemies.Sort((a, b) => 
        {
            float aDistance = a.GlobalPosition.DistanceSquaredTo(player.GlobalPosition);
            float bDistance = b.GlobalPosition.DistanceSquaredTo(player.GlobalPosition);
            return aDistance.CompareTo(bDistance);
        });

        WorkCardAbility workCardInstance = (WorkCardAbility)workCardAbility.Instantiate();

        Node2D foregroundLayer = (Node2D)GetTree().GetFirstNodeInGroup("foreground_layer");
        foregroundLayer.AddChild(workCardInstance);
        workCardInstance.hitboxComponent.Damage = damage;

        workCardInstance.GlobalPosition = enemies[0].GlobalPosition;
        workCardInstance.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0, Math.Tau)) * 4;

        Vector2 enemyDirection = enemies[0].GlobalPosition - workCardInstance.GlobalPosition;
        workCardInstance.Rotation = enemyDirection.Angle();
    }

    private void OnAbilityUpgradeAdded(
        AbilityUpgrade upgrade,
        Godot.Collections.Dictionary<string, Godot.Collections.Dictionary<string, Variant>> currentUpgrades)
    {
        if (upgrade.Id != "work_card_rate")
            return;

        // Cada "work_card_rate" aumenta a redução em 10%
        float percentReduction = (float)currentUpgrades["work_card_rate"]["quantity"] * 0.1f;

        // Limite a redução para não exceder 99%
        // (impede que chegue a 100% ou mais e vire 0 ou negativo)
        if (percentReduction >= 1f)
            percentReduction = 0.99f;

        // Calcule o novo tempo e garanta que nunca seja menor que 0.1
        float newWaitTime = (float)baseWaitTime * (1 - percentReduction);
        if (newWaitTime < 0.1f)
            newWaitTime = 0.1f;

        timer.WaitTime = newWaitTime;
        timer.Start();
    }
}
