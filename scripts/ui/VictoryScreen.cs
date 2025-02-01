using Godot;
using System;

// Classe que gerencia a tela de vitória
public partial class VictoryScreen : CanvasLayer
{
    private Button restartButton;
    private Button quitButton;

    public override void _Ready()
    {
        GetTree().Paused = true;

        restartButton = GetNode<Button>("%RestartButton");
        quitButton = GetNode<Button>("%QuitButton");

        restartButton.Pressed += OnRestartButtonPressed;
        quitButton.Pressed += OnQuitButtonPressed;
    }

    private void OnRestartButtonPressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://scenes/level/main.tscn");
    }

    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
