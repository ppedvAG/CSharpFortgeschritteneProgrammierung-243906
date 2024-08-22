namespace Multitasking;

public class ExceptionsImTask
{
    static void Main(string[] args)
    {
		Task t1 = new Task(Run, 25);
		t1.Start();

		Task t2 = new Task(Run, 50);
		t2.Start();

		Task t3 = new Task(Run, 75);
		t3.Start();

		try
		{
			Task.WaitAll(t1, t2, t3);
		}
		catch (AggregateException ex) //AggregateException: Sammelexception für mehrere Tasks
		{
			//Problem: Hier werden alle Exception gleichzeitig ausgegeben
			//D.h.: Wenn ein Task schon vorher fehlschlägt, bekommen wir das hier nicht mit
			foreach (Exception e in ex.InnerExceptions)
			{
                Console.WriteLine(e.Message);
            }
		}
    }

	static void Run(object o)
	{
		for (int i = 0; i < 100; i++)
		{
            Console.WriteLine($"Run: {i}");
			Thread.Sleep(20);
			if (i > (int) o)
				throw new FieldAccessException();
        }
	}
}