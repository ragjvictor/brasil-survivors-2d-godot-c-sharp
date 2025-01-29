using Godot;
using System;

public partial class ExperienceBar : CanvasLayer
{
	[Export]
	public ExperienceManager experienceManager;

	private ProgressBar progressBar;

	public override void _Ready()
	{
		progressBar = GetNode<ProgressBar>("MarginContainer/ProgressBar");
		progressBar.Value = 0;
		
		experienceManager.ExperienceUpdated += OnExperienceUpdated;
	}

	private void OnExperienceUpdated(float currentExperience, float targetExperience)
	{
		float percent = currentExperience / targetExperience;
		progressBar.Value = percent;
	}
}
