using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Delegates;

public class Events
{
	/// <summary>
	/// Event
	/// 
	/// Statischer Punkt (nicht static) an welchen eine Methode angehängt werden kann
	/// 
	/// Zweiseitige Entwicklung:
	/// - Entwicklerseite: Definiert das Event (event [Delegate] [Name]), und ruft dieses auf
	/// -- Definiert auch, unter welchen Bedingungen das Event ausgeführt wird (if's)
	/// - Anwenderseite: Hängt eine Methode an das Event (+=) an, und definiert dadurch, was bei dem Event passieren soll
	/// 
	/// Beispiel: Click-Event
	/// - Wann wird das Click-Event ausgeführt? Wenn der Cursor auf dem Button ist, wenn der Button aktiviert ist, wenn keine anderen UI-Elemente darüber sind, ...
	/// - Was soll beim Click-Event passieren? Kommt auf die Anwendung an
	/// </summary>
	public event EventHandler TestEvent; //Entwicklerseite

	public event EventHandler<DataEventArgs> ArgsEvent;

	public event Action<int> IntEvent; //EventHandler ist nicht erzwungen für Events (hier kann ein beliebiges Delegate eingesetzt werden)

	static void Main(string[] args) => new Events();

    public Events()
    {
		TestEvent += Events_TestEvent; //Anwenderseite
		TestEvent?.Invoke(this, EventArgs.Empty); //Entwicklerseite

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent?.Invoke(this, new DataEventArgs() { Status = "Erfolg" });

		IntEvent += Events_IntEvent;
		IntEvent?.Invoke(10);

		//Praktisches Beispiel: ObservableCollection
		//Hat einen Benachrichtigungsmechanismus, dieser ist über Events implementiert
		ObservableCollection<int> zahlen = [];
		zahlen.CollectionChanged += Zahlen_CollectionChanged; //Wird immer gefeuert, wenn sich etwas ändert
		zahlen.Add(1); //Benachrichtigung
		zahlen.Remove(1); //Benachrichtigung
	}

	/// <summary>
	/// Das Event gibt uns eine Schnittstelle, um auf Änderungen zu reagieren
	/// Über diese Methode definieren wir, was bei bestimmten Aktionen passieren soll
	/// </summary>
	private void Zahlen_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		switch (e.Action)
		{
			case NotifyCollectionChangedAction.Add:
                Console.WriteLine($"Neues Item hinzugefügt: {e.NewItems[0]}");
                break;
			case NotifyCollectionChangedAction.Remove:
				Console.WriteLine($"Item entfernt: {e.OldItems[0]}");
				break;
		}
	}

	/// <summary>
	/// Methodenzeiger für das Delegate
	/// EventHandler gibt sender und args vor
	/// </summary>
	private void Events_TestEvent(object? sender, EventArgs e)
	{
        Console.WriteLine("TestEvent ausgeführt");
	}

	private void Events_ArgsEvent(object? sender, DataEventArgs e)
	{
        Console.WriteLine(e.Status);
	}

	private void Events_IntEvent(int obj)
	{
		Console.WriteLine(obj);
	}
}

public class DataEventArgs : EventArgs
{
	public string Status { get; set; }
}