namespace Multitasking;

public class MutexDemo
{
    static void Main(string[] args)
    {
		Mutex m;
		if (Mutex.TryOpenExisting("MutexDemo", out m))
		{
			Console.WriteLine("Applikation bereits gestartet");
			//Environment.Exit(0);
		}
		else
		{
			m = new Mutex(true, "MutexDemo");
            Console.WriteLine("Mutex geöffnet");
        }
		Console.ReadKey();
		m.Close();
	}
}