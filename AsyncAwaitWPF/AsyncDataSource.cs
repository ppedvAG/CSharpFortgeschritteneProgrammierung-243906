namespace AsyncAwaitWPF;

public class AsyncDataSource
{
	//IAsyncEnumerable
	//Funktioniert wie IEnumerable, aber die Daten kommen nicht sofort, sondern in irregulären Intervallen
	//yield return: Beim Ausführen der Anleitung, gib den nächsten Wert zurück
	public async IAsyncEnumerable<int> GetNumbers()
	{
		//Verwendung: GetNumbers() aufrufen, bei jedem Schleifendurchlauf await benutzen (weil wir auf die Zahlen warten müssen)
		while (true)
		{
			await Task.Delay(Random.Shared.Next(100, 1000));
			yield return Random.Shared.Next();
		}
	}
}