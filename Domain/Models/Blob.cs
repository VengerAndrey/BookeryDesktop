namespace Domain.Models
{
    public class Blob : DomainObject
    {
        public string Name { get; set; }
        public Container Container { get; set; }
    }
}
