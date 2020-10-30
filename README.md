# IMGUI in .net howto
  
## 1. Add ImGui.NET as a nuget package
  
dotnet add package ImGui.NET  

## Copy Files from imgui.net xna sample
  
. COPY the ImGuiRenderer from here which includes these files:  
  
https://github.com/mellinoe/ImGui.NET/blob/master/src/ImGui.NET.SampleProgram.XNA/ImGuiRenderer.cs
https://github.com/mellinoe/ImGui.NET/blob/master/src/ImGui.NET.SampleProgram.XNA/DrawVertDeclaration.cs
    
## Add Code to Game class (or wherever you want)

```csharp

using ImGuiNET;
// if you copy from imgui.net github you need to also add 
// using ImGuiNET.SampleProgram.XNA;

private ImGuiRenderer _imGuiRenderer;

protected override void Initialize()
{
    // ....
    IsMouseVisible = true; // So you can see the mouse pointer over the controls
    _imGuiRenderer = new ImGuiRenderer(this); // Initialize the ImGui renderer
    _imGuiRenderer.RebuildFontAtlas(); // Required so fonts are available for rendering
    
    base.Initialize();
}

protected override void Draw(GameTime gameTime)
{
    // Something may have altered the rendering state, and this might cause ImGui's textures to render
    // with artifacts.  So we want to reset the render state just for ImGui
    var oldSamplerState = GraphicsDevice.SamplerStates[0];
    GraphicsDevice.SamplerStates[0] = new SamplerState();
    
    _imGuiRenderer.BeforeLayout(gameTime); // Must be called prior to calling any ImGui controls
    ImGui.ShowDemoWindow(); // Render the built in demonstration window
    _imGuiRenderer.AfterLayout(); // Must be called after ImGui control calls

    // Reset the sample state to what was originally set
    GraphicsDevice.SamplerStates[0] = oldSamplerState;

    base.Draw(gameTime);
}

```