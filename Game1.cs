using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mgImGui
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ImGuiRenderer _imGuiRenderer; // Add this declaration here

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true; 
        }

        protected override void Initialize()
        {
            // ....
            IsMouseVisible = true; // So you can see the mouse pointer over the controls
            _imGuiRenderer = new ImGuiRenderer(this); // Initialize the ImGui renderer
            _imGuiRenderer.RebuildFontAtlas(); // Required so fonts are available for rendering
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

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
    }
}
