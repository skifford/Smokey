namespace Smokey.Demo.Features.ValuesCollecting
{
    public sealed class Element
    {
        public string Name { get; private set; }

        public Element SetName(string name)
        {
            Name = name;
            return this;
        }
    }
}
