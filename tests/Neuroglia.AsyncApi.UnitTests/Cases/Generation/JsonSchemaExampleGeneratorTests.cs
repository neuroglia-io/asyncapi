using Json.Schema;
using Json.Schema.Generation;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Generation;
using Neuroglia.Serialization;
using System.Collections;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Generation;

public class JsonSchemaExampleGeneratorTests
    : IDisposable
{

    public JsonSchemaExampleGeneratorTests()
    {
        var services = new ServiceCollection();
        services.AddSerialization();
        services.AddJsonSerializer();
        services.AddSingleton<IJsonSchemaExampleGenerator, JsonSchemaExampleGenerator>();
        this.ServiceProvider = services.BuildServiceProvider();
        this.ExampleGenerator = this.ServiceProvider.GetRequiredService<IJsonSchemaExampleGenerator>();
    }

    protected ServiceProvider ServiceProvider { get; }

    protected IJsonSchemaExampleGenerator ExampleGenerator { get; }

    [Fact]
    public void Generate_Example_Array_Should_Work()
    {
        //arrange
        var schema = new JsonSchemaBuilder().FromType(typeof(List<int>));

        //act
        var generated = ((IEnumerable)this.ExampleGenerator.GenerateExample(schema)!).OfType<int>();

        //assert
        generated.Should().NotBeNull();
        generated.Should().HaveCountGreaterThanOrEqualTo(1);
    }

    [Fact]
    public void Generate_Example_Boolean_Should_Work()
    {
        //arrange
        var schema = new JsonSchemaBuilder().FromType(typeof(bool));

        //act
        var generated = (bool?)this.ExampleGenerator.GenerateExample(schema);

        //assert
        generated.Should().NotBeNull();
    }

    [Fact]
    public void Generate_Example_Integer_Should_Work()
    {
        //arrange
        var schema = new JsonSchemaBuilder().FromType(typeof(int));

        //act
        var generated = (int?)this.ExampleGenerator.GenerateExample(schema);

        //assert
        generated.Should().NotBeNull();
    }

    [Fact]
    public void Generate_Example_Number_Should_Work()
    {
        //arrange
        var schema = new JsonSchemaBuilder().FromType(typeof(decimal));

        //act
        var generated = (decimal?)this.ExampleGenerator.GenerateExample(schema);

        //assert
        generated.Should().NotBeNull();
    }

    [Fact]
    public void Generate_Example_Object_Should_Work()
    {
        //arrange
        var schema = new JsonSchemaBuilder().FromType(typeof(object));

        //act
        var generated = this.ExampleGenerator.GenerateExample(schema);

        //assert
        generated.Should().NotBeNull();
    }

    [Fact]
    public void Generate_Example_String_Should_Work()
    {
        //arrange
        var schema = new JsonSchemaBuilder().FromType(typeof(string));

        //act
        var generated = (string?)this.ExampleGenerator.GenerateExample(schema);

        //assert
        generated.Should().NotBeNull();
    }

    void IDisposable.Dispose()
    {
        this.ServiceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

}
