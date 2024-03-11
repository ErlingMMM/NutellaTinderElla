namespace NutellaTinderEllaApi.Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string type, int id)
            : base($"{type} with Id: {id} could not be found.")
        {
        }

        public EntityNotFoundException(string type, int id, int matchedUserId)
            : base($"{type} with Id: {id} and MatchedUserId: {matchedUserId} could not be found.")
        {
        }
    }
}
