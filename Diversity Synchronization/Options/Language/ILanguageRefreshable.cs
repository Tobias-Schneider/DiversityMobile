using System;
using System.Collections.Generic;
using System.Text;

namespace Diversity_Synchronization.Options.Language
{
    /**
     * Wird die Sprache der Anzeige während der Laufzeit geändert, können
     * untergeordnete Fenster/Seiten darüber informiert werden und ihre Anzeige
     * aktualisieren.
     */
    public interface ILanguageRefreshable
    {
        /**
         * Weist ein Fenster/Seite darauf hin, dass sich die Sprache geändert hat und
         * die Anzeige aktualisiert werden muss.
         */
        void RefreshLanguage();
    }
}
