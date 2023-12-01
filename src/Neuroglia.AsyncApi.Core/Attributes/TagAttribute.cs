using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents an <see cref="Attribute"/> used to tag an <see cref="OperationDefinition"/> method
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class TagAttribute
    : Attribute
{
    
    /// <summary>
    /// Initializes a new <see cref="TagAttribute"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="TagDefinition"/> to generate</param>
    /// <param name="description">The description of the <see cref="TagDefinition"/> to generate</param>
    public TagAttribute(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Name = name;
        this.Description = description;
    }

    /// <summary>
    /// Initializes a new <see cref="TagAttribute"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="TagDefinition"/> to generate</param>
    public TagAttribute(string name) : this(name, null) { }

    /// <summary>
    /// Gets the name of the <see cref="TagDefinition"/> to generate
    /// </summary>
    public virtual string Name { get; }

    /// <summary>
    /// Gets the description of the <see cref="TagDefinition"/> to generate
    /// </summary>
    public virtual string? Description { get; set; }

}
