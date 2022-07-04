using Smokey.Features.ValuesCollecting;

namespace Smokey.Demo.Features.ValuesCollecting
{
    public class Component
    {
        [Collectable(Key = Constants.Keys.Element)]
        public Element Element { get; }
        
        public ICollectableProperty<string> Title { get; }
        
        public ICollectableProperty<string> Description { get; }

        public Component()
        {
            Element = new Element();
            
            Title = new CollectableProperty<string>(
                key: Key.Create(nameof(Title), nameof(Component)),
                collectableType: CollectableType.Setter)
            {
                Value = string.Empty
            };
            
            Description = new CollectableProperty<string>(
                key: Key.Create(nameof(Description), nameof(Component)),
                collectableType: CollectableType.Setter)
            {
                Value = string.Empty
            };
        }

        public Component SetElementName(string name)
        {
            Element.SetName(name);
            return this;
        }
        
        public Component SetTitle(string title)
        {
            Title.Value = title;
            return this;
        }

        public Component SetDescription(string description)
        {
            Description.Value = description;
            return this;
        }
    }
}
