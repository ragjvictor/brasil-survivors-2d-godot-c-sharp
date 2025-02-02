using Godot;
using System;

// Classe que gerencia a tela de finalização (Vitória ou Derrota)
public partial class EndScreen : CanvasLayer
{
    private Button restartButton;
    private Button quitButton;
    private Label titleLabel;
    private Label descriptionLabel;

    public override void _Ready()
    {
        GetTree().Paused = true;

        restartButton = GetNode<Button>("%RestartButton");
        quitButton = GetNode<Button>("%QuitButton");
        titleLabel = GetNode<Label>("%TiltleLabel");
        descriptionLabel = GetNode<Label>("%DescriptionLabel");

        restartButton.Pressed += OnRestartButtonPressed;
        quitButton.Pressed += OnQuitButtonPressed;
    }

    public void SetDefeat()
    {
        titleLabel.Text = "DEFEAT_NAME";
        descriptionLabel.Text = "DEFEAT_DESC";
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
