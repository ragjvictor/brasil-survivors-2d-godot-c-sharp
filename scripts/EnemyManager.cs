using Godot;
using System;

public partial class EnemyManager : Node
{
	[Export]
	public PackedScene basicEnemyScene;

	private Timer timer;

	const int SpawnRadius = 330;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.Timeout += OnTimerTimeout;
	}

	private void OnTimerTimeout()
	{
		Node2D player = (Node2D)GetTree().GetFirstNodeInGroup("player");
		if (player == null)
		{
			return;
		}

		Vector2 randomDirection = Vector2.Right.Rotated((float)GD.RandRange(0, Math.Tau));
		Vector2 spawnPosition = player.GlobalPosition + (randomDirection * SpawnRadius);

		Node2D enemy = (Node2D)basicEnemyScene.Instantiate();
		GetParent().AddChild(enemy);
		enemy.GlobalPosition = spawnPosition;


	}

}
