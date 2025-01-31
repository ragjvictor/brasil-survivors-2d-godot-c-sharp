using Godot;
using System;

// Classe que representa um upgrade de habilidade
[GlobalClass]
public partial class AbilityUpgrade : Resource
{
    [Export]
    private String id; // Identificador único da habilidade

    [Export]
    private String name; // Nome da habilidade
    
    [Export(PropertyHint.MultilineText)]
    private String description; // Descrição da habilidade

    public String Id
    {
        get { return id; }
    }

    public String Name
    {
        get { return name; }
    }

    public String Description
    {
        get { return description; }
    }
}
