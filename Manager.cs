using Godot;
using System;

public partial class Manager : Control
{
    [Export] private PackedScene profileScene;
    private Button btn;
    private FileDialog fileDialog;

    public override void _Ready()
    {
        //SignalBus.Instance.LineAdded += createLine;
        btn = GetNode<Button>("Foto");
        btn.Pressed += createFileDialog;
    }

    private void createFileDialog(){
        fileDialog = new();
        fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
        fileDialog.Access = FileDialog.AccessEnum.Filesystem;
        fileDialog.UseNativeDialog = true;
        fileDialog.Filters = ["*.png","*.jpeg"];
        AddChild(fileDialog);
        fileDialog.FileSelected += createProfile;
        fileDialog.PopupCenteredRatio();
        
    }


    private void createProfile(String dir){
        Image image = new();
        ImageTexture texture = new();
        image.Load(dir);
        texture.SetImage(image);
        texture.SetSizeOverride(new Vector2I(128,128));
        Profile inst = profileScene.Instantiate<Profile>();
        inst.Texture = texture;
        AddChild(inst);

    }



}
