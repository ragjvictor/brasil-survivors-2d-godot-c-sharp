using Godot;
using System;

// Componente que gerencia a queda de frascos
public partial class VialDropComponent : Node
{
    [Export]
    public PackedScene vialScene; // Cena do frasco a ser instanciada

    [Export]
    public HealthComponent healthComponent; // Componente de saúde associado

    [Export(PropertyHint.Range, "0, 1")]
    public float dropPercent = 0.5f; // Percentual de chance de queda do frasco

    // Método chamado quando o nó está pronto
    public override void _Ready()
    {
        healthComponent.Died += OnDied; // Associa o evento de morte ao método OnDied
    }

    // Método chamado quando a saúde do componente chega a zero
    private void OnDied()
    {       
        // Verifica se a queda deve ocorrer com base na probabilidade
        if (GD.Randf() > dropPercent) 
            return;

        // Verifica se a cena do frasco está definida
        if (vialScene == null) 
            return;

        // Verifica se o proprietário é um Node2D
        if (!(Owner is Node2D owner))
            return;

        // Posição de spawn do frasco
        Vector2 spawnPosition = owner.GlobalPosition;
        Node2D vialInstance = (Node2D)vialScene.Instantiate(); // Instancia o frasco
        Node2D entitiesLayer = (Node2D)GetTree().GetFirstNodeInGroup("entities_layer"); // Obtém o grupo de entidades
        entitiesLayer.CallDeferred("add_child", vialInstance); // Adiciona o frasco ao grupo de entidades
        vialInstance.GlobalPosition = spawnPosition; // Define a posição do frasco
    }
}
