using Godot;
using System;

// Classe responsável por gerenciar o tempo da arena
public partial class ArenaTimeManager : Node
{    
    [Export]
    public PackedScene endScreenScene; // Exporta a Cena de finalização

    // Timer para controlar o tempo
    private Timer timer;

    public override void _Ready()
    {
        // Obtém o nó Timer da cena
        timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout;
    }

    // Retorna o tempo decorrido
    public float GetTimeElapsed()
    {		
        // Calcula e retorna o tempo decorrido
        return (float)(timer.WaitTime - timer.TimeLeft);
    }

    // Método usado quando o limite do Timer é atingido
    private void OnTimerTimeout()
    {
        var endScreenInstance = endScreenScene.Instantiate(); // Instancia a Cena de finalização
        AddChild(endScreenInstance);
    }
}
