using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions
{
    public abstract class QueryComponent
    {

        public abstract StringBuilder ToSqlString();
    }

    public class QueryGenerator : QueryComponent
    {
        List<QueryComponent> _QueryComponents;


        public QueryGenerator()
        {
            _QueryComponents = new List<QueryComponent>();
        }

        public void AddQueryComponent(QueryComponent component)
        {
            _QueryComponents.Add(component);
        }

        
        public void ReplacePlaceHolders(QueryGenerator generator)
        {    
            for (int i = 0; i < _QueryComponents.Count; i++)
            {
                if (_QueryComponents[i] is PlaceHolderComponent)
                {
                    _QueryComponents[i] = generator;
                }

                //Diese Variante endet sehr bald einen Stackoverflow --> keine Rekursion
                //else if (_QueryComponents[i] is QueryGenerator)
                //{
                //    ((QueryGenerator)_QueryComponents[i]).ReplacePlaceHolders(generator);
                //}
            }

            //Iteratives Vorgehen vermeidet den StachOverflow
            List<QueryComponent> tmp = new List<QueryComponent>();
            foreach (QueryComponent c in _QueryComponents)
            {
                if (c is QueryGenerator)
                {
                    tmp.AddRange(((QueryGenerator)c)._QueryComponents);
                } 
                else 
                {
                    tmp.Add(c);
                }
            }

            _QueryComponents = tmp;
        }

        public override StringBuilder ToSqlString()
        {
            StringBuilder ret = new StringBuilder();
            foreach (QueryComponent c in _QueryComponents)
            {
                ret.Append(c.ToSqlString());
            }

            return ret;
        }
    }

    public class SqlStringComponent : QueryComponent
    {
        private StringBuilder _SqlString;

        public SqlStringComponent(String sqlString)
        {
            _SqlString = new StringBuilder();
            _SqlString.Append(sqlString);
        }
        
        public SqlStringComponent(StringBuilder sqlString)
        {
            _SqlString = sqlString;
        }

        public override StringBuilder ToSqlString()
        {
            return _SqlString;
        }
    }

    public class PlaceHolderComponent : QueryComponent
    {


        public override StringBuilder ToSqlString()
        {
            throw new InvalidOperationException("PlaceHolderComponent cannot be transformed to a StringBuilder");
        }
    }
}
