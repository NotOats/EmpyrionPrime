using Bogus;
using Eleon.Modding;
using EmpyrionPrime.RemoteClient.Tests.TheoryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionPrime.RemoteClient.Tests;

public class CommandSerializerTests
{
    [Theory]
    [ClassData(typeof(EmpyrionApiCommandsTheoryData))]
    public void Test_Full_Serialization(CommandId id, object original)
    {
        var data = CommandSerializer.Serialize(id, original);
        Assert.NotNull(data);
        Assert.NotEmpty(data);

        var returned = CommandSerializer.Deserialize(id, data);
        Assert.NotNull(returned);

        Assert.Equal(original.GetType(), returned.GetType());
        Assert.True(PublicInstancePropertiesEqual(original, returned));
    }

    [Fact]
    public void Test_Invalid_Type_Throws()
    {
        var faker = new Faker<PlayfieldLoad>()
            .StrictMode(true)
            .RuleFor(o => o.sec, f => f.Random.Float())
            .RuleFor(o => o.playfield, f => f.Random.Word())
            .RuleFor(o => o.processId, f => f.Random.Int());

        var original = faker.Generate();

        Assert.Throws<ArgumentException>(() => CommandSerializer.Serialize(CommandId.Event_ChatMessage, original));
    }

    private static bool PublicInstancePropertiesEqual(object x, object y)
    {
        // Not null or both not null
        if (x == null || y == null)
            return x == y;

        // Types match
        if (x.GetType() != y.GetType())
            return false;

        // Loop through properties
        var type = x.GetType();
        foreach(var pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var xVal = pi.GetValue(x);
            var yVal = pi.GetValue(y);

            if (xVal != yVal)
                return false;
        }

        return true;
    }
}
