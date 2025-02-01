using Godot;
using System;

// Classe que gerencia a barra de experiência
public partial class ExperienceBar : CanvasLayer
{
	[Export]
	public ExperienceManager experienceManager; // Gerenciador de experiência

	private ProgressBar progressBar; // Barra de progresso para exibir a experiência

	public override void _Ready()
	{
		progressBar = GetNode<ProgressBar>("MarginContainer/ProgressBar"); // Obtém o nó da barra de progresso
		progressBar.Value = 0; // Inicializa o EXP em 0
		
		experienceManager.ExperienceUpdated += OnExperienceUpdated;
	}
	
	// Método chamado ao ganhar experiência
	private void OnExperienceUpdated(float currentExperience, float targetExperience)
	{
		float percent = currentExperience / targetExperience; // Calcula a porcentagem de experiência atual em relação à experiência alvo
		progressBar.Value = percent; // Atualiza o valor da barra de progresso
	}
}
