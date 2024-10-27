namespace Excel
{
    using System;
    
    public class TypeToObject : System.Attribute
    {
        public Type Type;
        
        public TypeToObject(Type type)
        {
            Type = type;
        }
    }
    
    public class ExcelToCSVFormat : System.Attribute
    {
        public Type Type;
        
        public ExcelToCSVFormat(Type type)
        {
            Type = type;
        }
    }
    
    public class AttributExcelExtendType : System.Attribute
    {
        public Type Type;
        public string Keyword;
        
        public AttributExcelExtendType(Type type,string keyword)
        {
            Type = type;
            Keyword = keyword;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ExcelStringKey : System.Attribute
    {
    }
    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class RecordKeyField : System.Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ExcelCheckAttribute : Attribute
    {
        public Type Type { get; private set; }

        public ExcelCheckAttribute(Type type)
        {
            Type = type;
        }
    }
    
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class TableResetDataAttribute : Attribute
    {
        public Type Type { get; private set; }

        public TableResetDataAttribute(Type type)
        {
            Type = type;
        }
    }
}


