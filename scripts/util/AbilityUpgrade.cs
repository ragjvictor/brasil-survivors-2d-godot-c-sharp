using Godot;
using System;

[GlobalClass]
public partial class AbilityUpgrade : Resource
{
    [Export]
    private String id;

    [Export]
    private String name;
    
    [Export(PropertyHint.MultilineText)]
    private String description;

    public String Id
    {
        get { return id; }
	}
}
