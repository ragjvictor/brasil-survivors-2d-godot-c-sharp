using Godot;
using System;

public partial class CameraController : Camera2D
{
    private Godot.Collections.Array<Godot.Node> playerNodes;

    Godot.Vector2 targetPosition = Godot.Vector2.Zero;

    public override void _Ready()
    {
        MakeCurrent();
    }

    public override void _Process(double delta)
    {
        acquireTarget();
        GlobalPosition = GlobalPosition.Lerp(targetPosition, (float)(1.0 - Math.Exp(-delta * 20)));
    }

    private void acquireTarget()
    {
        playerNodes = GetTree().GetNodesInGroup("player");

        if (playerNodes.Count > 0)
        {
            Node2D player = (Node2D)playerNodes[0];
            targetPosition = player.GlobalPosition;
        }
    }
}
