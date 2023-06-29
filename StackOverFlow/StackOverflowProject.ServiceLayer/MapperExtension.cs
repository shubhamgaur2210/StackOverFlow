using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace StackOverflowProject.ServiceLayer
{
    public static class MapperExtension
    {
        private static void IgnoreUnmappedProperties(TypeMap Map, IMappingExpression Exp)
        {
            foreach(string PropertyName in Map.GetUnmappedPropertyNames())
            {
                if(Map.SourceType.GetProperty(PropertyName) != null)
                {
                    Exp.ForSourceMember(PropertyName, opt => opt.Ignore());
                }
                if(Map.DestinationType.GetProperty(PropertyName) != null)
                {
                    Exp.ForMember(PropertyName, opt => opt.Ignore());
                }
            }
        }

        public static void IgnoreUnmapped(this IProfileExpression Profile)
        {
            Profile.ForAllMaps(IgnoreUnmappedProperties);
        }
    }
}
