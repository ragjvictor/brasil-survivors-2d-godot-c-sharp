using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public partial class WorkCardAbilityController : Node
{
    [Export]
    public PackedScene WorkCardAbility;

    private Timer timer;

    private float damage = 5;

    const int MaxRange = 150;

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout; // Conecta ao sinal de timeout em C#
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

        WorkCardAbility workCardInstance = (WorkCardAbility)WorkCardAbility.Instantiate();
        player.GetParent().AddChild(workCardInstance);
        workCardInstance.HitboxComponent.Damage = damage;

        workCardInstance.GlobalPosition = enemies[0].GlobalPosition;
        workCardInstance.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0, Math.Tau)) * 4;

        Vector2 enemyDirection = enemies[0].GlobalPosition - workCardInstance.GlobalPosition;
        workCardInstance.Rotation = enemyDirection.Angle();
    }
}
