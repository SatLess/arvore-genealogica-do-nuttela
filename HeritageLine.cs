using Godot;
using System;
using System.Drawing;
using System.Threading.Tasks;

public partial class HeritageLine : Line2D
{

    private Connector start, end;
    [Export] private PackedScene connector;

    public HeritageLine(Connector con1){
        start = con1;
        AddPoint(ToLocal(start.GlobalPosition + start.CustomMinimumSize/2));
        AddPoint(Vector2.Zero);
        
    }

    public override void _Ready()
    {
        connector = ResourceLoader.Load<PackedScene>("uid://d3lttusth02rc");

        SignalBus.Instance.LineConnected += (Connector con2) => {
            if(IsInstanceValid(end)) return; //Prevent Redirect, might need more work
            end = con2;
            start.lineEnabled = true;
            start.isConnected = true;
            end.lineEnabled = true;
            end.isConnected = true;
            addConnector();
        };

        SignalBus.Instance.LineFreed += freeLine;
    }

    public override void _PhysicsProcess(double delta)
    {
        if(IsInstanceValid(start)) {
            if(IsInstanceValid(end)){
                SetPointPosition(0,ToLocal(start.GlobalPosition + start.CustomMinimumSize/2));
                SetPointPosition(1,ToLocal(end.GlobalPosition + end.CustomMinimumSize/2));
                }
            else
            {
                SetPointPosition(1,GetLocalMousePosition());
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        if(IsInstanceValid(end) && IsInstanceValid(start)) return; //return to not cause problems with other inputs
        if(@event.IsActionReleased("click")){
            Vector2 oldPos = GetPointPosition(1);
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.LineReleased,ToGlobal(GetPointPosition(1)));
            if(oldPos == GetPointPosition(1) && !IsInstanceValid(end)) { //Kinda spaghetti
                freeLine();
            }
            GetViewport().SetInputAsHandled();
        }
    }


    private void freeLine(){
    start.lineEnabled = false;
    start.isConnected = false;
    if(IsInstanceValid(end)){
            end.lineEnabled = false;
            end.isConnected = false;
    }
    QueueFree();
    }

    private void addConnector(){
        Connector inst = connector.Instantiate<Connector>();
        Vector2 p1 = GetPointPosition(0);
         Vector2 p2 = GetPointPosition(1);
        inst.GlobalPosition = ToGlobal((p1 + p2))/2;
        AddChild(inst);
    }

}
