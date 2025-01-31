using Godot;
using System;

// Classe responsável por gerenciar o tempo da arena
public partial class ArenaTimeManager : Node
{    
    // Timer para controlar o tempo
    private Timer timer;

    // Método chamado quando o nó está pronto
    public override void _Ready()
    {
        // Obtém o nó Timer da cena
        timer = GetNode<Timer>("Timer");
    }

    // Retorna o tempo decorrido
    public float GetTimeElapsed()
    {		
        // Calcula e retorna o tempo decorrido
        return (float)(timer.WaitTime - timer.TimeLeft);
    }
}
