namespace Shendeti.Infrastructure.Exceptions;

public class EntityNullException : Exception
{
    public EntityNullException(Type type, int id) : base($"Entity {type.Name} with id {id} not found!")
    {
    }


    public EntityNullException(Type type) : base($"Entity {type.Name} not found!")
    {
        
    }
}