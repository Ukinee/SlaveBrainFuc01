namespace ApplicationCode.Core.Infrastructure.IdGenerators
{
    public class IdGenerator : IIdGenerator
    {
        private int _id;

        public IdGenerator(int initialValue = 0)
        {
            _id = initialValue;
        }

        public int Generate() =>
            _id++;
    }
}