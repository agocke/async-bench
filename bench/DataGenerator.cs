
extern alias SerdeSync;
using System;
using SerdeSync::Serde;

namespace Benchmarks
{
    internal static class DataGenerator
    {
        public static T GenerateSerialize<T>() where T : SerdeSync::Serde.ISerializeProvider<T>
        {
            if (typeof(T) == typeof(Location))
                return (T)(object)CreateLocation();
            if (typeof(T) == typeof(AllInOne))
                return (T)(object)new AllInOne();

            throw new InvalidOperationException();
        }

        public static Location CreateLocation() => new Location
        {
            Id = 1234,
            Address1 = "The Street Name",
            Address2 = "20/11",
            City = "The City",
            State = "The State",
            PostalCode = "abc-12",
            Name = "Nonexisting",
            PhoneNumber = "+0 11 222 333 44",
            Country = "The Greatest"
        };

        public static string GenerateDeserialize<T>()
        {
            if (typeof(T) == typeof(Location))
                return LocationSample;
            if (typeof(T) == typeof(AllInOne))
                return AllInOne.SampleSerialized;

            throw new InvalidOperationException("Unexpected type");
        }

        public const string LocationSample = """
{
    "id": 1234,
    "address1": "The Street Name",
    "address2": "20/11",
    "city": "The City",
    "state": "The State",
    "postalCode": "abc-12",
    "name": "Nonexisting",
    "phoneNumber": "+0 11 222 333 44",
    "country": "The Greatest"
}
""";
    }
}