using Godot;
using System;

public partial class VialDropComponent : Node
{
    [Export]
    public PackedScene vialScene;

    [Export]
    public HealthComponent healthComponent;

    [Export(PropertyHint.Range, "0, 1")]
    public float dropPercent = 0.5f;

    public override void _Ready()
    {
        healthComponent.Died += OnDied;
    }

    private void OnDied()
    {       
        if (GD.Randf() > dropPercent) 
            return;

        if (vialScene == null) 
            return;

        if (!(Owner is Node2D owner))
            return;

        Vector2 spawnPosition = owner.GlobalPosition;
        Node2D vialInstance = (Node2D)vialScene.Instantiate();
        Node2D entitiesLayer = (Node2D)GetTree().GetFirstNodeInGroup("entities_layer");
        entitiesLayer.CallDeferred("add_child", vialInstance);
        vialInstance.GlobalPosition = spawnPosition;
    }
}
