using System.Linq;
using SFML.Graphics;

namespace Sokoban {
	public static class Resources {
		private const string ResPath = "Resources/";
		private const string LevelPath = "Resources/Levels/";

		public static Texture bricksTexture;
		public static Texture groundTexture;
		public static Texture targetTexture;
		public static Texture crateTexture;
		public static Texture playerTexture;

		public static Font gameFont;

		public static int[][,] levels;

		private static readonly string[] levelNames = new[] {
			"level1",
			"level2",
			"sokoban_deluxe_1-1",
			"microban_5",
			"sokoban_deluxe_1-2",
			"sokoban_deluxe_1-4",
			"sokoban_deluxe_1-5",
			"sokoban_deluxe_1-6",
		};

		public static void Load() {
			bricksTexture = new Texture(ResPath + "bricks.png");
			groundTexture = new Texture(ResPath + "ground.png");
			targetTexture = new Texture(ResPath + "ground_target.png");
			crateTexture = new Texture(ResPath + "crate.png");
			playerTexture = new Texture(ResPath + "ball.png");

			gameFont = new Font(ResPath + "consola.ttf");

			levels = levelNames
				.Select(name => Tilemap.Load(LevelPath + name + ".tmx"))
				.ToArray();
		}
	}
}