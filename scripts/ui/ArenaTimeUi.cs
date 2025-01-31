using Godot;
using System;
using System.ComponentModel;

// Classe que gerencia a interface do tempo da arena
public partial class ArenaTimeUi : CanvasLayer
{
	[Export]
	public ArenaTimeManager arenaTimeManager; // Gerenciador do tempo da arena

	private Label label; // Label para exibir o tempo

	public override void _Ready()
	{
		label = GetNode<Label>("MarginContainer/Label"); // Obtém o nó da label
	}

	public override void _Process(double delta)
	{
		if (arenaTimeManager == null) 
		{
			return; // Retorna se o gerenciador de tempo não estiver definido
		}

		float timeElapsed = arenaTimeManager.GetTimeElapsed(); // Obtém o tempo decorrido
		label.Text = GetSecondsToString(timeElapsed); // Atualiza o texto da label
	}

	private string GetSecondsToString(float seconds)
	{
		double minutes = Math.Floor(seconds / 60); // Calcula os minutos
		double remainingSeconds = Math.Floor(seconds - (minutes * 60)); // Calcula os segundos restantes

		return string.Format("{0:D2}:{1:D2}", (int)minutes, (int)remainingSeconds); // Formata o tempo como mm:ss
	}
}
