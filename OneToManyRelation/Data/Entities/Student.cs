namespace OneToManyRelation.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        //public int? TeacherId { get; set; } // nullable FK
        //public Teacher? Teacher { get; set; }
    }
}
