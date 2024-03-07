using Godot;
using System;
using Luusco;


public partial class PlayerMovement : RigidBody3D
{
	enum GrabbingState {
		Idle,
		Grabbing,
		Releasing
	}
	GrabbingState Grabbing;
	public Generic6DofJoint3D joint;
	RayCast3D ray;
	
    public override void _Ready()
    {
        base._Ready();
		joint = GetNode<Generic6DofJoint3D>("Joint");
		ray = GetNode<RayCast3D>("Neck/Head/RayCast3D");
		ray.AddException(this);

    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		Godot.GodotObject collider = ray.GetCollider();
		if (Input.IsActionJustPressed("click")) {
			Grabbing = GrabbingState.Grabbing;
		} else if (Input.IsActionJustReleased("click")) {
			Grabbing = GrabbingState.Releasing;
		} else {
			Grabbing = GrabbingState.Idle;
		}

		switch (Grabbing){
			case GrabbingState.Grabbing:
			break;
			case GrabbingState.Releasing:
			break;
			case GrabbingState.Idle:
			break;
		}
    }
}
