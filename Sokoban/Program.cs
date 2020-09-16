namespace Sokoban {
	internal static class Program {
		private static void Main(string[] args) {
			Resources.Load();
			Game game = new Game();
			game.Run();
		}
	}
}