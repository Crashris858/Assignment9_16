using Godot;
using System;
using System.Numerics;

// Player class script
public partial class Player : CharacterBody2D
{
	//variables 
	//flipped boolean 
	public bool IsPlatformer = false; 
	//animation 
	public AnimatedSprite2D Animation;
	//current animation 
	public string currentDirection ="front";
	//export speed
	[Export] public int TopDownSpeed { get; set; } = 200;
	[Export] public int PlatformerSpeed { get; set; } = 200;
	// jump Velocity
	[Export] public float JumpVelocity = -300;
	//gravity
	[Export] public float Gravity = 800;
	//signal for disbaling barriers
	[Signal] public delegate void BarrierChangeEventHandler();
	//get input
	private void GetInput()
	{
		//get inputs
		Godot.Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		//normalize
		inputDirection = inputDirection.Normalized();
		//set Velocity
		Velocity = inputDirection * TopDownSpeed;
	}
	//update animation function  top-down
	private void UpdateAnimationTop()
	{
		//set direction
		string direction = "";
		//if player is moving
		if (Velocity.Length() > 0)
		{
			//get current direction
			if (Velocity.Y > 0)
			{
				direction = "front";
			}
			else if (Velocity.Y < 0)
			{
				direction = "back";
			}
			else if (Velocity.X > 0)
			{
				direction = "side";
				Animation.FlipH = false;
			}
			else if (Velocity.X < 0)
			{
				direction = "side";
				Animation.FlipH = true;
			}
			//change current direction
			currentDirection = direction;
			//play walking animation 
			Animation.Play("walk_" + currentDirection);
		}
		//else play idle in the direction 
		else
		{
			Animation.Play("idle_" + currentDirection);
		}
	}
	//platformer movement 
	private void platformerMove(double delta)
	{
		//get the horizontal movment
		Godot.Vector2 Tempv = Velocity;
		//get the x input
		Tempv.X = (Input.GetActionStrength("right") - Input.GetActionStrength("left")) * PlatformerSpeed;
		//get the y input
		Tempv.Y += Gravity * (float)delta;
		//if the plater is on the floor and just jumped
		if (IsOnFloor() && Input.IsActionJustPressed("jump"))
		{
			//set y to jump
			Tempv.Y = JumpVelocity;
		}
		//asign to velocity
		Velocity = Tempv;
		//moving and sliding
		MoveAndSlide();
	}
	//update platformer animation function 
	private void UpdateAnimationPlatformer()
	{
		//if velocityx is greater than zero 
		if (Velocity.X > 0)
		{
			Animation.Play("walk_side");
			Animation.FlipH = false;
		}
		//if velocityx is less than zero
		else if (Velocity.X < 0)
		{
			Animation.Play("walk_side");
			Animation.FlipH = true;
		}
		//else idle
		else
		{
			Animation.Play("idle_side");
		}
	}
	//ready 
	public override void _Ready()
	{
		//get animated sprite 
		Animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	//physics process
	public override void _PhysicsProcess(double delta)
	{
		//if it is platformer
		if (!IsPlatformer)
		{
			//get input
			GetInput();
			//move
			MoveAndSlide();
		}
		//if platforming 
		else
		{
			//platformer movement
			platformerMove(delta); 
		}
	}
	//process 
	public override void _Process(double delta)
	{
		if (!IsPlatformer)
		{
			//update animation function top-down
			UpdateAnimationTop();
		}
		//update animation function platformer
		else
		{
			UpdateAnimationPlatformer();
		}
	}
	//Function to change to Platformer 
	public void ChangetoPlatformer()
	{
		//change bool and reset velocity.
		IsPlatformer = true;
		Velocity = Godot.Vector2.Zero;
		//emit barrier change
		EmitSignal(SignalName.BarrierChange);
	}
	//change to Top-down 
	public void ChangetoTopDown()
	{
		IsPlatformer = false;
		Velocity = Godot.Vector2.Zero;
		//emit barrier change 
		EmitSignal(SignalName.BarrierChange);
	}
}
