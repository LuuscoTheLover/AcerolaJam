using Godot;
using System;

public partial class GravitationArea : Area3D
{
	CollisionShape3D collisionBox;
	MeshInstance3D mesh;
	[Export] public Vector3 ShapeSize = new Vector3(10f,10f,10f);

	public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
		collisionBox = GetNode<CollisionShape3D>("CollisionShape3D");
		BoxShape3D shape = new BoxShape3D();
		shape.Size = ShapeSize;
		collisionBox.Shape = shape;
		BoxMesh newMesh = new BoxMesh();
		newMesh.Size = ShapeSize;
		mesh.Mesh = newMesh;
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
