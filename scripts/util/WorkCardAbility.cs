using Godot;
using System;

[GlobalClass]
public partial class WorkCardAbility : Node2D
{
	public HitboxComponent hitboxComponent;

	public override void _Ready()
	{
		hitboxComponent = GetNode<HitboxComponent>("HitboxComponent");
	}
}
