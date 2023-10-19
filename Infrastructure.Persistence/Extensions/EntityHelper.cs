using System.Reflection;

namespace Infrastructure.Persistence.Extensions
{
    public static class EntityHelper
    {
        public static void CreateEntity<T>(T entity) where T : new()
        {
            PropertyInfo createdDateProperty = typeof(T).GetProperty("CreatedDate");
            PropertyInfo createdByProperty = typeof(T).GetProperty("CreatedBy");

            createdDateProperty.SetValue(entity, DateTime.UtcNow);
            createdByProperty.SetValue(entity, "user1");

        }

        public static void UpdateEntity<T>(T entity) where T : new()
        {
            PropertyInfo modifiedDateProperty = typeof(T).GetProperty("ModifiedDate");
            PropertyInfo modifiedByProperty = typeof(T).GetProperty("ModifiedBy");

            modifiedDateProperty.SetValue(entity, DateTime.UtcNow);
            modifiedByProperty.SetValue(entity, "user2");

        }
    }
}