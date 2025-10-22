using Godot;
using System;
using System.IO;

public partial class Box : RigidBody2D
{
	//variables
	//path to player node 
	[Export] public NodePath PlayerPath;
	//player refrence
	private Player _player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//disable gravity 
		GravityScale = 0;
		//enable contact monitoring so body_entered signals are reported
		ContactMonitor = true;
		//if player path is not null
		if(PlayerPath != null)
		{
			//get node
			_player = GetNode<Player>(PlayerPath);
		}
    }

	// physics process 
	public override void _PhysicsProcess(double delta)
	{
		//if player moved and slide and not platformer and is close enough to push
		if(_player!=null&& !_player.IsPlatformer && _player.GlobalPosition.DistanceTo(GlobalPosition) < 20)
		{
			//debug print
			GD.Print("Pushing box");
			//push box away from player
			Godot.Vector2 PushDirection = (GlobalPosition - _player.GlobalPosition).Normalized();
			//apply impulse (one time) with more force
			ApplyForce(PushDirection * 200);
		}
		//if box is moving
		if (LinearVelocity.Length() > 0)
		{
			//apply friction
			LinearVelocity = LinearVelocity * 0.95f;
		}
	}
}
