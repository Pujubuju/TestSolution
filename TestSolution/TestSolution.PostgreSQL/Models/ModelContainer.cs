namespace TestSolution.PostgreSQL.Models
{
    public class ModelContainer : Entity
    {
        public virtual string Name { get; set; }
        public virtual Model Model { get; set; }
    }
}
