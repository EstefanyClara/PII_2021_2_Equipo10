
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proyect
{
    /// <summary>
    /// Handler utilizado para la seralizacion.
    /// </summary>
    public class MyReferenceHandler : ReferenceHandler
    {
        private static MyReferenceHandler instance;

        /// <summary>
        /// La instancia del handler de referencia.
        /// </summary>
        /// <value></value>
        public static MyReferenceHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MyReferenceHandler();
                }
                return instance;
            }
        }

        /// <summary>
        /// Constructor del handler.
        /// </summary>
        /// <returns></returns>
        public MyReferenceHandler() => Reset();
        private ReferenceResolver _rootedResolver;

        /// <summary>
        /// Resolvedor de referencias.
        /// </summary>
        /// <returns></returns>
        public override ReferenceResolver CreateResolver() => _rootedResolver;

        /// <summary>
        /// Reseteador del handler.
        /// </summary>
        public void Reset() => _rootedResolver = new MyReferenceResolver();
    }

    class MyReferenceResolver : ReferenceResolver
    {
        private uint _referenceCount;
        private readonly Dictionary<string, object> _referenceIdToObjectMap = new();
        private readonly Dictionary<object, string> _objectToReferenceIdMap = new(ReferenceEqualityComparer.Instance);

        public override void AddReference(string referenceId, object value)
        {
            if (!_referenceIdToObjectMap.TryAdd(referenceId, value))
            {
                throw new JsonException();
            }
        }

        public override string GetReference(object value, out bool alreadyExists)
        {
            if (_objectToReferenceIdMap.TryGetValue(value, out string referenceId))
            {
                alreadyExists = true;
            }
            else
            {
                _referenceCount++;
                referenceId = _referenceCount.ToString();
                _objectToReferenceIdMap.Add(value, referenceId);
                alreadyExists = false;
            }

            return referenceId;
        }

        public override object ResolveReference(string referenceId)
        {
            if (!_referenceIdToObjectMap.TryGetValue(referenceId, out object value))
            {
                throw new JsonException();
            }

            return value;
        }
    }
}