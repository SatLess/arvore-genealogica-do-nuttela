using Godot;
using System;
#nullable enable

public partial class Connector : ColorRect
{

    [Signal] public delegate void LineAttemptedEventHandler();

    [Export] private float size = 30f;
    
    private bool mouseEntered = false;
    public bool lineEnabled = false; // WARNING kinda unsafe, use only inside line2d
    public bool isConnected = false; //TODO 
    
    #region Virtual Methods
    public override void _Ready()
    {
        base._Ready();
        //GetParent<Profile>().PMoving += () => MouseFilter = MouseFilterEnum.Ignore; //TODO: Maybe try a foreach IG
       // GetParent<Profile>().PStatic += () => MouseFilter = MouseFilterEnum.Stop;

        CustomMinimumSize = Vector2.One * size;
        //Signal Handling
        //SignalBus.Instance.LineReleased += checkForCollision;
		MouseEntered += () => mouseEntered = true;
		MouseExited += () => mouseEntered = false;

    }

    public override void _GuiInput(InputEvent @event)
    {
        
        if(@event.IsActionPressed("click") && !lineEnabled){
            lineEnabled = true;
           createLine();
        }
    }
    #endregion
    

    public bool checkForCollision(Vector2 vecGlobal){
        //WARNING as it stands it can connect to its own profile
        if (isConnected) return false;
        if(lineEnabled) return false; // So it does not connect to itself
        if(vecGlobal.DistanceTo(GlobalPosition) < size)
        {
            //SignalBus.Instance.EmitSignal(SignalBus.SignalName.LineConnected,this);
            return true;
        } return false;
    }
        private void createLine(){
        HeritageLine line = new(this);
        this.AddChild(line);
        
    }

}
