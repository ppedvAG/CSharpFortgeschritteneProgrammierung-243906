using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//Task.Run(Toast);
		//Task.Run(Tasse);
		//Task.Run(Kaffee);
		//Hier wird Code normal weitergeführt
		//Console.WriteLine(sw.ElapsedMilliseconds); //0s (Hmm)

		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Tasse);
		//t2.Wait(); //Problem: Blockieren des Main Threads
		//Task t3 = Task.Run(Kaffee);
		//Task.WaitAll(t1, t2, t3); //Problem: Blockieren des Main Threads
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Eine Lösung: ContinueWith
		//bool output = false;
		//Task t1 = Task.Run(Toast).ContinueWith(t =>
		//{
		//	if (!output)
		//	{
		//		Console.WriteLine(sw.ElapsedMilliseconds);
		//		output = true;
		//	}
		//}); //Problem: Was ist wenn der Toast schneller fertig wird?
		//Task t2 = new Task(Tasse);
		//t2.ContinueWith(t => Kaffee())
		//	.ContinueWith(t =>
		//	{
		//		if (!output)
		//		{ 
		//			Console.WriteLine(sw.ElapsedMilliseconds);
		//			output = true;
		//		}
		//	});
		//t2.Start();

		//Bessere Lösung: Async/Await
		//Task t1 = Task.Run(Toast);
		//Task t2 = new Task(Tasse);
		//t2.ContinueWith(t => Kaffee());
		//t2.Start();

		////await: Ähnlich wie t.Wait(), aber blockiert nicht
		////await kann nur innerhalb von async Methoden verwendet werden
		//await t1; //Kann nur auf Methoden/Klassen angewandt werden, welche Awaitable sind
		//await t2;
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//async: Methode, welche den Task selbst darstellt
		//-> Die Methode muss nicht mehr mit Task.Run/new Task ausgeführt werden
		//Wenn eine Async Methode (ohne void) gestartet wird, wird diese automatisch als Task gestartet

		//Rückgabetypen von async Methoden
		//async void: Kann selbst await benutzen, aber kann selbst nicht awaited werden
		//async Task: Kann selbst await benutzen, und kann selbst awaited werden
		//async Task<T>: Kann selbst await benutzen, kann selbst awaited werden und hat ein Ergebnis (Objekt per return)
		//Task t1 = ToastAsync();
		//Task t2 = TasseAsync().ContinueWith(t => KaffeeAsync());
		////await t2; //await Statements sollten generell nach ihrer Dauer sortiert sein (schnellstes zuerst)
		////await t1;
		//await Task.WhenAll(t1, t2); //Mehrere awaits konsolidieren

		//Task<Toast> t1 = ToastObjectAsync();
		//Task<Tasse> t2 = TasseObjectAsync();
		//Tasse tasse = await t2;
		//Task<Kaffee> t3 = KaffeeObjectAsync(tasse);
		//Kaffee k = await t3;
		//Toast t = await t1; //Äquivalent zu t.Result, aber nicht blockierend
		//Fruehstueck f = new Fruehstueck(k, t);
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Aufbau:
		//- Aufgabe(n) starten
		//- Zwischenschritte (optional)
		//- Auf Ergebnisse warten
		 
		//Kompakter
		Task<Toast> t1 = ToastObjectAsync();
		Task<Tasse> t2 = TasseObjectAsync();
		Task<Kaffee> t3 = KaffeeObjectAsync(await t2);
		Fruehstueck f = new Fruehstueck(await t3, await t1);

		Console.ReadKey();
	}

	#region Synchron
	static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	////////////////////////////////////////

	#region Asynchron
	static async Task ToastAsync()
	{
		await Task.Delay(4000); //Alternative zu Thread.Sleep
		Console.WriteLine("Toast fertig");
		//Hier kein return notwendig
	}

	static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	////////////////////////////////////////

	#region Asynchron + Objekt
	static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
	#endregion
}

public record Toast();

public record Tasse();

public record Kaffee(Tasse t);

public record Fruehstueck(Kaffee k, Toast t);