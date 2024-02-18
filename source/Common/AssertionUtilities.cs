using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Company.Product.WebApi.Common;

/// <summary>Provides a set of methods to supplement <see cref="Debug" />.</summary>
public static class AssertionUtilities
{
    //
    // The public entry points are generic to avoid boxing in the success case
    //

    /// <summary>Asserts that a condition is <c>true</c>.</summary>
    /// <param name="condition">The condition to assert.</param>
    /// <param name="conditionExpression">The expression of the condition that caused the exception.</param>
    /// <exception cref="InvalidOperationException">TerraFX based assertions are disabled.</exception>
    [Conditional("DEBUG")]
    public static void Assert([DoesNotReturnIf(false)] bool condition, [CallerArgumentExpression(nameof(condition))] string? conditionExpression = null)
    {
        if (!condition)
        {
            Fail(conditionExpression);
        }
    }

    /// <summary>Asserts that <paramref name="value" /> is not <c>null</c>.</summary>
    /// <typeparam name="T">The type of <paramref name="value" />.</typeparam>
    /// <param name="value">The value to assert is not <c>null</c>.</param>
    [Conditional("DEBUG")]
    public static void AssertNotNull<T>([NotNull] T? value)
        where T : class => Assert(value is not null);

    /// <summary>Throws an <see cref="UnreachableException" />.</summary>
    [Conditional("DEBUG")]
    [DoesNotReturn]
    public static void Fail(string? message = null)
        => ThrowUnreachableException(message);
}
