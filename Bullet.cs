using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public float Speed = 600f;
	[Export] public float Lifetime = 3.0f;
	// 
	public override void _Ready()
	{
		GetTree().CreateTimer(Lifetime).Timeout += () => QueueFree();
		
		BodyEntered += OnBodyEntered;
	}

	public override void _Process(double delta)
	{
		Position += -Transform.Y*Speed*(float)delta;
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body.Name != "Tank")
		{
			GD.Print("Попадание в: " + body.Name);
			QueueFree();
		}
	}
}
