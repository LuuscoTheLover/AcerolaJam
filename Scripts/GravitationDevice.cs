using Godot;
using System;
using System.Collections.Generic;

public partial class GravitationDevice : Node
{
	public PackedScene GravitationArea = GD.Load<PackedScene>("res://Assets/GravitationArea.tscn");
	public Player player;
	public GravitationArea actualArea;
	[Export] public float DelaySeconds = 10f;
	public bool GravTimer = true;
	public float timer = 0f;

	public override void _Ready()
	{
		player = GetParent<Player>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("gravitationarea") && GravTimer == true) {
			(Vector3 RoomPos, Vector3 RoomSize) = SelectCenter();
			SpawnArea(RoomPos, RoomSize);
			GravTimer = false;
		}
		if (GravTimer == false) {
			timer += (float)delta;
			if (timer >= DelaySeconds) {
				timer = 0f;
				actualArea.QueueFree();
				GravTimer = true;
			}
		}
	}


	public void SpawnArea(Vector3 position, Vector3 size) {
		GravitationArea Area = GravitationArea.Instantiate<GravitationArea>();
		Area.ShapeSize = size;
		Area.Position = position;
		actualArea = Area;
		GetTree().Root.AddChild(Area);
	}

	public (Vector3 position, Vector3 size) SelectCenter() {
		Godot.Collections.Array<Node> RoomCenterNodes = GetTree().GetNodesInGroup("RoomCenter");
		RoomCenter node0 = (RoomCenter)RoomCenterNodes[0];
		float closestdistace = player.Position.DistanceSquaredTo(node0.Position);
		RoomCenter closerRoomCenter = node0;
		for (int i = 0; i < RoomCenterNodes.Count; i++) {
			RoomCenter nodeipos = (RoomCenter)RoomCenterNodes[i];
			float d = player.Position.DistanceSquaredTo(nodeipos.Position);
			if (d < closestdistace) {
				closestdistace = d;
				closerRoomCenter = (RoomCenter)RoomCenterNodes[i];
			}
		}
		return (closerRoomCenter.Position, closerRoomCenter.RoomSize);
	}
}	
