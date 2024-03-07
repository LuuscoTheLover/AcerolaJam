using Godot;
using System;

public partial class PlayerMovement : Node
{
	[ExportGroup("Atatchables")]
	public Camera3D Head;
	public Player Player;

	[ExportGroup("Player Stats")]
	[Export] public float WalkSpeed = 5.0f;
	[Export] public float RunSpeed = 8.0f;
	[Export] public float LowGravityJump = 1.0f;
	[Export] public float NormalJump = 4.5f;
	public float JumpVelocity = 4.5f;
	public float AirMovementMultiplier = 0.3f;
	private float jump;
	private float speed;

	public float gravity = 9.8f;
	private CustomSignals _customSignal;

	public override void _Ready()
	{
		base._Ready();
		Player = GetParent<Player>();
		Head = Player.GetNode<Camera3D>("Neck/Head");
		speed = WalkSpeed;
		Input.MouseMode = Input.MouseModeEnum.Captured;
		_customSignal = GetNode<CustomSignals>("/root/CustomSignals");
		_customSignal.NewGravity += UpdateGravity;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Input.IsActionJustPressed("esc") && Input.MouseMode == Input.MouseModeEnum.Captured)
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else if (Input.IsActionJustPressed("click") && Input.MouseMode == Input.MouseModeEnum.Visible)
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Player.Velocity = MoveAndJump((float)delta);
		SpeedJumpHandler();
		Player.MoveAndSlide();
	}

	//Player movement, jump, and gravity	
	public Vector3 MoveAndJump(float delta)
	{
		Vector3 movement = Player.Velocity;
		movement.Y -= gravity * delta;

		if (Input.IsActionJustPressed("jump") && Player.IsOnFloor())
		{
			movement.Y = JumpVelocity;
		}
		Vector2 inputDir = Input.GetVector("left", "right", "foward", "backward");
		Vector3 direction = (Head.GlobalTransform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (Player.IsOnFloor())
		{
			if (direction != Vector3.Zero)
			{
				movement.X = direction.X * speed;
				movement.Z = direction.Z * speed;
			}
			else
			{
				movement.X = 0f;
				movement.Z = 0f;
			}
		}
		else
		{
			if (direction != Vector3.Zero)
			{
				movement.X = direction.X * speed * AirMovementMultiplier;
				movement.Z = direction.Z * speed * AirMovementMultiplier;
			}
			else if (gravity > 5.0f)
			{
				movement.X = Mathf.Lerp(movement.X, direction.X * speed, delta * 2.0f);
				movement.Z = Mathf.Lerp(movement.Z, direction.Z * speed, delta * 2.0f);
			}
		}
		return movement;
	}

	public void SpeedJumpHandler()
	{
		float newSpeed;
		float multiplier;
		if (gravity > 5.0f)
		{
			multiplier = 1f;
			JumpVelocity = NormalJump;
		}
		else
		{
			multiplier = 0.2f;
			JumpVelocity = LowGravityJump;
		}
		if (Input.IsActionPressed("run"))
		{
			newSpeed = RunSpeed * multiplier;
		}
		else
		{
			newSpeed = WalkSpeed * multiplier;
		}
		speed = newSpeed;
	}

	public void UpdateGravity(float newGrav)
	{
		gravity = newGrav;
	}
}
