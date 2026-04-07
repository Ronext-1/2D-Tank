using Godot;
using System;

public partial class Tank : CharacterBody2D
{
	[Export] public float FireRate = 2.0f;
	private float _fireTimer = 0.0f;
	[Export] public PackedScene BulletScene;
	private Marker2D _muzzle;
	[Export] public float Speed = 200.0f;
	[Export] public float RotationSpeed = 2.0f;
	[Export] public float TurretSpeed = 5.0f;
	
	private Node2D _turret;

	public override void _Ready()
	{
		// Путь обновлен согласно иерархии на твоем скриншоте
		_muzzle = GetNode<Marker2D>("Turret/gun/Marker2D");
		_turret = GetNode<Node2D>("Turret");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		float d = (float)delta;
		
		_fireTimer += (float)delta;
		
		// Поворот корпуса
		float rotationDir = Input.GetAxis("ui_left", "ui_right");
		Rotation += rotationDir * RotationSpeed * d;
		
		// Движение вперед/назад
		float moveDir = -Input.GetAxis("ui_up", "ui_down");
		Vector2 forwardVector = Transform.Y;
		Velocity = -forwardVector * moveDir * Speed;
		
		MoveAndSlide();
		
		// Логика башни
		Vector2 mousePos = GetGlobalMousePosition();
		float targetAngle = (mousePos - _turret.GlobalPosition).Angle() + Mathf.Pi / 2;
		_turret.GlobalRotation = Mathf.LerpAngle(_turret.GlobalRotation, targetAngle, TurretSpeed * d);
		
		// Стрельба
		if (Input.IsActionJustPressed("ui_accept") || Input.IsMouseButtonPressed(MouseButton.Left) && _fireTimer >= FireRate)
		{
			Shoot();
			_fireTimer = 0.0f;
		}
	}

	private void Shoot()
	{
		if (BulletScene == null) return;
		
		var bullet = (Bullet)BulletScene.Instantiate();
		
		// Исправлена опечатка Gloabal -> Global
		bullet.GlobalPosition = _muzzle.GlobalPosition;
		bullet.GlobalRotation = _muzzle.GlobalRotation;
		
		// Исправлена опечатка Addchild -> AddChild
		GetTree().Root.AddChild(bullet);
	}
	public float GetFireTimer()
	{
		return _fireTimer;
	}
}
