using Godot;
using System;

public partial class test : TextureRect
{

        public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here.
        GD.Print("Hello World");
    }

}
