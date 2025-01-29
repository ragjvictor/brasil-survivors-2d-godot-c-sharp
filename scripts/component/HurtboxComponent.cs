using Godot;
using System;

[GlobalClass]
public partial class HurtboxComponent : Area2D
{
	[Export]
	public HealthComponent healthComponent;

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	private void OnAreaEntered(Area2D area2D)
	{
		if (!(area2D is HitboxComponent))
			return;

		if (healthComponent == null)
			return;

		HitboxComponent hitboxComponent = (HitboxComponent)area2D;
		healthComponent.Damage(hitboxComponent.Damage);
	}
}
