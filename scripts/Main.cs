using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene endScreenScene;

	private PlayerController player;

	public override void _Ready()
	{
		TranslationServer.SetLocale("pt");
		player = GetNode<PlayerController>("%Player");
		player.healthComponent.Died += OnPlayerDied;
	}

	private void OnPlayerDied()
	{
		EndScreen endScreenInstance = (EndScreen)endScreenScene.Instantiate();
		AddChild(endScreenInstance);
		endScreenInstance.SetDefeat();
	}
}
