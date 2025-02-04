using Godot;
using Godot.Collections;
using System;
using System.Linq;

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

	private Vector2 GetSpawnPosition()
	{
		Node2D player = (Node2D)GetTree().GetFirstNodeInGroup("player"); // Obtém o jogador
		if (player == null)
		{
			return Vector2.Zero;
		}

		Vector2 spawnPosition = Vector2.Zero;
		Vector2 randomDirection = Vector2.Right.Rotated((float)GD.RandRange(0, Math.Tau));


		// Loop para buscar uma área de spawn válida para os inimigos
		foreach (int i in Enumerable.Range(0, 4))
		{
			// Gera uma direção aleatória para o spawn
			spawnPosition = player.GlobalPosition + (randomDirection * SpawnRadius); // Calcula a posição de spawn

			// Verifica se a posição do spawn não está em conflito com as areas de colisão do layer do Terreno
			var queryParameters = PhysicsRayQueryParameters2D.Create(player.GlobalPosition, spawnPosition, 1);
			Dictionary result = GetTree().Root.World2D.DirectSpaceState.IntersectRay(queryParameters);

			if (result.Count == 0)
			{
				break;
			}
			else
			{
				randomDirection = randomDirection.Rotated(Mathf.DegToRad(90));
			}
		}

		return spawnPosition;

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


		Node2D enemy = (Node2D)basicEnemyScene.Instantiate(); // Instancia o inimigo

		Node2D entitiesLayer = (Node2D)GetTree().GetFirstNodeInGroup("entities_layer"); // Obtém o grupo de entidades
		entitiesLayer.AddChild(enemy); // Adiciona o inimigo ao grupo de entidades
		enemy.GlobalPosition = GetSpawnPosition(); // Define a posição do inimigo
	}

	private void OnArenaDificultyIncreased(int arenaDificulty)
	{
		float timeOff = (0.1f / 12) * arenaDificulty;
		timeOff = Math.Min(timeOff, 0.7f);
		timer.WaitTime = baseSpawnTime - timeOff;
	}
}