using Godot;
using System;

public partial class GravityComponent : Node
{
    RigidBody3D Parent;
    public float Gravitation = 9.8f;
    public override void _Ready()
    {
        base._Ready();
        Parent = GetParentOrNull<RigidBody3D>();
    }
    public override void _PhysicsProcess(double delta)
    {
        base._Process(delta);
        if (Parent is RigidBody3D) {
            Vector3 velocity = Parent.LinearVelocity;
            velocity.Y -= Gravitation * (float)delta;
            Parent.LinearVelocity = velocity;
        }
    }
}
