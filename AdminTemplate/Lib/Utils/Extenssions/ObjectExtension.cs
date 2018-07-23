using System;
using System.Collections.Generic;

public static class ObjectExtension
{
    public static object GetPropByName(this object o, string propriedade)
    {
        var tipo = o.GetType();
        var prop = tipo.GetProperty(propriedade);
        return prop.GetValue(o);
    }

    public static void SetPropByName(this object o, string propriedade, object value)
    {
        var tipo = o.GetType();
        var prop = tipo.GetProperty(propriedade);
        prop.SetValue(o, value);
    }

    public static T GetPropByName<T>(this object o, string propriedade)
    {
        return (T)o.GetPropByName(propriedade);
    }

    public static string GetPropByNameAsString(this object o, string propriedade)
    {
        var valor = o.GetPropByName(propriedade);
        if (valor == null)
        {
            return null;
        }
        else
        {
            return valor.ToString();
        }
    }

    public static int GetPropByNameAsInt(this object o, string propriedade)
    {
        return Convert.ToInt32(o.GetPropByNameAsString(propriedade));
    }

    public static T GetFirstOrDefaultAttribute<T>(this object o) where T : Attribute
    {
        var fi = o.GetType().GetField(o.ToString());
        try
        {
            var attributes = fi.GetCustomAttributes(typeof(T), false);
            if (attributes != null && attributes.Length > 0)
                return (T)attributes[0];
        }
        catch
        {
        }
        return default(T);
    }

    public static T MapTo<T>(this object value)
    {
        return AutoMapper.Mapper.Map<T>(value);
    }

    public static List<T> ListTo<T>(this object value)
    {
        return AutoMapper.Mapper.Map<List<T>>(value);
    }
}