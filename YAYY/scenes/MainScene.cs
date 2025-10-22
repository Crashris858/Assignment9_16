using Godot;
using System;

public partial class MainScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//connect signals
		//connect player hit signal from watcher
		var watcherNode = GetNode<Watcher>("Watcher");
		watcherNode.PlayerHit += OnPlayerHit;
		//get Node 2D Area end and connect end signal
		var endArea = GetNode<EndZone>("EndZone");
		endArea.EndGame += OnEndGame; 

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	//onplayerhit function
	public void OnPlayerHit()
	{
		//show black screen for a second

		//reset the scene 
		GetTree().ReloadCurrentScene();
	}
	//On End Game
	public void OnEndGame()
	{
		//swap to ending scene. 
		GetTree().ChangeSceneToFile("res://scenes/ending.tscn");
    }

}
