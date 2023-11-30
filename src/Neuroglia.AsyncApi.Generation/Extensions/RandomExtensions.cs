namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Defines extensions for <see cref="Random"/>s
/// </summary>
/// <remarks>Code taken from <see href="https://stackoverflow.com/a/28860710/3637555">Bryan Loeper</see></remarks>
public static class RandomExtensions
{

    /// <summary>
    /// Generates a new random decimal
    /// </summary>
    /// <param name="random">The extended <see cref="Random"/></param>
    /// <returns>A new random decimal</returns>
    public static decimal NextDecimal(this Random random) =>random == null ? throw new ArgumentNullException(nameof(random)) : NextDecimal(random, decimal.MaxValue);

    /// <summary>
    /// Generates a new random decimal
    /// </summary>
    /// <param name="random">The extended <see cref="Random"/></param>
    /// <param name="maxValue">The maximal decimal value</param>
    /// <returns>A new random decimal</returns>
    public static decimal NextDecimal(this Random random, decimal maxValue) => random == null ? throw new ArgumentNullException(nameof(random)) : NextDecimal(random, decimal.Zero, maxValue);

    /// <summary>
    /// Generates a new random decimal
    /// </summary>
    /// <param name="random">The extended <see cref="Random"/></param>
    /// <param name="minValue">The minimal decimal value</param>
    /// <param name="maxValue">The maximal decimal value</param>
    /// <returns>A new random decimal</returns>
    public static decimal NextDecimal(this Random random, decimal minValue, decimal maxValue)
    {
        ArgumentNullException.ThrowIfNull(random);

        var nextDecimalSample = NextDecimalSample(random);
        return maxValue * nextDecimalSample + minValue * (1 - nextDecimalSample);
    }

    static decimal NextDecimalSample(Random random)
    {
        ArgumentNullException.ThrowIfNull(random);

        var sample = 1m;
        while (sample >= 1)
        {
            var lo = random.Next();
            var mid = random.Next();
            var hi = random.Next(542101087);
            sample = new(lo, mid, hi, false, 28);
        }

        return sample;
    }

}
