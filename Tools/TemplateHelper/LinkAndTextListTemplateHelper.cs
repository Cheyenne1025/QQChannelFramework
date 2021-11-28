using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QQChannelFramework.Tools.TemplateHelper
{
    sealed partial class MessageTemplateHelper
    {
        LinkAndTextListTemplateHelper _linkAndTextListTemplateHelper;

        /// <summary>
        /// 获取链接+文本消息模版构建器实例
        /// </summary>
        /// <returns></returns>
        public LinkAndTextListTemplateHelper GetLinkAndTextListTemplateHelper()
        {
            _linkAndTextListTemplateHelper = new();

            return _linkAndTextListTemplateHelper;
        }
    }

    /// <summary>
    /// 链接+文本消息模版构建类
    /// </summary>
    public class LinkAndTextListTemplateHelper
    {
        const string DescribeType = "#DESC#";
        const string PromptType = "#PROMPT#";
        const string ListType = "#LIST#";

        Dictionary<string, object> _dic;

        JObject _template;

        List<Dictionary<string, object>> _obj_kvs;

        public LinkAndTextListTemplateHelper()
        {
            _obj_kvs = new List<Dictionary<string, object>>();

            _dic = new Dictionary<string, object>()
            {
                {"msg_id", string.Empty},
                {"ark",new Dictionary<string,object>()
                {
                    {"template_id",23 },
                    {"kv",new List<Dictionary<string,object>>()
                    {
                        new()
                        {
                            {"key",DescribeType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",PromptType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",ListType },
                            {"obj",new List<Dictionary<string,object>>()}
                        }
                    } }
                }
                }
            };

            _template = JObject.Parse(JsonConvert.SerializeObject(_dic));
        }

        /// <summary>
        /// 设置描述
        /// </summary>
        /// <param name="describe"></param>
        /// <returns></returns>
        public LinkAndTextListTemplateHelper SetDescribe(string describe)
        {
            _template["ark"]["kv"][0]["value"] = describe;

            return this;
        }

        /// <summary>
        /// 设置提示消息
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public LinkAndTextListTemplateHelper SetPrompt(string prompt)
        {
            _template["ark"]["kv"][1]["value"] = prompt;

            return this;
        }

        /// <summary>
        /// 添加一行文字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public LinkAndTextListTemplateHelper AddText(string text)
        {
            _obj_kvs.Add(new Dictionary<string, object>()
            {
                {"obj_kv",new List<Dictionary<string,string>>()
                {
                    new Dictionary<string, string>()
                    {
                        {"key","desc" },
                        {"value",text }
                    }
                }
                }
            });

            return this;
        }

        /// <summary>
        /// 添加一行带链接的文字 (链接需经过审核)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public LinkAndTextListTemplateHelper AddTextWithLink(string text,string link)
        {
            _obj_kvs.Add(new Dictionary<string, object>()
            {
                {"obj_kv",new List<Dictionary<string,string>>()
                {
                    new Dictionary<string, string>()
                    {
                        {"key","desc" },
                        {"value",text }
                    },
                    new Dictionary<string, string>()
                    {
                        {"key","link" },
                        {"value",link }
                    }
                }
                }
            });

            return this;
        }

        /// <summary>
        /// 携带回复的消息ID
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public LinkAndTextListTemplateHelper WithReplyMessageId(string messageId)
        {
            _template["msg_id"] = messageId;

            return this;
        }

        /// <summary>
        /// 构建模版
        /// </summary>
        /// <returns></returns>
        public JObject BuildTemplate()
        {
            if(_obj_kvs.Count > 0)
            {
               _template["ark"]["kv"][2]["obj"] = JToken.Parse(JsonConvert.SerializeObject(_obj_kvs));
            }

            return _template;
        }
    }
}