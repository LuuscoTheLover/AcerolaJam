using Godot;
using System;

public partial class HitboxComponent : Area3D
{
    [Export] public HealthComponent HealthComponent;

    public void DoDamage(float attack) {
        if (HealthComponent != null) {
            HealthComponent.Damage(attack);
        }
    }
}
