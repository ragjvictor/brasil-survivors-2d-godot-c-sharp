using Godot;
using System;

// Classe que controla a câmera do jogo
public partial class CameraController : Camera2D
{
    // Array que armazena os nós dos jogadores
    private Godot.Collections.Array<Godot.Node> playerNodes;

    // Posição alvo da câmera
    Godot.Vector2 targetPosition = Godot.Vector2.Zero;

    // Método chamado quando a cena está pronta
    public override void _Ready()
    {
        MakeCurrent(); // Define esta câmera como a câmera atual
    }

    // Método chamado a cada frame
    public override void _Process(double delta)
    {
        acquireTarget(); // Adquire o alvo (jogador)
        // Atualiza a posição da câmera suavemente em direção à posição alvo
        GlobalPosition = GlobalPosition.Lerp(targetPosition, (float)(1.0 - Math.Exp(-delta * 20)));
    }

    // Método para adquirir o alvo (jogador)
    private void acquireTarget()
    {
        // Obtém todos os nós do grupo "player"
        playerNodes = GetTree().GetNodesInGroup("player");

        // Se houver jogadores, atualiza a posição alvo
        if (playerNodes.Count > 0)
        {
            Node2D player = (Node2D)playerNodes[0]; // Pega o primeiro jogador
            targetPosition = player.GlobalPosition; // Atualiza a posição alvo para a posição do jogador
        }
    }
}
