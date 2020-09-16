using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Sokoban {
	public class Game {
		private RenderWindow window;
		private Clock clock;
		private float deltaTime;

		private Level level;
		private bool solved;
		private int currentLevelIndex;

		private Text goodJobText = new Text("Good job!", Resources.gameFont, 42);
		private Text nextLevelTimerText = new Text("", Resources.gameFont, 24);
		private Text currentLevelText = new Text("", Resources.gameFont, 24);

		public void Run() {
			window = new RenderWindow(new VideoMode(960, 960), "Sokoban");
			window.SetVerticalSyncEnabled(true);
			window.Closed += (sender, args) => window.Close();
			window.KeyPressed += OnKeyPressed;
			window.Resized += (sender, args) => {
				window.SetView(new View(new FloatRect(0, 0, args.Width, args.Height)));
			};

			clock = new Clock();

			Init();

			while (window.IsOpen) {
				deltaTime = clock.ElapsedTime.AsSeconds();
				clock.Restart();

				window.DispatchEvents();
				Update();
				Draw();
			}
		}

		private void Init() {
			goodJobText.Position = new Vector2f((window.Size.X - goodJobText.GetGlobalBounds().Width) / 2f, 0);
			LoadLevel(0);
		}

		private void Update() {
			if (!solved && level.CheckSolved()) {
				solved = true;
				Console.Beep(440, 100);

				// Update the "next level text" and make sure it's centered.
				if (currentLevelIndex < Resources.levels.Length - 1)
					nextLevelTimerText.DisplayedString = "Press space to continue";
				else
					nextLevelTimerText.DisplayedString = "A winner is you";

				nextLevelTimerText.Position =
					new Vector2f((window.Size.X - nextLevelTimerText.GetGlobalBounds().Width) / 2f, 42 + 10);
			}
		}

		private void Draw() {
			window.Clear(new Color(0x1e1e1eff));

			// Make the tiles a bit larger and center the level.
			const float scale = 2;
			RenderStates renderStates = RenderStates.Default;
			renderStates.Transform.Scale(scale, scale);
			renderStates.Transform.Translate(((Vector2f)window.Size / scale - (Vector2f)level.PixelSize) / 2f);

			window.Draw(level, renderStates);
			window.Draw(level.Player, renderStates);

			// Reset the render matrix.
			renderStates = RenderStates.Default;
			window.Draw(currentLevelText, renderStates);

			if (solved) {
				window.Draw(goodJobText, renderStates);
				window.Draw(nextLevelTimerText, renderStates);
			}

			window.Display();
		}

		private void OnKeyPressed(object sender, KeyEventArgs e) {
			switch (e.Code) {
				case Keyboard.Key.W:
				case Keyboard.Key.Up:
					level.Player.Move(0, -1);
					break;

				case Keyboard.Key.D:
				case Keyboard.Key.Right:
					level.Player.Move(1, 0);
					break;

				case Keyboard.Key.S:
				case Keyboard.Key.Down:
					level.Player.Move(0, 1);
					break;

				case Keyboard.Key.A:
				case Keyboard.Key.Left:
					level.Player.Move(-1, 0);
					break;

				case Keyboard.Key.R:
					level.Reset();
					break;

				case Keyboard.Key.Space:
					if (solved) StartNextLevel();
					break;

				// Developer cheats.
			#if DEBUG
				case Keyboard.Key.PageDown:
					if (currentLevelIndex + 1 < Resources.levels.Length)
						LoadLevel(currentLevelIndex + 1);
					break;

				case Keyboard.Key.PageUp:
					if (currentLevelIndex > 0)
						LoadLevel(currentLevelIndex - 1);
					break;
			#endif
			}
		}

		private void StartNextLevel() {
			if (currentLevelIndex + 1 >= Resources.levels.Length)
				// No more levels.
				return;

			LoadLevel(currentLevelIndex + 1);
		}

		private void LoadLevel(int levelIndex) {
			if (currentLevelIndex >= Resources.levels.Length)
				throw new Exception("Level index out of bounds.");

			level = new Level(Resources.levels[levelIndex]);
			currentLevelIndex = levelIndex;
			solved = false;

			currentLevelText.DisplayedString = $"Level {currentLevelIndex + 1}";
		}
	}
}