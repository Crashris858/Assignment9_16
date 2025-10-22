using Godot;
using System;
using System.Numerics;

public partial class Watcher : CharacterBody2D
{
	//variables
	//pull speed
	[Export] public float Speed = 150f;
	//get player node
	[Export] public NodePath PlayerPath;
	//boolean is flipped
	public bool IsFlipped = false;
	//Target refrence
	private Player _target;
	//navigation agent
	private NavigationAgent2D _navAgent;
	//private area 2d
	private Area2D _collisonArea;
	//player hit singal 
	[Signal] public delegate void PlayerHitEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//get area 2d
		_collisonArea = GetNode<Area2D>("Area2D");
		//connect on body entered
		_collisonArea.BodyEntered += OnBodyEntered;
		//get navigation agent 
		_navAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		//if target path is not null 
		if (PlayerPath != null)
		{
			//assign target to player
			_target = GetNode<Player>(PlayerPath);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//if null abort
		if (_target == null || _navAgent == null)
		{
			return;
		}
		//State: if player is flipped
		if (_target.IsPlatformer == true)
		{	//if not already false
			if(_collisonArea.Monitoring)
			{
				//change area 2d to disabled
				_collisonArea.Monitoring = false; 
			}
		}
		else
		{
			//if collison monitroing is false
			if(!_collisonArea.Monitoring)
			{
				//make true
				_collisonArea.Monitoring = true;
			}
			//face the player's position
			Rotation = 0;
			//if 
			LookAt(_target.GlobalPosition);
		}
	}
	//physics process
	public override void _PhysicsProcess(double delta)
	{
		//if anything crucial is null please return...
		//double this as a state flag for platformer vs top-down
		if (_target == null || _navAgent == null || _target.IsPlatformer == true)
		{
			return;
		}
		//set navigation agent to target's current postion 
		_navAgent.TargetPosition = _target.GlobalPosition;
		//get the next point 
		Godot.Vector2 nextPoint = _navAgent.GetNextPathPosition();
		//get direction 
		Godot.Vector2 direction = (nextPoint - GlobalPosition).Normalized();
		//move toward the target
		Velocity = direction * Speed;
		MoveAndSlide();
	}
	//on body entered
	private void OnBodyEntered(Node2D body)
	{
		//if body is player
		if (body is Player)
		{
			//emit player hit singal 
			EmitSignal(SignalName.PlayerHit);
		}
	}
}
