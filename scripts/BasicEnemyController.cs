using Godot;
using System;


public partial class BasicEnemyController : CharacterBody2D
{
	const float MaxSpeed = 50.0f;

	private Area2D area2D;

	public AnimatedSprite2D animationEnemy;

	public HealthComponent healthComponent;

	public override void _Ready()
	{
		healthComponent = GetNode<HealthComponent>("HealthComponent");

		animationEnemy = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animationEnemy.Stop();
		animationEnemy.Play("run");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = GetDirectionToPlayer();
		Velocity = direction * MaxSpeed;
		
		if (direction.X > 0)
		{
			animationEnemy.FlipH = false;
		}
		else
		{
			animationEnemy.FlipH = true;
		}

		MoveAndSlide();
	}

	private Vector2 GetDirectionToPlayer()
	{
		Node2D player = (Node2D)GetTree().GetFirstNodeInGroup("player");

		if (player != null)
		{
			return (player.GlobalPosition - GlobalPosition).Normalized();
		}

		return Vector2.Zero;
	}
}
