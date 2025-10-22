using Godot;
using System;

public partial class Begining : TextureRect
{

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        //if space is pressed change to main scene
        if(Input.IsActionJustPressed("jump"))
        {
			GetTree().ChangeSceneToFile("res://scenes/main_scene.tscn");
        }
    }
}
