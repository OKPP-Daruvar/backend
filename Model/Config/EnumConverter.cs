using Google.Cloud.Firestore;

namespace Forms.Model.Config
{
    public class EnumConverter<TEnum> : IFirestoreConverter<TEnum> where TEnum : Enum
    {
        public object ToFirestore(TEnum value)
        {
            return value.ToString(); 
        }

        public TEnum FromFirestore(object value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value.ToString());
        }
    }
}
