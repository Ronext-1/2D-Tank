using Godot;
using System;

public partial class Crosshair : Node2D
{
	// Это поле появится в инспекторе Godot, туда мы перетащим Танк
	[Export] public CharacterBody2D PlayerTank; 

	// private ProgressBar _bar;
	private Label _label;

	public override void _Ready()
	{
		// Скрываем обычную мышь
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		
		// Находим узлы внутри прицела (проверь, чтобы имена в кавычках совпадали!)
		// _bar = GetNode<ProgressBar>("ReloadBar");
		_label = GetNode<Label>("ReloadText");
	}

	public override void _Process(double delta)
	{
		// Двигаем прицел за мышкой
		GlobalPosition = GetGlobalMousePosition();

		// Если мы "привязали" танк в инспекторе, берем из него данные
		if (PlayerTank != null)
		{
			// У танка в коде должен быть тип 'Tank' (или как его назвал)
			// Приводим тип, чтобы вызвать метод GetFireTimer
			var tankScript = PlayerTank as Tank; 

			if (tankScript != null)
			{
				// 1. Обновляем полоску
				// _bar.MaxValue = tankScript.FireRate;
				// _bar.Value = tankScript.GetFireTimer();

				// 2. Считаем секунды до выстрела
				float timeLeft = tankScript.FireRate - tankScript.GetFireTimer();
				
				if (timeLeft > 0)
				{
					_label.Text = timeLeft.ToString("0.0") + "s";
				}
				else
				{
					_label.Text = "READY";
				}
			}
		}
	}
}
