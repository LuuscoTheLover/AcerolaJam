using Godot;
using System.Collections.Generic;
using GravityComponentScript;



public partial class GravityController : Node
{
    [Signal] public delegate void NewGravityEventHandler(float newGrav);
    
    [ExportGroup("Gravity")]
    public float NormalGravity = 9.8f;
    public float LowGravity = 0.1f;
    public List<RigidBody3D> BodyList = new List<RigidBody3D>();

    public override void _EnterTree()
    {
     
    }
    public override void _Process(double delta)
    {
        

    }
    public void GravityBodyEnter(Node body){
        if (body is RigidBody3D) {
            if (body.HasNode("GravityComponent")) {
                GravityComponent controller = (GravityComponent)body.GetNode("GravityComponent");
                controller.Gravitation = LowGravity;
            }
        }
    }
}
