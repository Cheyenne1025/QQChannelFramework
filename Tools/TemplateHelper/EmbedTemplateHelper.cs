using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QQChannelFramework.Tools.TemplateHelper;

sealed partial class MessageTemplateHelper
{
    EmbedTemplateHelper _embedTemplateHelper;

    /// <summary>
    /// 获取大图消息模版构建器实例
    /// </summary>
    /// <returns></returns>
    public EmbedTemplateHelper GetEmbedTemplateHelper()
    {
        _embedTemplateHelper = new();

        return _embedTemplateHelper;
    }
}

/// <summary>
/// Embed消息模版构建类
/// </summary>
public class EmbedTemplateHelper
{
    Dictionary<string, object> _dic;

    new List<Dictionary<string, string>> _fields;

    JObject _template;

    public EmbedTemplateHelper()
    {
        _fields = new();

        _dic = new Dictionary<string, object>()
            {
                {"title",string.Empty },
                {"prompt",string.Empty },
                {"thumbnail",new Dictionary<string,object>()
                {
                    {"url",null }
                }
                },
                {"fields",new List<Dictionary<string,object>>()}
            };

        _template = JObject.Parse(JsonConvert.SerializeObject(_dic));
    }

    /// <summary>
    /// 设置标题
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public EmbedTemplateHelper SetTitle(string title)
    {
        _template["title"] = title;

        return this;
    }

    /// <summary>
    /// 设置提醒消息
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public EmbedTemplateHelper SetPrompt(string prompt)
    {
        _template["prompt"] = prompt;

        return this;
    }

    /// <summary>
    /// 设置略缩图
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public EmbedTemplateHelper SetThumbnail(string url)
    {
        _template["thumbnail"]["url"] = url;

        return this;
    }

    /// <summary>
    /// 增加一行文字
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public EmbedTemplateHelper AddField(string text)
    {
        _fields.Add(new()
        {
            { "name", text }
        });

        return this;
    }

    /// <summary>
    /// 构建模版
    /// </summary>
    /// <returns></returns>
    public JObject BuildTemplate()
    {
        if(_fields.Count > 0)
        {
            _template["fields"] = JToken.Parse(JsonConvert.SerializeObject(_fields));
        }

        return _template;
    }
}