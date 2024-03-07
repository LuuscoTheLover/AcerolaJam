using Godot;
using System;
using System.Collections.Generic;

namespace Luusco;
public static class Utils
{
    public static void InstanciateFromScene<T>(PackedScene scene, Node Parent, Vector3 Pos)
    {
        Node3D instance = scene.Instantiate<Node3D>();
        instance.Position = Pos;
        Parent.AddChild(instance);
    }
    public static T InstanciateFromType<T>(Node Parent, Vector3 Pos = new Vector3()) where T : Node3D, new()
    {
        T instance = new T();
        instance.Position = Pos;
        Parent.AddChild(instance);
        return instance;
    }
    public static void ReparentNode(Node node, Node NewParent)
    {
        node.GetParent().RemoveChild(node);
        NewParent.AddChild(node);
    }
    public static void ReparentAnMove(Node3D node, Node3D NewParent, Vector3 GlobalPos)
    {
        node.GetParent().RemoveChild(node);
        NewParent.AddChild(node);
        node.GlobalPosition = GlobalPos;
    }
    public static void GetZeroed(Node3D node)
    {
        node.Transform = Transform3D.Identity;
    }
    public static void MotionKill(RigidBody3D rb)
    {
        rb.LinearVelocity = Vector3.Zero;
        rb.AngularVelocity = Vector3.Zero;
    }
    public static Vector3 GetPointVelocity(RigidBody3D rb, Vector3 PointGlobalPos)
    {
        return rb.LinearVelocity + rb.AngularVelocity.Cross(PointGlobalPos - rb.GlobalTransform.Origin);
    }
    public static void ApplyForceAtPosition(RigidBody3D rb, Vector3 GlobalForce, Vector3 GlobalPos)
    {
        rb.ApplyForce(GlobalForce, GlobalPos - rb.GlobalPosition);
    }
    public static void ApplyImpulseAtPosition(RigidBody3D rb, Vector3 GlobalImpuse, Vector3 GlobalPos)
    {
        rb.ApplyImpulse(GlobalImpuse, GlobalPos - rb.GlobalPosition);
    }
}