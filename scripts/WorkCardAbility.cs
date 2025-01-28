using Godot;
using System;

[GlobalClass]
public partial class WorkCardAbility : Node2D
{
	public HitboxComponent HitboxComponent;

	public override void _Ready()
	{
		HitboxComponent = GetNode<HitboxComponent>("HitboxComponent");
	}
}
