namespace Delegates;

/// <summary>
/// Entwicklerseite
/// </summary>
public class Component
{
	public event Action Start;

	public event Action Stop;

	public event Action<int> Progress;

	/// <summary>
	/// Komponente, welche einen länger andauernden Prozess durchführt, und den Status über Events weitergibt
	/// </summary>
	public void StartProcess()
	{
		Start?.Invoke(); //Wenn der User keine Methode anhängt, soll das Event nicht ausgeführt werden

		for (int i = 0; i< 10; i++)
		{
			Progress?.Invoke(i);
			Thread.Sleep(200);
		}

		Stop?.Invoke();
	}
}