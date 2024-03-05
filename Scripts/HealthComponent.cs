using Godot;
using System;

public partial class HealthComponent : Node
{
    [Export] public float MaxHealth = 50f;
    float health;

    public override void _Ready()
    {
        base._Ready();
        health = MaxHealth;
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
        GD.Print(health);
    }
    public void Damage(float attack) {
        health -= attack;
        if (health <= 0f) {
            Die();
        }
    }
    public void Die() {
        GetParent().QueueFree();
    }
}
