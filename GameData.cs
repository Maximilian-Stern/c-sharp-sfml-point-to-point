using System;
using System.Collections;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using GameUtils;

namespace GameData
{   class Game
    {
        const int WIDTH = 1080;
        const int HEIGHT = 728;
        const string TITLE = "Drive Path";

        public RenderWindow window;
        public Texture playerTexture;
        public Sprite playerSprite;
        public View view;
        public Clock clock;
        public float gameTime;
        public Vector2f moveVector;
        int speed = 250;
        int index;
        Vector2f[] points = {
            new Vector2f(0f, 0f),
            new Vector2f(-150f, -300f),
            new Vector2f(0f, -450f),
            new Vector2f(300f, -450f),
            new Vector2f(300f, -300f),
            new Vector2f(500f, -300f),
            new Vector2f(450f, -50f)
        };
        Vector2f nextPointVector;

        public Game()
        {
            clock = new Clock();

            VideoMode mode = new VideoMode(WIDTH, HEIGHT);
            window = new RenderWindow(mode, TITLE);

            window.Closed += CloseGame;
            window.KeyPressed += CloseGame;
            window.Resized += Resize;
        }

        private void CloseGame(object? sender, EventArgs e)
        {
            window?.Close();
        }

        private void Resize(object? sender, EventArgs e)
        {
            view.Size = (Vector2f)window.Size;
            view.Center = playerSprite.Position;
        }

        private void Initialize()
        {
            window.SetFramerateLimit(60);
            window.SetVerticalSyncEnabled(true);

            index = 0;
            nextPointVector = points[1] - points[0];

            playerTexture = new Texture(@".\Assets\car.png");
            playerSprite = new Sprite(playerTexture);
            playerSprite.Origin = new Vector2f(playerTexture.Size.X/2, playerTexture.Size.Y/2);
            playerSprite.Scale = new Vector2f(0.2f, 0.2f);
            playerSprite.Rotation = Utils.AngleBetween(playerSprite.Position, points[1]);
            view = new View(playerSprite.Position, (Vector2f)window.Size);
        }

        private void HandleEvents()
        {
            window.DispatchEvents();
        }

        private void Update(float deltaTime)
        {
            gameTime = deltaTime;
            PlayerMove(gameTime);
            view.Center = playerSprite.Position;
        }

        private void PlayerMove(float deltaTime)
        {
            nextPointVector = points[Utils.Index(index + 1, points.Length)] - playerSprite.Position;

            if (nextPointVector.SqrMagnitude() < 5)
            {
                playerSprite.Position = points[Utils.Index(index + 1, points.Length)];

                index++;
                nextPointVector = points[Utils.Index(index + 1, points.Length)] - points[Utils.Index(index, points.Length)];

                playerSprite.Rotation = Utils.AngleBetween(playerSprite.Position, points[Utils.Index(index + 1, points.Length)]);
            }

            moveVector = nextPointVector.Normalize() * speed * deltaTime;
            playerSprite.Position += moveVector;
        }

        private void DrawLine(Vector2f startPoint, Vector2f endPoint, Color color)
        {
            VertexArray line = new VertexArray(PrimitiveType.Lines, 2);
            line[0] = new Vertex(startPoint, color);
            line[1] = new Vertex(endPoint, color);
            window.Draw(line);
        }

        private void DrawPath()
        {
            int i = 0;
            foreach (Vector2f point in points)
            {
                i++;
                if(i == points.Length)
                {
                    DrawLine(point, points[0], Color.Red);
                }
                else
                {
                    DrawLine(point, points[i], Color.Red);
                }
            }
        }

        private void DrawCircle(Vector2f position, int radius, Color color)
        {
            CircleShape circle = new CircleShape(radius, 30);
            circle.Origin = new Vector2f(radius, radius);
            circle.Position = position;
            circle.FillColor = color;
            window.Draw(circle);
        }

        private void DrawWaypoints()
        {
            int circlesRadius = 15;
            Color color = Color.Red;

            foreach(Vector2f point in points)
            {
                DrawCircle(point, circlesRadius, color);
            }
        }

        private void DrawRectOutline(Vector2f position, int width, int height, Color color)
        {
            RectangleShape rectangle = new RectangleShape(new Vector2f(width, height));
            rectangle.Origin = new Vector2f(width/2, height/2);
            rectangle.Position = position;
            rectangle.FillColor = Color.Transparent;
            rectangle.OutlineColor = Color.Red;
            rectangle.OutlineThickness = 3;
            window.Draw(rectangle);
        }

        private void Draw()
        {
            window.Clear(Color.Blue);
            window.SetView(view);
            DrawPath();
            window.Draw(playerSprite);
            DrawRectOutline(playerSprite.Position, (int)playerSprite.GetGlobalBounds().Width, (int)playerSprite.GetGlobalBounds().Height, Color.Red);
            DrawWaypoints();
            window.Display();
        }

        public void Run()
        {
            Initialize();

            while(window.IsOpen)
            {
                var deltaTime = clock.Restart().AsSeconds();

                HandleEvents();

                Update(deltaTime);

                Draw();
            }
        }
    }
}