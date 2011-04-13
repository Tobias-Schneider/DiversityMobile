using System;
using System.Collections.Generic;
using System.Text;

namespace Diversity_Synchronization.Options.Language
{
    /**
     * Interface für den standardisierten Zugriff auf die Texte
     */
    public interface ILanguage
    {
        /**
         * Gibt die angeforderte Zeichenkette, die durch die id identifiziert
         * wird zurück.
         * Falls diese nicht existiert, wird eine leere Zeichenkette zurückgegeben.
         */
        string getLanguageString(long id);
    }
}
