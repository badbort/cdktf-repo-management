using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdktfTest.Serialization
{

    public class PropertyNamesContractContractResolver : DefaultContractResolver
    {
        private static readonly object TypeContractCacheLock = new object();
        private static readonly DefaultJsonNameTable NameTable = new DefaultJsonNameTable();
        private static Dictionary<(Type, Type), JsonContract>? _contractCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CamelCasePropertyNamesContractResolver"/> class.
        /// </summary>
        public PropertyNamesContractContractResolver(NamingStrategy strategy)
        {
            NamingStrategy = strategy;
        }

        /// <summary>
        /// Resolves the contract for a given type.
        /// </summary>
        /// <param name="type">The type to resolve a contract for.</param>
        /// <returns>The contract for a given type.</returns>
        public override JsonContract ResolveContract(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            // for backwards compadibility the CamelCasePropertyNamesContractResolver shares contracts between instances
            StructMultiKey<Type, Type> key = new StructMultiKey<Type, Type>(GetType(), type);
            Dictionary<StructMultiKey<Type, Type>, JsonContract>? cache = _contractCache;
            if (cache == null || !cache.TryGetValue(key, out JsonContract contract))
            {
                contract = CreateContract(type);

                // avoid the possibility of modifying the cache dictionary while another thread is accessing it
                lock (TypeContractCacheLock)
                {
                    cache = _contractCache;
                    Dictionary<StructMultiKey<Type, Type>, JsonContract> updatedCache = (cache != null)
                        ? new Dictionary<StructMultiKey<Type, Type>, JsonContract>(cache)
                        : new Dictionary<StructMultiKey<Type, Type>, JsonContract>();
                    updatedCache[key] = contract;

                    _contractCache = updatedCache;
                }
            }

            return contract;
        }

        internal override DefaultJsonNameTable GetNameTable()
        {
            return NameTable;
        }
    }
}
