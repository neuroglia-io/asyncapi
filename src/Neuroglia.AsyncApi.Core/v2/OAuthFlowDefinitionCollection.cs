using System.Collections;

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents a collection of <see cref="OAuthFlowDefinition"/>s
/// </summary>
[DataContract]
public record OAuthFlowDefinitionCollection
    : IEnumerable<KeyValuePair<string, OAuthFlowDefinition>>
{

    /// <summary>
    /// Gets/sets the configuration for the OAuth Implicit flow
    /// </summary>
    [DataMember(Order = 1, Name = "implicit"), JsonPropertyOrder(1), JsonPropertyName("implicit"), YamlMember(Order = 1, Alias = "implicit")]
    public virtual OAuthFlowDefinition? Implicit { get; set; }

    /// <summary>
    /// Gets/sets the configuration for the OAuth Resource Owner Protected Credentials flow
    /// </summary>
    [DataMember(Order = 2, Name = "password"), JsonPropertyOrder(2), JsonPropertyName("password"), YamlMember(Order = 2, Alias = "password")]
    public virtual OAuthFlowDefinition? Password { get; set; }

    /// <summary>
    /// Gets/sets the configuration for the OAuth Client Credentials flow
    /// </summary>
    [DataMember(Order = 3, Name = "clientCredentials"), JsonPropertyOrder(3), JsonPropertyName("clientCredentials"), YamlMember(Order = 3, Alias = "clientCredentials")]
    public virtual OAuthFlowDefinition? ClientCredentials { get; set; }

    /// <summary>
    /// Gets/sets the configuration for the OAuth Authorization Code flow
    /// </summary>
    [DataMember(Order = 4, Name = "authorizationCode"), JsonPropertyOrder(4), JsonPropertyName("authorizationCode"), YamlMember(Order = 4, Alias = "authorizationCode")]
    public virtual OAuthFlowDefinition? AuthorizationCode { get; set; }

    /// <inheritdoc/>
    public virtual IEnumerator<KeyValuePair<string, OAuthFlowDefinition>> GetEnumerator()
    {
        if (Implicit != null) yield return new(nameof(Implicit), Implicit);
        if (Password != null) yield return new(nameof(Password), Password);
        if (ClientCredentials != null) yield return new(nameof(ClientCredentials), ClientCredentials);
        if (AuthorizationCode != null) yield return new(nameof(AuthorizationCode), AuthorizationCode);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}