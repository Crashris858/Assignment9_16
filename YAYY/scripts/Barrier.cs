using Godot;
using System;

public partial class Barrier : Sprite2D
{
    //variables 
    //pull particles and collison 
    private GpuParticles2D _particles;
    private CollisionShape2D _collisionShape;
    //on ready 
    public override void _Ready()
    {
        //get respective nodes 
        _particles = GetNode<GpuParticles2D>("GPUParticles2D");
        _collisionShape = GetNode<CollisionShape2D>("StaticBody2D/CollisionShape2D");
        var playerNode = GetNode<Player>("/root/MainScene/Player");
        //if player is not null 
        if (playerNode != null)
        {
            //debug signal recievd
            GD.Print("Barrier connected to player"); 
            //connect signal for barrier change
            playerNode.BarrierChange += OnBarrierChange;
        }
    }
    //on barrier change
    public void OnBarrierChange()
    {
        //print debug
        GD.Print("Barrier change signal received");
        //set collison and particle to inverse 
        if (_collisionShape.IsDisabled())
        {
            //set to enabled
            _collisionShape.SetDeferred("disabled", false);
            GD.Print("collison diavked");
        }
        //else diable it
        else
        {
            _collisionShape.SetDeferred("disabled", true);
        }        
        _particles.Emitting = !_particles.Emitting;
    }
}
