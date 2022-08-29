using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace CdktfTest.Serialization;


public class TypeInspector : ITypeInspector
{
    private readonly ITypeInspector _decorated;
    public TypeInspector(ITypeInspector decorated)
    {
        _decorated = decorated;
    }

    public IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container)
    {
        return _decorated.GetProperties(type, container);
    }

    public IPropertyDescriptor GetProperty(Type type, object? container, string name, [MaybeNullWhen(true)] bool ignoreUnmatched)
    {
        var a = _decorated.GetProperty(type, container, name, ignoreUnmatched);
        return a;
    }
}

public class TypeResolver : ITypeResolver
{
    public Type Resolve(Type staticType, object? actualValue)
    {
        

        return null;
    }
}

public class NodeResolver : INodeTypeResolver
{
    public bool Resolve(NodeEvent? nodeEvent, ref Type currentType)
    {
        if(nodeEvent is Scalar)
        {

        }
        var t = nodeEvent.GetType();

        return false;
    }
}

public class TypeBlah : YamlDotNet.Serialization.IYamlTypeConverter
{
    public bool Accepts(Type type)
    {
        return false;
    }

    public object? ReadYaml(IParser parser, Type type)
    {
        throw new NotImplementedException();
    }

    public void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        throw new NotImplementedException();
    }
}

/*
public class ProviderTypeDiscriminator : ITypeDiscriminator
{
    private readonly Dictionary<string, Type> typeLookup;

    public ProviderTypeDiscriminator(INamingConvention namingConvention)
    {
        typeLookup = new Dictionary<string, Type>() {
            { "url", typeof(UrlFoo) },
            { "ftp" , typeof(FtpFoo) },
            // more here...
        };
    }

    public Type BaseType => typeof(FooBase);

    public bool TryResolve(ParsingEventBuffer buffer, out Type suggestedType)
    {
        if (buffer.TryFindMappingEntry(
            scalar => typeLookup.ContainsKey(scalar.Value),
            out Scalar key,
            out ParsingEvent _))
        {
            suggestedType = typeLookup[key.Value];
            return true;
        }

        suggestedType = null;
        return false;
    }
}
*/