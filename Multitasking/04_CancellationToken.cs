namespace Multitasking;

public class CancellationTokenDemo
{
    static void Main(string[] args)
    {
		CancellationTokenSource cts = new(); //Quelle, stellt neue CTs aus
		Console.WriteLine(cts.Token); //Neuen Token erstellen, Tokens sind structs, bei jedem Zugriff wird ein neuer Token erstellt

		CancellationToken ct = cts.Token;
		Task t = new Task(Run, ct); //Hier Token mitgeben
		t.Start();

		Thread.Sleep(500);

		cts.Cancel(); //Auf der Source den Cancel-Request machen, um alle Tokens zu canceln, welche von dieser Source gekommen sind

		Console.ReadKey();
    }

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				ct.ThrowIfCancellationRequested(); //Hier wird eine Exception geworfen, bricht den Task ab

				Console.WriteLine($"Run: {i}");
				Thread.Sleep(20);
			}
		}
	}
}