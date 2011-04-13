using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Diversity_Synchronization
{
   public static class PropertyHelper
	{        
	    public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
	    {
	        if (propertyExpression == null)
	        {
	            throw new ArgumentNullException("propertyExpression");
	        }
	 
	        var memberExpression = propertyExpression.Body as MemberExpression;
	 
	        if (memberExpression == null)
	        {
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
	        }
	 
	        var property = memberExpression.Member as PropertyInfo;
	 
	        if (property == null)
	        {
	            throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
	        }
	             
	        return memberExpression.Member.Name;
	    }

        /// <summary>
        /// Erweiterungmethode für Typsichere INotifyPropertyChanged 
        /// </summary>       
        /// <param name="propertyExpression">Lambda-Expression der Form "()=>PropertyName" aus der der Name des Felds extrahiert werden kann</param>
        /// <param name="handler">Handler die aufgerufen werden sollen (das event PropertyChanged)</param>	 
	    public static void RaisePropertyChanged<T>(this INotifyPropertyChanged src, Expression<Func<T>> propertyExpression, PropertyChangedEventHandler handler)
	    {            
	        if (handler != null)
	        {
	            handler(src, new PropertyChangedEventArgs(ExtractPropertyName(propertyExpression)));
	        }
	    }        
        /// <summary>
        /// Erweiterungsmethode, die auf gesetzte Flags prüft
        /// </summary>
        /// <param name="src">Zu prüfender Wert</param>
        /// <param name="f">gesuchte Flag(s)</param>
        /// <returns>Ob alle gewünschten Flags gesetzt sind.</returns>       
        public static bool CheckForFlags(this ConnectionsAccess.ConnectionState src, ConnectionsAccess.ConnectionState f)
        {
            return CheckForFlags(src, f, true);
        }
        /// <summary>
        /// Erweiterungsmethode, die auf gesetzte Flags prüft
        /// </summary>
        /// <param name="src">Zu prüfender Wert</param>
        /// <param name="f">gesuchte Flag(s)</param>        
        /// <param name="all">Ob alle Flags erfüllt sein müssen</param>
        /// <returns></returns>
        public static bool CheckForFlags(this ConnectionsAccess.ConnectionState src, ConnectionsAccess.ConnectionState f, bool all)
        {
            var a = (src & f);
            return (all ? ((src & f) == f) : ((src & f) != ConnectionsAccess.ConnectionState.None));
        }

        public static bool hasFlags(this ConnectionsAccess.ConnectionState state, ConnectionsAccess.ConnectionState flags)
        {
            return (state & flags) == flags;
        }

        public static void AddAll<T>(this ObservableCollection<T> coll, IEnumerable<T> list)
        {
            foreach (T t in list)
                coll.Add(t);
        }

        public static string nullToEmpty(this string s)
        {
            return s != null ? s : "";
        }
	}
}
