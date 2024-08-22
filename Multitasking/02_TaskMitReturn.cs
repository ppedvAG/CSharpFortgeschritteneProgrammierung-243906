namespace Multitasking;

public class TaskMitReturn
{
    static void Main(string[] args)
    {
		Task<int> t = new Task<int>(Berechne, 23);
		t.ContinueWith(t => Console.WriteLine(t.Result)); //Wenn t fertig ist, führe diesen Folgetask aus
		t.Start();

        //Console.WriteLine(t.Result); //Problem: Diese Codezeile blockiert den restlichen Code danach
		//Lösungen: ContinueWith, Async/Await

		for (int i = 0; i < 100; i++)
		{
            Console.WriteLine($"Main: {i}");
			Thread.Sleep(20);
        }
	}

	static int Berechne(object o)
	{
		if (o is int i)
		{
			Thread.Sleep(1000);
			return i * i;
		}
		return 0;
	}
}