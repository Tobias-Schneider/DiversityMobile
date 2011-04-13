﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using log4net;
using log4net.Config;

namespace Diversity_Synchronization.Options.Language
{
    /**
     * Implementierung der Sprache im XML-Format.
     */
    public class LanguageXML : ILanguage
    {
        private string filePath;
        protected static readonly ILog logger = log4net.LogManager.GetLogger(typeof(LanguageXML));

        private Dictionary<long, string> dictValues = new Dictionary<long, string>();

        /**
         * Konstruktor benötigt den Pfad der verwendeten Sprachdatei.
         * Die Datei wird beim Aufruf eingelesen und die Texte können anhand
         * ihrer ID anschließend abgefragt werden.
         */
        public LanguageXML(string filePath) {
            this.filePath = filePath;

           
            readXMLFile();
        }

        /**
         * Gibt die angeforderte Zeichenkette, die durch die id identifiziert
         * wird zurück.
         * Falls diese nicht existiert, wird eine leere Zeichenkette zurückgegeben.
         */
        public string getLanguageString(long id)
        {
            if (dictValues.ContainsKey(id))
            {
                return dictValues[id];
            }
            else
            {
                return "";
            }
        }

        /**
         * Liest die im Konstruktor angegebene Sprachdatei aus und speichert die Werte
         * für die nachfolgende Abfrage.
         */
        private void readXMLFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNodeList nodes = doc.GetElementsByTagName("String");
            long id = -1;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Attributes["id"] != null)
                {
                    try
                    {
                        id = long.Parse(nodes[i].Attributes["id"].InnerText);
                        dictValues.Add(id, nodes[i].InnerText.Replace("\\n", "\n"));
                    }
                    catch
                    {
                       
                        logger.Warn("There is an error with an identifier in file " + filePath + " at ID: " + id);
                    }
                }
                else
                {
                    logger.Warn("String without identifier found in file " + filePath + " at position " + i);
                }
            }
        }
    }
}