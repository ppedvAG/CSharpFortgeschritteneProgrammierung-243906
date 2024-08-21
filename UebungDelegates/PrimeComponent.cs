namespace UebungDelegates;

public class PrimeComponent
{
	public event Action<int> Prime;

	public event EventHandler<Prime100EventArgs> Prime100;

	//public event Action<int, int> NotPrime;

	public event EventHandler<NotPrimeEventArgs> NotPrime;

	public void CalculateNumbers()
	{
		int counter = 0;
		for (int i = 3; true; i++)
		{
			bool isPrime = CheckPrime(i);
			if (isPrime)
			{
				counter++;
				if (counter % 100 == 0 && counter != 0) //counter = 100, 200, 300, 400, ...
				{
					Prime100?.Invoke(this, new Prime100EventArgs(i));
				}
				else
				{
					Prime?.Invoke(i);
				}
			}
			Thread.Sleep(30);
		}
	}

	public bool CheckPrime(int num)
	{
		if (num % 2 == 0)
		{
			NotPrime(this, new NotPrimeEventArgs(num, 2));
			return false;
		}

		for (int i = 3; i <= num / 2; i += 2)
		{
			if (num % i == 0)
			{
				NotPrime(this, new NotPrimeEventArgs(num, i));
				return false;
			}
		}
		return true;
	}
}

public record Prime100EventArgs(int zahl);

public record NotPrimeEventArgs(int zahl, int teiler);