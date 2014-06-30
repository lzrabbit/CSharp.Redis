using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Redis.Client
{
    public class PipelineCommand
    {
        public object[] Args { get; set; }

        public byte[] ArgsBuffer { get; set; }

        public Type ReturnType { get; set; }

        public Action<object> CallBack { get; set; }

        public bool IsSingle { get; set; }
        
    }
  
}
