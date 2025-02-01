using Godot;
using System;

// Gerenciador de inimigos do jogo
public partial class EnemyManager : Node
{
	[Export]
	public PackedScene basicEnemyScene; // Cena do inimigo básico a ser instanciada

	[Export]
	public ExperienceManager experienceManager; // Gerenciador de experiência do jogo

	private Timer timer; // Timer para controle de spawn

	const int SpawnRadius = 330; // Raio de spawn dos inimigos

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");

		experienceManager.LevelUp += OnLevelUpPlayer; // Conecta o evento de level up do jogador

		timer.Timeout += OnTimerTimeout; // Associa o evento de timeout ao método OnTimerTimeout
	}

	// Método chamado quando o timer atinge o tempo limite
	private void OnTimerTimeout()
	{
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

	// Método chamado quando o jogador sobe de level
	private void OnLevelUpPlayer(int currentLevel)
	{
		if (timer.WaitTime <= 0.5)
		{
			return;
		}
		// Reduz o tempo de spawn dos inimigos em 0,1 segundos a cada aumento de level do player
		timer.WaitTime -= 0.1;
	}
}
