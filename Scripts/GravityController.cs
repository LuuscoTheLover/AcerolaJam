using Godot;
using System.Collections.Generic;

public partial class GravityController : Node
{

    [ExportGroup("Gravity")]
    public float NormalGravity = 9.8f;
    public float LowGravity = 0.01f;
    private CustomSignals _customSignal;
    public List<RigidBody3D> BodyList;
    public Area3D area;

    public override void _Ready()
    {
        base._Ready();
        _customSignal = GetNode<CustomSignals>("/root/CustomSignals");
        BodyList = new List<RigidBody3D>();
    }

    public void GravityBodyEnter(Node body){
        if (body is RigidBody3D) {
            if (body.HasNode("GravityComponent") && body is RigidBody3D) {
                BodyList.Add((RigidBody3D)body);
                for (int i = 0; i < BodyList.Count; i++) {
                    GD.Print(BodyList[i].GetNode<GravityComponent>("GravityComponent").Gravitation);
                    BodyList[i].GetNode<GravityComponent>("GravityComponent").Gravitation = LowGravity;
                    
                }
            }
        }
        if (body is Player) {
            _customSignal.EmitSignal(nameof(CustomSignals.NewGravity), 0.1f);
        }
    }
    public void GravityBodyExited(Node body){
        if (body is RigidBody3D) {
            body.GetNode<GravityComponent>("GravityComponent").Gravitation = NormalGravity;
            for (int i = 0; i < BodyList.Count; i++) {
                    if (body == BodyList[i])
                    BodyList.Remove(BodyList[i]);
            }
        }
        if (body is Player) {
            _customSignal.EmitSignal(nameof(CustomSignals.NewGravity), 9.8f);
        }
    }
}
