using System.Collections;

namespace Generics;

public class Constraints
{
	public void Test<T>() where T : class { }

	public void Test5<T>() where T : class, new()
	{
		T t = new T(); //Nicht möglich, außer das new Constraint existiert
		t = null; //Nicht möglich, außer das class Constraint existiert
	}
}

public class Test1<T> where T : class { } //T muss ein Referenztyp sein

public class Test2<T> where T : struct { } //T muss ein Wertetyp sein

public class Test3<T> where T : IEnumerable { } //T muss von IEnumerable erben (T muss ein Listentyp sein)

public class Test4<T> where T : Program { } //T muss von Program erben

public class Test5<T> where T : new() { } //T muss einen Standardkonstruktor haben

public class Test6<T> where T : unmanaged { } //T muss ein Basisdatentyp sein (int, long, bool, float, Enumwert, Pointertyp, ...)

public class Test7<T> where T : notnull { } //T darf nicht nullable sein

public class Test8<T> where T : Delegate { } //T muss ein Delegatetyp sein

public class Test9<T> where T : Enum { } //T muss ein Enumtyp sein

public class Test10<T> where T : class, new() { } //Mehrere Constraints

//Mehrere Constraints auf mehreren Generics
public class Test11<T1, T2>
	where T1 : unmanaged
	where T2 : class
{

}