using System;
using System.Collections.Generic;
using System.Text;

namespace Diversity_Synchronization.Options.Serializer
{
    /**
     * Interface für den standardisierten Zugriff auf Optionen
     */
    public interface IOptionsSerializer<T> where T : IComparable, new()
    {
        /**
         * Speichern der angegebenen Optionen
         */
        void serialize(T options);

        /**
         * Abfragen der Optionen
         */
        T deserialize();
    }
}
