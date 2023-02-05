namespace BelkiHakiki.Core
{
    public class Passenger : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }
        public int Age { get; set; }
        public int GenderId { get; set; }
    }
}
