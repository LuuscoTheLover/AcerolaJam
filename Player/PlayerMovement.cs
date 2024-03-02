using Godot;
using System;

namespace PlayerMovement;
[GlobalClass]
public partial class PlayerMovement : Godot.CharacterBody3D
{	
	[ExportGroup("Atatchables")]
	[Export] public Node3D Neck;
	[Export] public Camera3D Head;

	[ExportGroup("Player Stats")]
	[Export] public float WalkSpeed = 5.0f;
	[Export] public float RunSpeed = 8.0f;
	[Export] public float LowGravityJump = 1.5f;
	[Export] public float NormalJump = 4.5f;
	public float JumpVelocity = 4.5f;
	private float jump;
	private float speed;

	[ExportGroup("Camera")]
	[Export(PropertyHint.Range, "0.005, 0.02, 0.001")] public float CameraSense = 0.01f;

	public float gravity = 9.8f;
	float airTime = 0.0f;
	float bonusGrav = 2.0f;
	float multiplier;

    public override void _Ready()
    {
        base._Ready();
		speed = WalkSpeed;
		Input.MouseMode = Input.MouseModeEnum.Captured;
    }
    public override void _PhysicsProcess(double delta)
	{	
		Velocity = MoveAndJump((float)delta);
		SpeedJumpHandler();
		MoveAndSlide();
	}

	//Player movement, jump, and gravity	
	public Vector3 MoveAndJump(float delta){
		Vector3 movement = Velocity;
		if (IsOnFloor()){
			airTime = 0.0f;
		}
		else {
			airTime += (float)delta;
		}

		if (gravity > 1.0f){
			movement.Y -= (gravity + (gravity * airTime * bonusGrav)) * (float)delta;
		}
		else {
			movement.Y -= gravity * (float)delta;
		}

		if (Input.IsActionJustPressed("jump") && IsOnFloor()) {
			movement.Y = JumpVelocity;
		}

		Vector2 inputDir = Input.GetVector("left", "right", "foward", "backward");
		Vector3 direction = (Head.GlobalTransform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero) {
			movement.X = direction.X * speed;
			movement.Z = direction.Z * speed;
		}
		else {
			movement.X = Mathf.MoveToward(movement.X, 0, speed);
			movement.Z = Mathf.MoveToward(movement.Z, 0, speed);
		}

		return movement;
	}

	public void SpeedJumpHandler(){
		float newSpeed;
		multiplier = Mathf.Clamp((gravity / 2.0f), 0.5f, 1.0f);
		if (Input.IsActionPressed("run")){
			newSpeed = RunSpeed * multiplier;
		}
		else{
			newSpeed = WalkSpeed * multiplier;
		}
		speed = newSpeed;
		if (gravity < 5.0){
			JumpVelocity = LowGravityJump;
		}
		else {
			JumpVelocity = NormalJump;
		}
	}

	//Camera Movement
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if (@event is InputEventMouseMotion ) {
			InputEventMouseMotion mouseMotion = @event as InputEventMouseMotion;
			Neck.RotateY(-mouseMotion.Relative.X * CameraSense);
			Head.RotateX(-mouseMotion.Relative.Y * CameraSense);

			Vector3 CamRot = Head.RotationDegrees;
			CamRot.X = Mathf.Clamp(CamRot.X, -80f, 60f);
			Head.RotationDegrees = CamRot;
		}
    }
}
