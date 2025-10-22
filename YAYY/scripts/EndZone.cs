using Godot;
using System;
using System.Runtime.Serialization;

public partial class EndZone : Area2D
{
	//signal to end the game
	[Signal] public delegate void EndGameEventHandler(); 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        //connect built in method
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//onbody enterd 
	public void OnBodyEntered(Node2D body)
	{
        //if body is player
		if(body is Player)
		{
			//emit signal 
			EmitSignal(SignalName.EndGame);
        }
    }
}
