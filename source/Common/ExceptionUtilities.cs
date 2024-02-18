// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Company.Product.WebApi.Common;

public static class ExceptionUtilities
{
    /// <summary>Throws an <see cref="ArgumentNullException" /> if <paramref name="value" /> is <c>null</c>.</summary>
    /// <typeparam name="T">The type of <paramref name="value" />.</typeparam>
    /// <param name="value">The value to be checked for <c>null</c>.</param>
    /// <param name="valueExpression">The expression of the value being checked.</param>
    /// <exception cref="ArgumentNullException"><paramref name="value" /> is <c>null</c>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string? valueExpression = null)
        where T : class
    {
        if (value is null)
        {
            AssertNotNull(valueExpression);
            ThrowArgumentNullException(valueExpression);
        }
    }

    /// <summary>Throws an <see cref="ArgumentException" />.</summary>
    /// <param name="message">The message detailing the cause of the exception.</param>
    /// <param name="paramName">The name of the value that caused the exception.</param>
    /// <exception cref="ArgumentException"><paramref name="message" /></exception>
    [DoesNotReturn]
    public static void ThrowArgumentException(string message, string paramName)
        => throw new ArgumentException(message, paramName);

    /// <summary>Throws an <see cref="ArgumentException" />.</summary>
    /// <param name="message">The message detailing the cause of the exception.</param>
    /// <param name="paramName">The name of the value that caused the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    /// <exception cref="ArgumentException"><paramref name="message" /></exception>
    [DoesNotReturn]
    public static void ThrowArgumentException(string message, string paramName, Exception innerException)
        => throw new ArgumentException(message, paramName, innerException);

    /// <summary>Throws an <see cref="ArgumentNullException" />.</summary>
    /// <param name="paramName">The name of the value that is <c>null</c>.</param>
    /// <exception cref="InvalidOperationException"><paramref name="paramName" /> is <c>null</c>.</exception>
    [DoesNotReturn]
    public static void ThrowArgumentNullException(string paramName)
    {
        var message = string.Format(CultureInfo.InvariantCulture, "'{0}' is null", paramName);
        throw new ArgumentNullException(paramName, message);
    }

    /// <summary>Throws an <see cref="ArgumentOutOfRangeException" />.</summary>
    /// <typeparam name="T">The type of <paramref name="actualValue" />.</typeparam>
    /// <param name="paramName">The name of the value that caused the exception.</param>
    /// <param name="actualValue">The value that caused the exception.</param>
    /// <param name="message">The message detailing the cause of the exception.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="message" /></exception>
    [DoesNotReturn]
    public static void ThrowArgumentOutOfRangeException<T>(string paramName, T actualValue, string message)
        => throw new ArgumentOutOfRangeException(paramName, actualValue, message);

    /// <summary>Throws a <see cref="InvalidOperationException" />.</summary>
    /// <param name="message">The message detailing the cause of the exception.</param>
    /// <exception cref="InvalidOperationException"><paramref name="message" />.</exception>
    [DoesNotReturn]
    public static void ThrowInvalidOperationException(string message)
        => throw new InvalidOperationException(message);

    /// <summary>Throws an <see cref="UnreachableException" />.</summary>
    /// <param name="message">The message detailing the cause of the exception.</param>
    /// <exception cref="UnreachableException"><paramref name="message" /></exception>
    [DoesNotReturn]
    public static void ThrowUnreachableException(string? message) => throw new UnreachableException(message);
}
