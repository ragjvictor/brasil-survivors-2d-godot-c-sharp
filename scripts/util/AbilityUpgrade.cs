using Godot;
using System;

// Classe que representa um upgrade de habilidade
[GlobalClass]
public partial class AbilityUpgrade : Resource
{    
    [Export]
    private String id; // Identificador único da habilidade

    [Export]
    private int maxQuantity;

    private string GetTextAbility(string field)
    {       
        return $"UP_{id.ToUpper()}_{field.ToUpper()}";
    }

    public String Id
    {
        get { return id; }
    }

    public int MaxQuantity
    {
        get { return maxQuantity; }
    }

    public String Name
    {
        get { return GetTextAbility("name"); }
    }

    public String Description
    {
        get { return GetTextAbility("desc"); }
    }
}
