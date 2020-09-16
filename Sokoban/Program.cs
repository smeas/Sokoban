namespace Sokoban {
	internal static class Program {
		private static void Main() {
			Resources.Load();
			Game game = new Game();
			game.Run();
		}
	}
}