using System;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class TableAttribute : Attribute, ITarget
    {
        private string _tableMapping = null;
        private string _target = AttributeConstants.DEFAULT_TARGET;

        public TableAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableAttribute"/> class.
        /// </summary>
        /// <param name="tableMapping">The name of the table in the data base.</param>
        public TableAttribute(string tableMapping)
        {
            TableMapping = tableMapping;
        }

        public string TableMapping { get { return this._tableMapping; } protected set { this._tableMapping = value; } }
        public string Target { get { return _target; } set { _target = value; } }
    }
}
