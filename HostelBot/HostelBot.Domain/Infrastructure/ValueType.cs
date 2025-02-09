﻿using System.Reflection;

namespace HostelBot.Domain.Infrastructure;

public class ValueType<T> : DddObject<T>
    where T : ValueType<T>
{
    private static readonly string ClassName;
    
    static ValueType()
    {
        ClassName = typeof(T).Name;
    }
		
    public bool Equals(T other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        var values1 = Properties.Select(property => property.GetValue(this)).ToArray();
        var values2 = Properties.Select(property => property.GetValue(other)).ToArray();
        return values1.SequenceEqual(values2);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as T);
    }

    public override int GetHashCode()
    {
        var hash = 0;
        var values = Properties.Select(property => property.GetValue(this)).ToArray();
        foreach (var value in values)
        {
            unchecked
            {
                hash *= 16777619;
                hash += value?.GetHashCode() ?? 0;
            }
        }

        return hash;
    }

    public override string ToString()
    {
        var values = Properties.Select(property => property.GetValue(this)?.ToString()).ToArray();
        var property2Name = values
            .Zip(Properties, (value, property) => $"{property.Name}: {value ?? ""}");
        return $"{ClassName}({string.Join("; ", property2Name)})";
    }
}