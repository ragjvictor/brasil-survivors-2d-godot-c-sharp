using Godot;
using System;
using System.ComponentModel;

public partial class ArenaTimeUi : CanvasLayer
{
	[Export]
	public ArenaTimeManager arenaTimeManager;

	private Label label;

	public override void _Ready()
	{
		label = GetNode<Label>("MarginContainer/Label");
	}

	public override void _Process(double delta)
	{
		if (arenaTimeManager == null) 
		{
			return;
		}

		float timeElapsed = arenaTimeManager.GetTimeElapsed();
		label.Text = GetSecondsToString(timeElapsed);
	}

	private string GetSecondsToString(float seconds)
	{
		double minutes = Math.Floor(seconds / 60);
		double remainingSeconds = Math.Floor(seconds - (minutes * 60));

		return string.Format("{0:D2}:{1:D2}", (int)minutes, (int)remainingSeconds);
	}
}
