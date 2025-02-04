using Godot;
using System;
using System.Numerics;

// Classe responsável por gerenciar o tempo da arena
public partial class ArenaTimeManager : Node
{
    // Sinal criado para alertar o aumento de dificuldade por intervalo de tempo
    [Signal]
    public delegate void ArenaDificultyIncreasedEventHandler(int arenaDificulty);

    [Export]
    public PackedScene endScreenScene; // Exporta a Cena de finalização

    //  Constante do intervalo de tempo para o aumento de dificuldade
    const int DificultyInterval = 5;

    // Timer para controlar o tempo
    private Timer timer;

    private int arenaDificulty = 0; // Dificuldade da Arena
    private Double previousTime = 0; // Tempo anterior

    public override void _Ready()
    {
        // Obtém o nó Timer da cena
        timer = GetNode<Timer>("Timer");
        previousTime = timer.WaitTime;

        timer.Timeout += OnTimerTimeout;
    }

    public override void _Process(double delta)
    {   
        EmitIncreasedArenaDificulty();
    }

    // Método chamado para emitir um sinal no aumento de dificuldade
    private void EmitIncreasedArenaDificulty()
    {
        double nestTimeTarget = timer.WaitTime - ((arenaDificulty + 1) * DificultyInterval);
        if (timer.TimeLeft <= nestTimeTarget)
        {
            arenaDificulty += 1;
            EmitSignal(SignalName.ArenaDificultyIncreased, arenaDificulty);
        }
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
