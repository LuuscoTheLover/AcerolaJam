using Godot;
using System;


public partial class CameraMovementScript : Camera3D
{
    [ExportGroup("Camera")]
	[Export(PropertyHint.Range, "0.001, 0.015, 0.001")] public float CameraSense = 0.007f;
    [Export] public Node3D Neck;
	[Export] public Camera3D Head;

    public override void _Process(double delta)
    {
        base._Process(delta);
		if (Input.IsActionJustPressed("esc") && Input.MouseMode == Input.MouseModeEnum.Captured) {
			Input.MouseMode = Input.MouseModeEnum.Visible;
		} else if (Input.IsActionJustPressed("click") && Input.MouseMode == Input.MouseModeEnum.Visible) {
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if (@event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) {
			InputEventMouseMotion mouseMotion = (InputEventMouseMotion)@event;
			Neck.RotateY(-mouseMotion.Relative.X * CameraSense);
			Head.RotateX(-mouseMotion.Relative.Y * CameraSense);

			Vector3 CamRot = Head.RotationDegrees;
			CamRot.X = Mathf.Clamp(CamRot.X, -80f, 60f);
			Head.RotationDegrees = CamRot;
		}
    }
}
