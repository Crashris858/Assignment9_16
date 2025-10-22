using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class PickUp : Area2D
{
	//variables
	//active bool
	private bool Active = true; 
	//timer node
	private Timer _timer;
	//animation player
	private AnimatedSprite2D _animation;
	//particles 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//get timer
		_timer = GetNode<Timer>("refresh");
		//connect signals 
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		//connect timer timeout
		_timer.Connect("timeout", new Callable(this, nameof(OnTimerTimeout)));
		//play animation 
		_animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animation.Play("default");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	//on body entered
	private void OnBodyEntered(Node2D body)
	{
		//check if body is player
		if (body is Player && Active)
		{
			//Change Player's state to platformer or TopDown 
			var player = body as Player;
			//play the respective method for the change
			if (player.IsPlatformer)
			{
				//method change to topdown
				player.ChangetoTopDown();
			}
			//else the opposite
			else
            {
				player.ChangetoPlatformer();
            }
			//temporarlily disable the pick up for 5 seconds 
			Visible = false;
			//set active to false 
			Active = false; 
			//start timer
			_timer.Start();
		}
	}
	//on timertimeout
	private void OnTimerTimeout()
	{
		//make visible 
		Visible = true;
		//set active to true
		Active = true;  
    }

}
