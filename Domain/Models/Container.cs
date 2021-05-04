namespace Domain.Models
{
    public class Container : DomainObject
    {
        public string Name { get; set; }
        public User Owner { get; set; }
    }
}
