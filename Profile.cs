using Godot;
using Godot.Collections;
using System;

public partial class Profile : TextureRect
{
	private enum ConPos
	{
		Top,
		Bottom,
		Left,
		Right,
	}

	[Signal] public delegate void PMovingEventHandler();
	[Signal] public delegate void PStaticEventHandler();

	[Export] private PackedScene connector;

	private bool canInteract = false;
	private bool mouseEntered = false;
	

#region Virtual Methods
	public override void _Ready()
	{
		createConnectors();
	}

    public override void _GuiInput(InputEvent @event)
    {
		if(@event.IsActionPressed("click")) canInteract = true;
		else if(@event.IsActionReleased("click")) canInteract = false;
    }

    public override void _PhysicsProcess(double delta)
    {	
		if(canInteract){
			GlobalPosition = GetViewport().GetMousePosition();
			EmitSignal(SignalName.PMoving);
		}  
		else{
			EmitSignal(SignalName.PStatic);}
    }
#endregion

	private void createConnectors(){
		
		for(int  i = 0; i < 4; i++){
			Connector inst = connector.Instantiate<Connector>();
			Vector2 vec = getConnectorPos(i);
			inst.GlobalPosition += vec;
			AddChild(inst);
			
		}
	}

	private Vector2 getConnectorPos(int idx){
		Vector2 size = Texture.GetSize();
		if(idx == (int)ConPos.Top) return new Vector2(size.X/2,0);
		if(idx == (int)ConPos.Bottom) return new Vector2(size.X/2,size.Y);
		if(idx == (int)ConPos.Left) return new Vector2(0,size.Y/2);
		else return new Vector2(Size.X,size.Y/2);
	}

	

}
