using System;
using System.Collections.Generic;

namespace QQChannelFramework.Models.MessageModels
{
    public class MessageArkKv
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public List<MessageArkObj> Obj { get; set; }
    }
}

