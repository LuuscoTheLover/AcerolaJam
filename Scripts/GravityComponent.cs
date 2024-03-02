using Godot;
using System;


namespace GravityComponentScript;
[GlobalClass]
public partial class GravityComponent : Node
{
    RigidBody3D Parent;
    [Export] public float Gravitation = 0.1f;
    public override void _Ready()
    {
        base._Ready();
        Parent = GetParentOrNull<RigidBody3D>();
        GD.Print(Parent);
    }
    public override void _PhysicsProcess(double delta)
    {
        base._Process(delta);
        if (Parent is RigidBody3D) {
            Vector3 pos = Parent.Position;
            pos.Y -= Gravitation * (float)delta;
            Parent.Position = pos;

        }
    }
    public void UpdateGravity(float newGrav){

    }
}
