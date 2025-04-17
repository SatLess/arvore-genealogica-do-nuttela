using Godot;
using System;

public partial class SignalBus : Node
{
    public static SignalBus Instance;

    [Signal] public delegate void LineAddedEventHandler(Connector con1);
    [Signal] public delegate void LineReleasedEventHandler(Vector2 vec);
    [Signal] public delegate void LineConnectedEventHandler(Connector con2);
    [Signal] public delegate void LineFreedEventHandler();
    [Signal] public delegate void ProfileToggledEventHandler();
    [Signal] public delegate void LineRemoveEventHandler(Connector con2);

    public override void _Ready()
    {
        Instance = this;
    }

}
