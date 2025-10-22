using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Hud : CanvasLayer
{
	//pull Animated Sprite 2D signal
	private AnimatedSprite2D _animated;
	//get simillar for player 
	[Export]public NodePath PlayerPath;
	private Player _player;
	//private timer
	private  double _timer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//get node 
		_animated = GetNode<AnimatedSprite2D>("Control/AnimatedSprite2D");
		//get player node
		_player = GetNode<Player>(PlayerPath);
		//connect signal 
		_player.BarrierChange += OnBarrierChange;
	}
	//process 
	public override void _Process(double delta)
	{
		//increment timer
		if (!(_timer < 10))
		{
			_timer += delta;
		}
		else if (_timer==10)
		{
			//change animation to default
			_animated.Play("default");
			//stop timer
			_timer += 1; 
        }
    }
	//on Barrier Change
	private void OnBarrierChange()
	{
		//if player is platfomer
		if (_player.IsPlatformer)
		{
			//change animated sprite to platformer
			_animated.Play("Platformer");
		}
		//else play top donw
		else
		{
			_animated.Play("TopDown");
		}
		//reset timer to zero 
		_timer = 0; 
	}
}
