using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace OneLine
{
    [AttributeUsage(validOn: AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class OneLineAttribute : PropertyAttribute
    {
        public LineHeader Header { get; set; }
    }

    [AttributeUsage(validOn: AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class OneLineWithHeaderAttribute : OneLineAttribute
    {
        public OneLineWithHeaderAttribute() : base()
        {
            Header = LineHeader.Short;
        }
    }

    public enum LineHeader
    {
        None = 0,
        Short = 1
    }
}