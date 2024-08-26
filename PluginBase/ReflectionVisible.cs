namespace PluginBase;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class ReflectionVisible : Attribute
{
	public string Name { get; set; }

    public ReflectionVisible(string name)
    {
		Name = name;
    }
}