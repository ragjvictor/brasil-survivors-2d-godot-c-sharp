using Godot;
using System;

// Gerenciador de inimigos do jogo
public partial class EnemyManager : Node
{
	[Export]
	public PackedScene basicEnemyScene; // Cena do inimigo básico a ser instanciada

	[Export]
	public ArenaTimeManager arenaTimeManager; // Cena do gerenciador de tempo do jogo

	private Double baseSpawnTime = 0;

	private Timer timer; // Timer para controle de spawn

	const int SpawnRadius = 330; // Raio de spawn dos inimigos

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		baseSpawnTime = timer.WaitTime;
		arenaTimeManager.ArenaDificultyIncreased += OnArenaDificultyIncreased;
		timer.Timeout += OnTimerTimeout; // Associa o evento de timeout ao método OnTimerTimeout
	}

	// Método chamado quando o timer atinge o tempo limite
	private void OnTimerTimeout()
	{
		timer.Start();

		Node2D player = (Node2D)GetTree().GetFirstNodeInGroup("player"); // Obtém o jogador
		if (player == null)
		{
			return;
		}

		// Gera uma direção aleatória para o spawn
		Vector2 randomDirection = Vector2.Right.Rotated((float)GD.RandRange(0, Math.Tau));
		Vector2 spawnPosition = player.GlobalPosition + (randomDirection * SpawnRadius); // Calcula a posição de spawn

		Node2D enemy = (Node2D)basicEnemyScene.Instantiate(); // Instancia o inimigo

		Node2D entitiesLayer = (Node2D)GetTree().GetFirstNodeInGroup("entities_layer"); // Obtém o grupo de entidades
		entitiesLayer.AddChild(enemy); // Adiciona o inimigo ao grupo de entidades
		enemy.GlobalPosition = spawnPosition; // Define a posição do inimigo
	}

	private void OnArenaDificultyIncreased(int arenaDificulty)
	{
		float timeOff = (0.1f/12) * arenaDificulty;
		timeOff = Math.Min(timeOff, 0.7f);
		GD.Print(timeOff);
		timer.WaitTime = baseSpawnTime - timeOff;
	}
}