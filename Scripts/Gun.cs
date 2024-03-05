using Godot;
using System;

public partial class Gun : Node3D
{
    public AnimationPlayer AnimPlayer;
    public Node3D BulletPoint;

    public override void _Ready()
    {
        base._Ready();
        AnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        BulletPoint = GetNode<Node3D>("BulletPoint");
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionPressed("click") && !AnimPlayer.IsPlaying()) {
            AnimPlayer.Play("Shooting");
        }
    }
}
