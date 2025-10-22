using Godot;
using System;

public partial class Ending : TextureRect
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//quit if space is pressed 
        if(Input.IsActionJustPressed("jump"))
        {
			GetTree().Quit(); 
        }
    }
}
